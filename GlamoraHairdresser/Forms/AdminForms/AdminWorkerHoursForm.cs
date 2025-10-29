using GlamoraHairdresser.Data;
using GlamoraHairdresser.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GlamoraHairdresser.WinForms.Forms.AdminForms
{
    public partial class AdminWorkerHoursForm : Form
    {
        private readonly GlamoraDbContext _db;

        public AdminWorkerHoursForm(GlamoraDbContext db)
        {
            InitializeComponent();
            _db = db;
        }

        // ===================== Helpers =====================
        private static bool IsSalonClosedCell(object salonHoursCellValue)
        {
            var s = salonHoursCellValue?.ToString()?.Trim();
            return string.Equals(s, "Closed", StringComparison.OrdinalIgnoreCase);
        }

        private static bool TryParseTimeOnly(object value, out TimeOnly t)
        {
            t = default;
            if (value is TimeOnly tt) { t = tt; return true; }
            var s = Convert.ToString(value);
            if (string.IsNullOrWhiteSpace(s)) return false;
            return TimeOnly.TryParse(s, out t);
        }

        private void ApplyRowGuards()
        {
            foreach (DataGridViewRow row in HoursGrid.Rows)
            {
                if (row.IsNewRow) continue;

                bool salonClosed = IsSalonClosedCell(row.Cells["SalonHours"].Value);

                var cIsOpen = row.Cells["IsOpen"];
                var cOpen = row.Cells["OpenTime"];
                var cClose = row.Cells["CloseTime"];

                if (salonClosed)
                {
                    cIsOpen.Value = false;
                    cIsOpen.ReadOnly = true;
                    cOpen.ReadOnly = true;
                    cClose.ReadOnly = true;

                    foreach (var c in new[] { cIsOpen, cOpen, cClose })
                    {
                        c.Style.ForeColor = Color.Gray;
                        c.Style.BackColor = Color.Gainsboro;
                        c.ToolTipText = "Salon is closed this day.";
                    }
                }
                else
                {
                    cIsOpen.ReadOnly = false;
                    cOpen.ReadOnly = false;
                    cClose.ReadOnly = false;

                    foreach (var c in new[] { cIsOpen, cOpen, cClose })
                    {
                        c.Style.ForeColor = HoursGrid.DefaultCellStyle.ForeColor;
                        c.Style.BackColor = HoursGrid.DefaultCellStyle.BackColor;
                        c.ToolTipText = string.Empty;
                    }
                }
            }
        }
        // ====================================================

        private async void AdminWorkerHoursForm_Load(object sender, EventArgs e)
        {
            await LoadWorkersAsync();

            if (HoursGrid.Columns.Contains("SalonHours"))
            {
                HoursGrid.Columns["SalonHours"].ReadOnly = true;
                HoursGrid.Columns["SalonHours"].DefaultCellStyle.ForeColor = Color.Gray;
            }

            // Wire events
            HoursGrid.CellBeginEdit += HoursGrid_CellBeginEdit;
            HoursGrid.CellValidating += HoursGrid_CellValidating;
            HoursGrid.DataBindingComplete += (_, __) => ApplyRowGuards();
            HoursGrid.DataError += (s, args) =>
            {
                // prevent exceptions on parse errors
                args.Cancel = true;
                if (args.Exception != null)
                    MessageBox.Show("Invalid value. Use time format HH:mm (e.g., 09:00).",
                                    "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            };
        }

        private async Task LoadWorkersAsync()
        {
            var workers = await _db.Workers
                .Include(w => w.Salon)
                .AsNoTracking()
                .ToListAsync();

            WorkerComboBox.DataSource = workers;
            WorkerComboBox.DisplayMember = "FullName"; // or "Name"
            WorkerComboBox.ValueMember = "Id";
        }

        private async void WorkerComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (WorkerComboBox.SelectedValue is int workerId)
                await LoadWorkerHoursAsync(workerId);
        }

        private async Task LoadWorkerHoursAsync(int workerId)
        {
            HoursGrid.Rows.Clear();

            var worker = await _db.Workers
                .Include(w => w.Salon)
                .FirstOrDefaultAsync(w => w.Id == workerId);

            if (worker == null)
            {
                MessageBox.Show("Worker not found.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var salonHours = await _db.WorkingHours
                .Where(h => h.SalonId == worker.SalonId)
                .OrderBy(h => h.DayOfWeek)
                .ToListAsync();

            var workerHours = await _db.WorkerWorkingHours
                .Where(h => h.WorkerId == workerId)
                .OrderBy(h => h.DayOfWeek)
                .ToListAsync();

            string[] days = { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };

            for (int i = 0; i < 7; i++)
            {
                var salonDay = salonHours.FirstOrDefault(h => h.DayOfWeek == i);
                var workerDay = workerHours.FirstOrDefault(h => h.DayOfWeek == i);

                bool isOpen = workerDay?.IsOpen ?? (salonDay?.IsOpen ?? false);

                string salonTime = (salonDay != null && salonDay.IsOpen)
                    ? $"{salonDay.OpenTime:HH:mm} - {salonDay.CloseTime:HH:mm}"
                    : "Closed";

                string openTime = workerDay?.OpenTime.ToString("HH:mm")
                                  ?? salonDay?.OpenTime.ToString("HH:mm")
                                  ?? "09:00";

                string closeTime = workerDay?.CloseTime.ToString("HH:mm")
                                   ?? salonDay?.CloseTime.ToString("HH:mm")
                                   ?? "17:00";

                HoursGrid.Rows.Add(
                    i,          // Day (0–6)
                    days[i],    // Day Name
                    salonTime,  // Salon Hours
                    isOpen,     // Is Open
                    openTime,   // Open Time
                    closeTime   // Close Time
                );
            }

            ApplyRowGuards();
        }

        // Block editing on closed salon days
        private void HoursGrid_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            var col = HoursGrid.Columns[e.ColumnIndex].Name;
            if (col is "IsOpen" or "OpenTime" or "CloseTime")
            {
                var row = HoursGrid.Rows[e.RowIndex];
                if (IsSalonClosedCell(row.Cells["SalonHours"].Value))
                {
                    e.Cancel = true;
                    MessageBox.Show("Salon is closed on this day. You cannot set worker hours.",
                                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        // Validate time logic while editing
        private void HoursGrid_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            var grid = (DataGridView)sender;
            var row = grid.Rows[e.RowIndex];
            var colName = grid.Columns[e.ColumnIndex].Name;
            row.ErrorText = string.Empty;

            if (colName == "IsOpen")
            {
                bool newVal = false;
                if (e.FormattedValue is bool b) newVal = b;
                else bool.TryParse(Convert.ToString(e.FormattedValue), out newVal);

                if (IsSalonClosedCell(row.Cells["SalonHours"].Value) && newVal)
                {
                    e.Cancel = true;
                    row.ErrorText = "Salon is closed on this day.";
                    MessageBox.Show("Cannot enable working hours because the salon is closed.",
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (colName is "OpenTime" or "CloseTime")
            {
                bool isOpen = Convert.ToBoolean(row.Cells["IsOpen"].Value ?? false);
                if (!isOpen) return;

                var openObj = colName == "OpenTime" ? e.FormattedValue : row.Cells["OpenTime"].Value;
                var closeObj = colName == "CloseTime" ? e.FormattedValue : row.Cells["CloseTime"].Value;

                if (TryParseTimeOnly(openObj, out var open) && TryParseTimeOnly(closeObj, out var close))
                {
                    if (open >= close)
                    {
                        e.Cancel = true;
                        row.ErrorText = "Close time must be after open time.";
                        MessageBox.Show("Invalid time range: close time must be greater than open time.",
                                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    e.Cancel = true;
                    row.ErrorText = "Time must be in HH:mm (e.g., 09:00).";
                }
            }
        }

        private async void SaveBtn_Click(object sender, EventArgs e)
        {
            if (WorkerComboBox.SelectedValue is not int workerId)
            {
                MessageBox.Show("Please select a worker first.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var worker = await _db.Workers
                .Include(w => w.Salon)
                .FirstOrDefaultAsync(w => w.Id == workerId);

            if (worker == null)
            {
                MessageBox.Show("Worker not found.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var salonHours = await _db.WorkingHours
                .Where(h => h.SalonId == worker.SalonId)
                .ToListAsync();

            foreach (DataGridViewRow row in HoursGrid.Rows)
            {
                if (row.IsNewRow) continue;
                if (row.Cells["Day"].Value == null) continue;

                int day = Convert.ToInt32(row.Cells["Day"].Value);
                bool isOpen = Convert.ToBoolean(row.Cells["IsOpen"].Value ?? false);

                string openStr = row.Cells["OpenTime"].Value?.ToString() ?? "09:00";
                string closeStr = row.Cells["CloseTime"].Value?.ToString() ?? "17:00";

                if (!TimeOnly.TryParse(openStr, out var open))
                    open = new TimeOnly(9, 0);
                if (!TimeOnly.TryParse(closeStr, out var close))
                    close = new TimeOnly(17, 0);

                var salonDay = salonHours.FirstOrDefault(h => h.DayOfWeek == day);

                // 1) No work on closed salon days
                if (salonDay == null || !salonDay.IsOpen)
                {
                    if (isOpen)
                    {
                        MessageBox.Show($"[{row.Cells["DayName"].Value}] Salon is closed. You cannot set worker hours.",
                                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        HoursGrid.CurrentCell = row.Cells["IsOpen"];
                        return;
                    }
                }
                else
                {
                    // 2) Close > Open
                    if (isOpen && open >= close)
                    {
                        MessageBox.Show($"[{row.Cells["DayName"].Value}] Invalid time range (close ≤ open).",
                                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        HoursGrid.CurrentCell = row.Cells["CloseTime"];
                        return;
                    }

                    // 3) Worker hours must be within salon hours
                    if (isOpen && (open < salonDay.OpenTime || close > salonDay.CloseTime))
                    {
                        MessageBox.Show(
                            $"[{row.Cells["DayName"].Value}] Worker hours must be within salon hours ({salonDay.OpenTime:HH:mm} - {salonDay.CloseTime:HH:mm}).",
                            "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        HoursGrid.CurrentCell = row.Cells["OpenTime"];
                        return;
                    }
                }

                var existing = await _db.WorkerWorkingHours
                    .FirstOrDefaultAsync(h => h.WorkerId == workerId && h.DayOfWeek == day);

                if (existing == null)
                {
                    _db.WorkerWorkingHours.Add(new WorkerWorkingHour
                    {
                        WorkerId = workerId,
                        DayOfWeek = day,
                        IsOpen = isOpen,
                        OpenTime = isOpen ? open : new TimeOnly(0, 0),
                        CloseTime = isOpen ? close : new TimeOnly(0, 0)
                    });
                }
                else
                {
                    existing.IsOpen = isOpen;
                    existing.OpenTime = isOpen ? open : new TimeOnly(0, 0);
                    existing.CloseTime = isOpen ? close : new TimeOnly(0, 0);
                }
            }

            try
            {
                await _db.SaveChangesAsync();
                MessageBox.Show("Worker hours saved successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
