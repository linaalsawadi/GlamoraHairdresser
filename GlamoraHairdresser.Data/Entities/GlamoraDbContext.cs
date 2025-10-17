using Microsoft.EntityFrameworkCore;
using GlamoraHairdresser.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamoraHairdresser.Data
{
    public class GlamoraDbContext : DbContext
    {// ----------- DbSets -----------
        public DbSet<Salon> Salons => Set<Salon>();
        public DbSet<WorkingHour> WorkingHours => Set<WorkingHour>();
        public DbSet<ServiceOffering> ServiceOfferings => Set<ServiceOffering>();

        public DbSet<User> Users => Set<User>();
        public DbSet<Admin> Admins => Set<Admin>();
        public DbSet<Worker> Workers => Set<Worker>();
        public DbSet<Customer> Customers => Set<Customer>();

        public DbSet<EmployeeSkill> EmployeeSkills => Set<EmployeeSkill>();
        public DbSet<WorkerAvailability> WorkerAvailabilities => Set<WorkerAvailability>();
        public DbSet<Appointment> Appointments => Set<Appointment>();

        // ----------- Ctors -----------
        public GlamoraDbContext() { }
        public GlamoraDbContext(DbContextOptions<GlamoraDbContext> options) : base(options) { }

        // ----------- Configuration -----------
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // يسمح بالتهيئة من DI؛ وإن لم تكن مهيّأة نستخدم سلسلة اتصال افتراضية محلية
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Server=DESKTOP-DHABHQ9\\SQLEXPRESS05;" +
                    "Database=GlamoraDb;" +
                    "Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");
            }
        }

        // ----------- Model building -----------
        protected override void OnModelCreating(ModelBuilder model)
        {
            ConfigureUsers(model);
            ConfigureSalon(model);
            ConfigureWorkingHours(model);
            ConfigureServiceOffering(model);
            ConfigureWorker(model);
            ConfigureEmployeeSkill(model);
            ConfigureWorkerAvailability(model);
            ConfigureAppointment(model);
        }

        // ===== Users (TPH) =====
        private static void ConfigureUsers(ModelBuilder model)
        {
            model.Entity<User>()
                 .HasDiscriminator<string>("UserType")
                 .HasValue<Admin>("Admin")
                 .HasValue<Worker>("Worker")
                 .HasValue<Customer>("Customer");

            model.Entity<User>()
                 .HasIndex(u => u.Email)
                 .IsUnique();

            model.Entity<User>()
                 .Property(u => u.Email)
                 .IsRequired()
                 .HasMaxLength(450);

            model.Entity<User>()
                 .Property(u => u.PasswordHash)
                 .IsRequired()
                 .HasMaxLength(200);

            // فحص منطقي
            model.Entity<User>()
                 .ToTable(t =>
                 {
                     t.HasCheckConstraint("CK_UserType_NotEmpty", "[UserType] IN ('Admin','Worker','Customer')");
                 });
        }


        // ===== Salon =====
        private static void ConfigureSalon(ModelBuilder model)
        {
            model.Entity<Salon>()
                 .HasKey(s => s.Id);

            // خصائص
            model.Entity<Salon>()
                 .Property(s => s.Name)
                 .IsRequired()
                 .HasMaxLength(150);

            model.Entity<Salon>()
                 .Property(s => s.Address)
                 .HasMaxLength(300);

            model.Entity<Salon>()
                 .Property(s => s.PhoneNumber)
                 .IsRequired()
                 .HasMaxLength(25);

            model.Entity<Salon>()
                .HasIndex(s => new { s.Name, s.Address })
                .IsUnique();               // اسم الصالون فريد (احذفها إن ما تريده فريداً)

            model.Entity<Salon>()
                 .HasIndex(s => s.PhoneNumber)
                 .IsUnique();                // رقم الهاتف فريد لمنع التكرار (اختياري لكنه مفيد)

            // قيود منطقية (Check Constraints)
            model.Entity<Salon>()
                 .ToTable(t =>
                 {
                     // يمنع PhoneNumber الفارغ بعد الـRequired والـMaxLength
                     t.HasCheckConstraint("CK_Salon_Phone_NotBlank", "LEN([PhoneNumber]) >= 7");
                     // يمنع اسم صالون مكوّن من مسافات فقط
                     t.HasCheckConstraint("CK_Salon_Name_NotBlank", "LEN(LTRIM(RTRIM([Name]))) > 0");
                 });
        }


        private static void ConfigureWorkingHours(ModelBuilder model)
        {
            model.Entity<WorkingHour>().ToTable("WorkingHours");

            model.Entity<WorkingHour>()
                 .HasKey(w => w.Id);

            // Salon (1) -> (N) WorkingHours
            model.Entity<WorkingHour>()
                 .HasOne(w => w.Salon)
                 .WithMany(s => s.WorkingHours)
                 .HasForeignKey(w => w.SalonId)
                 .OnDelete(DeleteBehavior.Cascade);

            // خصائص مطلوبة
            model.Entity<WorkingHour>().Property(w => w.DayOfWeek).IsRequired();
            model.Entity<WorkingHour>().Property(w => w.OpenTime).IsRequired();
            model.Entity<WorkingHour>().Property(w => w.CloseTime).IsRequired();

            // صف واحد فقط لكل (Salon, Day)
            model.Entity<WorkingHour>()
                 .HasIndex(w => new { w.SalonId, w.DayOfWeek })
                 .IsUnique();

            // قيود منطقية مفيدة
            model.Entity<WorkingHour>()
                 .ToTable(t =>
                 {
                     t.HasCheckConstraint("CK_WorkingHour_Day", "[DayOfWeek] BETWEEN 1 AND 7");
                     t.HasCheckConstraint("CK_WorkingHour_Time", "[OpenTime] < [CloseTime]");
                 });
        }


        private static void ConfigureServiceOffering(ModelBuilder model)
        {
            model.Entity<ServiceOffering>().ToTable("ServiceOfferings");

            model.Entity<ServiceOffering>()
                 .HasKey(s => s.Id);

            model.Entity<ServiceOffering>()
                 .Property(s => s.SalonId)
                 .IsRequired();

            model.Entity<ServiceOffering>()
                 .Property(s => s.Name)
                 .IsRequired()
                 .HasMaxLength(150);                 // اسم الخدمة بطول منطقي

            model.Entity<ServiceOffering>()
                 .Property(s => s.DurationMinutes)
                 .IsRequired();

            model.Entity<ServiceOffering>()
                 .Property(s => s.Price)
                 .IsRequired()
                 .HasPrecision(10, 2);               // أو .HasColumnType("decimal(10,2)")

            model.Entity<ServiceOffering>()
                 .HasOne(s => s.Salon)
                 .WithMany(sa => sa.Services)
                 .HasForeignKey(s => s.SalonId)
                 .OnDelete(DeleteBehavior.Cascade);

            model.Entity<ServiceOffering>()
                 .HasIndex(s => new { s.SalonId, s.Name })
                 .IsUnique();                         // منع تكرار اسم الخدمة داخل نفس الصالون

            model.Entity<ServiceOffering>()
                 .ToTable(t =>
                 {
                     t.HasCheckConstraint("CK_Service_Duration", "[DurationMinutes] BETWEEN 5 AND 600");
                     t.HasCheckConstraint("CK_Service_Price", "[Price] >= 0");
                 });
        }


        // ===== Worker : User (Salon 1 → N Workers) =====
        private static void ConfigureWorker(ModelBuilder model)
        {
            model.Entity<Worker>()
                 .HasOne(w => w.Salon)
                 .WithMany(s => s.Workers)
                 .HasForeignKey(w => w.SalonId)
                 .OnDelete(DeleteBehavior.Cascade);

            model.Entity<Worker>()
                .Property(w => w.SalonId)
                .IsRequired();
        }

        // ===== EmployeeSkill (Many-to-Many: Worker ↔ ServiceOffering) =====
        private static void ConfigureEmployeeSkill(ModelBuilder model)
        {
            model.Entity<EmployeeSkill>()
                 .HasKey(es => new { es.WorkerId, es.ServiceOfferingId });

            model.Entity<EmployeeSkill>()
                 .HasOne(es => es.Worker)
                 .WithMany(w => w.Skills)
                 .HasForeignKey(es => es.WorkerId)
                 .OnDelete(DeleteBehavior.Restrict);

            model.Entity<EmployeeSkill>()
                 .HasOne(es => es.ServiceOffering)
                 .WithMany(s => s.Workers)
                 .HasForeignKey(es => es.ServiceOfferingId)
                 .OnDelete(DeleteBehavior.Cascade);
        }

        // ===== WorkerAvailability (Worker 1 → N Availabilities) =====
        // داخل GlamoraDbContext
        private static void ConfigureWorkerAvailability(ModelBuilder model)
        {
            model.Entity<WorkerAvailability>().ToTable("WorkerAvailabilities");

            // PK
            model.Entity<WorkerAvailability>().HasKey(a => a.Id);

            // FK: Worker (1 -> N)
            model.Entity<WorkerAvailability>()
                 .HasOne(a => a.Worker)
                 .WithMany(w => w.Availabilities)
                 .HasForeignKey(a => a.WorkerId)
                 .OnDelete(DeleteBehavior.Cascade);

            // Required + أنواع الأعمدة
            model.Entity<WorkerAvailability>()
                 .Property(a => a.WorkerId)
                 .IsRequired();

            model.Entity<WorkerAvailability>()
                 .Property(a => a.Date)
                 .HasColumnType("date")
                 .IsRequired();

            // غيّر الدقة إلى time(2) إن احتجت ثواني
            model.Entity<WorkerAvailability>()
                 .Property(a => a.Start)
                 .HasColumnType("time(0)")
                 .IsRequired();

            model.Entity<WorkerAvailability>()
                 .Property(a => a.End)
                 .HasColumnType("time(0)")
                 .IsRequired();

            model.Entity<WorkerAvailability>()
                 .Property(a => a.Note)
                 .HasMaxLength(500)
                 .IsUnicode(true);

            // فهرس للاستعلام السريع: (عامل + يوم)
            model.Entity<WorkerAvailability>()
                 .HasIndex(a => new { a.WorkerId, a.Date })
                 .HasDatabaseName("IX_Avail_Worker_Date");

            // فهرس عام على اليوم لتسريع "جدول اليوم"
            model.Entity<WorkerAvailability>()
                 .HasIndex(a => a.Date)
                 .HasDatabaseName("IX_Avail_Date");

            // منع تكرار نفس الفترة حرفياً لنفس العامل في نفس اليوم
            model.Entity<WorkerAvailability>()
                 .HasIndex(a => new { a.WorkerId, a.Date, a.Start, a.End })
                 .IsUnique()
                 .HasDatabaseName("UX_Avail_Worker_Date_TimeRange");

            // CHECK: وقت البداية < وقت النهاية
            model.Entity<WorkerAvailability>()
                 .ToTable(t => t.HasCheckConstraint("CK_Avail_Time", "[Start] < [End]"));
        }



        private static void ConfigureAppointment(ModelBuilder model)
        {
            // اسم الجدول
            model.Entity<Appointment>().ToTable("Appointments");

            // PK
            model.Entity<Appointment>()
                 .HasKey(a => a.Id);

            // ================= علاقات FK =================

            // مرجعية الصالون (سلامة البيانات)
            model.Entity<Appointment>()
                 .Property(a => a.SalonId).IsRequired();

            model.Entity<Appointment>()
                 .HasOne(a => a.Salon)
                 .WithMany()                         // لا نحتفظ بقائمة Appointments داخل Salon لتخفيف التحميل
                 .HasForeignKey(a => a.SalonId)
                 .OnDelete(DeleteBehavior.Restrict); // لا نحذف الحجز تاريخيًا بحذف الصالون

            // خدمة واحدة لكل موعد (ServiceOffering 1 → N Appointments)
            model.Entity<Appointment>()
                 .Property(a => a.ServiceOfferingId).IsRequired();

            model.Entity<Appointment>()
                 .HasOne(a => a.ServiceOffering)
                 .WithMany(s => s.Appointments)
                 .HasForeignKey(a => a.ServiceOfferingId)
                 .OnDelete(DeleteBehavior.Restrict);

            // عامل واحد لكل موعد (Worker 1 → N Appointments)
            model.Entity<Appointment>()
                 .Property(a => a.WorkerId).IsRequired();

            model.Entity<Appointment>()
                 .HasOne(a => a.Worker)
                 .WithMany(w => w.Appointments)
                 .HasForeignKey(a => a.WorkerId)
                 .OnDelete(DeleteBehavior.Restrict);

            // زبون واحد لكل موعد (Customer 1 → N Appointments)
            model.Entity<Appointment>()
                 .Property(a => a.CustomerId).IsRequired();

            model.Entity<Appointment>()
                 .HasOne(a => a.Customer)
                 .WithMany(c => c.Appointments)
                 .HasForeignKey(a => a.CustomerId)
                 .OnDelete(DeleteBehavior.Restrict);

            // ================= خصائص الأعمدة =================

            // الأوقات بالتاريخ/الوقت: نخزّنها كـ datetime2(0 أو 3) حسب دقتك
            model.Entity<Appointment>()
                 .Property(a => a.StartUtc)
                 .HasColumnType("datetime2(0)")
                 .IsRequired();

            model.Entity<Appointment>()
                 .Property(a => a.EndUtc)
                 .HasColumnType("datetime2(0)")
                 .IsRequired();

            // الحالة (enum): يُخزَّن كـ tinyint تلقائيًا
            model.Entity<Appointment>()
                 .Property(a => a.Status)
                 .HasConversion<byte>()          // وضّح التخزين كـ byte
                 .IsRequired();

            // السعر وقت الحجز
            model.Entity<Appointment>()
                 .Property(a => a.PriceAtBooking)
                 .HasPrecision(10, 2);           // decimal(10,2)

            // مدة الحجز (اختياري)
            model.Entity<Appointment>()
                 .Property(a => a.DurationMinutes);

            // الملاحظات
            model.Entity<Appointment>()
                 .Property(a => a.Notes)
                 .HasMaxLength(1000)
                 .IsUnicode(true);

            // RowVersion لمنع الكتابة المتزامنة
            model.Entity<Appointment>()
                 .Property(a => a.RowVersion)
                 .IsRowVersion();

            // ================= فهارس/قيود =================

            // فهرس يساعد على اكتشاف التعارضات بسرعة (نفس العامل ونفس الفترة)
            model.Entity<Appointment>()
                 .HasIndex(a => new { a.WorkerId, a.StartUtc, a.EndUtc })
                 .IsUnique(); // يمنع تكرار نفس الموعد حرفيًا (نفس العامل ونفس البداية/النهاية)

            // فهارس مفيدة للتقارير والاستعلامات
            model.Entity<Appointment>()
                 .HasIndex(a => new { a.SalonId, a.StartUtc })      // كل حجوزات صالون في يوم/مدى
                 .HasDatabaseName("IX_Appt_Salon_Start");

            model.Entity<Appointment>()
                 .HasIndex(a => new { a.CustomerId, a.StartUtc })   // تاريخ العميل
                 .HasDatabaseName("IX_Appt_Customer_Start");

            // قيد منطقي لزمن الموعد
            model.Entity<Appointment>()
                 .ToTable(t =>
                 {
                     t.HasCheckConstraint("CK_Appt_Time", "[StartUtc] < [EndUtc]");
                 });

            // (اختياري) التحقق من أن المدة معقولة إذا خُزنت
            // model.Entity<Appointment>()
            //      .ToTable(t => t.HasCheckConstraint("CK_Appt_Duration", "[DurationMinutes] IS NULL OR [DurationMinutes] BETWEEN 5 AND 600"));
        }

    }
}