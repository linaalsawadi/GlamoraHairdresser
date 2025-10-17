using GlamoraHairdresser.Data.Entities;
using GlamoraHairdresser.Data;
using GlamoraHairdresser.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GlamoraHairdresser.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly GlamoraDbContext _db;
        public AuthService(GlamoraDbContext db) => _db = db;

        public async Task<AuthResult> AuthenticateAsync(string email, string passwordPlain)
        {
            var user = await _db.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
            if (user is null || !Verify(passwordPlain, user.PasswordHash))
                return new AuthResult { Success = false, Message = "Invalid credentials." };

            var role = user switch
            {
                Admin => "Admin",
                Worker => "Worker",
                Customer => "Customer",
                _ => user.GetType().Name
            };

            return new AuthResult
            {
                Success = true,
                Message = "OK",
                UserId = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = role
            };
        }

        public async Task<bool> EmailExistsAsync(string email)
            => await _db.Users.AnyAsync(u => u.Email == email);

        public async Task<AuthResult> RegisterCustomerAsync(string fullName, string email, string passwordPlain)
        {
            if (await EmailExistsAsync(email))
                return new AuthResult { Success = false, Message = "Email already exists." };

            var cust = new Customer
            {
                FullName = fullName.Trim(),
                Email = email.Trim(),
                PasswordHash = HashPassword(passwordPlain)
            };
            _db.Customers.Add(cust);
            await _db.SaveChangesAsync();

            return new AuthResult
            {
                Success = true,
                Message = "Registered successfully.",
                UserId = cust.Id,
                FullName = cust.FullName,
                Email = cust.Email,
                Role = "Customer"
            };
        }

        public string HashPassword(string passwordPlain) => BCrypt.Net.BCrypt.HashPassword(passwordPlain);
        public bool Verify(string passwordPlain, string passwordHash) => BCrypt.Net.BCrypt.Verify(passwordPlain, passwordHash);
    }
}
