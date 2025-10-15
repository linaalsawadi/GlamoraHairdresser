using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamoraHairdresser.Data.Entities
{
    public class Salon:BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Address { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public ICollection<WorkingHour> WorkingHours { get; set; } = new List<WorkingHour>();
        public ICollection<ServiceOffering> Services { get; set; } = new List<ServiceOffering>();
        public ICollection<Worker> Workers { get; set; } = new List<Worker>();
    }
}
