using System;
using System.Linq;
using System.Threading.Tasks;
using GlamoraHairdresser.Data;
using GlamoraHairdresser.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GlamoraHairdresser.Services.SalonHours
{
    public class SalonHoursService : ISalonHoursService
    {
        private readonly GlamoraDbContext _db;

        public SalonHoursService(GlamoraDbContext db)
        {
            _db = db;
        }

        public async Task<(bool isOpen, TimeOnly? open, TimeOnly? close)> GetEffectiveHoursAsync(int salonId, DateOnly date)
        {
            var special = await _db.SpecialWorkingHours
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.SalonId == salonId && s.Date == date);

            if (special != null)
            {
                if (special.IsClosed)
                    return (false, null, null);
                return (true, special.OpenTime, special.CloseTime);
            }

            int dow = (int)date.DayOfWeek;
            if (dow == 0) dow = 7; // الأحد = 7

            var regular = await _db.WorkingHours
                .AsNoTracking()
                .FirstOrDefaultAsync(w => w.SalonId == salonId && w.DayOfWeek == dow);

            if (regular == null)
                return (false, null, null);

            return (true,
                regular.OpenTime,
                regular.CloseTime);
        }

        public async Task<bool> IsSalonOpenAtAsync(int salonId, DateTime localDateTime)
        {
            var date = DateOnly.FromDateTime(localDateTime);
            var time = TimeOnly.FromDateTime(localDateTime);

            var (isOpen, open, close) = await GetEffectiveHoursAsync(salonId, date);
            if (!isOpen || open == null || close == null)
                return false;

            return time >= open && time < close;
        }
    }
}
