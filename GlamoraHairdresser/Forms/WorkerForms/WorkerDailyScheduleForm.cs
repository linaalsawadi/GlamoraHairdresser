using GlamoraHairdresser.Data;
using GlamoraHairdresser.Data.Entities;
using GlamoraHairdresser.Services.Auth;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows.Forms;

namespace GlamoraHairdresser.WinForms.Forms.WorkerForms
{
    public partial class WorkerDailyScheduleForm : Form
    {
        private readonly GlamoraDbContext _db;
        private readonly int _workerId;

        public WorkerDailyScheduleForm(GlamoraDbContext db)
        {
            InitializeComponent();
            _db = db;
            _workerId = SessionManager.CurrentUser.Id;

            dgvSchedule.DataSource = null; // جدول فاضي أول الدخول
        }

        // ============================================
        // LOAD BUTTON
        // ============================================
        private void btnLoad_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            dgvSchedule.DataSource = null;

            DateTime selectedDate = datePicker.Value.Date;

            // check if worker is off
            var workerDay = _db.WorkerWorkingHours
                .FirstOrDefault(h => h.WorkerId == _workerId &&
                                     h.DayOfWeek == (int)selectedDate.DayOfWeek);

            if (workerDay == null || workerDay.IsOpen == false)
            {
                lblMessage.Text = "You are off this day.";
                return;
            }

            // FIX: search by UTC range
            DateTime dayStartUtc = selectedDate.ToUniversalTime();
            DateTime dayEndUtc = selectedDate.AddDays(1).ToUniversalTime();

            var appts = _db.Appointments
                .Include(a => a.Customer)
                .Include(a => a.ServiceOffering)
                .Where(a =>
                    a.WorkerId == _workerId &&
                    a.Status == AppointmentStatus.Approved &&
                    a.StartUtc >= dayStartUtc &&
                    a.StartUtc < dayEndUtc
                )
                .OrderBy(a => a.StartUtc)
                .Select(a => new
                {
                    Id = a.Id,
                    Customer = a.Customer.FullName,
                    Service = a.ServiceOffering.Name,
                    Date = a.StartUtc.ToLocalTime().ToString("yyyy-MM-dd"),
                    Start = a.StartUtc.ToLocalTime().ToString("HH:mm"),
                    End = a.EndUtc.ToLocalTime().ToString("HH:mm")
                })
                .ToList();

            if (appts.Count == 0)
            {
                lblMessage.Text = "No appointments for this day.";
                return;
            }

            dgvSchedule.DataSource = appts;
        }


        // ============================================
        // BACK BUTTON
        // ============================================
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
