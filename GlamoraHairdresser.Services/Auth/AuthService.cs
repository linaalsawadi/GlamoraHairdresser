using GlamoraHairdresser.Data;
using GlamoraHairdresser.Data.Entities;
using GlamoraHairdresser.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace GlamoraHairdresser.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly GlamoraDbContext _db;

        public AuthService(GlamoraDbContext db)
        {
            _db = db;
        }

        // ============================
        // LOGIN
        // ============================
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

            // ---- PBKDF2 Verification ----
            bool isValid = PasswordHelper.VerifyPassword(
                passwordPlain,
                user.PasswordHash,
                user.Salt,
                user.IterationCount,
                user.Prf
            );

            if (!isValid)
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

        // ============================
        // EMAIL CHECK
        // ============================
        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _db.Users.AnyAsync(u => u.Email == email);
        }

        // ============================
        // REGISTER CUSTOMER
        // ============================
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

            var (hash, salt, iteration, prf) = PasswordHelper.HashPassword(passwordPlain);

            var customer = new Customer
            {
                FullName = fullName,
                Email = email,
                PasswordHash = hash,
                Salt = salt,
                Prf = 1,
                IterationCount = 100_000,
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
