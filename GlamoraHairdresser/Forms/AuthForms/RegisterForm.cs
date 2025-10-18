using GlamoraHairdresser.Services.Auth;
using GlamoraHairdresser.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
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

namespace GlamoraHairdresser.WinForms.Forms.AuthForms
{
    public partial class RegisterForm : Form
    {
        private readonly IAuthService _authService;
        public RegisterForm(IAuthService authService)
        {
            InitializeComponent();
            _authService = authService;

            RegPassTxtBox.PasswordChar = '●';
        }




        private async void CusRegisterBtn_Click(object sender, EventArgs e)
        {
            string name = RegNameTxtBox.Text.Trim();
            string email = RegEmailTxtBox.Text.Trim();
            string password = RegPassTxtBox.Text.Trim();

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("⚠️ Please fill in all fields.", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // تسجيل الكاستمر في الداتا بيز
                var result =  await _authService.RegisterCustomerAsync(name, email, password);

                if (result.Success)
                {
                    MessageBox.Show("✅ Registration successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // فتح صفحة تسجيل الدخول بعد النجاح
                    var loginForm = Program.Services.GetRequiredService<LoginForm>();
                    loginForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show($"❌ {result.Message}", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

        
    
