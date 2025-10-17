using GlamoraHairdresser.Data.Entities;
using GlamoraHairdresser.Data;
using GlamoraHairdresser.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GlamoraHairdresser.Services.Appointments
{
    public class AppointmentService : IAppointmentService
    {
        private readonly GlamoraDbContext _db;
        private static readonly TimeZoneInfo DefaultTz =
            TimeZoneInfo.FindSystemTimeZoneById("Europe/Istanbul");

        public AppointmentService(GlamoraDbContext db)
        {
            _db = db;
        }

        private TimeZoneInfo GetSalonTz(int salonId)
        {
            // لاحقًا: لو خزّنت TimeZoneId داخل جدول Salon، استخرجه من هناك.
            return DefaultTz;
        }

        public async Task<int> CreateAppointmentAsync(Appointment appt)
        {
            // 1) جلب الخدمة والعامل والتحقق من الكيانات الأساسية
            var service = await _db.ServiceOfferings.AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == appt.ServiceOfferingId)
                ?? throw new InvalidOperationException("Service not found.");

            var worker = await _db.Workers.AsNoTracking()
                .FirstOrDefaultAsync(w => w.Id == appt.WorkerId)
                ?? throw new InvalidOperationException("Worker not found.");

            if (!await _db.Customers.AsNoTracking().AnyAsync(c => c.Id == appt.CustomerId))
                throw new InvalidOperationException("Customer not found.");
            if (!await _db.Salons.AsNoTracking().AnyAsync(s => s.Id == appt.SalonId))
                throw new InvalidOperationException("Salon not found.");

            // 2) اتساق الصالون
            if (service.SalonId != appt.SalonId)
                throw new InvalidOperationException("Service does not belong to the selected salon.");
            if (worker.SalonId != appt.SalonId)
                throw new InvalidOperationException("Worker does not belong to the selected salon.");

            // 3) امتلاك العامل للمهارة
            bool hasSkill = await _db.EmployeeSkills
                .AnyAsync(es => es.WorkerId == appt.WorkerId &&
                                es.ServiceOfferingId == appt.ServiceOfferingId);
            if (!hasSkill)
                throw new InvalidOperationException("Worker does not have the required skill.");

            // 4) حساب النهاية والمنطق الزمني
            var duration = appt.DurationMinutes ?? service.DurationMinutes;
            if (duration <= 0) throw new InvalidOperationException("Invalid duration.");

            var startUtc = appt.StartUtc;
            var endUtc = appt.EndUtc == default ? appt.StartUtc.AddMinutes(duration) : appt.EndUtc;
            if (startUtc >= endUtc) throw new InvalidOperationException("StartUtc must be before EndUtc.");

            // 5) منع التداخل على العامل
            if (await HasWorkerOverlapAsync(appt.WorkerId, startUtc, endUtc))
                throw new InvalidOperationException("Worker has an overlapping appointment.");

            // 6) ضمن ساعات الصالون وتوافر العامل (محليًا)
            var tz = GetSalonTz(appt.SalonId);
            var startLoc = TimeZoneInfo.ConvertTimeFromUtc(startUtc, tz);
            var endLoc = TimeZoneInfo.ConvertTimeFromUtc(endUtc, tz);
            var dayLocal = DateOnly.FromDateTime(startLoc);
            var tStart = TimeOnly.FromDateTime(startLoc);
            var tEnd = TimeOnly.FromDateTime(endLoc);
            byte dow = (byte)((int)startLoc.DayOfWeek == 0 ? 7 : (int)startLoc.DayOfWeek); // 1..7

            var wh = await _db.WorkingHours.AsNoTracking()
                .FirstOrDefaultAsync(w => w.SalonId == appt.SalonId && w.DayOfWeek == dow)
                ?? throw new InvalidOperationException("Salon is closed on this day.");

            bool insideSalonHours = wh.OpenTime <= tStart && tEnd <= wh.CloseTime;
            if (!insideSalonHours)
                throw new InvalidOperationException("Appointment is outside salon working hours.");

            var avails = await _db.WorkerAvailabilities.AsNoTracking()
                .Where(a => a.WorkerId == appt.WorkerId && a.Date == dayLocal)
                .ToListAsync();

            bool insideAvailability = avails.Any(a => a.Start <= tStart && tEnd <= a.End);
            if (!insideAvailability)
                throw new InvalidOperationException("Worker is not available at this time.");

            // 7) Snapshot التسعير والمدة
            appt.PriceAtBooking ??= service.Price;
            appt.DurationMinutes ??= duration;
            appt.EndUtc = endUtc;

            _db.Appointments.Add(appt);
            await _db.SaveChangesAsync();
            return appt.Id;
        }

        public async Task<bool> HasWorkerOverlapAsync(int workerId, DateTime startUtc, DateTime endUtc, int? ignoreId = null)
        {
            return await _db.Appointments
                .Where(a => a.WorkerId == workerId && (ignoreId == null || a.Id != ignoreId))
                .AnyAsync(a => a.StartUtc < endUtc && startUtc < a.EndUtc);
        }

        public async Task CancelAsync(int appointmentId)
        {
            var appt = await _db.Appointments.FirstOrDefaultAsync(a => a.Id == appointmentId)
                ?? throw new InvalidOperationException("Appointment not found.");

            appt.Status = AppointmentStatus.Canceled;
            await _db.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<Appointment>> GetWorkerDayScheduleAsync(int workerId, DateOnly dayLocal, int salonId)
        {
            var tz = GetSalonTz(salonId);
            var startLocal = dayLocal.ToDateTime(TimeOnly.MinValue);
            var endLocal = dayLocal.ToDateTime(TimeOnly.MaxValue);

            var startUtc = TimeZoneInfo.ConvertTimeToUtc(startLocal, tz);
            var endUtc = TimeZoneInfo.ConvertTimeToUtc(endLocal, tz);

            return await _db.Appointments
                .Where(a => a.WorkerId == workerId && a.StartUtc >= startUtc && a.StartUtc <= endUtc)
                .OrderBy(a => a.StartUtc)
                .ToListAsync();
        }

        public async Task<(DateTime startUtc, DateTime endUtc)?> FindNextFreeSlotAsync(int salonId, int serviceId, int workerId, DateTime fromLocal)
        {
            var tz = GetSalonTz(salonId);
            var service = await _db.ServiceOfferings.FirstAsync(s => s.Id == serviceId);
            int duration = service.DurationMinutes;

            var dayLocal = DateOnly.FromDateTime(fromLocal);
            byte dow = (byte)((int)fromLocal.DayOfWeek == 0 ? 7 : (int)fromLocal.DayOfWeek);

            var wh = await _db.WorkingHours.AsNoTracking()
                .FirstOrDefaultAsync(w => w.SalonId == salonId && w.DayOfWeek == dow);
            if (wh == null) return null;

            var avails = await _db.WorkerAvailabilities.AsNoTracking()
                .Where(a => a.WorkerId == workerId && a.Date == dayLocal)
                .OrderBy(a => a.Start)
                .ToListAsync();
            if (!avails.Any()) return null;

            TimeOnly cursor = TimeOnly.FromDateTime(fromLocal);
            foreach (var slot in avails)
            {
                var startCandidate = cursor < slot.Start ? slot.Start : cursor;
                while (startCandidate.AddMinutes(duration) <= slot.End)
                {
                    var startLocalCandidate = dayLocal.ToDateTime(startCandidate);
                    var endLocalCandidate = startLocalCandidate.AddMinutes(duration);

                    if (!(wh.OpenTime <= startCandidate && startCandidate.AddMinutes(duration) <= wh.CloseTime))
                    {
                        break; // خارج دوام الصالون
                    }

                    var startUtc = TimeZoneInfo.ConvertTimeToUtc(startLocalCandidate, tz);
                    var endUtc = TimeZoneInfo.ConvertTimeToUtc(endLocalCandidate, tz);

                    bool overlap = await HasWorkerOverlapAsync(workerId, startUtc, endUtc);
                    if (!overlap)
                        return (startUtc, endUtc);

                    startCandidate = startCandidate.AddMinutes(5); // جرّب بعد 5 دقائق
                }
            }
            return null;
        }
    }
}
