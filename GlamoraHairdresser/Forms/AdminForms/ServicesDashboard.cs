using GlamoraHairdresser.Data;
using GlamoraHairdresser.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows.Forms;

namespace GlamoraHairdresser.WinForms.Forms.Services
{
    public partial class ServicesDashboard : Form
    {
        private readonly GlamoraDbContext _db;
        private int _selectedSalonId;

        public ServicesDashboard(GlamoraHairdresser.Data.GlamoraDbContext db)
        {
            InitializeComponent();
            _db = db;

            // اضمن أن هذه العناصر موجودة في المصمّم بنفس الأسماء:
            // SalonCombo (ComboBox) / ServiceNameCmb (ComboBox) / ServicesGrid (DataGridView)
            // PriceTxt, DurationTxt (TextBox) / AddBtn, UpdateBtn, DeleteBtn, CleanBtn (Button)

            ServiceNameCmb.DropDownStyle = ComboBoxStyle.DropDown;
            ServiceNameCmb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            ServiceNameCmb.AutoCompleteSource = AutoCompleteSource.ListItems;

            Load += ServicesDashboard_Load;
            SalonCombo.SelectedIndexChanged += SalonCombo_SelectedIndexChanged;
            ServicesGrid.SelectionChanged += ServicesGrid_SelectionChanged;

            AddBtn.Click += AddBtn_Click;
            UpdateBtn.Click += UpdateBtn_Click;
            DeleteBtn.Click += DeleteBtn_Click;
            CleanBtn.Click += (_, __) => ClearServiceInputs();
        }

        private void ServicesDashboard_Load(object sender, EventArgs e)
        {
            LoadSalons();
            LoadServiceCatalogNames();
            if (SalonCombo.Items.Count > 0) SalonCombo.SelectedIndex = 0;
        }

        private void LoadSalons()
        {
            var salons = _db.Salons.AsNoTracking()
                                   .OrderBy(s => s.Name)
                                   .Select(s => new { s.Id, s.Name })
                                   .ToList();

            SalonCombo.DisplayMember = "Name";
            SalonCombo.ValueMember = "Id";
            SalonCombo.DataSource = salons;
        }

        // كتالوج عالمي لأسماء الخدمات (Distinct)
        private void LoadServiceCatalogNames()
        {
            var names = _db.ServiceOfferings.AsNoTracking()
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

            if (ServicesGrid.Columns.Contains("Id"))
                ServicesGrid.Columns["Id"].Visible = false;
            if (ServicesGrid.Columns.Contains("Name"))
                ServicesGrid.Columns["Name"].HeaderText = "Service";
            if (ServicesGrid.Columns.Contains("Price"))
                ServicesGrid.Columns["Price"].HeaderText = "Price";
            if (ServicesGrid.Columns.Contains("DurationMinutes"))
                ServicesGrid.Columns["DurationMinutes"].HeaderText = "Duration (min)";
        }

        // ------- CRUD -------

        private void AddBtn_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs(out var msg)) { MessageBox.Show(msg, "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            var name = ServiceNameCmb.Text.Trim();
            var price = decimal.TryParse(PriceTxt.Text, out var p) ? p : 0;
            var duration = int.TryParse(DurationTxt.Text, out var d) ? d : 30;

            bool exists = _db.ServiceOfferings.Any(s => s.SalonId == _selectedSalonId &&
                                                        s.Name.ToLower() == name.ToLower());
            if (exists)
            {
                MessageBox.Show("This service already exists for the selected salon.", "Duplicate",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var svc = new ServiceOffering
            {
                SalonId = _selectedSalonId,
                Name = name,
                Price = price,
                DurationMinutes = duration
            };

            _db.ServiceOfferings.Add(svc);
            _db.SaveChanges();

            LoadServicesForSalon(_selectedSalonId);
            LoadServiceCatalogNames();
            ServiceNameCmb.Text = name;

            MessageBox.Show("Service added.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            if (ServicesGrid.CurrentRow == null)
            {
                MessageBox.Show("Select a service row to update.", "Update", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!ValidateInputs(out var msg)) { MessageBox.Show(msg, "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            var idObj = ServicesGrid.CurrentRow.Cells["Id"]?.Value;
            if (idObj == null) return;
            int id = (int)idObj;

            var svc = _db.ServiceOfferings.Find(id);
            if (svc == null) return;

            var newName = ServiceNameCmb.Text.Trim();
            var newPrice = decimal.TryParse(PriceTxt.Text, out var p) ? p : svc.Price;
            var newDuration = int.TryParse(DurationTxt.Text, out var d) ? d : svc.DurationMinutes;

            bool duplicate = _db.ServiceOfferings.Any(s => s.SalonId == _selectedSalonId &&
                                                           s.Id != id &&
                                                           s.Name.ToLower() == newName.ToLower());
            if (duplicate)
            {
                MessageBox.Show("Another service with the same name exists in this salon.",
                    "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            svc.Name = newName;
            svc.Price = newPrice;
            svc.DurationMinutes = newDuration;

            _db.SaveChanges();
            LoadServicesForSalon(_selectedSalonId);
            LoadServiceCatalogNames();

            MessageBox.Show("Service updated.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (ServicesGrid.CurrentRow == null)
            {
                MessageBox.Show("Select a service row to delete.", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var idObj = ServicesGrid.CurrentRow.Cells["Id"]?.Value;
            if (idObj == null) return;
            int id = (int)idObj;

            var svc = _db.ServiceOfferings.Find(id);
            if (svc == null) return;

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

            MessageBox.Show("Service deleted.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ------- Grid → Inputs -------

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

        // ------- Helpers -------

        private void ClearServiceInputs()
        {
            ServiceNameCmb.Text = "";
            PriceTxt.Text = "";
            DurationTxt.Text = "";
            ServicesGrid.ClearSelection();
        }

        private bool ValidateInputs(out string message)
        {
            message = "";
            if (_selectedSalonId <= 0) { message = "Please select a salon."; return false; }

            var name = (ServiceNameCmb.Text ?? "").Trim();
            if (string.IsNullOrEmpty(name))
            {
                message = "Service name is required.";
                return false;
            }

            if (!string.IsNullOrWhiteSpace(PriceTxt.Text) && !decimal.TryParse(PriceTxt.Text, out _))
            {
                message = "Invalid price format.";
                return false;
            }

            if (!string.IsNullOrWhiteSpace(DurationTxt.Text) && !int.TryParse(DurationTxt.Text, out _))
            {
                message = "Invalid duration (minutes).";
                return false;
            }

            return true;
        }
    }
}
