using GlamoraHairdresser.Data;
using GlamoraHairdresser.Data.Entities;
using GlamoraHairdresser.Services;
using GlamoraHairdresser.Services.Appointments;
using GlamoraHairdresser.Services.Auth;
using GlamoraHairdresser.Services.Interfaces;
using GlamoraHairdresser.Services.SalonHours;
using GlamoraHairdresser.Services.WorkerHours;
using GlamoraHairdresser.WinForms.Forms.AdminForms;
using GlamoraHairdresser.WinForms.Forms.ApointmentForm;
using GlamoraHairdresser.WinForms.Forms.AuthForms;
using GlamoraHairdresser.WinForms.Forms.CustomerForms;
using GlamoraHairdresser.WinForms.Forms.SalonForms;
using GlamoraHairdresser.WinForms.Forms.WorkerForms;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Windows.Forms;

namespace GlamoraHairdresser
{
    internal static class Program
    {
        public static ServiceProvider Services { get; private set; } = default!;

        [STAThread]
        static void Main()
        {
            var sc = new ServiceCollection();

            // ============================
            //  Database Connection
            // ============================
            sc.AddDbContext<GlamoraDbContext>(opt =>
                opt.UseSqlServer(
                    "Server=DESKTOP-DHABHQ9\\SQLEXPRESS05;" +
                    "Database=GlamoraDb;" +
                    "Trusted_Connection=True;" +
                    "MultipleActiveResultSets=true;" +
                    "TrustServerCertificate=True"));

            // ============================
            //  Services DI Registration
            // ============================
            sc.AddScoped<IAuthService, AuthService>();
            sc.AddScoped<IAppointmentService, AppointmentService>();
            sc.AddScoped<IWorkerHoursService, WorkerHoursService>();
            sc.AddScoped<ISalonHoursService, SalonHoursService>();

            // ============================
            //  UI Forms
            // ============================
            sc.AddTransient<LoginForm>();
            sc.AddTransient<RegisterForm>();

            sc.AddTransient<AdminDashboard>();
            sc.AddTransient<CustomerDashboard>();
            sc.AddTransient<WorkerDashboard>();
            sc.AddTransient<WorkerForm>();
            sc.AddTransient<SalonForm>();

            sc.AddTransient<MakeAppointment>();
            sc.AddTransient<MyAppointment>();

            sc.AddTransient<AdminHoursForm>();


            // Build DI Container
            Services = sc.BuildServiceProvider();

            // ============================
            //  Database Initialization & Admin Seeding
            // ============================
            using (var scope = Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<GlamoraDbContext>();

                // Apply migrations automatically
                db.Database.Migrate();

                // Seed Admin if not exists
                if (!db.Admins.Any())
                {
                    // Generate PBKDF2 password
                    var (hash, salt, iteration, prf) = PasswordHelper.HashPassword("admin123");

                    var admin = new Admin
                    {
                        FullName = "System Administrator",
                        Email = "admin@glamora.com",
                        PasswordHash = hash,
                        Salt = salt,
                        IterationCount = iteration,
                        Prf = prf,
                        Permissions = "FullAccess",
                        CreatedAt = DateTime.UtcNow
                    };


                    db.Admins.Add(admin);
                    db.SaveChanges();
                }
            }

            // ============================
            //  Start Application
            // ============================
            ApplicationConfiguration.Initialize();
            Application.Run(Services.GetRequiredService<LoginForm>());
        }
    }
}
