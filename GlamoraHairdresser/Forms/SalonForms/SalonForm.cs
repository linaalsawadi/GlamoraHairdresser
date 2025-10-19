using GlamoraHairdresser.Data;
using GlamoraHairdresser.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace GlamoraHairdresser.WinForms.Forms.SalonForms
{
    public partial class SalonForm : Form
    {
        private readonly GlamoraDbContext _db;
        private readonly BindingSource _bs = new();

        private void LoadData()
        {
            _db.ChangeTracker.Clear();
            _db.Salons.Load();
            _bs.DataSource = _db.Salons.Local.ToBindingList();
            dataGridViewSalon.DataSource = _bs;

            // ترتيب الأعمدة بعد الربط
            var g = dataGridViewSalon;

            // اجعل Id أول عمود
            if (g.Columns.Contains("Id"))
            {
                g.Columns["Id"].DisplayIndex = 0;
                g.Columns["Id"].ReadOnly = true;   // لا يُعدَّل
                g.Columns["Id"].Width = 60;
            }

            // أخفِ أعمدة الـ navigation التي لا تُعرض في الجدول
            string[] navCols = { "Services", "Workers", "WorkingHours" };
            foreach (var colName in navCols)
            {
                if (g.Columns.Contains(colName))
                    g.Columns[colName].Visible = false;
            }

            // (اختياري) أعِد ترتيب بقية الأعمدة كما تحب
            int i = 1;
            void setIdx(string name)
            {
                if (g.Columns.Contains(name)) g.Columns[name].DisplayIndex = i++;
            }
            setIdx("Name");
            setIdx("Address");
            setIdx("PhoneNumber");
            setIdx("CreatedAt");

            // (اختياري) تنسيق CreatedAt
            if (g.Columns.Contains("CreatedAt"))
                g.Columns["CreatedAt"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
        }

        public SalonForm(GlamoraDbContext db)
        {
            InitializeComponent();
            _db = db;
            this.Load += SalonForm_Load;

        }

        private void SalonForm_Load(object sender, EventArgs e)
        {
            LoadData();
            // ✅ عند تحميل الصفحة، اربط الحدث لتنسيق الوقت المحلي
            dataGridViewSalon.CellFormatting += DataGridViewSalon_CellFormatting;

        }
        private void DataGridViewSalon_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            var grid = (DataGridView)sender!;
            if (grid.Columns[e.ColumnIndex].DataPropertyName == "CreatedAt" && e.Value is DateTime dt)
            {
                // عالج Kind غير المحدد باعتباره UTC ثم حوّله للمحلي
                if (dt.Kind == DateTimeKind.Unspecified)
                    dt = DateTime.SpecifyKind(dt, DateTimeKind.Utc);

                var local = dt.ToLocalTime();

                // أعد القيمة كنص (string) لتوافق عمود TextBox
                e.Value = local.ToString("yyyy-MM-dd HH:mm");
                e.FormattingApplied = true;
            }
        }

        private void DataGridViewSalon_SelectionChanged(object? sender, EventArgs e) => FillFormFromSelection();

        private void FillFormFromSelection()
        {
            if (_bs.Current is Salon s)
            {
                SalonIdTxtBox.Text = s.Id.ToString();
                SalonNameTxtBox.Text = s.Name ?? string.Empty;
                AddressTxtBox.Text = s.Address ?? string.Empty;
                SalonPhoneNumTxtBox.Text = s.PhoneNumber ?? string.Empty;
            }
            else
            {
                ClearInputs();
            }
        }
        private void ClearInputs()
        {
            SalonIdTxtBox.Text = "0";
            SalonNameTxtBox.Text = "";
            AddressTxtBox.Text = "";
            SalonPhoneNumTxtBox.Text = "";
            SalonNameTxtBox.Focus();
        }

        private bool ValidateInputs(out string message)
        {
            if (string.IsNullOrWhiteSpace(SalonNameTxtBox.Text))
            {
                message = "Salon Name is required.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(SalonPhoneNumTxtBox.Text) || SalonPhoneNumTxtBox.Text.Trim().Length < 11)
            {
                message = "Phone number must be at least 10 digits.";
                return false;
            }
            message = "";
            return true;
        }

        private void SalonPhoneNumLbl_Click(object sender, EventArgs e)
        {

        }

        private void AddBtn_Click(object? sender, EventArgs e)
        {

            if (!ValidateInputs(out var msg))
            {
                MessageBox.Show(msg, "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var s = new Salon
            {
                Name = SalonNameTxtBox.Text.Trim(),
                Address = AddressTxtBox.Text.Trim(),
                PhoneNumber = SalonPhoneNumTxtBox.Text.Trim(),
                CreatedAt = DateTime.UtcNow
            };

            _db.Salons.Add(s);
            try
            {
                _db.SaveChanges();
                LoadData(); // إعادة تحميل لعرض الصف الجديد
                            // تحديد الصف المضاف
                var index = _db.Salons.Local.ToList().FindIndex(x => x.Id == s.Id);
                if (index >= 0) dataGridViewSalon.Rows[index].Selected = true;
                MessageBox.Show("Added.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (DbUpdateException ex)
            {
                MessageBox.Show(ex.InnerException?.Message ?? ex.Message, "DB Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateBtn_Click(object sender, EventArgs e)
        {

            if (_bs.Current is not Salon current)
            {
                MessageBox.Show("Select a row first.", "Update", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!ValidateInputs(out var msg))
            {
                MessageBox.Show(msg, "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            current.Name = SalonNameTxtBox.Text.Trim();
            current.Address = AddressTxtBox.Text.Trim();
            current.PhoneNumber = SalonPhoneNumTxtBox.Text.Trim();

            try
            {
                _db.SaveChanges();
                _bs.ResetBindings(false);
                MessageBox.Show("Updated.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (DbUpdateException ex)
            {
                MessageBox.Show(ex.InnerException?.Message ?? ex.Message, "DB Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {

            if (_bs.Current is not Salon current)
            {
                MessageBox.Show("Select a row first.", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show($"Delete salon '{current.Name}'?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            _db.Salons.Remove(current);
            try
            {
                _db.SaveChanges();
                LoadData();
                MessageBox.Show("Deleted.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (DbUpdateException ex)
            {
                MessageBox.Show(ex.InnerException?.Message ?? ex.Message, "DB Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                _db.Entry(current).State = EntityState.Unchanged;
            }
        }

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            ClearInputs(); // فقط تنظيف الحقول النصية دون أي تعامل مع قاعدة البيانات

        }
    }
}
