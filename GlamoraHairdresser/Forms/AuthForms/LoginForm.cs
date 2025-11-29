using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using GlamoraHairdresser.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using GlamoraHairdresser.WinForms.Forms.AdminForms;
using GlamoraHairdresser.WinForms.Forms.CustomerForms;
using GlamoraHairdresser.WinForms.Forms.WorkerForms;
using GlamoraHairdresser.Services.Auth;

namespace GlamoraHairdresser.WinForms.Forms.AuthForms
{
    public partial class LoginForm : Form
    {
        private readonly IAuthService _auth;

        public LoginForm(IAuthService auth)
        {
            InitializeComponent();
            _auth = auth;

            SetupPlaceholders();
            SetupPasswordVisibility();

            this.KeyPreview = true;
            this.KeyDown += LoginForm_KeyDown;
        }

        // ===========================
        // UI Placeholders
        // ===========================

        private void SetupPlaceholders()
        {
            // Email
            EmailTxtBox.ForeColor = Color.Gray;
            EmailTxtBox.Text = "Enter email...";

            EmailTxtBox.GotFocus += (s, e) =>
            {
                if (EmailTxtBox.Text == "Enter email...")
                {
                    EmailTxtBox.Text = "";
                    EmailTxtBox.ForeColor = Color.Black;
                }
            };

            EmailTxtBox.LostFocus += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(EmailTxtBox.Text))
                {
                    EmailTxtBox.Text = "Enter email...";
                    EmailTxtBox.ForeColor = Color.Gray;
                }
            };

            // Password
            PassTxtBox.ForeColor = Color.Gray;
            PassTxtBox.PasswordChar = '\0';
            PassTxtBox.Text = "Enter password...";

            PassTxtBox.GotFocus += (s, e) =>
            {
                if (PassTxtBox.Text == "Enter password...")
                {
                    PassTxtBox.Text = "";
                    PassTxtBox.ForeColor = Color.Black;
                    PassTxtBox.PasswordChar = ShowPassChkBox.Checked ? '\0' : '●';
                }
            };

            PassTxtBox.LostFocus += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(PassTxtBox.Text))
                {
                    PassTxtBox.PasswordChar = '\0';
                    PassTxtBox.Text = "Enter password...";
                    PassTxtBox.ForeColor = Color.Gray;
                }
            };
        }

        private void SetupPasswordVisibility()
        {
            ShowPassChkBox.CheckedChanged += (s, e) =>
            {
                if (PassTxtBox.Text != "Enter password...")
                {
                    PassTxtBox.PasswordChar = ShowPassChkBox.Checked ? '\0' : '●';
                }
            };
        }

        // Press ENTER to Login
        private void LoginForm_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                LoginBtn.PerformClick();
        }

        // Reset fields
        public void ClearInputs()
        {
            EmailTxtBox.Text = "Enter email...";
            EmailTxtBox.ForeColor = Color.Gray;

            PassTxtBox.Text = "Enter password...";
            PassTxtBox.ForeColor = Color.Gray;
            PassTxtBox.PasswordChar = '\0';

            EmailTxtBox.Focus();
        }

        // ===========================
        // Mini REGISTER button
        // ===========================

        private async void btnRegister_Click(object sender, EventArgs e)
        {
            string email = EmailTxtBox.Text.Trim();
            string password = PassTxtBox.Text.Trim();

            if (email == "Enter email..." || password == "Enter password..." ||
                string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please fill both email & password to register quickly.",
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (await _auth.EmailExistsAsync(email))
            {
                MessageBox.Show("This email already exists. Try logging in.",
                    "Exists", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Ask for name
            string name = Microsoft.VisualBasic.Interaction.InputBox(
                "Enter your full name:", "Name Required", "");

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Name cannot be empty.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Ask for phone
            string phone = Microsoft.VisualBasic.Interaction.InputBox(
                "Enter your phone number:", "Phone Required", "");

            if (string.IsNullOrWhiteSpace(phone))
            {
                MessageBox.Show("Phone number is required.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var result = await _auth.RegisterCustomerAsync(name, email, password, phone);

            if (result.Success)
            {
                MessageBox.Show("Registration successful! Please login.",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ClearInputs();
            }
            else
            {
                MessageBox.Show(result.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ===========================
        // LOGIN button
        // ===========================

        private async void LoginBtn_Click(object sender, EventArgs e)
        {
            string email = EmailTxtBox.Text.Trim();
            string password = PassTxtBox.Text.Trim();

            if (email == "Enter email..." || password == "Enter password..." ||
                string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter both email and password.",
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var result = await _auth.AuthenticateAsync(email, password);

                if (!result.Success)
                {
                    MessageBox.Show($"❌ {result.Message}", "Login Failed",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                SessionManager.SetUser(result.User!);

                MessageBox.Show($"Welcome {result.User.FullName}!",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                switch (result.User.UserType)
                {
                    case "Admin":
                        Program.Services.GetRequiredService<AdminDashboard>().Show();
                        break;

                    case "Customer":
                        Program.Services.GetRequiredService<CustomerDashboard>().Show();
                        break;

                    case "Worker":
                        Program.Services.GetRequiredService<WorkerForm>().Show();
                        break;

                    default:
                        MessageBox.Show("Unknown user type!", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                }

                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}",
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ===========================
        // Go to Full Register Form
        // ===========================
        private void RegisterBtn_Click(object sender, EventArgs e)
        {
            var registerForm = Program.Services.GetRequiredService<RegisterForm>();
            registerForm.Show();
            this.Hide();
        }
    }
}
