using GlamoraHairdresser.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Windows.Forms;

namespace GlamoraHairdresser.WinForms.Forms.AdminForms
{
    public partial class AdminAppointmentForm : Form
    {
        private readonly GlamoraDbContext _db;

        public AdminAppointmentForm(GlamoraDbContext db)
        {
            InitializeComponent();
            _db = db;

            LoadAppointments();
        }

        // =====================================================
        // LOAD ALL APPOINTMENTS (LIST ONLY)
        // =====================================================
        private void LoadAppointments()
        {
            try
            {
                var list = _db.Appointments
                    .Include(a => a.Customer)
                    .Include(a => a.Worker)
                    .Include(a => a.ServiceOffering)
                    .OrderByDescending(a => a.StartUtc)
                    .Select(a => new
                    {
                        a.Id,
                        Customer = a.Customer.FullName,
                        Worker = a.Worker.FullName,
                        Service = a.ServiceOffering.Name,
                        Date = a.StartUtc.ToLocalTime().ToString("yyyy-MM-dd"),
                        Start = a.StartUtc.ToLocalTime().ToString("HH:mm"),
                        End = a.EndUtc.ToLocalTime().ToString("HH:mm"),
                        Price = a.PriceAtBooking,
                        Status = a.Status.ToString()
                    })
                    .ToList();

                dgvAppointments.DataSource = list;
                FormatGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading appointments:\n" + ex.Message);
            }
        }

        // =====================================================
        // FORMAT GRID
        // =====================================================
        private void FormatGrid()
        {
            if (dgvAppointments.Columns["Id"] != null)
            {
                dgvAppointments.Columns["Id"].Width = 50;
                dgvAppointments.Columns["Id"].HeaderText = "ID";
            }

            if (dgvAppointments.Columns["Customer"] != null)
                dgvAppointments.Columns["Customer"].Width = 150;

            if (dgvAppointments.Columns["Worker"] != null)
                dgvAppointments.Columns["Worker"].Width = 150;

            if (dgvAppointments.Columns["Service"] != null)
                dgvAppointments.Columns["Service"].Width = 150;

            if (dgvAppointments.Columns["Price"] != null)
                dgvAppointments.Columns["Price"].DefaultCellStyle.Format = "0.00";
        }

        // =====================================================
        // REFRESH BUTTON
        // =====================================================     

        private void btnBack_Click(object sender, EventArgs e)
        {
            var adminPage = Program.Services.GetRequiredService<AdminDashboard>();
            adminPage.Show();
        }

        private void btnRefresh_Click_1(object sender, EventArgs e)
        {
            LoadAppointments();
        }
    }
}
