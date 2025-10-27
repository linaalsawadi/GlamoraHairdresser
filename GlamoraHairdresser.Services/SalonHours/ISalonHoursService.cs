using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamoraHairdresser.Services.SalonHours
{
    public interface ISalonHoursService
    {
        Task<(bool isOpen, TimeOnly? open, TimeOnly? close)> GetEffectiveHoursAsync(int salonId, DateOnly date);
        Task<bool> IsSalonOpenAtAsync(int salonId, DateTime localDateTime);
    }
}
