using System;
using System.Windows.Forms;
using GlamoraHairdresser.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using GlamoraHairdresser.Data;
using GlamoraHairdresser.Services;
using GlamoraHairdresser.WinForms.Forms.AuthForms;
using GlamoraHairdresser.Services.Appointments;
using GlamoraHairdresser.Data.Entities;
using GlamoraHairdresser.Services.Auth;
using System.Text;
using System.Security.Cryptography;
using GlamoraHairdresser.WinForms.Forms.CustomerForms;
using GlamoraHairdresser.WinForms.Forms.WorkerForms;
using GlamoraHairdresser.WinForms.Forms.AdminForms;


namespace GlamoraHairdresser
{
    internal static class Program
    {
        public static byte[] HashPassword(string password)
        {
            using (var sha = SHA256.Create())
            {
                return sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
        public static ServiceProvider Services { get; private set; } = default!;


        [STAThread]
        static void Main()
        {
            var sc = new ServiceCollection();

            sc.AddDbContext<GlamoraDbContext>(opt =>
                opt.UseSqlServer("Server=DESKTOP-DHABHQ9\\SQLEXPRESS05;Database=GlamoraDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"));

            sc.AddScoped<IAuthService, AuthService>();
            sc.AddScoped<IAppointmentService, AppointmentService>();

            sc.AddTransient<LoginForm>(); // UI
            sc.AddTransient<AdminDashboard>();
            sc.AddTransient<CustomerDashboard>();
            sc.AddTransient<WorkerDashboard>();
            Services = sc.BuildServiceProvider();
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.

            // ================================
            // ???? ??? ?? ???? ??? ????? ??? Admin ????
            // ================================

            using (var scope = Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<GlamoraDbContext>();
                var auth = scope.ServiceProvider.GetRequiredService<IAuthService>();

                db.Database.Migrate(); // ???? ?????????? ????????

                // ?? ?? ?? ?? Admin ???? ???? ???????
                if (!db.Admins.Any())
                {
                    var admin = new Admin
                    {
                        FullName = "System Administrator",
                        Email = "admin@glamora.com",
                        PasswordHash = auth.HashPassword("admin123"), // ???? byte[]
                        Permissions = "FullAccess",
                        CreatedAt = DateTime.UtcNow,
                    };

                    db.Admins.Add(admin);
                    db.SaveChanges();
                }
            }

            // ================================
            // ???? ????? ??? ????? ??? Admin ????
            // ================================

            ApplicationConfiguration.Initialize();
            Application.Run(Services.GetRequiredService<LoginForm>()); // ? ???? LoginForm ?????
        }
    }
}