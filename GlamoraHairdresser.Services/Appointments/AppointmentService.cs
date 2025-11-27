using GlamoraHairdresser.Data;
using GlamoraHairdresser.Data.Entities;
using GlamoraHairdresser.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GlamoraHairdresser.Services.Appointments
{
    public class AppointmentService : IAppointmentService
    {
        private readonly GlamoraDbContext _db;

        public AppointmentService(GlamoraDbContext db)
        {
            _db = db;
        }

        // ✔ 1) Get Salon Services
        public async Task<List<ServiceOffering>> GetServicesForSalon(int salonId)
        {
            return await _db.ServiceOfferings
                .Where(s => s.SalonId == salonId)
                .OrderBy(s => s.Name)
                .ToListAsync();
        }

        // ✔ 2) Get workers that can perform a service
        public async Task<List<Worker>> GetWorkersForSalon(int salonId, int serviceId)
        {
            return await _db.Workers
                .Where(w => w.SalonId == salonId &&
                            w.Skills.Any(sk => sk.ServiceOfferingId == serviceId))
                .OrderBy(w => w.FullName)
                .ToListAsync();
        }

        // ✔ 3) Generate available time slots
        public async Task<List<TimeOnly>> GetAvailableSlotsAsync(int workerId, DateOnly date, int serviceDuration)
        {
            var workerHours = await _db.WorkerWorkingHours
                .AsNoTracking()
                .FirstOrDefaultAsync(h => h.WorkerId == workerId && h.DayOfWeek == (int)date.DayOfWeek);

            if (workerHours == null || !workerHours.IsOpen)
                return new List<TimeOnly>(); // no work on this day

            TimeOnly open = workerHours.OpenTime;
            TimeOnly close = workerHours.CloseTime;

            // Get existing appointments
            var appointments = await _db.Appointments
                .Where(a => a.WorkerId == workerId &&
                            a.StartUtc.Date == date.ToDateTime(TimeOnly.MinValue).Date &&
                            a.Status != AppointmentStatus.Canceled &&
                            a.Status != AppointmentStatus.Rejected)
                .Select(a => new
                {
                    Start = TimeOnly.FromDateTime(a.StartUtc),
                    End = TimeOnly.FromDateTime(a.EndUtc)
                })
                .ToListAsync();

            var slots = new List<TimeOnly>();

            for (var t = open; t.AddMinutes(serviceDuration) <= close; t = t.AddMinutes(serviceDuration))
            {
                TimeOnly slotStart = t;
                TimeOnly slotEnd = t.AddMinutes(serviceDuration);

                bool overlaps = appointments.Any(a =>
                    !(slotEnd <= a.Start || slotStart >= a.End)
                );

                if (!overlaps)
                    slots.Add(slotStart);
            }

            return slots;
        }

        // ✔ 4) Book an appointment
        public async Task<(bool success, string message)> BookAsync(int customerId, int salonId,
            int workerId, int serviceId, DateTime startUtc)
        {
            var service = await _db.ServiceOfferings.FindAsync(serviceId);
            if (service == null)
                return (false, "Service not found.");

            DateTime endUtc = startUtc.AddMinutes(service.DurationMinutes);

            // collision check
            bool overlap = await _db.Appointments
                .AnyAsync(a =>
                    a.WorkerId == workerId &&
                    !(endUtc <= a.StartUtc || startUtc >= a.EndUtc) &&
                    a.Status != AppointmentStatus.Canceled &&
                    a.Status != AppointmentStatus.Rejected
                );

            if (overlap)
                return (false, "This time slot is no longer available.");

            var appt = new Appointment
            {
                CustomerId = customerId,
                WorkerId = workerId,
                SalonId = salonId,
                ServiceOfferingId = serviceId,
                StartUtc = startUtc,
                EndUtc = endUtc,
                Status = AppointmentStatus.Pending,
                PriceAtBooking = service.Price,
                DurationMinutes = service.DurationMinutes,
                CreatedAt = DateTime.UtcNow
            };

            _db.Appointments.Add(appt);
            await _db.SaveChangesAsync();

            return (true, "Appointment submitted and waiting for worker confirmation.");
        }

        // ✔ 5) Suggest next available day
        public async Task<(bool success, string message, DateOnly? nextAvailableDay)>
            SuggestNextAvailableDay(int workerId, int serviceId, DateOnly date)
        {
            var service = await _db.ServiceOfferings.FindAsync(serviceId);
            if (service == null)
                return (false, "Service not found.", null);

            for (int i = 1; i <= 14; i++)
            {
                var testDate = date.AddDays(i);

                var slots = await GetAvailableSlotsAsync(workerId, testDate, service.DurationMinutes);

                if (slots.Any())
                    return (true, "Next available day found.", testDate);
            }

            return (false, "No available days within the next 14 days.", null);
        }
    }
}
