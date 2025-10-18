using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamoraHairdresser.Data.Entities
{
    public enum AppointmentStatus : byte
    {
        Pending = 0,   // بانتظار الموافقة
        Approved = 1,  // تم قبول الموعد
        Rejected = 2,  // تم الرفض
        Canceled = 3  // تم الإلغاء

    }

    public class Appointment : BaseEntity
    {
        // مفاتيح الربط
        public int SalonId { get; set; }
        public Salon Salon { get; set; } = default!;

        public int ServiceOfferingId { get; set; }
        public ServiceOffering ServiceOffering { get; set; } = default!;

        public int WorkerId { get; set; }
        public Worker Worker { get; set; } = default!;

        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = default!;

        // التوقيتات
        public DateTime StartUtc { get; set; }
        public DateTime EndUtc { get; set; }

        // حالة الحجز
        public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending;

        // معلومات مالية وتشغيلية
        public decimal? PriceAtBooking { get; set; }
        public int? DurationMinutes { get; set; }

        // ملاحظات
        public string? Notes { get; set; }

        // حماية من التعديل المتزامن
        public byte[]? RowVersion { get; set; }
    }
}
