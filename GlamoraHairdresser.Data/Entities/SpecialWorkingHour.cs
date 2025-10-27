using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamoraHairdresser.Data.Entities
{
    public class SpecialWorkingHour
    {
        public int Id { get; set; }

        public int SalonId { get; set; }
        public Salon Salon { get; set; } = default!;

        public DateOnly Date { get; set; }              // اليوم المحدد
        public TimeOnly? OpenTime { get; set; }         // وقت الفتح (null إذا عطلة)
        public TimeOnly? CloseTime { get; set; }        // وقت الإغلاق (null إذا عطلة)

        public bool IsClosed { get; set; }              // true = عطلة
        public string? Reason { get; set; }  
    }
}
