using GlamoraHairdresser.Data;
using GlamoraHairdresser.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace GlamoraHairdresser.WinForms.Forms.AdminForms
{
    public partial class AdminHoursForm : Form
    {
        private readonly GlamoraDbContext _db;

        // serialize EF usage + guard flags
        private readonly SemaphoreSlim _dbGate = new(1, 1);
        private bool _suppressSelectionChanged = false;
        private bool _isLoadingHours = false;
        private bool _isSaving = false;

        // accept minutes (13:30 / 14:20) and dot by mistake
        private static readonly string[] TimeFormats = { "HH:mm", "H:mm", "HH.mm", "H.mm" };

        public AdminHoursForm(GlamoraDbContext db)
        {
            InitializeComponent();
            _db = db;

            Load += AdminHoursForm_Load;
            SalonComboBox.SelectedIndexChanged += SalonComboBox_SelectedIndexChanged;

            // grid events
            HoursGrid.CellValidating += HoursGrid_CellValidating;
            HoursGrid.CellParsing += HoursGrid_CellParsing;
            HoursGrid.EditingControlShowing += HoursGrid_EditingControlShowing;
            HoursGrid.CurrentCellDirtyStateChanged += HoursGrid_CurrentCellDirtyStateChanged;
            HoursGrid.CellValueChanged += HoursGrid_CellValueChanged;
            HoursGrid.CellBeginEdit += HoursGrid_CellBeginEdit;
            HoursGrid.DataBindingComplete += (_, __) => ApplyRowGuards();
            HoursGrid.DataError += (s, e) => { e.Cancel = true; };

            SaveBtn.Click += SaveBtn_Click;
        }

        // ---------- helpers ----------
        private static bool TryParseFlexibleTime(object value, out TimeOnly t)
        {
            t = default;
            if (value is TimeOnly ti) { t = ti; return true; }

            var s = Convert.ToString(value)?.Trim();
            if (string.IsNullOrWhiteSpace(s)) return false;

            if (TimeOnly.TryParseExact(s, TimeFormats, CultureInfo.InvariantCulture,
                                       DateTimeStyles.None, out t))
                return true;

            if (int.TryParse(s, out var hour) && hour >= 0 && hour <= 23)
            {
                t = new TimeOnly(hour, 0);
                return true;
            }
            return false;
        }
        private static string ToHHmm(TimeOnly t) => t.ToString("HH:mm");

        // ---------- UI guards for IsOpen ----------
        private void ApplyRowGuards()
        {
            foreach (DataGridViewRow row in HoursGrid.Rows)
            {
                if (row.IsNewRow) continue;
                var cIsOpen = row.Cells["IsOpen"];
                var cOpen = row.Cells["OpenTime"];
                var cClose = row.Cells["CloseTime"];

                bool isOpen = Convert.ToBoolean(cIsOpen.Value ?? false);

                cOpen.ReadOnly = !isOpen;
                cClose.ReadOnly = !isOpen;

                if (!isOpen)
                {
                    cOpen.Value = "00:00";
                    cClose.Value = "00:00";
                    cOpen.Style.BackColor = Color.Gainsboro;
                    cClose.Style.BackColor = Color.Gainsboro;
                    cOpen.Style.ForeColor = Color.Gray;
                    cClose.Style.ForeColor = Color.Gray;
                    cOpen.ToolTipText = "Enable 'Is Open' to edit times.";
                    cClose.ToolTipText = "Enable 'Is Open' to edit times.";
                }
                else
                {
                    cOpen.Style.BackColor = HoursGrid.DefaultCellStyle.BackColor;
                    cClose.Style.BackColor = HoursGrid.DefaultCellStyle.BackColor;
                    cOpen.Style.ForeColor = HoursGrid.DefaultCellStyle.ForeColor;
                    cClose.Style.ForeColor = HoursGrid.DefaultCellStyle.ForeColor;
                    cOpen.ToolTipText = "";
                    cClose.ToolTipText = "";
                }
            }
        }

        private void UpdateRowEditability(DataGridViewRow row)
        {
            if (row?.IsNewRow ?? true) return;
            var cIsOpen = row.Cells["IsOpen"];
            var cOpen = row.Cells["OpenTime"];
            var cClose = row.Cells["CloseTime"];

            bool isOpen = Convert.ToBoolean(cIsOpen.Value ?? false);
            cOpen.ReadOnly = !isOpen;
            cClose.ReadOnly = !isOpen;

            if (!isOpen)
            {
                cOpen.Value = "00:00";
                cClose.Value = "00:00";
                cOpen.Style.BackColor = Color.Gainsboro;
                cClose.Style.BackColor = Color.Gainsboro;
                cOpen.Style.ForeColor = Color.Gray;
                cClose.Style.ForeColor = Color.Gray;
            }
            else
            {
                cOpen.Style.BackColor = HoursGrid.DefaultCellStyle.BackColor;
                cClose.Style.BackColor = HoursGrid.DefaultCellStyle.BackColor;
                cOpen.Style.ForeColor = HoursGrid.DefaultCellStyle.ForeColor;
                cClose.Style.ForeColor = HoursGrid.DefaultCellStyle.ForeColor;
            }
        }
        // -------------------------------------------

        private async void AdminHoursForm_Load(object sender, EventArgs e)
        {
            await LoadSalonsAsync();
        }

        private async Task LoadSalonsAsync()
        {
            List<Salon> salons;

            await _dbGate.WaitAsync();
            try
            {
                salons = await _db.Salons.AsNoTracking().OrderBy(s => s.Name).ToListAsync();

                _suppressSelectionChanged = true;
                SalonComboBox.DataSource = salons;
                SalonComboBox.DisplayMember = "Name";
                SalonComboBox.ValueMember = "Id";
                _suppressSelectionChanged = false;
            }
            finally
            {
                _dbGate.Release();
            }

            if (SalonComboBox.SelectedValue is int salonId)
                await LoadSalonHoursAsync(salonId);
        }

        private async void SalonComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressSelectionChanged || _isLoadingHours || _isSaving) return;
            if (SalonComboBox.SelectedValue is int salonId)
                await LoadSalonHoursAsync(salonId);
        }

        private async Task LoadSalonHoursAsync(int salonId)
        {
            if (_isLoadingHours) return;
            _isLoadingHours = true;

            List<WorkingHour> hours;

            await _dbGate.WaitAsync();
            try
            {
                hours = await _db.WorkingHours
                    .Where(h => h.SalonId == salonId)
                    .AsNoTracking()
                    .OrderBy(h => h.DayOfWeek)
                    .ToListAsync();
            }
            finally
            {
                _dbGate.Release();
                _isLoadingHours = false;
            }

            HoursGrid.Rows.Clear();

            string[] days = { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };

            for (int i = 0; i < 7; i++)
            {
                var existing = hours.FirstOrDefault(h => h.DayOfWeek == i);
                bool isOpen = existing?.IsOpen ?? false;

                HoursGrid.Rows.Add(
                    i,                                    // Day
                    days[i],                              // Day Name
                    isOpen,                               // Is Open
                    (existing?.OpenTime ?? new TimeOnly(0, 0)).ToString("HH:mm"),
                    (existing?.CloseTime ?? new TimeOnly(0, 0)).ToString("HH:mm")
                );
            }

            ApplyRowGuards();
        }

        // ====== Grid input / validation ======
        private void HoursGrid_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (HoursGrid.IsCurrentCellDirty)
                HoursGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void HoursGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (HoursGrid.Columns[e.ColumnIndex].Name == "IsOpen")
                UpdateRowEditability(HoursGrid.Rows[e.RowIndex]);
        }

        private void HoursGrid_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            var col = HoursGrid.Columns[e.ColumnIndex].Name;
            if (col is "OpenTime" or "CloseTime")
            {
                var row = HoursGrid.Rows[e.RowIndex];
                bool isOpen = Convert.ToBoolean(row.Cells["IsOpen"].Value ?? false);
                if (!isOpen)
                {
                    e.Cancel = true;
                    MessageBox.Show("Enable 'Is Open' to edit the times.", "Info",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void HoursGrid_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            var col = HoursGrid.CurrentCell?.OwningColumn?.Name;
            if (col is "OpenTime" or "CloseTime")
            {
                if (e.Control is TextBox tb)
                {
                    tb.KeyPress -= TimeKeyPressFilter;
                    tb.KeyPress += TimeKeyPressFilter; // allow digits, colon, dot
                }
            }
        }
        private void TimeKeyPressFilter(object? sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ':' && e.KeyChar != '.')
                e.Handled = true;
        }

        // Normalize to HH:mm before committing to cell
        private void HoursGrid_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            var col = HoursGrid.Columns[e.ColumnIndex].Name;
            if (col is "OpenTime" or "CloseTime")
            {
                if (TryParseFlexibleTime(e.Value, out var t))
                {
                    e.Value = ToHHmm(t);
                    e.ParsingApplied = true;
                }
            }
        }

        // Validate: if open → Close > Open and valid format
        private void HoursGrid_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            var grid = (DataGridView)sender;
            var row = grid.Rows[e.RowIndex];
            var col = grid.Columns[e.ColumnIndex].Name;
            row.ErrorText = string.Empty;

            if (col is "OpenTime" or "CloseTime")
            {
                bool isOpen = Convert.ToBoolean(row.Cells["IsOpen"].Value ?? false);
                if (!isOpen) return;

                var openObj = col == "OpenTime" ? e.FormattedValue : row.Cells["OpenTime"].Value;
                var closeObj = col == "CloseTime" ? e.FormattedValue : row.Cells["CloseTime"].Value;

                if (!TryParseFlexibleTime(openObj, out var open) ||
                    !TryParseFlexibleTime(closeObj, out var close))
                {
                    e.Cancel = true;
                    row.ErrorText = "Time must be in HH:mm (e.g., 09:00 or 15:30).";
                    return;
                }

                if (open >= close)
                {
                    e.Cancel = true;
                    row.ErrorText = "Close time must be after open time.";
                    MessageBox.Show("Invalid time range: close time must be greater than open time.",
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (col == "OpenTime") row.Cells["OpenTime"].Value = ToHHmm(open);
                    if (col == "CloseTime") row.Cells["CloseTime"].Value = ToHHmm(close);
                }
            }
        }
        // =====================================

        // ----------------------- SAVE -----------------------
        private async void SaveBtn_Click(object sender, EventArgs e)
        {
            if (_isLoadingHours || _isSaving) return;
            _isSaving = true;

            try
            {
                if (SalonComboBox.SelectedValue is not int salonId)
                {
                    MessageBox.Show("Please select a salon first.", "Warning",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                HoursGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);
                HoursGrid.EndEdit();

                // final validation
                foreach (DataGridViewRow row in HoursGrid.Rows)
                {
                    if (row.IsNewRow || row.Cells["Day"].Value == null) continue;

                    bool isOpen = Convert.ToBoolean(row.Cells["IsOpen"].Value ?? false);
                    if (!isOpen) continue;

                    var openStr = row.Cells["OpenTime"].Value?.ToString() ?? "09:00";
                    var closeStr = row.Cells["CloseTime"].Value?.ToString() ?? "17:00";

                    if (!TryParseFlexibleTime(openStr, out var open) ||
                        !TryParseFlexibleTime(closeStr, out var close) || open >= close)
                    {
                        MessageBox.Show($"[{row.Cells["DayName"].Value}] Check times (HH:mm) and make sure Close > Open.",
                                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                await _dbGate.WaitAsync();
                try
                {
                    foreach (DataGridViewRow row in HoursGrid.Rows)
                    {
                        if (row.IsNewRow || row.Cells["Day"].Value == null) continue;

                        int day = Convert.ToInt32(row.Cells["Day"].Value);
                        bool isOpen = Convert.ToBoolean(row.Cells["IsOpen"].Value ?? false);

                        var openStr = row.Cells["OpenTime"].Value?.ToString() ?? "00:00";
                        var closeStr = row.Cells["CloseTime"].Value?.ToString() ?? "00:00";

                        TryParseFlexibleTime(openStr, out var open);
                        TryParseFlexibleTime(closeStr, out var close);

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
                        }
                        else
                        {
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
                    }
                    await _db.SaveChangesAsync();
                }
                finally
                {
                    _dbGate.Release();
                }

                MessageBox.Show("Working hours saved successfully!",
                                "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                await LoadSalonHoursAsync((int)SalonComboBox.SelectedValue);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while saving: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _isSaving = false;
            }
        }
    }
}
