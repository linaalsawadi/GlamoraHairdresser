using GlamoraHairdresser.Data;
using GlamoraHairdresser.Data.Entities;
using GlamoraHairdresser.Services.Auth;
using GlamoraHairdresser.WinForms.Forms.AdminForms;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Windows.Forms;

namespace GlamoraHairdresser.WinForms.Forms.WorkerForms
{
    public partial class WorkerDashboard : Form
    {
        private readonly GlamoraDbContext _db;
        private readonly BindingSource _bs = new();

        public WorkerDashboard(GlamoraDbContext db)
        {
            InitializeComponent();
            _db = db;

            Skillsclb.Enabled = true;
            Skillsclb.CheckOnClick = true;

            DataGridViewWorker.SelectionChanged += (_, __) => FillFormFromSelection();

            WorkerSalonTxtBox.TextChanged += (_, __) =>
            {
                if (TryResolveSalonId(out var sid))
                    LoadSkillsForSalon(sid);
                else
                    Skillsclb.Items.Clear();
            };
        }

        // ============================================================
        //      Resolve Salon ID (By Name or By Number)
        // ============================================================
        private bool TryResolveSalonId(out int salonId)
        {
            salonId = 0;
            var raw = (WorkerSalonTxtBox.Text ?? "").Trim();

            if (string.IsNullOrEmpty(raw)) return false;

            if (int.TryParse(raw, out var parsed))
            {
                salonId = _db.Salons
                    .Where(s => s.Id == parsed)
                    .Select(s => s.Id)
                    .FirstOrDefault();

                return salonId > 0;
            }

            salonId = _db.Salons
                .Where(s => s.Name.ToLower() == raw.ToLower())
                .Select(s => s.Id)
                .FirstOrDefault();

            return salonId > 0;
        }

        // ============================================================
        //      Load Skills for Selected Salon
        // ============================================================
        private void LoadSkillsForSalon(int salonId)
        {
            var services = _db.ServiceOfferings
                .Where(s => s.SalonId == salonId)
                .OrderBy(s => s.Name)
                .Select(s => new { s.Id, s.Name })
                .ToList();

            Skillsclb.BeginUpdate();
            Skillsclb.Items.Clear();

            foreach (var svc in services)
                Skillsclb.Items.Add(svc);

            Skillsclb.DisplayMember = "Name";
            Skillsclb.ValueMember = "Id";

            Skillsclb.EndUpdate();
        }

        // ============================================================
        //      Load Workers into Grid
        // ============================================================
        private void LoadData()
        {
            _db.ChangeTracker.Clear();
            _db.Workers.Include(w => w.Salon).Load();

            _bs.DataSource = _db.Workers.Local.ToBindingList();
            DataGridViewWorker.DataSource = _bs;

            string[] hideColumns =
            {
                "Appointments","Availabilities","EmployeeSkills",
                "PasswordHash","Salt","IterationCount","Prf","Permissions"
            };

            foreach (var c in hideColumns)
                if (DataGridViewWorker.Columns.Contains(c))
                    DataGridViewWorker.Columns[c].Visible = false;
        }

        // ============================================================
        //      Fill Form From Selection
        // ============================================================
        private void FillFormFromSelection()
        {
            if (_bs.Current is not Worker w)
            {
                ClearInputs();
                return;
            }

            WorkerIdTxtBox.Text = w.Id.ToString();
            WorkerNameTxtBox.Text = w.FullName;
            WorkerEmailTxtBox.Text = w.Email;
            WorkerPhoneTxtBox.Text = w.Phone;

            WorkerSalonTxtBox.Text = _db.Salons
                .Where(s => s.Id == w.SalonId)
                .Select(s => s.Name)
                .FirstOrDefault();

            LoadSkillsForSalon(w.SalonId);

            var ownedSkills = _db.EmployeeSkills
                .Where(es => es.WorkerId == w.Id)
                .Select(es => es.ServiceOfferingId)
                .ToHashSet();

            for (int i = 0; i < Skillsclb.Items.Count; i++)
            {
                var svc = Skillsclb.Items[i];
                int svcId = (int)svc.GetType().GetProperty("Id")!.GetValue(svc);
                Skillsclb.SetItemChecked(i, ownedSkills.Contains(svcId));
            }
        }

        // ============================================================
        //      Save Skills for Worker   ←  YOU WANTED THIS ONE!
        // ============================================================
        private void SaveSkillsSelection(int workerId)
        {
            var selectedIds = Skillsclb.CheckedItems
                .Cast<object>()
                .Select(o => (int)o.GetType().GetProperty("Id")!.GetValue(o))
                .ToHashSet();

            var existing = _db.EmployeeSkills
                .Where(es => es.WorkerId == workerId)
                .Select(es => es.ServiceOfferingId)
                .ToList();

            // Add new skills
            foreach (var id in selectedIds.Except(existing))
            {
                _db.EmployeeSkills.Add(new EmployeeSkill
                {
                    WorkerId = workerId,
                    ServiceOfferingId = id,
                    AssignedDate = DateTime.UtcNow
                });
            }

            // Remove unchecked skills
            var toRemove = existing.Except(selectedIds).ToList();

            if (toRemove.Count > 0)
            {
                var rowsToRemove = _db.EmployeeSkills
                    .Where(es => es.WorkerId == workerId && toRemove.Contains(es.ServiceOfferingId));

                _db.EmployeeSkills.RemoveRange(rowsToRemove);
            }

            _db.SaveChanges();
        }

        // ============================================================
        //      Clear Inputs
        // ============================================================
        private void ClearInputs()
        {
            WorkerIdTxtBox.Text = "0";
            WorkerNameTxtBox.Text = "";
            WorkerEmailTxtBox.Text = "";
            WorkerPhoneTxtBox.Text = "";
            WorkerSalonTxtBox.Text = "";

            Skillsclb.Items.Clear();
        }

        // ============================================================
        //      VALIDATION
        // ============================================================
        private bool ValidateInputs(out string message, out int salonId)
        {
            message = "";
            salonId = 0;

            if (string.IsNullOrWhiteSpace(WorkerNameTxtBox.Text))
            { message = "Name is required."; return false; }

            if (string.IsNullOrWhiteSpace(WorkerEmailTxtBox.Text))
            { message = "Email is required."; return false; }

            if (string.IsNullOrWhiteSpace(WorkerPhoneTxtBox.Text))
            { message = "Phone number is required."; return false; }

            if (!WorkerEmailTxtBox.Text.Contains("@"))
            { message = "Invalid email format."; return false; }

            if (!TryResolveSalonId(out salonId))
            { message = "Salon not found."; return false; }

            return true;
        }

        // ============================================================
        //      ADD Worker
        // ============================================================
        private void AddBtn_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs(out var msg, out var salonId))
            {
                MessageBox.Show(msg);
                return;
            }

            if (string.IsNullOrWhiteSpace(PasstxtBox.Text))
            {
                MessageBox.Show("Password required.");
                return;
            }

            var (hash, salt, iteration, prf) = PasswordHelper.HashPassword(PasstxtBox.Text);

            var worker = new Worker
            {
                FullName = WorkerNameTxtBox.Text.Trim(),
                Email = WorkerEmailTxtBox.Text.Trim(),
                Phone = WorkerPhoneTxtBox.Text.Trim(),
                SalonId = salonId,
                PasswordHash = hash,
                Salt = salt,
                IterationCount = iteration,
                Prf = prf,
                CreatedAt = DateTime.UtcNow
            };

            _db.Workers.Add(worker);
            _db.SaveChanges();

            SaveSkillsSelection(worker.Id);

            LoadData();
            MessageBox.Show("Worker Added!");
        }

        // ============================================================
        //      UPDATE Worker
        // ============================================================
        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            if (_bs.Current is not Worker w)
            {
                MessageBox.Show("Select worker first.");
                return;
            }

            if (!ValidateInputs(out var msg, out var salonId))
            {
                MessageBox.Show(msg);
                return;
            }

            w.FullName = WorkerNameTxtBox.Text.Trim();
            w.Email = WorkerEmailTxtBox.Text.Trim();
            w.Phone = WorkerPhoneTxtBox.Text.Trim();
            w.SalonId = salonId;

            if (!string.IsNullOrWhiteSpace(PasstxtBox.Text))
            {
                var (hash, salt, iter, prf) = PasswordHelper.HashPassword(PasstxtBox.Text);
                w.PasswordHash = hash;
                w.Salt = salt;
                w.IterationCount = iter;
                w.Prf = prf;
            }

            _db.SaveChanges();
            SaveSkillsSelection(w.Id);

            LoadData();
            MessageBox.Show("Worker Updated!");
        }

        // ============================================================
        //      DELETE Worker
        // ============================================================
        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (_bs.Current is not Worker w)
            {
                MessageBox.Show("Select a worker first.");
                return;
            }

            var confirm = MessageBox.Show("Delete this worker?",
                "Confirm", MessageBoxButtons.YesNo);

            if (confirm != DialogResult.Yes) return;

            var skills = _db.EmployeeSkills.Where(es => es.WorkerId == w.Id);
            _db.EmployeeSkills.RemoveRange(skills);

            _db.Workers.Remove(w);
            _db.SaveChanges();

            LoadData();
            ClearInputs();
        }

        // ============================================================
        private void ClearBtn_Click(object sender, EventArgs e)
        {
            ClearInputs();
        }

        private void WorkerDashboard_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void WorkerWorkingHoursBtn_Click(object sender, EventArgs e)
        {
            using var scope = Program.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<GlamoraHairdresser.Data.GlamoraDbContext>();
            new AdminWorkerHoursForm(db).ShowDialog();
        }
    }
}
