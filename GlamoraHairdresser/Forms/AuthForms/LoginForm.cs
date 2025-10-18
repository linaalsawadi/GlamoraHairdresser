using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using GlamoraHairdresser.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using GlamoraHairdresser.WinForms.Forms.AdminForms;
using GlamoraHairdresser.WinForms.Forms.CustomerForms;
using GlamoraHairdresser.WinForms.Forms.WorkerForms;

namespace GlamoraHairdresser.WinForms.Forms.AuthForms
{
    public partial class LoginForm : Form
    {
        private readonly IAuthService _auth;

        public LoginForm(IAuthService auth)
        {
            InitializeComponent();
            _auth = auth;
            PassTxtBox.PasswordChar = '●';
        }



        // ✅ عند الضغط على زر Register
        private async void btnRegister_Click(object sender, EventArgs e)
        {
            string email = EmailTxtBox.Text.Trim();
            string password = PassTxtBox.Text.Trim();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please fill all fields.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var exists = await _auth.EmailExistsAsync(email);
            if (exists)
            {
                MessageBox.Show("This email is already registered. Please login instead.",
                    "Already Exists", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string name = Microsoft.VisualBasic.Interaction.InputBox(
                "Enter your full name:", "Register", "");

            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Name cannot be empty.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var result = await _auth.RegisterCustomerAsync(name, email, password);

            if (result.Success)
            {
                MessageBox.Show("Registration successful! You can now log in.",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(result.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void LoginBtn_Click(object sender, EventArgs e)
        {
            string email = EmailTxtBox.Text.Trim();
            string password = PassTxtBox.Text.Trim();

            // التحقق أن الحقول غير فارغة
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both email and password.",
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // استخدام الخدمة للتحقق من صحة بيانات المستخدم
                var result = await _auth.AuthenticateAsync(email, password);

                if (result.Success)
                {
                    MessageBox.Show($"✅ Welcome {result.User!.FullName}!",
                        "Login Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // 👇 نتحقق من نوع المستخدم
                    string userType = result.User.UserType;

                    // ✅ Admin
                    if (userType.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                    {
                        var adminDashboard = Program.Services.GetRequiredService<AdminDashboard>();
                        adminDashboard.Show();
                        this.Hide();
                    }
                    // 👤 Customer
                    else if (userType.Equals("Customer", StringComparison.OrdinalIgnoreCase))
                    {
                        var customerForm = Program.Services.GetRequiredService<CustomerDashboard>();
                        customerForm.Show();
                        this.Hide();
                    }
                    // 👷 Worker (اختياري لاحقًا)
                    else if (userType.Equals("Worker", StringComparison.OrdinalIgnoreCase))
                    {
                        var workerForm = Program.Services.GetRequiredService<WorkerDashboard>();
                        workerForm.Show();
                        this.Hide();
                    }
                }
                else
                {
                    MessageBox.Show($"❌ {result.Message}",
                        "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}",
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
    }
}