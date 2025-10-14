using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamoraHairdresser.Data.Entities
{
    class ServiceOffering:BaseEntity
    {
        public int SalonId { get; set; }
        public Salon Salon { get; set; } = default!;
        public string Name { get; set; } = string.Empty;
        public int DurationMinutes { get; set; }
        public decimal Price { get; set; }
    }
}
