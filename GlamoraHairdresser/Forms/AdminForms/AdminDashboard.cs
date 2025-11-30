using GlamoraHairdresser.WinForms.Forms.AuthForms;
using GlamoraHairdresser.WinForms.Forms.SalonForms;
using GlamoraHairdresser.WinForms.Forms.Services;
using GlamoraHairdresser.WinForms.Forms.WorkerForms;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GlamoraHairdresser.WinForms.Forms.AdminForms
{
    public partial class AdminDashboard : Form
    {

        public AdminDashboard()
        {
            InitializeComponent();
            this.SalonBtn.Click -= SalonBtn_Click;
            this.SalonBtn.Click += SalonBtn_Click;
        }

        private void SalonBtn_Click(object sender, EventArgs e)
        {
            using var scope = Program.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<GlamoraHairdresser.Data.GlamoraDbContext>();

            var salonForm = new SalonForm(db);
            salonForm.ShowDialog();
        }

        private void WorkerBtn_Click(object sender, EventArgs e)
        {
            using var scope = Program.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<GlamoraHairdresser.Data.GlamoraDbContext>();

            var workerForm = new WorkerDashboard(db);
            workerForm.ShowDialog();
        }

        private void CustomerBtn_Click(object sender, EventArgs e)
        {
            using var scope = Program.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<GlamoraHairdresser.Data.GlamoraDbContext>();

            var customerForm = new CustomerForm(db);
            customerForm.ShowDialog();
        }

        

        private void LogoutBtn_Click(object sender, EventArgs e)
        {

            // أظهر شاشة الدخول من الـDI
            var login = Program.Services.GetRequiredService<LoginForm>();
            // نظّف حقول الدخول اختياريًا
            login.ClearInputs();
            login.Show();

            // أغلق هذا النموذج
            this.Close();

        }

        private void ServiceBtn_Click(object sender, EventArgs e)
        {
            using var scope = Program.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<GlamoraHairdresser.Data.GlamoraDbContext>();

            var servicesForm = new ServicesDashboard(db);
            servicesForm.ShowDialog();
        }

        private void AppoBtn_Click(object sender, EventArgs e)
        {
            using var scope = Program.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<GlamoraHairdresser.Data.GlamoraDbContext>();

            var appointmentForm = new AdminAppointmentForm(db);
            appointmentForm.ShowDialog();
            this.Close();
        }
    }
}
