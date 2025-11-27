using GlamoraHairdresser.WinForms.Forms.AdminForms;
using GlamoraHairdresser.WinForms.Forms.ApointmentForm;
using GlamoraHairdresser.WinForms.Forms.AuthForms;
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

namespace GlamoraHairdresser.WinForms.Forms.CustomerForms
{
    public partial class CustomerDashboard : Form
    {
        public CustomerDashboard()
        {
            InitializeComponent();
        }

        private void BookBtn_Click(object sender, EventArgs e)
        {
            var makeAppointmentForm = Program.Services.GetRequiredService<MakeAppointment>();  // ← استدعاء الفورم من المجلد
            makeAppointmentForm.Show();
            this.Hide();
        }

        private void MyBookingBtn_Click(object sender, EventArgs e)
        {
            var myAppointmentForm = Program.Services.GetRequiredService<MyAppointment>();  // ← استدعاء الفورم من المجلد
            myAppointmentForm.Show();
            this.Hide();

        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            var adminPage = Program.Services.GetRequiredService<LoginForm>();
            adminPage.ClearInputs();
            adminPage.Show();
            this.Close();

        }

        private void BookLogoutBtn_Click(object sender, EventArgs e)
        {

        }
    }
}
