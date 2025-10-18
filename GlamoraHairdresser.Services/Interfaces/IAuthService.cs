using GlamoraHairdresser.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        string HashPassword(string passwordPlain);
        bool Verify(string passwordPlain, string passwordHash);
        Task<AuthResult> AuthenticateAsync(string email, string passwordPlain);
        Task<bool> EmailExistsAsync(string email);
        Task<AuthResult> RegisterCustomerAsync(string fullName, string email, string passwordPlain);

       
    }
}
