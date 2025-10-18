using GlamoraHairdresser.WinForms.Forms.AuthForms;
using GlamoraHairdresser.WinForms.Forms.SalonForms;
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
            var salonForm = Program.Services.GetRequiredService<SalonForm>();
            salonForm.Show();
        }

        private void WorkerBtn_Click(object sender, EventArgs e)
        {

        }

        private void CustomerBtn_Click(object sender, EventArgs e)
        {

        }

        private void ProfitBtn_Click(object sender, EventArgs e)
        {

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
        
        private void BackBtn_Click(object sender, EventArgs e)
        {

        }
    }
}
