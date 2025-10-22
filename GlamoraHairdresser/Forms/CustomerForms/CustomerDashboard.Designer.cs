namespace GlamoraHairdresser.WinForms.Forms.CustomerForms
{
    partial class CustomerDashboard
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
            BookBtn = new Button();
            MyBookingBtn = new Button();
            BookLogoutBtn = new Button();
            SuspendLayout();
            // 
            // BookBtn
            // 
            BookBtn.BackColor = SystemColors.ScrollBar;
            BookBtn.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            BookBtn.Location = new Point(370, 124);
            BookBtn.Name = "BookBtn";
            BookBtn.Size = new Size(118, 56);
            BookBtn.TabIndex = 9;
            BookBtn.Text = "Book NOW";
            BookBtn.UseVisualStyleBackColor = false;
            BookBtn.Click += BookBtn_Click;
            // 
            // MyBookingBtn
            // 
            MyBookingBtn.BackColor = SystemColors.ScrollBar;
            MyBookingBtn.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            MyBookingBtn.ForeColor = SystemColors.ActiveCaptionText;
            MyBookingBtn.Location = new Point(320, 186);
            MyBookingBtn.Name = "MyBookingBtn";
            MyBookingBtn.Size = new Size(231, 52);
            MyBookingBtn.TabIndex = 10;
            MyBookingBtn.Text = "My Booking";
            MyBookingBtn.UseVisualStyleBackColor = false;
            MyBookingBtn.Click += MyBookingBtn_Click;
            // 
            // BookLogoutBtn
            // 
            BookLogoutBtn.BackColor = SystemColors.ButtonShadow;
            BookLogoutBtn.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            BookLogoutBtn.Location = new Point(12, 393);
            BookLogoutBtn.Name = "BookLogoutBtn";
            BookLogoutBtn.Size = new Size(118, 45);
            BookLogoutBtn.TabIndex = 13;
            BookLogoutBtn.Text = "Logout";
            BookLogoutBtn.UseVisualStyleBackColor = false;
            BookLogoutBtn.Click += BookLogoutBtn_Click;
            // 
            // CustomerDashboard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(BookLogoutBtn);
            Controls.Add(MyBookingBtn);
            Controls.Add(BookBtn);
            Name = "CustomerDashboard";
            Text = "customerForm";
            ResumeLayout(false);
        }

        #endregion

        private Button BookBtn;
        private Button MyBookingBtn;
        private Button BookLogoutBtn;
    }
}