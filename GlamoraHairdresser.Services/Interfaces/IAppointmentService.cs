using GlamoraHairdresser.Data.Entities;

namespace GlamoraHairdresser.Services.Interfaces
{
    public interface IAppointmentService
    {
        Task<List<ServiceOffering>> GetServicesForSalon(int salonId);
        Task<List<Worker>> GetWorkersForSalon(int salonId, int serviceId);
        Task<List<TimeOnly>> GetAvailableSlotsAsync(int workerId, DateOnly date, int serviceDuration);
        Task<(bool success, string message)> BookAsync(int customerId, int salonId, int workerId,
                                                       int serviceId, DateTime startUtc);
        Task<(bool success, string message, DateOnly? nextAvailableDay)>
            SuggestNextAvailableDay(int workerId, int serviceId, DateOnly date);
    }
}
