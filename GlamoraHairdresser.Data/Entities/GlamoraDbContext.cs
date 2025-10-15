using Microsoft.EntityFrameworkCore;
using GlamoraHairdresser.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamoraHairdresser.Data.Entities
{
    class GlamoraDbContext:DbContext
    {
        public DbSet<Salon> Salons => Set<Salon>();
        public DbSet<WorkingHour> WorkingHours => Set<WorkingHour>();
        public DbSet<ServiceOffering> ServiceOfferings => Set<ServiceOffering>();

        public DbSet<User> Users => Set<User>();
        public DbSet<Admin> Admins => Set<Admin>();
        public DbSet<Worker> Workers => Set<Worker>();
        public DbSet<Customer> Customers => Set<Customer>();

        public DbSet<EmployeeSkill> EmployeeSkills => Set<EmployeeSkill>();
        public DbSet<WorkerAvailability> WorkerAvailabilities => Set<WorkerAvailability>();
        public DbSet<Appointment> Appointments => Set<Appointment>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // عدّل اسم السيرفر حسب جهازك إذا لزم
            optionsBuilder.UseSqlServer(
                "Server=DESKTOP-DHABHQ9\\SQLEXPRESS05;Database=GlamoraDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");
        }

        protected override void OnModelCreating(ModelBuilder model)
        {
            // ===== 1) TPH Inheritance for Users =====
            model.Entity<User>()
                 .HasDiscriminator<string>("UserType")
                 .HasValue<Admin>("Admin")
                 .HasValue<Worker>("Worker")
                 .HasValue<Customer>("Customer");

            model.Entity<User>()
                 .HasIndex(u => u.Email)
                 .IsUnique();

            // ===== 2) Salon =====
            model.Entity<Salon>().HasKey(s => s.Id);
            model.Entity<Salon>()
                 .HasIndex(s => s.Name)
                 .IsUnique();

            // ===== 3) WorkingHour (Salon 1 → N WorkingHours) =====
            model.Entity<WorkingHour>().HasKey(w => w.Id);
            model.Entity<WorkingHour>()
                 .HasOne(w => w.Salon)
                 .WithMany(s => s.WorkingHours)
                 .HasForeignKey(w => w.SalonId)
                 .OnDelete(DeleteBehavior.Cascade);

            model.Entity<WorkingHour>()
                 .HasIndex(w => new { w.SalonId, w.DayOfWeek })
                 .IsUnique();

            model.Entity<WorkingHour>()
                 .ToTable(t => t.HasCheckConstraint("CK_WorkingHour_Time", "[OpenTime] < [CloseTime]"));

            // ===== 4) ServiceOffering (Salon 1 → N Services) =====
            model.Entity<ServiceOffering>().HasKey(s => s.Id);
            model.Entity<ServiceOffering>()
                 .HasOne(s => s.Salon)
                 .WithMany(sa => sa.Services)
                 .HasForeignKey(s => s.SalonId)
                 .OnDelete(DeleteBehavior.Cascade);

            model.Entity<ServiceOffering>()
                 .HasIndex(s => new { s.SalonId, s.Name })
                 .IsUnique();

            model.Entity<ServiceOffering>()
                 .ToTable(t =>
                 {
                 t.HasCheckConstraint("CK_Service_Duration", "[DurationMinutes] BETWEEN 5 AND 600");
                 t.HasCheckConstraint("CK_Service_Price", "[Price] >= 0");
                 });

            // ===== 5) Worker (User派生) : Salon 1 → N Workers =====
            model.Entity<Worker>()
                 .HasOne(w => w.Salon)
                 .WithMany(s => s.Workers)
                 .HasForeignKey(w => w.SalonId)
                 .OnDelete(DeleteBehavior.Cascade);

            // ===== 6) EmployeeSkill (Many-to-Many: Worker ↔ ServiceOffering) =====
            model.Entity<EmployeeSkill>()
                 .HasKey(es => new { es.WorkerId, es.ServiceOfferingId });

            model.Entity<EmployeeSkill>()
                 .HasOne(es => es.Worker)
                 .WithMany(w => w.Skills)
                 .HasForeignKey(es => es.WorkerId)
                 .OnDelete(DeleteBehavior.Cascade);

            model.Entity<EmployeeSkill>()
                 .HasOne(es => es.ServiceOffering)
                 .WithMany(s => s.Workers)
                 .HasForeignKey(es => es.ServiceOfferingId)
                 .OnDelete(DeleteBehavior.Cascade);

            // ===== 7) WorkerAvailability (Worker 1 → N Availabilities) =====
            model.Entity<WorkerAvailability>().HasKey(a => a.Id);
            model.Entity<WorkerAvailability>()
                 .HasOne(a => a.Worker)
                 .WithMany(w => w.Availabilities)
                 .HasForeignKey(a => a.WorkerId)
                 .OnDelete(DeleteBehavior.Cascade);

            model.Entity<WorkerAvailability>()
                 .HasIndex(a => new { a.WorkerId, a.Date });

            model.Entity<WorkerAvailability>()
                 .ToTable(t => t.HasCheckConstraint("CK_Avail_Time", "[Start] < [End]"));

            // ===== 8) Appointment =====
            model.Entity<Appointment>().HasKey(a => a.Id);

            model.Entity<Appointment>()
                 .HasOne(a => a.Salon)
                 .WithMany()
                 .HasForeignKey(a => a.SalonId)
                 .OnDelete(DeleteBehavior.Restrict);

            model.Entity<Appointment>()
                 .HasOne(a => a.ServiceOffering)
                 .WithMany(s => s.Appointments)
                 .HasForeignKey(a => a.ServiceOfferingId)
                 .OnDelete(DeleteBehavior.Restrict);

            model.Entity<Appointment>()
                 .HasOne(a => a.Worker)
                 .WithMany(w => w.Appointments)
                 .HasForeignKey(a => a.WorkerId)
                 .OnDelete(DeleteBehavior.Restrict);

            model.Entity<Appointment>()
                 .HasOne(a => a.Customer)
                 .WithMany(c => c.Appointments)
                 .HasForeignKey(a => a.CustomerId)
                 .OnDelete(DeleteBehavior.Restrict);

            model.Entity<Appointment>()
                 .HasIndex(a => new { a.WorkerId, a.StartUtc, a.EndUtc });

            model.Entity<Appointment>()
                 .ToTable(t => t.HasCheckConstraint("CK_Appt_Time", "[StartUtc] < [EndUtc]"));
        }
    }
}
