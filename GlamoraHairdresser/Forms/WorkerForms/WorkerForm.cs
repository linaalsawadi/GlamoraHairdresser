using GlamoraHairdresser.Data;
using GlamoraHairdresser.Data.Entities;
using GlamoraHairdresser.Services.Auth;
using GlamoraHairdresser.WinForms.Forms.AdminForms;
using GlamoraHairdresser.WinForms.Forms.AuthForms;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Windows.Forms;

namespace GlamoraHairdresser.WinForms.Forms.WorkerForms
{
    public partial class WorkerForm : Form
    {
        private readonly GlamoraDbContext _db;
        private readonly int _workerId;

        public WorkerForm(GlamoraDbContext db)
        {
            InitializeComponent();
            _db = db;

            _workerId = SessionManager.CurrentUser.Id;

            appointmentsGrid.AutoGenerateColumns = true;
            appointmentsGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            appointmentsGrid.MultiSelect = false;

            LoadPendingAppointments();
            appointmentsGrid.SelectionChanged += (_, __) => ShowAppointmentDetails();
        }

        // ===============================================================
        // LOAD PENDING APPOINTMENTS FOR THIS WORKER
        // ===============================================================
        private void LoadPendingAppointments()
        {
            var list = _db.Appointments
                .Include(a => a.Customer)
                .Include(a => a.ServiceOffering)
                .Where(a => a.WorkerId == _workerId &&
                            a.Status == AppointmentStatus.Pending)
                .OrderBy(a => a.StartUtc)
                .Select(a => new
                {
                    Id = a.Id, // MUST BE EXACTLY "Id"
                    Customer = a.Customer.FullName,
                    Email = a.Customer.Email,
                    Service = a.ServiceOffering.Name,
                    Date = a.StartUtc.ToLocalTime().ToString("yyyy-MM-dd"),
                    Start = a.StartUtc.ToLocalTime().ToString("HH:mm"),
                    End = a.EndUtc.ToLocalTime().ToString("HH:mm"),
                    Price = a.PriceAtBooking
                })
                .ToList();

            appointmentsGrid.DataSource = list;

            if (appointmentsGrid.Columns["Id"] != null)
                appointmentsGrid.Columns["Id"].Width = 60;
        }

        // ===============================================================
        // SHOW APPOINTMENT DETAILS BELOW
        // ===============================================================
        private void ShowAppointmentDetails()
        {
            if (appointmentsGrid.CurrentRow == null)
                return;

            if (!int.TryParse(appointmentsGrid.CurrentRow.Cells["Id"].Value.ToString(), out int id))
                return;

            var appt = _db.Appointments
                .Include(a => a.Customer)
                .Include(a => a.ServiceOffering)
                .FirstOrDefault(a => a.Id == id);

            if (appt == null) return;

            lblCustomerInfo.Text =
                $"Name: {appt.Customer.FullName}\nEmail: {appt.Customer.Email}";

            lblServiceInfo.Text =
                $"Service: {appt.ServiceOffering.Name}";

            lblDateTimeInfo.Text =
                $"Date: {appt.StartUtc.ToLocalTime():yyyy-MM-dd}\n" +
                $"Time: {appt.StartUtc.ToLocalTime():HH:mm} - {appt.EndUtc.ToLocalTime():HH:mm}";
        }

        // ===============================================================
        // APPROVE APPOINTMENT
        // ===============================================================
        private void btnApprove_Click(object sender, EventArgs e)
        {
            if (!TryGetSelectedAppointmentId(out int id))
                return;

            var appt = _db.Appointments.FirstOrDefault(a => a.Id == id);

            if (appt == null)
            {
                MessageBox.Show("Appointment not found!", "Error");
                return;
            }

            appt.Status = AppointmentStatus.Approved;
            _db.SaveChanges();

            MessageBox.Show("Appointment approved!", "Success");

            LoadPendingAppointments();   // Refresh grid
        }

        // ===============================================================
        // REJECT APPOINTMENT
        // ===============================================================


        // ===============================================================
        // HELPER — SAFELY GET SELECTED APPOINTMENT ID
        // ===============================================================
        private bool TryGetSelectedAppointmentId(out int id)
        {
            id = 0;

            if (appointmentsGrid.CurrentRow == null)
            {
                MessageBox.Show("Please select an appointment.");
                return false;
            }

            if (!int.TryParse(appointmentsGrid.CurrentRow.Cells["Id"].Value.ToString(), out id))
            {
                MessageBox.Show("Invalid appointment ID!");
                return false;
            }

            return true;
        }

        private void btnReject_Click(object sender, EventArgs e)
        {
            if (!TryGetSelectedAppointmentId(out int id))
                return;

            var appt = _db.Appointments.FirstOrDefault(a => a.Id == id);

            if (appt == null)
            {
                MessageBox.Show("Appointment not found!", "Error");
                return;
            }

            appt.Status = AppointmentStatus.Rejected;
            _db.SaveChanges();

            MessageBox.Show("Appointment rejected.", "Rejected");

            LoadPendingAppointments();   // Refresh grid
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            var adminPage = Program.Services.GetRequiredService<LoginForm>();
            adminPage.Show();
            this.Hide();
        }

        private void LogoutBtn_Click(object sender, EventArgs e)
        {
            var adminPage = Program.Services.GetRequiredService<LoginForm>();
            adminPage.Show();
            this.Close();
        }
    }
}
