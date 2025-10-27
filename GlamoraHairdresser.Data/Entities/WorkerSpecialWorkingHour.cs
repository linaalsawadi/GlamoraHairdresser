using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamoraHairdresser.Data.Entities
{
    public class WorkerSpecialWorkingHour
    {
        [Key]
        public int Id { get; set; }

        public int WorkerId { get; set; }
        public Worker Worker { get; set; } = default!;

        public DateOnly Date { get; set; }
        public TimeOnly? OpenTime { get; set; }
        public TimeOnly? CloseTime { get; set; }

        public bool IsOffDay { get; set; }     // عطلة مؤقتة
        public string? Reason { get; set; }
    }
}
