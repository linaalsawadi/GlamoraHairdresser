using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamoraHairdresser.Data.Entities
{
    class Salon:BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Address { get; set; }
        public ICollection<WorkingHour> WorkingHours { get; set; } = new List<WorkingHour>();
        public ICollection<ServiceOffering> Services { get; set; } = new List<ServiceOffering>();

    }
}
