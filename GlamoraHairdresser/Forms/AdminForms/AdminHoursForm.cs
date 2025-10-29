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
                .OrderBy(h => h.DayOfWeek)
                .ToListAsync();

            // تعبئة الجدول بالأيام (0-6)
            string[] days = { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };

            for (int i = 0; i < 7; i++)
            {
                var existing = hours.FirstOrDefault(h => h.DayOfWeek == i);
                HoursGrid.Rows.Add(
                    i,                                    // Day (0–6)
                    days[i],                              // Day Name
                    existing?.IsOpen ?? false,            // Is Open
                    existing?.OpenTime.ToString("HH:mm") ?? "09:00",
                    existing?.CloseTime.ToString("HH:mm") ?? "17:00"
                );
            }
        }

        private async void SaveBtn_Click(object sender, EventArgs e)
        {
            if (SalonComboBox.SelectedValue is not int salonId)
            {
                MessageBox.Show("⚠️ Please select a salon first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            foreach (DataGridViewRow row in HoursGrid.Rows)
            {
                if (row.IsNewRow) continue;
                if (row.Cells["Day"].Value == null) continue;

                int day = Convert.ToInt32(row.Cells["Day"].Value);
                bool isOpen = Convert.ToBoolean(row.Cells["IsOpen"].Value);
                string openStr = row.Cells["OpenTime"].Value?.ToString() ?? "09:00";
                string closeStr = row.Cells["CloseTime"].Value?.ToString() ?? "17:00";

                if (!TimeOnly.TryParse(openStr, out var open))
                    open = new TimeOnly(9, 0);
                if (!TimeOnly.TryParse(closeStr, out var close))
                    close = new TimeOnly(17, 0);

                var existing = await _db.WorkingHours
                    .FirstOrDefaultAsync(h => h.SalonId == salonId && h.DayOfWeek == day);

                if (!isOpen)
                {
                    if (existing != null)
                        _db.WorkingHours.Remove(existing);
                    continue;
                }

                if (existing == null)
                {
                    // ➕ إضافة سجل جديد
                    var newHour = new WorkingHour
                    {
                        SalonId = salonId,
                        DayOfWeek = day,
                        IsOpen = isOpen,
                        OpenTime = open,
                        CloseTime = close,
                        CreatedAt = DateTime.UtcNow
                    };
                    _db.WorkingHours.Add(newHour);
                }
                else
                {
                    // ✏️ تحديث السجل الحالي
                    existing.IsOpen = isOpen;
                    existing.OpenTime = open;
                    existing.CloseTime = close;
                }
            }

            try
            {
                await _db.SaveChangesAsync();
                MessageBox.Show("✅ Working hours saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await LoadSalonHoursAsync(salonId); // تحديث مباشر بعد الحفظ
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Error while saving: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
