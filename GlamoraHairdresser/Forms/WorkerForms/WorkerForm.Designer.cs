namespace GlamoraHairdresser.WinForms.Forms.WorkerForms
{
    partial class WorkerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WorkerForm));
            appointmentsGrid = new DataGridView();
            btnApprove = new Button();
            btnReject = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            lblCustomerInfo = new Label();
            lblServiceInfo = new Label();
            lblDateTimeInfo = new Label();
            LogoutBtn = new Button();
            BackBtn = new Button();
            btnDailySchedule = new Button();
            ((System.ComponentModel.ISupportInitialize)appointmentsGrid).BeginInit();
            SuspendLayout();
            // 
            // appointmentsGrid
            // 
            appointmentsGrid.BackgroundColor = Color.FromArgb(255, 192, 192);
            appointmentsGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            appointmentsGrid.Location = new Point(0, 12);
            appointmentsGrid.Name = "appointmentsGrid";
            appointmentsGrid.Size = new Size(798, 262);
            appointmentsGrid.TabIndex = 0;
            // 
            // btnApprove
            // 
            btnApprove.BackColor = Color.RosyBrown;
            btnApprove.Font = new Font("Showcard Gothic", 9F);
            btnApprove.ForeColor = Color.Cornsilk;
            btnApprove.Location = new Point(700, 410);
            btnApprove.Name = "btnApprove";
            btnApprove.Size = new Size(75, 36);
            btnApprove.TabIndex = 1;
            btnApprove.Text = "Approve";
            btnApprove.UseVisualStyleBackColor = false;
            btnApprove.Click += btnApprove_Click;
            // 
            // btnReject
            // 
            btnReject.BackColor = Color.RosyBrown;
            btnReject.Font = new Font("Showcard Gothic", 9F);
            btnReject.ForeColor = Color.Cornsilk;
            btnReject.Location = new Point(609, 410);
            btnReject.Name = "btnReject";
            btnReject.Size = new Size(85, 36);
            btnReject.TabIndex = 2;
            btnReject.Text = "Reject";
            btnReject.UseVisualStyleBackColor = false;
            btnReject.Click += btnReject_Click;
            // 
            // label1
            // 
            label1.BackColor = Color.IndianRed;
            label1.Font = new Font("Showcard Gothic", 9F);
            label1.ForeColor = Color.Cornsilk;
            label1.Location = new Point(282, 291);
            label1.Name = "label1";
            label1.Size = new Size(100, 23);
            label1.TabIndex = 3;
            label1.Text = "Customer Info:";
            // 
            // label2
            // 
            label2.BackColor = Color.IndianRed;
            label2.Font = new Font("Showcard Gothic", 9F);
            label2.ForeColor = Color.Cornsilk;
            label2.Location = new Point(282, 331);
            label2.Name = "label2";
            label2.Size = new Size(100, 23);
            label2.TabIndex = 4;
            label2.Text = "Service: ";
            // 
            // label3
            // 
            label3.BackColor = Color.IndianRed;
            label3.Font = new Font("Showcard Gothic", 9F);
            label3.ForeColor = Color.Cornsilk;
            label3.Location = new Point(282, 371);
            label3.Name = "label3";
            label3.Size = new Size(100, 23);
            label3.TabIndex = 5;
            label3.Text = "Date & Time:";
            // 
            // lblCustomerInfo
            // 
            lblCustomerInfo.BackColor = Color.FromArgb(255, 192, 192);
            lblCustomerInfo.Location = new Point(442, 291);
            lblCustomerInfo.Name = "lblCustomerInfo";
            lblCustomerInfo.Size = new Size(100, 23);
            lblCustomerInfo.TabIndex = 6;
            // 
            // lblServiceInfo
            // 
            lblServiceInfo.BackColor = Color.FromArgb(255, 192, 192);
            lblServiceInfo.Location = new Point(442, 332);
            lblServiceInfo.Name = "lblServiceInfo";
            lblServiceInfo.Size = new Size(100, 23);
            lblServiceInfo.TabIndex = 7;
            // 
            // lblDateTimeInfo
            // 
            lblDateTimeInfo.BackColor = Color.FromArgb(255, 192, 192);
            lblDateTimeInfo.Location = new Point(442, 371);
            lblDateTimeInfo.Name = "lblDateTimeInfo";
            lblDateTimeInfo.Size = new Size(100, 23);
            lblDateTimeInfo.TabIndex = 8;
            // 
            // LogoutBtn
            // 
            LogoutBtn.BackColor = SystemColors.ActiveCaptionText;
            LogoutBtn.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            LogoutBtn.ForeColor = SystemColors.ButtonHighlight;
            LogoutBtn.Location = new Point(0, 410);
            LogoutBtn.Name = "LogoutBtn";
            LogoutBtn.Size = new Size(118, 38);
            LogoutBtn.TabIndex = 13;
            LogoutBtn.Text = "Logout";
            LogoutBtn.UseVisualStyleBackColor = false;
            LogoutBtn.Click += LogoutBtn_Click;
            // 
            // BackBtn
            // 
            BackBtn.BackColor = SystemColors.ActiveCaptionText;
            BackBtn.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            BackBtn.ForeColor = SystemColors.ButtonHighlight;
            BackBtn.Location = new Point(127, 410);
            BackBtn.Name = "BackBtn";
            BackBtn.Size = new Size(118, 38);
            BackBtn.TabIndex = 14;
            BackBtn.Text = "Back";
            BackBtn.UseVisualStyleBackColor = false;
            BackBtn.Click += BackBtn_Click;

            
            btnDailySchedule.Text = "Daily Schedule";
            btnDailySchedule.Location = new Point(500, 410);
            btnDailySchedule.Width = 100;
            btnDailySchedule.BackColor = Color.RosyBrown;
            btnDailySchedule.ForeColor = Color.White;
            btnDailySchedule.Click += btnDailySchedule_Click;
            Controls.Add(btnDailySchedule);

            // 
            // WorkerForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(800, 450);
            Controls.Add(BackBtn);
            Controls.Add(LogoutBtn);
            Controls.Add(lblDateTimeInfo);
            Controls.Add(lblServiceInfo);
            Controls.Add(lblCustomerInfo);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnReject);
            Controls.Add(btnApprove);
            Controls.Add(appointmentsGrid);
            Name = "WorkerForm";
            Text = "WorkerForm";
            ((System.ComponentModel.ISupportInitialize)appointmentsGrid).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView appointmentsGrid;
        private Button btnApprove;
        private Button btnReject;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label lblCustomerInfo;
        private Label lblServiceInfo;
        private Label lblDateTimeInfo;
        private Button LogoutBtn;
        private Button BackBtn;
        private Button btnDailySchedule;
    }
}