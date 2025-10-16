using System;
using System.Windows.Forms;
using GlamoraHairdresser.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using GlamoraHairdresser.Data;
using GlamoraHairdresser.Services;

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
                opt.UseSqlServer("Server=DEEMAZAINELDEEN\\SQLEXPRESS;Database=GlamoraDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"));

            sc.AddScoped<IAppointmentService, AppointmentService>();

            sc.AddTransient<BookingForm>(); // UI

            Services = sc.BuildServiceProvider();
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(Services.GetRequiredService<BookingForm>());
        }
    }
}