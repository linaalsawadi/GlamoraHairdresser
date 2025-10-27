using GlamoraHairdresser.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GlamoraHairdresser.Services.WorkerHours
{
    public class WorkerHoursService : IWorkerHoursService
    {
        private readonly GlamoraDbContext _db;

        public WorkerHoursService(GlamoraDbContext db)
        {
            _db = db;
        }

        public async Task<(bool isWorking, TimeOnly? start, TimeOnly? end)> GetEffectiveHoursAsync(int workerId, DateOnly date)
        {
            var special = await _db.WorkerSpecialWorkingHours
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.WorkerId == workerId && s.Date == date);

            if (special != null)
            {
                if (special.IsOffDay) return (false, null, null);
                return (true, special.OpenTime, special.CloseTime);
            }

            int dow = (int)date.DayOfWeek;
            if (dow == 0) dow = 7;

            var regular = await _db.WorkerWorkingHours
                .AsNoTracking()
                .FirstOrDefaultAsync(w => w.WorkerId == workerId && w.DayOfWeek == dow);

            if (regular == null)
                return (false, null, null);

            return (true, regular.OpenTime, regular.CloseTime);
        }

        public async Task<bool> IsWorkerAvailableAtAsync(int workerId, DateTime localDateTime)
        {
            var date = DateOnly.FromDateTime(localDateTime);
            var time = TimeOnly.FromDateTime(localDateTime);

            var (isWorking, start, end) = await GetEffectiveHoursAsync(workerId, date);
            if (!isWorking || start == null || end == null)
                return false;

            return time >= start && time < end;
        }
    }
}
