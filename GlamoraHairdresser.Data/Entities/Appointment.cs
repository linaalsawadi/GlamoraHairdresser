using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamoraHairdresser.Data.Entities
{
    public class Appointment: BaseEntity
    {
        public int SalonId { get; set; }
        public Salon Salon { get; set; } = default!;

        public int ServiceOfferingId { get; set; }
        public ServiceOffering ServiceOffering { get; set; } = default!;

        public int WorkerId { get; set; }
        public Worker Worker { get; set; } = default!;

        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = default!;

        public DateTime StartUtc { get; set; }
        public DateTime EndUtc { get; set; }
        public string? Notes { get; set; }
    }
}
