using GlamoraHairdresser.Data;
using GlamoraHairdresser.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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

namespace GlamoraHairdresser.WinForms.Forms.WorkerForms
{
    public partial class WorkerDashboard : Form
    {
        private readonly GlamoraDbContext _db;
        private readonly int _workerId;
        private readonly BindingSource _bs = new();

        public WorkerDashboard(GlamoraDbContext db)
        {
            InitializeComponent();
            _db = db;
            Skillsclb.CheckOnClick = true; // اختياري
            DataGridViewWorker.SelectionChanged += Skillsclb_SelectedIndexChanged;

            Skillsclb.Enabled = true;
            Skillsclb.SelectionMode = SelectionMode.One;   // مهم
            Skillsclb.CheckOnClick = true;                 // نقرة واحدة تكفي
            Skillsclb.DataSource = null;   // نملؤها يدويًا

        }

        // قائمة افتراضية بالإنجليزية
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

        private void SaveSkillsSelection(int workerId, int salonId)
        {
            // IDs للخدمات المعلَّمة في القائمة
            var selectedIds = Skillsclb.CheckedItems
                .Cast<object>()
                .Select(o => (int)o.GetType().GetProperty("Id")!.GetValue(o)!)
                .ToHashSet();

            // IDs الحالية المخزنة للعامل
            var currentIds = _db.EmployeeSkills
                .Where(es => es.WorkerId == workerId)
                .Select(es => es.ServiceOfferingId)
                .ToList();

            // إضافة الجديد
            foreach (var sid in selectedIds.Except(currentIds))
                _db.EmployeeSkills.Add(new EmployeeSkill
                {
                    WorkerId = workerId,
                    ServiceOfferingId = sid,
                    AssignedDate = DateTime.UtcNow
                });

            // إزالة غير المعلّم
            var toRemove = currentIds.Except(selectedIds).ToList();
            if (toRemove.Count > 0)
            {
                var rows = _db.EmployeeSkills
                    .Where(es => es.WorkerId == workerId && toRemove.Contains(es.ServiceOfferingId));
                _db.EmployeeSkills.RemoveRange(rows);
            }

            _db.SaveChanges();
        }


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




        private void LoadData()
        {
            _db.ChangeTracker.Clear();

            // حمّل العمال + صالونهم (للاسم)
            _db.Workers.Include(w => w.Salon).Load();

            _bs.DataSource = _db.Workers.Local.ToBindingList();
            DataGridViewWorker.AutoGenerateColumns = true; // اتركها = true ما لم تكن تضيف أعمدة يدويًا

            DataGridViewWorker.DataSource = _bs;


            // ترتيب/إخفاء أعمدة
            var g = DataGridViewWorker;
            if (g.Columns.Contains("Id")) { g.Columns["Id"].DisplayIndex = 0; g.Columns["Id"].ReadOnly = true; g.Columns["Id"].Width = 60; }

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

            // لو العامل بلا صالون: أفرغ القائمة واخرج
            if (w.SalonId <= 0)
            {
                Skillsclb.Items.Clear();
                return;
            }

            // لو لا توجد خدمات للصالون: زَرْع الافتراضي
            if (!_db.ServiceOfferings.Any(s => s.SalonId == w.SalonId))
                EnsureSalonServicesSeeded(w.SalonId);

            // اجلب كل خدمات الصالون
            var allServices = _db.ServiceOfferings.AsNoTracking()
                                .Where(s => s.SalonId == w.SalonId)
                                .OrderBy(s => s.Name)
                                .Select(s => new { s.Id, s.Name })
                                .ToList();

            // حماية: إن بقيت صفر رغم الزرع
            if (allServices.Count == 0)
            {
                Skillsclb.Items.Clear();
                return;
            }

            // اجلب خدمات العامل الحالية (للتعليم)
            var ownedIds = _db.EmployeeSkills
                              .Where(es => es.WorkerId == w.Id)
                              .Select(es => es.ServiceOfferingId)
                              .ToHashSet();

            // املأ CheckedListBox
            Skillsclb.BeginUpdate();
            Skillsclb.Items.Clear();

            Skillsclb.DisplayMember = "Name";
            Skillsclb.ValueMember = "Id";

            foreach (var svc in allServices)
            {
                int idx = Skillsclb.Items.Add(svc);   // نخزّن (Id, Name)
                if (ownedIds.Contains(svc.Id))
                    Skillsclb.SetItemChecked(idx, true);
            }

            Skillsclb.EndUpdate();

            // ✳️ اختبار تشخيصي (احذفه لاحقاً):
            // MessageBox.Show($"SalonId={w.SalonId}, services={allServices.Count}");
        }
        private void ClearInputs()
        {
            WorkerIdTxtBox.Text = "0";
            WorkerNameTxtBox.Text = "";
            WorkerEmailTxtBox.Text = "";   // ← كان مكررًا بالاسم الغلط
            WorkerSalonTxtBox.Text = "";
            Skillsclb.Items.Clear();
            WorkerNameTxtBox.Focus();
        }

        private bool ValidateInputs(out string message, out int? salonId)
        {
            message = "";
            salonId = null;

            if (string.IsNullOrWhiteSpace(WorkerNameTxtBox.Text))
            {
                message = "Worker Name is required.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(WorkerEmailTxtBox.Text))
            {
                message = "Worker Email is required.";
                return false;
            }

            // (اختياري) التحقق من صيغة الإيميل
            if (!WorkerEmailTxtBox.Text.Contains("@") || !WorkerEmailTxtBox.Text.Contains("."))
            {
                message = "Invalid email format.";
                return false;
            }

            // ربط العامل بصالون عبر الاسم المكتوب
            if (!string.IsNullOrWhiteSpace(WorkerSalonTxtBox.Text))
            {
                var name = WorkerSalonTxtBox.Text.Trim();
                salonId = _db.Salons.Where(s => s.Name == name).Select(s => (int?)s.Id).FirstOrDefault();
                if (salonId is null)
                {
                    message = $"Salon '{name}' not found.";
                    return false;
                }
            }

            return true;
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            // 1) تحقق الحقول
            if (!ValidateInputs(out var msg, out var salonId))
            {
                MessageBox.Show(msg, "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2) منع تكرار البريد
            var email = WorkerEmailTxtBox.Text.Trim();
            if (_db.Users.Any(u => u.Email == email))
            {
                MessageBox.Show("Email already exists.", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 3) أنشئ العامل واحفظه أولاً للحصول على Id
            var worker = new Worker
            {
                FullName = WorkerNameTxtBox.Text.Trim(),
                Email = email,
                SalonId = salonId!.Value,
                CreatedAt = DateTime.UtcNow
            };

            _db.Workers.Add(worker);
            try
            {
                _db.SaveChanges(); // نحتاج Id

                // 4) تأكد أن خدمات الصالون موجودة (يزرع الافتراضي إن لزم)
                EnsureSalonServicesSeeded(worker.SalonId);

                // 5) جهّز قائمة الأسماء المعلَّمة من CheckedListBox
                //    تدعم حالتين: عناصر نصية (DefaultSkills) أو كائنات { Id, Name }
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

                // 6) احضر الخدمات الموجودة لهذه الأسماء
                var existingSvcs = _db.ServiceOfferings
                    .Where(s => s.SalonId == worker.SalonId && checkedNames.Contains(s.Name))
                    .ToList();

                // 7) أضف أي خدمة ناقصة (لو الاستخدام الأول)
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
                    existingSvcs.Add(svc); // حتى ندخلها في روابط المهارات
                }
                if (missing.Count > 0) _db.SaveChanges(); // لحصول الخدمات الجديدة على Id

                // 8) أنشئ روابط المهارات للعامل
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

                // 9) حدث الشبكة واختر السطر الجديد وأعد تعبئة التفاصيل
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

            // منع تكرار البريد لعامل آخر
            var email = WorkerEmailTxtBox.Text.Trim();
            if (_db.Users.Any(u => u.Email == email && u.Id != current.Id))
            {
                MessageBox.Show("Another user already uses this email.", "Duplicate",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // تحديث بيانات العامل
            current.FullName = WorkerNameTxtBox.Text.Trim();
            current.Email = email;
            current.SalonId = salonId!.Value;

            // تأكّد أن خدمات الصالون موجودة (لربط المهارات)
            EnsureSalonServicesSeeded(current.SalonId);

            try
            {
                _db.SaveChanges();                         // يحفظ بيانات العامل أولاً
                SaveSkillsSelection(current.Id, current.SalonId); // يحفظ المهارات المعلَّمة ✅

                // تحديث الواجهة
                _bs.ResetBindings(false);
                FillFormFromSelection(); // يعيد بناء القائمة ويُعلّم المهارات المحفوظة

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
                MessageBox.Show("Select a worker first.", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show($"Delete worker '{current.FullName}'?",
                                          "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            // احذف علاقات المهارات أولًا (إن كان FK مقيدًا)
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
                MessageBox.Show(ex.InnerException?.Message ?? ex.Message, "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _db.Entry(current).State = EntityState.Unchanged;
            }

        }

        private void Skillsclb_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillFormFromSelection();
        }

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            ClearInputs();
        }

        private void WorkerDashboard_Load(object sender, EventArgs e)
        {
            LoadData();                 // يعرض كل العمال في الـDataGridView
            if (_bs.Count > 0)          // حدّد أول صف ليتم تعبئة الحقول
                _bs.Position = 0;
            FillFormFromSelection();
        }
    }
}
