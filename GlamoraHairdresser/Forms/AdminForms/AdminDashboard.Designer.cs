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
            BackBtn = new Button();
            SuspendLayout();
            // 
            // SalonBtn
            // 
            SalonBtn.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            SalonBtn.Location = new Point(227, 144);
            SalonBtn.Name = "SalonBtn";
            SalonBtn.Size = new Size(118, 56);
            SalonBtn.TabIndex = 8;
            SalonBtn.Text = "Salon";
            SalonBtn.UseVisualStyleBackColor = true;
            SalonBtn.Click += SalonBtn_Click;
            // 
            // WorkerBtn
            // 
            WorkerBtn.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            WorkerBtn.Location = new Point(398, 144);
            WorkerBtn.Name = "WorkerBtn";
            WorkerBtn.Size = new Size(118, 56);
            WorkerBtn.TabIndex = 9;
            WorkerBtn.Text = "Worker";
            WorkerBtn.UseVisualStyleBackColor = true;
            WorkerBtn.Click += WorkerBtn_Click;
            // 
            // ProfitBtn
            // 
            ProfitBtn.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            ProfitBtn.Location = new Point(398, 251);
            ProfitBtn.Name = "ProfitBtn";
            ProfitBtn.Size = new Size(118, 56);
            ProfitBtn.TabIndex = 10;
            ProfitBtn.Text = "Profit";
            ProfitBtn.UseVisualStyleBackColor = true;
            ProfitBtn.Click += ProfitBtn_Click;
            // 
            // CustomerBtn
            // 
            CustomerBtn.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            CustomerBtn.Location = new Point(227, 251);
            CustomerBtn.Name = "CustomerBtn";
            CustomerBtn.Size = new Size(118, 56);
            CustomerBtn.TabIndex = 11;
            CustomerBtn.Text = "Customer";
            CustomerBtn.UseVisualStyleBackColor = true;
            CustomerBtn.Click += CustomerBtn_Click;
            // 
            // LogoutBtn
            // 
            LogoutBtn.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            LogoutBtn.Location = new Point(136, 418);
            LogoutBtn.Name = "LogoutBtn";
            LogoutBtn.Size = new Size(118, 29);
            LogoutBtn.TabIndex = 12;
            LogoutBtn.Text = "Logout";
            LogoutBtn.UseVisualStyleBackColor = true;
            LogoutBtn.Click += LogoutBtn_Click;
            // 
            // BackBtn
            // 
            BackBtn.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            BackBtn.Location = new Point(12, 418);
            BackBtn.Name = "BackBtn";
            BackBtn.Size = new Size(118, 29);
            BackBtn.TabIndex = 13;
            BackBtn.Text = "Back";
            BackBtn.UseVisualStyleBackColor = true;
            BackBtn.Click += BackBtn_Click;
            // 
            // AdminDashboard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(BackBtn);
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
        private Button BackBtn;
    }
}