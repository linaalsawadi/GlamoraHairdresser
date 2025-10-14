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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                            "Server=DESKTOP-DHABHQ9\\SQLEXPRESS05;Database=GlamoraDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");
        }
    }
}
