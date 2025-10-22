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

namespace GlamoraHairdresser.WinForms.Forms.Services
{
    public partial class ServicesDashboard : Form
    {
        private readonly GlamoraDbContext _db;
        private int _selectedSalonId;

        public ServicesDashboard(GlamoraDbContext db)
        {
            InitializeComponent();
            _db = db;

            // ComboBox لأسماء الخدمات يكون قابل للكتابة + إكمال تلقائي
            ServiceNameCmb.DropDownStyle = ComboBoxStyle.DropDown;
            ServiceNameCmb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            ServiceNameCmb.AutoCompleteSource = AutoCompleteSource.ListItems;

            // أحداث
            SalonCombo.SelectedIndexChanged += SalonCombo_SelectedIndexChanged;
            AddBtn.Click += AddBtn_Click;
            UpdateBtn.Click += UpdateBtn_Click;
            DeleteBtn.Click += DeleteBtn_Click;
            ServicesGrid.SelectionChanged += ServicesGrid_SelectionChanged;
        }

        private void ServicesDashboard_Load(object sender, EventArgs e)
        {
            LoadSalons();
            LoadServiceCatalogNames();   // يملأ الـCombo بأسماء الخدمات المميّزة
            if (SalonCombo.Items.Count > 0) SalonCombo.SelectedIndex = 0;
        }

        private void LoadSalons()
        {
            var salons = _db.Salons.AsNoTracking()
                                   .OrderBy(s => s.Name)
                                   .Select(s => new { s.Id, s.Name })
                                   .ToList();

            SalonCombo.DataSource = salons;
            SalonCombo.DisplayMember = "Name";
            SalonCombo.ValueMember = "Id";
        }
        private void LoadServiceCatalogNames()
        {
            var names = _db.ServiceOfferings
                           .AsNoTracking()
                           .Select(s => s.Name)
                           .Distinct()
                           .OrderBy(n => n)
                           .ToList();

            ServiceNameCmb.BeginUpdate();
            ServiceNameCmb.Items.Clear();
            foreach (var n in names) ServiceNameCmb.Items.Add(n);
            ServiceNameCmb.EndUpdate();
        }

        private void SalonCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SalonCombo.SelectedValue is int sid)
            {
                _selectedSalonId = sid;
                LoadServicesForSalon(sid);
                ClearServiceInputs();
            }
        }

        private void LoadServicesForSalon(int salonId)
        {
            var list = _db.ServiceOfferings
                          .Where(s => s.SalonId == salonId)
                          .OrderBy(s => s.Name)
                          .Select(s => new
                          {
                              s.Id,
                              s.Name,
                              s.Price,
                              s.DurationMinutes
                          })
                          .ToList();

            ServicesGrid.AutoGenerateColumns = true;
            ServicesGrid.DataSource = list;

            // تنسيقات اختيارية
            if (ServicesGrid.Columns.Contains("Id"))
                ServicesGrid.Columns["Id"].Visible = false;
            if (ServicesGrid.Columns.Contains("Name"))
                ServicesGrid.Columns["Name"].HeaderText = "Service";
            if (ServicesGrid.Columns.Contains("Price"))
                ServicesGrid.Columns["Price"].HeaderText = "Price";
            if (ServicesGrid.Columns.Contains("DurationMinutes"))
                ServicesGrid.Columns["DurationMinutes"].HeaderText = "Duration (min)";
        }

        // ========== أزرار CRUD ==========
        private void AddBtn_Click(object sender, EventArgs e)
        {
            if (_selectedSalonId <= 0)
            {
                MessageBox.Show("Select a salon first.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var name = (ServiceNameCmb.Text ?? "").Trim();
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Service name is required.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_db.ServiceOfferings.Any(s => s.SalonId == _selectedSalonId && s.Name.ToLower() == name.ToLower()))
            {
                MessageBox.Show("This service already exists for the selected salon.", "Duplicate",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var svc = new ServiceOffering
            {
                SalonId = _selectedSalonId,
                Name = name,
                Price = decimal.TryParse(PriceTxt.Text, out var p) ? p : 0,
                DurationMinutes = int.TryParse(DurationTxt.Text, out var d) ? d : 30
            };

            _db.ServiceOfferings.Add(svc);
            _db.SaveChanges();

            // ✅ بعد الإضافة: حدّث الكتالوج ليظهر الاسم الجديد في الـComboBox
            LoadServiceCatalogNames();
            // وحدث خدمات الصالون المعروض
            LoadServicesForSalon(_selectedSalonId);

            // اختياري: ضع المؤشر على الاسم المضاف
            ServiceNameCmb.Text = name;
        }

        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            if (ServicesGrid.CurrentRow == null)
            {
                MessageBox.Show("Select a service row to update.");
                return;
            }

            var idObj = ServicesGrid.CurrentRow.Cells["Id"]?.Value;
            if (idObj == null) return;
            int id = (int)idObj;

            var svc = _db.ServiceOfferings.Find(id);
            if (svc == null) return;

            var newName = (ServiceNameCmb.Text ?? "").Trim();
            if (string.IsNullOrEmpty(newName))
            {
                MessageBox.Show("Service name is required.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // منع تكرار الاسم داخل نفس الصالون
            bool duplicate = _db.ServiceOfferings
                .Any(s => s.SalonId == _selectedSalonId &&
                          s.Id != id &&
                          s.Name.ToLower() == newName.ToLower());
            if (duplicate)
            {
                MessageBox.Show("Another service with the same name exists in this salon.");
                return;
            }

            svc.Name = newName;
            svc.Price = decimal.TryParse(PriceTxt.Text, out var p) ? p : svc.Price;
            svc.DurationMinutes = int.TryParse(DurationTxt.Text, out var d) ? d : svc.DurationMinutes;

            _db.SaveChanges();

            // حدّث الكتالوج (لو الاسم الجديد غير موجود سابقًا)
            LoadServiceCatalogNames();
            LoadServicesForSalon(_selectedSalonId);
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (ServicesGrid.CurrentRow == null)
            {
                MessageBox.Show("Select a service row to delete.");
                return;
            }

            var idObj = ServicesGrid.CurrentRow.Cells["Id"]?.Value;
            if (idObj == null) return;
            int id = (int)idObj;

            var svc = _db.ServiceOfferings.Find(id);
            if (svc == null) return;

            // تحذير: لو عندك FK من EmployeeSkills قد تحتاج حذف/Cascade
            var confirm = MessageBox.Show($"Delete service '{svc.Name}'?",
                "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            // إن كان FK مقيّدًا، احذف علاقات EmployeeSkills أولاً
            var links = _db.EmployeeSkills.Where(es => es.ServiceOfferingId == id);
            _db.EmployeeSkills.RemoveRange(links);

            _db.ServiceOfferings.Remove(svc);
            _db.SaveChanges();

            LoadServicesForSalon(_selectedSalonId);
            LoadServiceCatalogNames();
            ClearServiceInputs();
        }

        // ========== مزامنة الحقول عند اختيار صف ==========
        private void ServicesGrid_SelectionChanged(object sender, EventArgs e)
        {
            if (ServicesGrid.CurrentRow?.DataBoundItem == null) return;

            var name = ServicesGrid.CurrentRow.Cells["Name"]?.Value?.ToString() ?? "";
            var price = ServicesGrid.CurrentRow.Cells["Price"]?.Value?.ToString() ?? "0";
            var duration = ServicesGrid.CurrentRow.Cells["DurationMinutes"]?.Value?.ToString() ?? "30";

            ServiceNameCmb.Text = name;
            PriceTxt.Text = price;
            DurationTxt.Text = duration;
        }

        private void ClearServiceInputs()
        {
            ServiceNameCmb.Text = "";
            PriceTxt.Text = "";
            DurationTxt.Text = "";
        }

        private void AddBtn_Click_1(object sender, EventArgs e)
        {

        }
    }
}