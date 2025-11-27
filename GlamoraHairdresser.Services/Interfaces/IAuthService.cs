using GlamoraHairdresser.Data.Entities;
using System.Threading.Tasks;

namespace GlamoraHairdresser.Services.Interfaces
{
    public sealed class AuthResult
    {
        public bool Success { get; set; }
        public string Message { get; set; } = "";
        public User? User { get; set; }
    }

    public interface IAuthService
    {
        // 🔐 تسجيل الدخول
        Task<AuthResult> AuthenticateAsync(string email, string passwordPlain);

        // 📧 التحقق من وجود البريد مسبقًا
        Task<bool> EmailExistsAsync(string email);

        // 🆕 تسجيل مستخدم جديد من نوع Customer
        Task<AuthResult> RegisterCustomerAsync(string fullName, string email, string passwordPlain);
    }
}
