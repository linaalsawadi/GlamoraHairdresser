using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamoraHairdresser.Services.Interfaces
{
    public interface IWorkerHoursService
    {
        Task<(bool isWorking, TimeOnly? start, TimeOnly? end)> GetEffectiveHoursAsync(int workerId, DateOnly date);
        Task<bool> IsWorkerAvailableAtAsync(int workerId, DateTime localDateTime);
    }
}
