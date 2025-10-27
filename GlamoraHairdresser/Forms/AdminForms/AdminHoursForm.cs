using GlamoraHairdresser.Data;
using GlamoraHairdresser.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GlamoraHairdresser.WinForms.Forms.AdminForms
{
    public partial class AdminHoursForm : Form
    {
        private readonly GlamoraDbContext _db;

        public AdminHoursForm(GlamoraDbContext db)
        {
            InitializeComponent();
            _db = db;
        }

        private async void AdminHoursForm_Load(object sender, EventArgs e)
        {
            await LoadSalonsAsync();
        }

        private async Task LoadSalonsAsync()
        {
            var salons = await _db.Salons.AsNoTracking().ToListAsync();
            SalonComboBox.DataSource = salons;
            SalonComboBox.DisplayMember = "Name";
            SalonComboBox.ValueMember = "Id";
        }

        private async void SalonComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SalonComboBox.SelectedValue is int salonId)
                await LoadSalonHoursAsync(salonId);
        }

        private async Task LoadSalonHoursAsync(int salonId)
        {
            HoursGrid.Rows.Clear();

            var hours = await _db.WorkingHours
                .Where(h => h.SalonId == salonId)
                .AsNoTracking()
                .ToListAsync();

            for (int i = 1; i <= 7; i++)
            {
                var existing = hours.FirstOrDefault(h => h.DayOfWeek == i);
                HoursGrid.Rows.Add(
                    i,
                    GetDayName(i),
                    existing != null,
                    existing?.OpenTime.ToString("HH:mm") ?? "",
                    existing?.CloseTime.ToString("HH:mm") ?? ""
                );
            }
        }

        private string GetDayName(int day)
        {
            return day switch
            {
                1 => "Monday",
                2 => "Tuesday",
                3 => "Wednesday",
                4 => "Thursday",
                5 => "Friday",
                6 => "Saturday",
                7 => "Sunday",
                _ => "Unknown"
            };
        }

        private async void SaveBtn_Click(object sender, EventArgs e)
        {
            if (SalonComboBox.SelectedValue is not int salonId)
                return;

            foreach (DataGridViewRow row in HoursGrid.Rows)
            {
                if (row.Cells["Day"].Value == null) continue;

                int day = Convert.ToInt32(row.Cells["Day"].Value);
                bool isOpen = Convert.ToBoolean(row.Cells["IsOpen"].Value);
                string openStr = row.Cells["OpenTime"].Value?.ToString();
                string closeStr = row.Cells["CloseTime"].Value?.ToString();

                var existing = await _db.WorkingHours.FirstOrDefaultAsync(h => h.SalonId == salonId && h.DayOfWeek == day);

                if (!isOpen)
                {
                    if (existing != null)
                        _db.WorkingHours.Remove(existing);
                    continue;
                }

                if (!TimeOnly.TryParse(openStr, out var open) || !TimeOnly.TryParse(closeStr, out var close))
                    continue;

                if (existing == null)
                {
                    _db.WorkingHours.Add(new WorkingHour
                    {
                        SalonId = salonId,
                        DayOfWeek = day,
                        IsOpen = isOpen,
                        OpenTime = open,
                        CloseTime = close
                    });
                }
                else
                {
                    existing.IsOpen = isOpen;
                    existing.OpenTime = open;
                    existing.CloseTime = close;
                }
            }

            await _db.SaveChangesAsync();
            MessageBox.Show("Saved successfully!", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void HoursGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
