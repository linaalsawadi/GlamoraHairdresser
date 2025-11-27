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
            dgvMyAppointments = new DataGridView();
            btnRefresh = new Button();
            btnClose = new Button();
            lblMessage = new Label();
            BackBtn = new Button();
            LogoutBtn = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvMyAppointments).BeginInit();
            SuspendLayout();
            // 
            // dgvMyAppointments
            // 
            dgvMyAppointments.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMyAppointments.Location = new Point(12, 12);
            dgvMyAppointments.Name = "dgvMyAppointments";
            dgvMyAppointments.Size = new Size(776, 250);
            dgvMyAppointments.TabIndex = 0;
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new Point(66, 308);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(75, 23);
            btnRefresh.TabIndex = 1;
            btnRefresh.Text = "Refresh";
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += btnRefresh_Click_1;
            // 
            // btnClose
            // 
            btnClose.Location = new Point(66, 362);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(75, 23);
            btnClose.TabIndex = 2;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = true;
            // 
            // lblMessage
            // 
            lblMessage.AutoSize = true;
            lblMessage.Location = new Point(598, 339);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(0, 15);
            lblMessage.TabIndex = 3;
            // 
            // BackBtn
            // 
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
            // 
            // LogoutBtn
            // 
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
            // 
            // MyAppointment
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
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
    }
}