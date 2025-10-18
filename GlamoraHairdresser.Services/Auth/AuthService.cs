using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GlamoraHairdresser.Data;
using GlamoraHairdresser.Data.Entities;
using GlamoraHairdresser.Services.Interfaces;

namespace GlamoraHairdresser.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly GlamoraDbContext _db;

        public AuthService(GlamoraDbContext db)
        {
            _db = db;
        }

        // ✅ تشفير كلمة المرور باستخدام BCrypt (لا حاجة لـ salt يدوي)
        public string HashPassword(string passwordPlain)
        {
            return BCrypt.Net.BCrypt.HashPassword(passwordPlain);
        }

        // ✅ التحقق من صحة كلمة المرور
        public bool Verify(string passwordPlain, string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(passwordPlain, passwordHash);
        }

        // ✅ التحقق من صحة بيانات الدخول (Login)
        public async Task<AuthResult> AuthenticateAsync(string email, string passwordPlain)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return new AuthResult
                {
                    Success = false,
                    Message = "User not found."
                };
            }

            if (!Verify(passwordPlain, user.PasswordHash))
            {
                return new AuthResult
                {
                    Success = false,
                    Message = "Invalid password."
                };
            }

            return new AuthResult
            {
                Success = true,
                Message = "Login successful.",
                User = user
            };
        }

        // ✅ التحقق إذا كان البريد موجود مسبقًا
        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _db.Users.AnyAsync(u => u.Email == email);
        }

        // ✅ تسجيل مستخدم جديد من نوع Customer
        public async Task<AuthResult> RegisterCustomerAsync(string fullName, string email, string passwordPlain)
        {
            if (await EmailExistsAsync(email))
            {
                return new AuthResult
                {
                    Success = false,
                    Message = "Email already exists."
                };
            }

            string hash = HashPassword(passwordPlain);

            var customer = new Customer
            {
                FullName = fullName,
                Email = email,
                PasswordHash = hash,
                CreatedAt = DateTime.UtcNow
            };

            _db.Customers.Add(customer);
            await _db.SaveChangesAsync();

            return new AuthResult
            {
                Success = true,
                Message = "Customer registered successfully.",
                User = customer
            };
        }
    }
}