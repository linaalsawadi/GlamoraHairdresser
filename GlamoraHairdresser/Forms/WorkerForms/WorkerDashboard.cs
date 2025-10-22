using GlamoraHairdresser.Data;
using GlamoraHairdresser.Data.Entities;
using Microsoft.EntityFrameworkCore;
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

            // إعدادات قائمة المهارات
            Skillsclb.Enabled = true;
            Skillsclb.SelectionMode = SelectionMode.One;
            Skillsclb.CheckOnClick = true;
            Skillsclb.DataSource = null;

            // عند تغيّر اختيار الصف في الجدول: عبّئ الحقول
            DataGridViewWorker.SelectionChanged += (_, __) => FillFormFromSelection();

            WorkerSalonTxtBox.TextChanged += (_, __) =>
            {
                if (TryResolveSalonId(out var sid)) LoadSkillsForSalon(sid);
                else LoadDefaultSkillsFallback();
            };
        }

        // قائمة افتراضية باللغة الإنجليزية
        private static readonly string[] DefaultSkills =
        {
            "Men Haircut","Women Haircut","Beard Trim","Fade Cut","Hair Styling",
            "Blow Dry","Curly / Iron Styling","Bride Hairstyle","Hair Coloring",
            "Highlights","Bleaching","Toner","Hair Keratin","Hair Protein Treatment",
            "Anti-Frizz Treatment","Hot Oil Treatment","Hair Mask","Scalp Massage",
            "Skin Cleansing","Deep Facial","Facial Mask","Steam Facial","Waxing / Hair Removal",
            "Eyebrow Shaping","Eyelash Extension","Eyebrow Tinting","Manicure","Pedicure",
            "Nail Trimming","Gel Nails","Acrylic Nails","Shellac Polish","Nail Art","Hand Massage",
            "Makeup Application","Bridal Makeup","Body Massage","Body Scrub","Spa Treatment",
            "Customer Service","Salon Cleaning","Reception Work","Product Knowledge","Team Work"
        };

        // زرع خدمات الصالون الافتراضية إن لم تكن موجودة
        private void EnsureSalonServicesSeeded(int salonId)
        {
            var existing = _db.ServiceOfferings
                              .Where(s => s.SalonId == salonId)
                              .Select(s => s.Name)
                              .ToHashSet(StringComparer.OrdinalIgnoreCase);

            var toInsert = DefaultSkills.Where(n => !existing.Contains(n)).ToList();
            if (toInsert.Count == 0) return;

            foreach (var name in toInsert)
            {
                _db.ServiceOfferings.Add(new ServiceOffering
                {
                    SalonId = salonId,
                    Name = name,
                    DurationMinutes = 30,
                    Price = 0
                });
            }
            _db.SaveChanges();
        }

        private bool TryResolveSalonId(out int salonId)
        {
            salonId = 0;
            var raw = (WorkerSalonTxtBox.Text ?? "").Trim();
            if (string.IsNullOrEmpty(raw)) return false;

            // رقم؟
            if (int.TryParse(raw, out var parsed))
            {
                salonId = _db.Salons.Where(s => s.Id == parsed).Select(s => s.Id).FirstOrDefault();
                return salonId > 0;
            }

            // اسم (غير حساس لحالة الأحرف)
            salonId = _db.Salons
                .Where(s => s.Name.ToLower() == raw.ToLower())
                .Select(s => s.Id)
                .FirstOrDefault();

            return salonId > 0;
        }

        private void LoadSkillsForSalon(int salonId)
        {
            if (salonId <= 0) { LoadDefaultSkillsFallback(); return; }

            // تأكّد من زرع الخدمات
            EnsureSalonServicesSeeded(salonId);

            var allServices = _db.ServiceOfferings.AsNoTracking()
                .Where(s => s.SalonId == salonId)
                .OrderBy(s => s.Name)
                .Select(s => new { s.Id, s.Name })
                .ToList();

            Skillsclb.BeginUpdate();
            Skillsclb.Items.Clear();
            Skillsclb.DisplayMember = "Name";
            Skillsclb.ValueMember = "Id";

            foreach (var svc in allServices)
                Skillsclb.Items.Add(svc); // بدون تعليم مبدئي في وضع "عامل جديد"

            Skillsclb.EndUpdate();
        }

        private void LoadDefaultSkillsFallback()
        {
            // نعرض المهارات الافتراضية كنصوص إلى أن يُحدَّد صالون
            Skillsclb.BeginUpdate();
            Skillsclb.Items.Clear();
            Skillsclb.DisplayMember = null;
            Skillsclb.ValueMember = null;

            foreach (var name in DefaultSkills.OrderBy(x => x))
                Skillsclb.Items.Add(name);

            Skillsclb.EndUpdate();
        }

        private void LoadData()
        {
            _db.ChangeTracker.Clear();
            _db.Workers.Include(w => w.Salon).Load();

            _bs.DataSource = _db.Workers.Local.ToBindingList();
            DataGridViewWorker.AutoGenerateColumns = true;
            DataGridViewWorker.DataSource = _bs;

            var g = DataGridViewWorker;
            if (g.Columns.Contains("Id"))
            {
                g.Columns["Id"].DisplayIndex = 0;
                g.Columns["Id"].ReadOnly = true;
                g.Columns["Id"].Width = 60;
            }

            string[] hide = { "Appointments", "Availabilities", "EmployeeSkills", "UserType", "PasswordHash", "Salt", "Permissions" };
            foreach (var c in hide) if (g.Columns.Contains(c)) g.Columns[c].Visible = false;

            int i = 1;
            void set(string n) { if (g.Columns.Contains(n)) g.Columns[n].DisplayIndex = i++; }
            set("FullName"); set("Email"); set("SalonId"); set("CreatedAt");
        }

        private void FillFormFromSelection()
        {
            if (_bs.Current is not Worker w)
            {
                ClearInputs();
                return;
            }

            WorkerIdTxtBox.Text = w.Id.ToString();
            WorkerNameTxtBox.Text = w.FullName ?? string.Empty;
            WorkerEmailTxtBox.Text = w.Email ?? string.Empty;

            var salonName = _db.Salons.AsNoTracking()
                             .Where(s => s.Id == w.SalonId)
                             .Select(s => s.Name)
                             .FirstOrDefault() ?? string.Empty;
            WorkerSalonTxtBox.Text = salonName;

            if (w.SalonId <= 0)
            {
                Skillsclb.Items.Clear();
                return;
            }

            if (!_db.ServiceOfferings.Any(s => s.SalonId == w.SalonId))
                EnsureSalonServicesSeeded(w.SalonId);

            var allServices = _db.ServiceOfferings.AsNoTracking()
                                .Where(s => s.SalonId == w.SalonId)
                                .OrderBy(s => s.Name)
                                .Select(s => new { s.Id, s.Name })
                                .ToList();

            if (allServices.Count == 0)
            {
                Skillsclb.Items.Clear();
                return;
            }

            var ownedIds = _db.EmployeeSkills
                              .Where(es => es.WorkerId == w.Id)
                              .Select(es => es.ServiceOfferingId)
                              .ToHashSet();

            Skillsclb.BeginUpdate();
            Skillsclb.Items.Clear();
            Skillsclb.DisplayMember = "Name";
            Skillsclb.ValueMember = "Id";

            foreach (var svc in allServices)
            {
                int idx = Skillsclb.Items.Add(svc);
                if (ownedIds.Contains(svc.Id))
                    Skillsclb.SetItemChecked(idx, true);
            }

            Skillsclb.EndUpdate();
        }

        private void ClearInputs()
        {
            WorkerIdTxtBox.Text = "0";
            WorkerNameTxtBox.Text = "";
            WorkerEmailTxtBox.Text = "";
            WorkerSalonTxtBox.Text = "";
            Skillsclb.Items.Clear();
            WorkerNameTxtBox.Focus();
        }

        // التحقق: يعيد salonId صالحًا (يدعم إدخال الاسم أو المعرف)
        private bool ValidateInputs(out string message, out int salonId)
        {
            message = "";
            salonId = 0;

            if (string.IsNullOrWhiteSpace(WorkerNameTxtBox.Text))
            { message = "Worker Name is required."; return false; }

            if (string.IsNullOrWhiteSpace(WorkerEmailTxtBox.Text))
            { message = "Worker Email is required."; return false; }

            if (!WorkerEmailTxtBox.Text.Contains("@") || !WorkerEmailTxtBox.Text.Contains("."))
            { message = "Invalid email format."; return false; }

            var raw = (WorkerSalonTxtBox.Text ?? "").Trim();
            if (string.IsNullOrEmpty(raw))
            { message = "Salon Name is required."; return false; }

            if (int.TryParse(raw, out var parsedId))
            {
                var byId = _db.Salons.Where(s => s.Id == parsedId).Select(s => s.Id).FirstOrDefault();
                if (byId == 0) { message = $"Salon with Id '{parsedId}' not found."; return false; }
                salonId = byId;
                return true;
            }

            var byName = _db.Salons
                .Where(s => s.Name.ToLower() == raw.ToLower())
                .Select(s => s.Id)
                .FirstOrDefault();

            if (byName == 0)
            { message = $"Salon '{raw}' not found."; return false; }

            salonId = byName;
            return true;
        }

        // حفظ تعليمات المهارات للعامل الحالي (العناصر يجب أن تكون {Id, Name})
        private void SaveSkillsSelection(int workerId)
        {
            var selectedIds = Skillsclb.CheckedItems
                .Cast<object>()
                .Select(o =>
                {
                    var p = o.GetType().GetProperty("Id");
                    return p != null ? (int)p.GetValue(o)! : 0;
                })
                .Where(id => id > 0)
                .ToHashSet();

            var currentIds = _db.EmployeeSkills
                .Where(es => es.WorkerId == workerId)
                .Select(es => es.ServiceOfferingId)
                .ToList();

            foreach (var sid in selectedIds.Except(currentIds))
                _db.EmployeeSkills.Add(new EmployeeSkill
                {
                    WorkerId = workerId,
                    ServiceOfferingId = sid,
                    AssignedDate = DateTime.UtcNow
                });

            var toRemove = currentIds.Except(selectedIds).ToList();
            if (toRemove.Count > 0)
            {
                var rows = _db.EmployeeSkills
                    .Where(es => es.WorkerId == workerId && toRemove.Contains(es.ServiceOfferingId));
                _db.EmployeeSkills.RemoveRange(rows);
            }

            _db.SaveChanges();
        }

        // ========= أزرار الواجهة =========

        private void AddBtn_Click(object sender, EventArgs e)
        {
            // 1) تحقق الحقول
            if (!ValidateInputs(out var msg, out var salonId))
            {
                MessageBox.Show(msg, "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2) منع تكرار البريد (عدّلي الجدول حسب متطلباتك)
            var email = WorkerEmailTxtBox.Text.Trim();
            if (_db.Users.Any(u => u.Email == email))
            {
                MessageBox.Show("Email already exists.", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 3) إنشاء العامل
            var worker = new Worker
            {
                FullName = WorkerNameTxtBox.Text.Trim(),
                Email = email,
                SalonId = salonId,
                CreatedAt = DateTime.UtcNow
            };

            _db.Workers.Add(worker);

            try
            {
                _db.SaveChanges(); // للحصول على Id

                // 4) ضمان وجود خدمات الصالون
                EnsureSalonServicesSeeded(worker.SalonId);

                // 5) تجهيز أسماء الخدمات المحددة (يدعم عناصر نصية أو {Id, Name})
                var checkedNames = Skillsclb.CheckedItems
                    .Cast<object>()
                    .Select(o =>
                    {
                        if (o is string s) return s;
                        var p = o.GetType().GetProperty("Name");
                        return p != null ? (string)p.GetValue(o)! : o.ToString()!;
                    })
                    .Where(s => !string.IsNullOrWhiteSpace(s))
                    .Select(s => s.Trim())
                    .Distinct(StringComparer.OrdinalIgnoreCase)
                    .ToList();

                // 6) الخدمات الموجودة
                var existingSvcs = _db.ServiceOfferings
                    .Where(s => s.SalonId == worker.SalonId && checkedNames.Contains(s.Name))
                    .ToList();

                // 7) أضف أي خدمة ناقصة
                var existingNames = existingSvcs.Select(s => s.Name).ToHashSet(StringComparer.OrdinalIgnoreCase);
                var missing = checkedNames.Where(n => !existingNames.Contains(n)).ToList();
                foreach (var name in missing)
                {
                    var svc = new ServiceOffering
                    {
                        SalonId = worker.SalonId,
                        Name = name,
                        DurationMinutes = 30,
                        Price = 0
                    };
                    _db.ServiceOfferings.Add(svc);
                    existingSvcs.Add(svc);
                }
                if (missing.Count > 0) _db.SaveChanges();

                // 8) روابط المهارات
                foreach (var svc in existingSvcs)
                {
                    _db.EmployeeSkills.Add(new EmployeeSkill
                    {
                        WorkerId = worker.Id,
                        ServiceOfferingId = svc.Id,
                        AssignedDate = DateTime.UtcNow
                    });
                }
                _db.SaveChanges();

                // 9) تحديث الواجهة
                LoadData();
                var idx = _db.Workers.Local.ToList().FindIndex(x => x.Id == worker.Id);
                if (idx >= 0 && idx < DataGridViewWorker.Rows.Count)
                    DataGridViewWorker.Rows[idx].Selected = true;

                FillFormFromSelection();

                MessageBox.Show("Worker and skills added.", "OK",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (DbUpdateException ex)
            {
                MessageBox.Show(ex.InnerException?.Message ?? ex.Message, "DB Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateBtn_Click(object? sender, EventArgs e)
        {
            if (_bs.Current is not Worker current)
            {
                MessageBox.Show("Select a worker first.", "Update",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateInputs(out var msg, out var salonId))
            {
                MessageBox.Show(msg, "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var email = WorkerEmailTxtBox.Text.Trim();
            if (_db.Users.Any(u => u.Email == email && u.Id != current.Id))
            {
                MessageBox.Show("Another user already uses this email.", "Duplicate",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            current.FullName = WorkerNameTxtBox.Text.Trim();
            current.Email = email;
            current.SalonId = salonId;

            EnsureSalonServicesSeeded(current.SalonId);

            try
            {
                _db.SaveChanges();
                SaveSkillsSelection(current.Id);
                _bs.ResetBindings(false);
                FillFormFromSelection();

                MessageBox.Show("Worker & skills updated.", "OK",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (DbUpdateException ex)
            {
                MessageBox.Show(ex.InnerException?.Message ?? ex.Message, "DB Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (_bs.Current is not Worker current)
            {
                MessageBox.Show("Select a worker first.", "Delete",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show($"Delete worker '{current.FullName}'?",
                                          "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            var skills = _db.EmployeeSkills.Where(es => es.WorkerId == current.Id);
            _db.EmployeeSkills.RemoveRange(skills);

            _db.Workers.Remove(current);
            try
            {
                _db.SaveChanges();
                LoadData();
                ClearInputs();
                MessageBox.Show("Worker deleted.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            ClearInputs();
        }

        private void WorkerDashboard_Load(object sender, EventArgs e)
        {
            LoadData();
            if (_bs.Count > 0) _bs.Position = 0;
            FillFormFromSelection();
        }
    }
}
