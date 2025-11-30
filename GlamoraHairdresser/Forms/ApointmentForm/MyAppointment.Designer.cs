namespace GlamoraHairdresser.WinForms.Forms.ApointmentForm
{
    partial class MyAppointment
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources =
                new System.ComponentModel.ComponentResourceManager(typeof(MyAppointment));

            dgvMyAppointments = new DataGridView();
            btnRefresh = new Button();
            btnClose = new Button();
            lblMessage = new Label();
            BackBtn = new Button();
            LogoutBtn = new Button();
            btnCancel = new Button();   // ← زر الإلغاء

            ((System.ComponentModel.ISupportInitialize)dgvMyAppointments).BeginInit();
            SuspendLayout();

            // ===================== DATAGRID =====================
            dgvMyAppointments.BackgroundColor = Color.FromArgb(255, 192, 192);
            dgvMyAppointments.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMyAppointments.Location = new Point(12, 12);
            dgvMyAppointments.Name = "dgvMyAppointments";
            dgvMyAppointments.Size = new Size(776, 375);
            dgvMyAppointments.TabIndex = 0;

            // ===================== REFRESH =====================
            btnRefresh.BackColor = Color.RosyBrown;
            btnRefresh.Font = new Font("Showcard Gothic", 9F);
            btnRefresh.ForeColor = Color.Cornsilk;
            btnRefresh.Location = new Point(706, 393);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(82, 42);
            btnRefresh.TabIndex = 1;
            btnRefresh.Text = "Refresh";
            btnRefresh.UseVisualStyleBackColor = false;
            btnRefresh.Click += btnRefresh_Click_1;

            // ===================== CLOSE =====================
            btnClose.BackColor = Color.RosyBrown;
            btnClose.Font = new Font("Showcard Gothic", 9F);
            btnClose.ForeColor = Color.Cornsilk;
            btnClose.Location = new Point(607, 393);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(82, 42);
            btnClose.TabIndex = 2;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += btnClose_Click_1;

            // ===================== CANCEL BUTTON =====================
            btnCancel.BackColor = Color.DarkRed;
            btnCancel.ForeColor = Color.White;
            btnCancel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnCancel.Location = new Point(500, 393);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(95, 42);
            btnCancel.TabIndex = 4;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;   // ← مهم لا تنساه

            // ===================== MESSAGE LABEL =====================
            lblMessage.AutoSize = true;
            lblMessage.Location = new Point(598, 339);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(0, 15);
            lblMessage.TabIndex = 3;

            // ===================== BACK =====================
            BackBtn.BackColor = SystemColors.ControlText;
            BackBtn.Font = new Font("Berlin Sans FB Demi", 20.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            BackBtn.ForeColor = Color.Cornsilk;
            BackBtn.Location = new Point(125, 393);
            BackBtn.Name = "BackBtn";
            BackBtn.Size = new Size(118, 45);
            BackBtn.TabIndex = 16;
            BackBtn.Text = "Back";
            BackBtn.UseVisualStyleBackColor = false;
            BackBtn.Click += BackBtn_Click;

            // ===================== LOGOUT =====================
            LogoutBtn.BackColor = SystemColors.ControlText;
            LogoutBtn.Font = new Font("Berlin Sans FB Demi", 20.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            LogoutBtn.ForeColor = Color.Cornsilk;
            LogoutBtn.Location = new Point(1, 393);
            LogoutBtn.Name = "LogoutBtn";
            LogoutBtn.Size = new Size(118, 45);
            LogoutBtn.TabIndex = 15;
            LogoutBtn.Text = "Logout";
            LogoutBtn.UseVisualStyleBackColor = false;
            LogoutBtn.Click += LogoutBtn_Click;

            // ===================== FORM =====================
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(800, 450);
            Controls.Add(btnCancel);
            Controls.Add(BackBtn);
            Controls.Add(LogoutBtn);
            Controls.Add(lblMessage);
            Controls.Add(btnClose);
            Controls.Add(btnRefresh);
            Controls.Add(dgvMyAppointments);
            Name = "MyAppointment";
            Text = "MyAppointment";

            ((System.ComponentModel.ISupportInitialize)dgvMyAppointments).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvMyAppointments;
        private Button btnRefresh;
        private Button btnClose;
        private Label lblMessage;
        private Button BackBtn;
        private Button LogoutBtn;
        private Button btnCancel;
    }
}
