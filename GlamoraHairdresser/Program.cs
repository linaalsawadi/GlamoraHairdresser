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
using System.Security.Cryptography;
using System.Text;
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

            sc.AddDbContext<GlamoraDbContext>(opt =>
                opt.UseSqlServer("Server=DESKTOP-DHABHQ9\\SQLEXPRESS05;Database=GlamoraDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"));

            sc.AddScoped<IAuthService, AuthService>();
            sc.AddScoped<IAppointmentService, AppointmentService>();
            sc.AddScoped<IWorkerHoursService, WorkerHoursService>();
            sc.AddScoped<ISalonHoursService, SalonHoursService>();

            sc.AddDbContext<GlamoraDbContext>();

            sc.AddTransient<LoginForm>();
            sc.AddTransient<RegisterForm>();// UI


            sc.AddTransient<LoginForm>(); // UI
            sc.AddTransient<AdminDashboard>();
            sc.AddTransient<CustomerDashboard>();
            sc.AddTransient<WorkerDashboard>();
            sc.AddTransient<SalonForm>();
            sc.AddTransient<MakeAppointment>();
            sc.AddTransient<MyAppointment>();
            sc.AddTransient<AdminHoursForm>();


            Services = sc.BuildServiceProvider();

            
            using (var scope = Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<GlamoraDbContext>();
                var auth = scope.ServiceProvider.GetRequiredService<IAuthService>();

                db.Database.Migrate(); 

                if (!db.Admins.Any())
                {
                    var admin = new Admin
                    {
                        FullName = "System Administrator",
                        Email = "admin@glamora.com",
                        PasswordHash = auth.HashPassword("admin123"), // ???? byte[]
                        Permissions = "FullAccess",
                    };

                    db.Admins.Add(admin);
                    db.SaveChanges();
                }
            }

            ApplicationConfiguration.Initialize();
            Application.Run(Services.GetRequiredService<LoginForm>()); // ? ???? LoginForm ?????
        }
    }
}