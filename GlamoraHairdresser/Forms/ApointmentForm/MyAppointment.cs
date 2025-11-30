using GlamoraHairdresser.Data;
using GlamoraHairdresser.Data.Entities;
using GlamoraHairdresser.Services.Auth;
using GlamoraHairdresser.WinForms.Forms.AuthForms;
using GlamoraHairdresser.WinForms.Forms.CustomerForms;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Windows.Forms;

namespace GlamoraHairdresser.WinForms.Forms.ApointmentForm
{
    public partial class MyAppointment : Form
    {
        private readonly GlamoraDbContext _db;
        private readonly int _customerId;

        public MyAppointment(GlamoraDbContext db)
        {
            InitializeComponent();
            _db = db;

            _customerId = SessionManager.CurrentUser.Id;

            // DEBUG: لتتأكدي أنكِ تسجلين دخول الـ Customer الصحيح
            Console.WriteLine("Current Customer ID = " + _customerId);

            LoadApprovedAppointments();
        }

        // ===============================================================
        // LOAD ONLY APPROVED APPOINTMENTS
        // ===============================================================
        private void LoadApprovedAppointments()
        {
            try
            {
                var list = _db.Appointments
                    .Include(a => a.ServiceOffering)
                    .Include(a => a.Worker)
                    .Where(a =>
                        a.CustomerId == _customerId &&
                        a.Status == AppointmentStatus.Approved)   // ← هنا بالضبط
                    .OrderBy(a => a.StartUtc)
                    .Select(a => new
                    {
                        a.Id,
                        Service = a.ServiceOffering.Name,
                        Worker = a.Worker.FullName,
                        Date = a.StartUtc.ToLocalTime().ToString("yyyy-MM-dd"),
                        Start = a.StartUtc.ToLocalTime().ToString("HH:mm"),
                        End = a.EndUtc.ToLocalTime().ToString("HH:mm"),
                        Price = a.PriceAtBooking
                    })
                    .ToList();

                dgvMyAppointments.DataSource = list;
                FormatGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading appointments:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // ===============================================================
        // FORMAT GRID
        // ===============================================================
        private void FormatGrid()
        {
            var g = dgvMyAppointments;
            g.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (g.Columns["Price"] != null)
            {
                g.Columns["Price"].DefaultCellStyle.Format = "0.00";
                g.Columns["Price"].HeaderText = "Price (₺)";
            }
        }

        // ===============================================================
        // REFRESH
        // ===============================================================
        private void btnRefresh_Click_1(object sender, EventArgs e)
        {
            LoadApprovedAppointments();
        }

        // ===============================================================
        // CLOSE
        // ===============================================================
        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            var CustomerPage = Program.Services.GetRequiredService<CustomerDashboard>();
            CustomerPage.Show();
            this.Hide();
        }

        private void LogoutBtn_Click(object sender, EventArgs e)
        {
            var adminPage = Program.Services.GetRequiredService<LoginForm>();
            adminPage.ClearInputs();
            adminPage.Show();
            this.Close();

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (dgvMyAppointments.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an appointment to cancel.",
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int appointmentId = (int)dgvMyAppointments.SelectedRows[0].Cells["Id"].Value;

            var appt = _db.Appointments
                .FirstOrDefault(a => a.Id == appointmentId && a.CustomerId == _customerId);

            if (appt == null)
            {
                MessageBox.Show("Appointment not found.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // تأكيد الإلغاء
            var result = MessageBox.Show(
                "Are you sure you want to cancel this appointment?",
                "Confirm Cancel",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.No)
                return;

            // تغيير الحالة إلى Canceled
            appt.Status = AppointmentStatus.Canceled;

            // وقت الإلغاء
            appt.Notes = "Canceled by customer";
            appt.DurationMinutes = null;
            appt.PriceAtBooking = null;

            _db.SaveChanges();

            MessageBox.Show("Appointment canceled successfully!",
                "Canceled", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // إعادة تحميل البيانات
            LoadApprovedAppointments();
        }

    }
}
