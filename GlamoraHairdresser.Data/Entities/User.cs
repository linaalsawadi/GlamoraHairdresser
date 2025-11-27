using System.Collections.Generic;

namespace GlamoraHairdresser.Data.Entities
{
    public abstract class User : BaseEntity
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; }

        // === PBKDF2 Fields ===
        public byte[] PasswordHash { get; set; } = Array.Empty<byte>();
        public byte[] Salt { get; set; } = Array.Empty<byte>();
        public int IterationCount { get; set; } = 100_000;
        public int Prf { get; set; } = 1;

        // TPH discriminator
        public string UserType { get; protected set; } = string.Empty;
    }


    public class Admin : User
    {
        public Admin() { UserType = nameof(Admin); }
        public string? Permissions { get; set; }
    }

    public class Worker : User
    {
        public Worker() { UserType = nameof(Worker); }

        public int SalonId { get; set; }
        public Salon Salon { get; set; } = default!;
        public string? JobTitle { get; set; }

        public ICollection<EmployeeSkill> Skills { get; set; } = new List<EmployeeSkill>();
        public ICollection<WorkerAvailability> Availabilities { get; set; } = new List<WorkerAvailability>();
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }

    public class Customer : User
    {
        public Customer() { UserType = nameof(Customer); }
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
