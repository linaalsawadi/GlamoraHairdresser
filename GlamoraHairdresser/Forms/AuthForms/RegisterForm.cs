using GlamoraHairdresser.Services.Auth;
using GlamoraHairdresser.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Forms;

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
            string phone = RegPhoneTxtBox.Text.Trim();

            // ------------------------------
            // VALIDATION
            // ------------------------------
            if (string.IsNullOrEmpty(name) ||
                string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(password) ||
                string.IsNullOrEmpty(phone))
            {
                MessageBox.Show("⚠️ Please fill in all fields.",
                    "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!email.Contains("@") || !email.Contains("."))
            {
                MessageBox.Show("⚠️ Invalid email format.",
                    "Email Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (phone.Length < 6)
            {
                MessageBox.Show("⚠️ Phone number is too short.",
                    "Phone Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // تسجيل الكاستمر في الداتا بيز
                var result = await _authService.RegisterCustomerAsync(
                    name, email, password, phone  // ← أضفنا الهاتف
                );

                if (result.Success)
                {
                    MessageBox.Show("✅ Registration successful!",
                        "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // الانتقال إلى تسجيل الدخول
                    var loginForm = Program.Services.GetRequiredService<LoginForm>();
                    loginForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show($"❌ {result.Message}",
                        "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Unexpected error while saving data:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
