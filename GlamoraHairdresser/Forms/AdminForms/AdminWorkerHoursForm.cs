using GlamoraHairdresser.Data;
using GlamoraHairdresser.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GlamoraHairdresser.WinForms.Forms.AdminForms
{
    public partial class AdminWorkerHoursForm : Form
    {
        private readonly GlamoraDbContext _db;

        // Concurrency guards
        private readonly SemaphoreSlim _dbGate = new(1, 1);
        private bool _suppressSelectionChanged = false;
        private bool _isLoadingHours = false;
        private bool _isSaving = false;

        // Accepted user input time formats
        private static readonly string[] TimeFormats = { "HH:mm", "H:mm", "HH.mm", "H.mm" };

        public AdminWorkerHoursForm(GlamoraDbContext db)
        {
            InitializeComponent();
            _db = db;

            HoursGrid.AutoGenerateColumns = false;
            SetupGridColumns();

            Load += AdminWorkerHoursForm_Load;
            WorkerComboBox.SelectedIndexChanged += WorkerComboBox_SelectedIndexChanged;
            SaveBtn.Click += SaveBtn_Click;

            HoursGrid.CellBeginEdit += HoursGrid_CellBeginEdit;
            HoursGrid.CellValidating += HoursGrid_CellValidating;   // لن نمنع الخروج من الخلية هنا
            HoursGrid.CellParsing += HoursGrid_CellParsing;         // normalize إلى HH:mm
            HoursGrid.CellValueChanged += HoursGrid_CellValueChanged;
            HoursGrid.CurrentCellDirtyStateChanged += HoursGrid_CurrentCellDirtyStateChanged;
            HoursGrid.EditingControlShowing += HoursGrid_EditingControlShowing;
            HoursGrid.DataError += (s, e) => e.Cancel = true;       // لا رسائل
            HoursGrid.DataBindingComplete += (_, __) => ApplyRowGuards();
        }

        // ===================== Helpers =====================
        private static bool IsSalonClosedCell(object salonHoursCellValue)
        {
            var s = salonHoursCellValue?.ToString()?.Trim();
            return string.Equals(s, "Closed", StringComparison.OrdinalIgnoreCase);
        }

        private static bool TryParseTime(object value, out TimeOnly time)
        {
            time = default;

            if (value is TimeOnly t)
            {
                time = t;
                return true;
            }
            var s = Convert.ToString(value)?.Trim();
            if (string.IsNullOrWhiteSpace(s)) return false;

            if (TimeOnly.TryParseExact(s, TimeFormats, CultureInfo.InvariantCulture,
                                       DateTimeStyles.None, out time))
                return true;

            if (int.TryParse(s, out var hour) && hour >= 0 && hour <= 23)
            {
                time = new TimeOnly(hour, 0);
                return true;
            }
            return false;
        }

        private static string ToHHmm(TimeOnly t) => t.ToString("HH:mm");

        private void SetupGridColumns()
        {
            HoursGrid.Columns.Clear();

            HoursGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "Day", HeaderText = "Day", Visible = false });
            HoursGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "DayName", HeaderText = "Day Name", ReadOnly = true, AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells });
            HoursGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "SalonHours",
                HeaderText = "Salon Hours",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                DefaultCellStyle = { ForeColor = Color.Gray }
            });
            HoursGrid.Columns.Add(new DataGridViewCheckBoxColumn { Name = "IsOpen", HeaderText = "Is Open", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells });
            HoursGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "OpenTime", HeaderText = "Open Time", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells });
            HoursGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "CloseTime", HeaderText = "Close Time", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells });
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

                    bool isOpen = Convert.ToBoolean(cIsOpen.Value ?? false);
                    cOpen.ReadOnly = !isOpen;
                    cClose.ReadOnly = !isOpen;

                    foreach (var c in new[] { cIsOpen, cOpen, cClose })
                    {
                        c.Style.ForeColor = HoursGrid.DefaultCellStyle.ForeColor;
                        c.Style.BackColor = isOpen || c == cIsOpen
                            ? HoursGrid.DefaultCellStyle.BackColor
                            : Color.Gainsboro;
                        c.ToolTipText = isOpen || c == cIsOpen
                            ? string.Empty
                            : "Enable 'Is Open' to edit times.";
                    }

                    if (!isOpen)
                    {
                        cOpen.Value = "00:00";
                        cClose.Value = "00:00";
                    }
                }
            }
        }

        private void UpdateRowEditability(DataGridViewRow row)
        {
            if (row?.IsNewRow ?? true) return;

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

                cIsOpen.Style.BackColor = Color.Gainsboro;
                cOpen.Style.BackColor = Color.Gainsboro;
                cClose.Style.BackColor = Color.Gainsboro;

                cIsOpen.Style.ForeColor = Color.Gray;
                cOpen.Style.ForeColor = Color.Gray;
                cClose.Style.ForeColor = Color.Gray;
            }
            else
            {
                cIsOpen.ReadOnly = false;

                bool isOpen = Convert.ToBoolean(cIsOpen.Value ?? false);
                cOpen.ReadOnly = !isOpen;
                cClose.ReadOnly = !isOpen;

                cIsOpen.Style.BackColor = HoursGrid.DefaultCellStyle.BackColor;
                cIsOpen.Style.ForeColor = HoursGrid.DefaultCellStyle.ForeColor;

                cOpen.Style.BackColor = isOpen ? HoursGrid.DefaultCellStyle.BackColor : Color.Gainsboro;
                cClose.Style.BackColor = isOpen ? HoursGrid.DefaultCellStyle.BackColor : Color.Gainsboro;

                cOpen.Style.ForeColor = HoursGrid.DefaultCellStyle.ForeColor;
                cClose.Style.ForeColor = HoursGrid.DefaultCellStyle.ForeColor;

                if (!isOpen)
                {
                    cOpen.Value = "00:00";
                    cClose.Value = "00:00";
                }
            }
        }

        // ===================== Load / Bind =====================
        private async void AdminWorkerHoursForm_Load(object sender, EventArgs e)
        {
            await LoadWorkersAsync();
        }

        private async Task LoadWorkersAsync()
        {
            await _dbGate.WaitAsync();
            try
            {
                var workers = await _db.Workers
                    .Include(w => w.Salon)
                    .AsNoTracking()
                    .ToListAsync();

                _suppressSelectionChanged = true;
                WorkerComboBox.DataSource = workers;
                WorkerComboBox.DisplayMember = "FullName";
                WorkerComboBox.ValueMember = "Id";
                _suppressSelectionChanged = false;
            }
            finally
            {
                _dbGate.Release();
            }

            if (WorkerComboBox.SelectedValue is int workerId)
                await LoadWorkerHoursAsync(workerId);
        }

        private async void WorkerComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressSelectionChanged || _isLoadingHours || _isSaving) return;
            if (WorkerComboBox.SelectedValue is int workerId)
                await LoadWorkerHoursAsync(workerId);
        }

        private async Task LoadWorkerHoursAsync(int workerId)
        {
            if (_isLoadingHours) return;
            _isLoadingHours = true;

            List<WorkingHour> salonHours;
            List<WorkerWorkingHour> workerHours;
            Worker worker;

            await _dbGate.WaitAsync();
            try
            {
                worker = await _db.Workers
                    .Include(w => w.Salon)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(w => w.Id == workerId);

                if (worker == null)
                {
                    MessageBox.Show("Worker not found.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                salonHours = await _db.WorkingHours
                    .Where(h => h.SalonId == worker.SalonId)
                    .OrderBy(h => h.DayOfWeek)
                    .AsNoTracking()
                    .ToListAsync();

                workerHours = await _db.WorkerWorkingHours
                    .Where(h => h.WorkerId == workerId)
                    .OrderBy(h => h.DayOfWeek)
                    .AsNoTracking()
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

                HoursGrid.Rows.Add(i, days[i], salonTime, isOpen, openTime, closeTime);
            }

            ApplyRowGuards();
        }

        // ===================== Editing & (light) Validation =====================
        private void HoursGrid_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            var col = HoursGrid.Columns[e.ColumnIndex].Name;
            if (col is "IsOpen" or "OpenTime" or "CloseTime")
            {
                var row = HoursGrid.Rows[e.RowIndex];
                if (IsSalonClosedCell(row.Cells["SalonHours"].Value))
                {
                    // نمنع التحرير هنا فقط لليوم المغلق
                    e.Cancel = true;
                    MessageBox.Show("Salon is closed on this day. You cannot set worker hours.",
                                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void HoursGrid_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (HoursGrid.IsCurrentCellDirty)
                HoursGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void HoursGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var colName = HoursGrid.Columns[e.ColumnIndex].Name;
            if (colName == "IsOpen")
            {
                var row = HoursGrid.Rows[e.RowIndex];
                UpdateRowEditability(row);
            }
        }

        private void HoursGrid_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            var col = HoursGrid.CurrentCell?.OwningColumn?.Name;
            if (col is "OpenTime" or "CloseTime")
            {
                if (e.Control is TextBox tb)
                {
                    tb.BorderStyle = BorderStyle.FixedSingle;
                    tb.KeyPress -= TimeKeyPressNumericColon;
                    tb.KeyPress += TimeKeyPressNumericColon; // allow digits, colon, dot
                }
            }
        }

        private void TimeKeyPressNumericColon(object? sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ':' && e.KeyChar != '.')
                e.Handled = true;
        }

        private void HoursGrid_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            var colName = HoursGrid.Columns[e.ColumnIndex].Name;
            if (colName is "OpenTime" or "CloseTime")
            {
                if (TryParseTime(e.Value, out var t))
                {
                    e.Value = ToHHmm(t); // keep grid text normalized
                    e.ParsingApplied = true;
                }
            }
        }

        // ملاحظة: لا نمنع الخروج من الخلية، فقط نكتب ErrorText (جمع الأخطاء سيتم عند Save)
        private void HoursGrid_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            var grid = (DataGridView)sender;
            var row = grid.Rows[e.RowIndex];
            var colName = grid.Columns[e.ColumnIndex].Name;

            row.ErrorText = string.Empty;

            if (colName is "OpenTime" or "CloseTime")
            {
                bool isOpen = Convert.ToBoolean(row.Cells["IsOpen"].Value ?? false);
                if (!isOpen) return;

                var openObj = colName == "OpenTime" ? e.FormattedValue : row.Cells["OpenTime"].Value;
                var closeObj = colName == "CloseTime" ? e.FormattedValue : row.Cells["CloseTime"].Value;

                if (!TryParseTime(openObj, out var open) || !TryParseTime(closeObj, out var close))
                {
                    row.ErrorText = "Time must be in HH:mm (e.g., 09:00).";
                    return;
                }

                if (open >= close)
                {
                    row.ErrorText = "Close time must be after open time.";
                    // بلا e.Cancel، بلا MessageBox
                }
                else
                {
                    if (colName == "OpenTime") row.Cells["OpenTime"].Value = ToHHmm(open);
                    if (colName == "CloseTime") row.Cells["CloseTime"].Value = ToHHmm(close);
                }
            }
        }

        // ===================== Save (collect all errors) =====================
        private async void SaveBtn_Click(object sender, EventArgs e)
        {
            if (_isLoadingHours || _isSaving) return;
            _isSaving = true;

            try
            {
                HoursGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);
                HoursGrid.EndEdit();

                if (WorkerComboBox.SelectedValue is not int workerId)
                {
                    MessageBox.Show("Please select a worker first.", "Warning",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Worker worker;
                List<WorkingHour> salonHours;

                await _dbGate.WaitAsync();
                try
                {
                    worker = await _db.Workers
                        .Include(w => w.Salon)
                        .FirstOrDefaultAsync(w => w.Id == workerId);

                    if (worker == null)
                    {
                        MessageBox.Show("Worker not found.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    salonHours = await _db.WorkingHours
                        .Where(h => h.SalonId == worker.SalonId)
                        .ToListAsync();

                    // --------- Final validation for ALL rows ----------
                    var errors = new List<string>();
                    DataGridViewRow? firstBadRow = null;

                    foreach (DataGridViewRow row in HoursGrid.Rows)
                    {
                        if (row.IsNewRow || row.Cells["Day"].Value == null) continue;

                        row.ErrorText = string.Empty;

                        int day = Convert.ToInt32(row.Cells["Day"].Value);
                        string dayName = Convert.ToString(row.Cells["DayName"].Value) ?? "?";
                        bool isOpen = Convert.ToBoolean(row.Cells["IsOpen"].Value ?? false);

                        var salonDay = salonHours.FirstOrDefault(h => h.DayOfWeek == day);
                        bool salonClosed = salonDay == null || !salonDay.IsOpen;

                        if (salonClosed && isOpen)
                        {
                            var msg = $"• {dayName}: Salon is closed; worker hours cannot be enabled.";
                            errors.Add(msg);
                            row.ErrorText = msg;
                            firstBadRow ??= row;
                            continue;
                        }

                        if (!isOpen) continue;

                        var openStr = row.Cells["OpenTime"].Value?.ToString() ?? "";
                        var closeStr = row.Cells["CloseTime"].Value?.ToString() ?? "";

                        if (!TryParseTime(openStr, out var open) || !TryParseTime(closeStr, out var close))
                        {
                            var msg = $"• {dayName}: invalid time format (use HH:mm).";
                            errors.Add(msg);
                            row.ErrorText = msg;
                            firstBadRow ??= row;
                            continue;
                        }

                        if (open >= close)
                        {
                            var msg = $"• {dayName}: Close must be AFTER Open (Open {ToHHmm(open)} ≥ Close {ToHHmm(close)}).";
                            errors.Add(msg);
                            row.ErrorText = msg;
                            firstBadRow ??= row;
                            continue;
                        }

                        // must be within salon hours if salon open
                        if (!salonClosed && (open < salonDay!.OpenTime || close > salonDay.CloseTime))
                        {
                            var msg = $"• {dayName}: Worker hours must be within salon hours ({ToHHmm(salonDay.OpenTime)} - {ToHHmm(salonDay.CloseTime)}).";
                            errors.Add(msg);
                            row.ErrorText = msg;
                            firstBadRow ??= row;
                            continue;
                        }

                        // normalize
                        row.Cells["OpenTime"].Value = ToHHmm(open);
                        row.Cells["CloseTime"].Value = ToHHmm(close);
                    }

                    if (errors.Count > 0)
                    {
                        if (firstBadRow != null)
                        {
                            HoursGrid.ClearSelection();
                            firstBadRow.Selected = true;
                            HoursGrid.FirstDisplayedScrollingRowIndex = firstBadRow.Index;
                        }

                        MessageBox.Show(
                            "Please fix the following before saving:\n\n" + string.Join(Environment.NewLine, errors),
                            "Validation errors", MessageBoxButtons.OK, MessageBoxIcon.Error
                        );
                        return; // لا نُكمل الحفظ
                    }
                    // ---------------------------------------------------

                    // Persist
                    foreach (DataGridViewRow row in HoursGrid.Rows)
                    {
                        if (row.IsNewRow || row.Cells["Day"].Value == null) continue;

                        int day = Convert.ToInt32(row.Cells["Day"].Value);
                        bool isOpen = Convert.ToBoolean(row.Cells["IsOpen"].Value ?? false);

                        var openStr = row.Cells["OpenTime"].Value?.ToString()?.Trim() ?? "00:00";
                        var closeStr = row.Cells["CloseTime"].Value?.ToString()?.Trim() ?? "00:00";
                        TryParseTime(openStr, out var open);
                        TryParseTime(closeStr, out var close);

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

                    await _db.SaveChangesAsync();
                }
                finally
                {
                    _dbGate.Release();
                }

                MessageBox.Show("Worker hours saved successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(GetDeepError(ex), "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _isSaving = false;
            }
        }

        private static string GetDeepError(Exception ex)
        {
            var sb = new StringBuilder();
            sb.AppendLine("An error occurred while saving changes.");
            var i = 1;
            for (var e = ex; e != null; e = e.InnerException)
            {
                sb.AppendLine($"[{i}] {e.GetType().Name}: {e.Message}");
                i++;
            }
            return sb.ToString();
        }
    }
}
