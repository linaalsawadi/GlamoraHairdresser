using GlamoraHairdresser.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamoraHairdresser.Services.Interfaces
{
    public interface IAppointmentService
    {
        Task<int> CreateAppointmentAsync(Appointment appt);
        Task CancelAsync(int appointmentId);
        Task<bool> HasWorkerOverlapAsync(int workerId, DateTime startUtc, DateTime endUtc, int? ignoreId = null);
        Task<IReadOnlyList<Appointment>> GetWorkerDayScheduleAsync(int workerId, DateOnly dayLocal, int salonId);
        Task<(DateTime startUtc, DateTime endUtc)?> FindNextFreeSlotAsync(int salonId, int serviceId, int workerId, DateTime fromLocal);
    }
}
