using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlamoraHairdresser.Services.Interfaces
{
    public sealed class AuthResult
    {
        public bool Success { get; init; }
        public string Message { get; init; } = string.Empty;
        public int UserId { get; init; }
        public string FullName { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
        public string Role { get; init; } = string.Empty; // Admin/Worker/Customer
    }
    public interface IAuthService
    {
        Task<AuthResult> AuthenticateAsync(string email, string passwordPlain);
        Task<bool> EmailExistsAsync(string email);
        Task<AuthResult> RegisterCustomerAsync(string fullName, string email, string passwordPlain);

        string HashPassword(string passwordPlain);
        bool Verify(string passwordPlain, string passwordHash);
    }
}
