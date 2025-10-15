using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamoraHairdresser.Data.Entities
{
    public class WorkerAvailability:BaseEntity
    {

        // 🔸 المفتاح الأجنبي الذي يربط العامل بهذه الفترة الزمنية
        public int WorkerId { get; set; }  // ⬅️ هذا السطر مفقود لديك!

        // 🔸 العلاقة إلى الكيان Worker
        public Worker Worker { get; set; } = default!;

        // 🔸 تاريخ اليوم الذي يعمل فيه العامل
        public DateOnly Date { get; set; }

        // 🔸 بداية فترة العمل
        public TimeOnly Start { get; set; }

        // 🔸 نهاية فترة العمل
        public TimeOnly End { get; set; }

        // 🔸 ملاحظات إضافية (اختياري)
        public string? Note { get; set; }
    }
}
