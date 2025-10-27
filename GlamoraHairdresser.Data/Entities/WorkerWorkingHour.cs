using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamoraHairdresser.Data.Entities
{
    public class WorkerWorkingHour
    {
        public int Id { get; set; }

        public int WorkerId { get; set; }
        public Worker Worker { get; set; } = default!;

        public int DayOfWeek { get; set; } // 1 = Monday ... 7 = Sunday
        public TimeOnly OpenTime { get; set; }
        public TimeOnly CloseTime { get; set; }
    }
}
