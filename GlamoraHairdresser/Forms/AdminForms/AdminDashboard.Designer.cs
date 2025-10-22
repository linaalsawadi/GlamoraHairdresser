namespace GlamoraHairdresser.WinForms.Forms.AdminForms
{
    partial class AdminDashboard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            SalonBtn = new Button();
            WorkerBtn = new Button();
            ProfitBtn = new Button();
            CustomerBtn = new Button();
            LogoutBtn = new Button();
            ServiceBtn = new Button();
            SuspendLayout();
            // 
            // SalonBtn
            // 
            SalonBtn.BackColor = SystemColors.Info;
            SalonBtn.Font = new Font("Showcard Gothic", 36F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            SalonBtn.Location = new Point(238, 52);
            SalonBtn.Name = "SalonBtn";
            SalonBtn.Size = new Size(318, 68);
            SalonBtn.TabIndex = 8;
            SalonBtn.Text = "Salon";
            SalonBtn.UseVisualStyleBackColor = false;
            SalonBtn.Click += SalonBtn_Click;
            // 
            // WorkerBtn
            // 
            WorkerBtn.BackColor = Color.DarkOrange;
            WorkerBtn.Font = new Font("Showcard Gothic", 36F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            WorkerBtn.Location = new Point(238, 200);
            WorkerBtn.Name = "WorkerBtn";
            WorkerBtn.Size = new Size(318, 68);
            WorkerBtn.TabIndex = 9;
            WorkerBtn.Text = "Worker";
            WorkerBtn.UseVisualStyleBackColor = false;
            WorkerBtn.Click += WorkerBtn_Click;
            // 
            // ProfitBtn
            // 
            ProfitBtn.BackColor = Color.IndianRed;
            ProfitBtn.Font = new Font("Showcard Gothic", 36F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            ProfitBtn.Location = new Point(238, 350);
            ProfitBtn.Name = "ProfitBtn";
            ProfitBtn.Size = new Size(318, 68);
            ProfitBtn.TabIndex = 10;
            ProfitBtn.Text = "Profit";
            ProfitBtn.UseVisualStyleBackColor = false;
            ProfitBtn.Click += ProfitBtn_Click;
            // 
            // CustomerBtn
            // 
            CustomerBtn.BackColor = Color.Crimson;
            CustomerBtn.Font = new Font("Showcard Gothic", 36F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            CustomerBtn.Location = new Point(238, 274);
            CustomerBtn.Name = "CustomerBtn";
            CustomerBtn.Size = new Size(318, 68);
            CustomerBtn.TabIndex = 11;
            CustomerBtn.Text = "Customer";
            CustomerBtn.UseVisualStyleBackColor = false;
            CustomerBtn.Click += CustomerBtn_Click;
            // 
            // LogoutBtn
            // 
            LogoutBtn.BackColor = SystemColors.ActiveCaptionText;
            LogoutBtn.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            LogoutBtn.ForeColor = SystemColors.ButtonHighlight;
            LogoutBtn.Location = new Point(12, 400);
            LogoutBtn.Name = "LogoutBtn";
            LogoutBtn.Size = new Size(118, 38);
            LogoutBtn.TabIndex = 12;
            LogoutBtn.Text = "Logout";
            LogoutBtn.UseVisualStyleBackColor = false;
            LogoutBtn.Click += LogoutBtn_Click;
            // 
            // ServiceBtn
            // 
            ServiceBtn.BackColor = Color.FromArgb(255, 192, 128);
            ServiceBtn.Font = new Font("Showcard Gothic", 36F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            ServiceBtn.Location = new Point(238, 126);
            ServiceBtn.Name = "ServiceBtn";
            ServiceBtn.Size = new Size(318, 68);
            ServiceBtn.TabIndex = 14;
            ServiceBtn.Text = "Services";
            ServiceBtn.UseVisualStyleBackColor = false;
            ServiceBtn.Click += ServiceBtn_Click;
            // 
            // AdminDashboard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(ServiceBtn);
            Controls.Add(LogoutBtn);
            Controls.Add(CustomerBtn);
            Controls.Add(ProfitBtn);
            Controls.Add(WorkerBtn);
            Controls.Add(SalonBtn);
            Name = "AdminDashboard";
            Text = "AdminDashboard";
            ResumeLayout(false);
        }

        #endregion
        private Button SalonBtn;
        private Button WorkerBtn;
        private Button ProfitBtn;
        private Button CustomerBtn;
        private Button LogoutBtn;
        private Button ServiceBtn;
    }
}