using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamoraHairdresser.Data.Entities
{
    public class EmployeeSkill
    {
        public int WorkerId { get; set; }
        public Worker Worker { get; set; } = default!;

        public int ServiceOfferingId { get; set; }
        public ServiceOffering ServiceOffering { get; set; } = default!;

        public DateTime AssignedDate { get; set; } = DateTime.UtcNow;

        public string? Notes { get; set; }
    }
}
