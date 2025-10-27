using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace GlamoraHairdresser.Data.Entities
{
    public class WorkingHour : BaseEntity
    {
        public int SalonId { get; set; }
        public Salon Salon { get; set; } = default!;

        public int DayOfWeek { get; set; }

        public System.TimeOnly OpenTime { get; set; }
        public System.TimeOnly CloseTime { get; set; }

        // ✅ خاصية جديدة لتحديد إذا كان اليوم مفتوح أو مغلق
        public bool IsOpen { get; set; } = true;
    }
}
