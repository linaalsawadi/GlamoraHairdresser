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

        public AdminHoursForm(GlamoraHairdresser.Data.GlamoraDbContext db)
        {
            InitializeComponent();
            _db = db;
        }

        // ---------- helpers ----------
        private static bool TryParseTimeOnly(object value, out TimeOnly t)
        {
            t = default;
            if (value is TimeOnly tt) { t = tt; return true; }
            var s = Convert.ToString(value);
            return !string.IsNullOrWhiteSpace(s) && TimeOnly.TryParse(s, out t);
        }
        // -----------------------------

        private async void AdminHoursForm_Load(object sender, EventArgs e)
        {
            await LoadSalonsAsync();

            // inline validation & input safety
            HoursGrid.CellValidating += HoursGrid_CellValidating;
            HoursGrid.DataError += (s, args) =>
            {
                // prevent grid from throwing on bad input
                args.Cancel = true;
                if (args.Exception != null)
                    MessageBox.Show("Invalid time. Use HH:mm (e.g., 09:00).",
                                    "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            };
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

        // Validate while editing: if day is open, CloseTime must be > OpenTime
        private void HoursGrid_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            var grid = (DataGridView)sender;
            var row = grid.Rows[e.RowIndex];
            var col = grid.Columns[e.ColumnIndex].Name;
            row.ErrorText = string.Empty;

            if (col is "OpenTime" or "CloseTime")
            {
                bool isOpen = Convert.ToBoolean(row.Cells["IsOpen"].Value ?? false);
                if (!isOpen) return; // closed day → ignore times

                var openObj = col == "OpenTime" ? e.FormattedValue : row.Cells["OpenTime"].Value;
                var closeObj = col == "CloseTime" ? e.FormattedValue : row.Cells["CloseTime"].Value;

                if (!TryParseTimeOnly(openObj, out var open) || !TryParseTimeOnly(closeObj, out var close))
                {
                    e.Cancel = true;
                    row.ErrorText = "Time must be in HH:mm (e.g., 09:00).";
                    return;
                }

                if (open >= close)
                {
                    e.Cancel = true;
                    row.ErrorText = "Close time must be after open time.";
                    MessageBox.Show("Invalid time range: close time must be greater than open time.",
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void SaveBtn_Click(object sender, EventArgs e)
        {
            if (SalonComboBox.SelectedValue is not int salonId)
            {
                MessageBox.Show("Please select a salon first.", "Warning",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // -------- final pass: block any close ≤ open before touching EF/SQL --------
            foreach (DataGridViewRow row in HoursGrid.Rows)
            {
                if (row.IsNewRow) continue;
                if (row.Cells["Day"].Value == null) continue;

                bool isOpen = Convert.ToBoolean(row.Cells["IsOpen"].Value ?? false);
                if (!isOpen) continue;

                var openStr = row.Cells["OpenTime"].Value?.ToString() ?? "09:00";
                var closeStr = row.Cells["CloseTime"].Value?.ToString() ?? "17:00";

                if (!TimeOnly.TryParse(openStr, out var open) ||
                    !TimeOnly.TryParse(closeStr, out var close))
                {
                    MessageBox.Show($"[{row.Cells["DayName"].Value}] Time must be HH:mm (e.g., 09:00).",
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    HoursGrid.CurrentCell = row.Cells["OpenTime"];
                    return;
                }

                if (open >= close)
                {
                    MessageBox.Show($"[{row.Cells["DayName"].Value}] Invalid time range: close ≤ open.",
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    HoursGrid.CurrentCell = row.Cells["CloseTime"];
                    return;
                }
            }
            // ---------------------------------------------------------------------------

            // Persist changes
            foreach (DataGridViewRow row in HoursGrid.Rows)
            {
                if (row.IsNewRow) continue;
                if (row.Cells["Day"].Value == null) continue;

                int day = Convert.ToInt32(row.Cells["Day"].Value);
                bool isOpen = Convert.ToBoolean(row.Cells["IsOpen"].Value);
                string openStr = row.Cells["OpenTime"].Value?.ToString() ?? "09:00";
                string closeStr = row.Cells["CloseTime"].Value?.ToString() ?? "17:00";

                TimeOnly.TryParse(openStr, out var open);
                TimeOnly.TryParse(closeStr, out var close);

                var existing = await _db.WorkingHours
                    .FirstOrDefaultAsync(h => h.SalonId == salonId && h.DayOfWeek == day);

                if (!isOpen)
                {
                    if (existing == null)
                    {
                        _db.WorkingHours.Add(new WorkingHour
                        {
                            SalonId = salonId,
                            DayOfWeek = day,
                            IsOpen = false,
                            OpenTime = new TimeOnly(0, 0),
                            CloseTime = new TimeOnly(0, 0),
                            CreatedAt = DateTime.UtcNow
                        });
                    }
                    else
                    {
                        existing.IsOpen = false;
                        existing.OpenTime = new TimeOnly(0, 0);
                        existing.CloseTime = new TimeOnly(0, 0);
                    }
                    continue;
                }

                if (existing == null)
                {
                    _db.WorkingHours.Add(new WorkingHour
                    {
                        SalonId = salonId,
                        DayOfWeek = day,
                        IsOpen = true,
                        OpenTime = open,
                        CloseTime = close,
                        CreatedAt = DateTime.UtcNow
                    });
                }
                else
                {
                    existing.IsOpen = true;
                    existing.OpenTime = open;
                    existing.CloseTime = close;
                }
            }

            try
            {
                await _db.SaveChangesAsync();
                MessageBox.Show("Working hours saved successfully!",
                                "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await LoadSalonHoursAsync(salonId); // refresh
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while saving: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
