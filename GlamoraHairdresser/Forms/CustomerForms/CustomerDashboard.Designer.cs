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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomerDashboard));
            BookBtn = new Button();
            MyBookingBtn = new Button();
            BookLogoutBtn = new Button();
            BackBtn = new Button();
            SuspendLayout();
            // 
            // BookBtn
            // 
            BookBtn.BackColor = Color.RosyBrown;
            BookBtn.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            BookBtn.Location = new Point(320, 124);
            BookBtn.Name = "BookBtn";
            BookBtn.Size = new Size(231, 56);
            BookBtn.TabIndex = 9;
            BookBtn.Text = "Book NOW";
            BookBtn.UseVisualStyleBackColor = false;
            BookBtn.Click += BookBtn_Click;
            // 
            // MyBookingBtn
            // 
            MyBookingBtn.BackColor = Color.RosyBrown;
            MyBookingBtn.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            MyBookingBtn.ForeColor = SystemColors.ActiveCaptionText;
            MyBookingBtn.Location = new Point(320, 186);
            MyBookingBtn.Name = "MyBookingBtn";
            MyBookingBtn.Size = new Size(231, 56);
            MyBookingBtn.TabIndex = 10;
            MyBookingBtn.Text = "My Booking";
            MyBookingBtn.UseVisualStyleBackColor = false;
            MyBookingBtn.Click += MyBookingBtn_Click;
            // 
            // BookLogoutBtn
            // 
            BookLogoutBtn.BackColor = SystemColors.ControlText;
            BookLogoutBtn.Font = new Font("Berlin Sans FB Demi", 20.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            BookLogoutBtn.ForeColor = Color.Cornsilk;
            BookLogoutBtn.Location = new Point(12, 393);
            BookLogoutBtn.Name = "BookLogoutBtn";
            BookLogoutBtn.Size = new Size(118, 45);
            BookLogoutBtn.TabIndex = 13;
            BookLogoutBtn.Text = "Logout";
            BookLogoutBtn.UseVisualStyleBackColor = false;
            BookLogoutBtn.Click += BookLogoutBtn_Click;
            // 
            // BackBtn
            // 
            BackBtn.BackColor = SystemColors.ControlText;
            BackBtn.Font = new Font("Berlin Sans FB Demi", 20.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            BackBtn.ForeColor = Color.Cornsilk;
            BackBtn.Location = new Point(136, 393);
            BackBtn.Name = "BackBtn";
            BackBtn.Size = new Size(118, 45);
            BackBtn.TabIndex = 14;
            BackBtn.Text = "Back";
            BackBtn.UseVisualStyleBackColor = false;
            BackBtn.Click += BackBtn_Click;
            // 
            // CustomerDashboard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(800, 450);
            Controls.Add(BackBtn);
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
        private Button BackBtn;
    }
}