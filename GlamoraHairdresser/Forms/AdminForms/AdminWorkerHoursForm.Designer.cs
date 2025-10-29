namespace GlamoraHairdresser.WinForms.Forms.AdminForms
{
    partial class AdminWorkerHoursForm
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
            TitleLabel = new Label();
            label1 = new Label();
            WorkerComboBox = new ComboBox();
            HoursGrid = new DataGridView();
            Day = new DataGridViewTextBoxColumn();
            DayName = new DataGridViewTextBoxColumn();
            SalonHours = new DataGridViewTextBoxColumn();
            IsOpen = new DataGridViewCheckBoxColumn();
            OpenTime = new DataGridViewTextBoxColumn();
            CloseTime = new DataGridViewTextBoxColumn();
            SaveBtn = new Button();
            ((System.ComponentModel.ISupportInitialize)HoursGrid).BeginInit();
            SuspendLayout();
            // 
            // TitleLabel
            // 
            TitleLabel.AutoSize = true;
            TitleLabel.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold);
            TitleLabel.ForeColor = Color.MediumSlateBlue;
            TitleLabel.Location = new Point(25, 20);
            TitleLabel.Name = "TitleLabel";
            TitleLabel.Size = new Size(402, 32);
            TitleLabel.TabIndex = 0;
            TitleLabel.Text = "🕒 Manage Worker Working Hours";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10F);
            label1.Location = new Point(30, 75);
            label1.Name = "label1";
            label1.Size = new Size(95, 19);
            label1.TabIndex = 1;
            label1.Text = "Select Worker:";
            // 
            // WorkerComboBox
            // 
            WorkerComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            WorkerComboBox.Font = new Font("Segoe UI", 10F);
            WorkerComboBox.FormattingEnabled = true;
            WorkerComboBox.Location = new Point(140, 72);
            WorkerComboBox.Name = "WorkerComboBox";
            WorkerComboBox.Size = new Size(300, 25);
            WorkerComboBox.TabIndex = 2;
            WorkerComboBox.SelectedIndexChanged += WorkerComboBox_SelectedIndexChanged;
            // 
            // HoursGrid
            // 
            HoursGrid.AllowUserToAddRows = false;
            HoursGrid.AllowUserToDeleteRows = false;
            HoursGrid.BackgroundColor = Color.WhiteSmoke;
            HoursGrid.BorderStyle = BorderStyle.None;
            HoursGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            HoursGrid.Columns.AddRange(new DataGridViewColumn[] { Day, DayName, SalonHours, IsOpen, OpenTime, CloseTime });
            HoursGrid.Location = new Point(76, 121);
            HoursGrid.MultiSelect = false;
            HoursGrid.Name = "HoursGrid";
            HoursGrid.RowHeadersVisible = false;
            HoursGrid.RowTemplate.Height = 30;
            HoursGrid.Size = new Size(651, 300);
            HoursGrid.TabIndex = 3;
            // 
            // Day
            // 
            Day.HeaderText = "Day";
            Day.Name = "Day";
            Day.Visible = false;
            // 
            // DayName
            // 
            DayName.HeaderText = "Day Name";
            DayName.Name = "DayName";
            DayName.Width = 120;
            // 
            // SalonHours
            // 
            SalonHours.HeaderText = "Salon Hours";
            SalonHours.Name = "SalonHours";
            SalonHours.Width = 150;
            // 
            // IsOpen
            // 
            IsOpen.HeaderText = "Is Open";
            IsOpen.Name = "IsOpen";
            IsOpen.Width = 80;
            // 
            // OpenTime
            // 
            OpenTime.HeaderText = "Open Time";
            OpenTime.Name = "OpenTime";
            OpenTime.Width = 150;
            // 
            // CloseTime
            // 
            CloseTime.HeaderText = "Close Time";
            CloseTime.Name = "CloseTime";
            CloseTime.Width = 150;
            // 
            // SaveBtn
            // 
            SaveBtn.BackColor = Color.MediumSlateBlue;
            SaveBtn.FlatAppearance.BorderSize = 0;
            SaveBtn.FlatStyle = FlatStyle.Flat;
            SaveBtn.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            SaveBtn.ForeColor = Color.White;
            SaveBtn.Location = new Point(590, 440);
            SaveBtn.Name = "SaveBtn";
            SaveBtn.Size = new Size(164, 40);
            SaveBtn.TabIndex = 4;
            SaveBtn.Text = "💾 Save Changes";
            SaveBtn.UseVisualStyleBackColor = false;
            SaveBtn.Click += SaveBtn_Click;
            // 
            // AdminWorkerHoursForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(800, 500);
            Controls.Add(SaveBtn);
            Controls.Add(HoursGrid);
            Controls.Add(WorkerComboBox);
            Controls.Add(label1);
            Controls.Add(TitleLabel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "AdminWorkerHoursForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Worker Working Hours";
            Load += AdminWorkerHoursForm_Load;
            ((System.ComponentModel.ISupportInitialize)HoursGrid).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox WorkerComboBox;
        private System.Windows.Forms.DataGridView HoursGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Day;
        private System.Windows.Forms.DataGridViewTextBoxColumn DayName;
        private System.Windows.Forms.DataGridViewTextBoxColumn SalonHours;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsOpen;
        private System.Windows.Forms.DataGridViewTextBoxColumn OpenTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn CloseTime;
        private System.Windows.Forms.Button SaveBtn;
    }
}
