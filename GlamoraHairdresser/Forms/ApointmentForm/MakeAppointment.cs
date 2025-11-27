using GlamoraHairdresser.Data;
using GlamoraHairdresser.Data.Entities;
using GlamoraHairdresser.Services.Auth;
using GlamoraHairdresser.WinForms.Forms.AdminForms;
using GlamoraHairdresser.WinForms.Forms.AuthForms;
using GlamoraHairdresser.WinForms.Forms.CustomerForms;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace GlamoraHairdresser.WinForms.Forms.ApointmentForm
{
    public partial class MakeAppointment : Form
    {
        private readonly GlamoraDbContext _db;
        private int _selectedSalonId;
        private int _selectedServiceId;
        private int _selectedWorkerId;

        public MakeAppointment(GlamoraDbContext db)
        {
            InitializeComponent();
            _db = db;

            Load += MakeAppointment_Load;
            datePicker.ValueChanged += (_, __) => LoadSalons();
            salonCmb.SelectedIndexChanged += (_, __) => LoadServices();
            serviceCmb.SelectedIndexChanged += (_, __) => LoadWorkers();
            workerCmb.SelectedIndexChanged += (_, __) => LoadAvailableSlots();
            bookBtn.Click += BookBtn_Click;
        }

        // ========================= LOAD SALONS =========================
        private void MakeAppointment_Load(object sender, EventArgs e)
        {
            datePicker.MinDate = DateTime.Today;
            LoadSalons();
        }

        private void LoadSalons()
        {
            var list = _db.Salons
                .OrderBy(s => s.Name)
                .Select(s => new { s.Id, s.Name })
                .ToList();

            salonCmb.DataSource = list;
            salonCmb.DisplayMember = "Name";
            salonCmb.ValueMember = "Id";
        }

        // ========================= LOAD SERVICES =========================
        private void LoadServices()
        {
            if (salonCmb.SelectedValue is not int sid) return;
            _selectedSalonId = sid;

            var list = _db.ServiceOfferings
                .Where(s => s.SalonId == sid)
                .OrderBy(s => s.Name)
                .Select(s => new { s.Id, s.Name, s.DurationMinutes })
                .ToList();

            serviceCmb.DataSource = list;
            serviceCmb.DisplayMember = "Name";
            serviceCmb.ValueMember = "Id";
        }

        // ========================= LOAD WORKERS =========================
        private void LoadWorkers()
        {
            if (serviceCmb.SelectedValue is not int serviceId) return;
            _selectedServiceId = serviceId;

            var workerIds = _db.EmployeeSkills
                .Where(es => es.ServiceOfferingId == serviceId)
                .Select(es => es.WorkerId)
                .ToList();

            var workers = _db.Workers
                .Where(w => workerIds.Contains(w.Id))
                .Select(w => new { w.Id, w.FullName })
                .ToList();

            workerCmb.DataSource = workers;
            workerCmb.DisplayMember = "FullName";
            workerCmb.ValueMember = "Id";
        }

        // ===================== LOAD AVAILABLE SLOTS =====================
        private void LoadAvailableSlots()
        {
            timeSlotsList.Items.Clear();

            if (workerCmb.SelectedValue is not int wid) return;
            _selectedWorkerId = wid;

            DateTime selectedDay = datePicker.Value.Date;

            var salonDay = _db.WorkingHours
                .Where(h => h.SalonId == _selectedSalonId && h.DayOfWeek == (int)selectedDay.DayOfWeek)
                .FirstOrDefault();

            if (salonDay == null || !salonDay.IsOpen)
            {
                timeSlotsList.Items.Add("Salon is closed this day.");
                return;
            }

            var workerDay = _db.WorkerWorkingHours
                .Where(h => h.WorkerId == wid && h.DayOfWeek == (int)selectedDay.DayOfWeek)
                .FirstOrDefault();

            bool isOpen = workerDay?.IsOpen ?? salonDay.IsOpen;

            TimeOnly open = workerDay?.OpenTime ?? salonDay.OpenTime;
            TimeOnly close = workerDay?.CloseTime ?? salonDay.CloseTime;

            var service = _db.ServiceOfferings.Find(_selectedServiceId);
            int duration = service.DurationMinutes;

            var existing = _db.Appointments
                .Where(a => a.WorkerId == wid && a.StartUtc.Date == selectedDay)
                .Select(a => new
                {
                    Start = a.StartUtc.ToLocalTime(),
                    End = a.EndUtc.ToLocalTime()
                })
                .ToList();

            DateTime slot = selectedDay.AddHours(open.Hour).AddMinutes(open.Minute);
            DateTime endOfDay = selectedDay.AddHours(close.Hour).AddMinutes(close.Minute);

            while (slot.AddMinutes(duration) <= endOfDay)
            {
                bool clash = existing.Any(a =>
                    slot < a.End &&
                    slot.AddMinutes(duration) > a.Start
                );

                if (!clash)
                    timeSlotsList.Items.Add(slot.ToString("HH:mm"));

                slot = slot.AddMinutes(duration);
            }

            if (timeSlotsList.Items.Count == 0)
                timeSlotsList.Items.Add("No available hours — try another day.");
        }

        // ========================= BOOK APPOINTMENT =========================
        private void BookBtn_Click(object sender, EventArgs e)
        {
            if (timeSlotsList.SelectedItem == null ||
                timeSlotsList.SelectedItem.ToString().Contains("No available") ||
                timeSlotsList.SelectedItem.ToString().Contains("closed"))
            {
                MessageBox.Show("Please select a valid slot.");
                return;
            }

            DateTime day = datePicker.Value.Date;
            string time = timeSlotsList.SelectedItem.ToString();

            DateTime start = DateTime.Parse($"{day:yyyy-MM-dd} {time}");
            var service = _db.ServiceOfferings.Find(_selectedServiceId);

            var appointment = new Appointment
            {
                CustomerId = SessionManager.CurrentUser.Id,
                SalonId = _selectedSalonId,
                ServiceOfferingId = _selectedServiceId,
                WorkerId = _selectedWorkerId,
                StartUtc = start.ToUniversalTime(),
                EndUtc = start.AddMinutes(service.DurationMinutes).ToUniversalTime(),
                Status = AppointmentStatus.Pending,
                PriceAtBooking = service.Price
            };

            _db.Appointments.Add(appointment);
            _db.SaveChanges();

            MessageBox.Show("Appointment booked! Waiting for worker confirmation.");

            this.Close();
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            var adminPage = Program.Services.GetRequiredService<CustomerDashboard>();
            adminPage.Show();
            this.Close();
        }

        private void BookLogoutBtn_Click(object sender, EventArgs e)
        {
            var adminPage = Program.Services.GetRequiredService<CustomerDashboard>();
            adminPage.Show();
        }
    }
}
