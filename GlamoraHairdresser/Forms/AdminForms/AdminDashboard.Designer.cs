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
            SuspendLayout();
            // 
            // SalonBtn
            // 
            SalonBtn.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            SalonBtn.Location = new Point(192, 76);
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
            WorkerBtn.Location = new Point(363, 76);
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
            ProfitBtn.Location = new Point(363, 183);
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
            CustomerBtn.Location = new Point(192, 183);
            CustomerBtn.Name = "CustomerBtn";
            CustomerBtn.Size = new Size(118, 56);
            CustomerBtn.TabIndex = 11;
            CustomerBtn.Text = "Customer";
            CustomerBtn.UseVisualStyleBackColor = true;
            CustomerBtn.Click += CustomerBtn_Click;
            // 
            // AdminDashboard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
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
    }
}