namespace GlamoraHairdresser.WinForms.Forms.AdminForms
{
    partial class AdminHoursForm
    {
        private System.ComponentModel.IContainer components = null;
        private ComboBox SalonComboBox;
        private DataGridView HoursGrid;
        private Button SaveBtn;

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminHoursForm));
            SalonComboBox = new ComboBox();
            HoursGrid = new DataGridView();
            Day = new DataGridViewTextBoxColumn();
            DayName = new DataGridViewTextBoxColumn();
            IsOpen = new DataGridViewCheckBoxColumn();
            OpenTime = new DataGridViewTextBoxColumn();
            CloseTime = new DataGridViewTextBoxColumn();
            SaveBtn = new Button();
            ((System.ComponentModel.ISupportInitialize)HoursGrid).BeginInit();
            SuspendLayout();
            // 
            // SalonComboBox
            // 
            SalonComboBox.ForeColor = Color.FromArgb(255, 192, 192);
            SalonComboBox.Location = new Point(81, 29);
            SalonComboBox.Name = "SalonComboBox";
            SalonComboBox.Size = new Size(121, 23);
            SalonComboBox.TabIndex = 0;
            SalonComboBox.SelectedIndexChanged += SalonComboBox_SelectedIndexChanged;
            // 
            // HoursGrid
            // 
            HoursGrid.BackgroundColor = Color.FromArgb(255, 192, 192);
            HoursGrid.Columns.AddRange(new DataGridViewColumn[] { Day, DayName, IsOpen, OpenTime, CloseTime });
            HoursGrid.Location = new Point(81, 94);
            HoursGrid.Name = "HoursGrid";
            HoursGrid.Size = new Size(544, 250);
            HoursGrid.TabIndex = 1;
            // 
            // Day
            // 
            Day.HeaderText = "Day";
            Day.Name = "Day";
            // 
            // DayName
            // 
            DayName.HeaderText = "Day Name";
            DayName.Name = "DayName";
            // 
            // IsOpen
            // 
            IsOpen.HeaderText = "Is Open";
            IsOpen.Name = "IsOpen";
            // 
            // OpenTime
            // 
            OpenTime.HeaderText = "Open Time";
            OpenTime.Name = "OpenTime";
            // 
            // CloseTime
            // 
            CloseTime.HeaderText = "Close Time";
            CloseTime.Name = "CloseTime";
            // 
            // SaveBtn
            // 
            SaveBtn.Location = new Point(307, 366);
            SaveBtn.Name = "SaveBtn";
            SaveBtn.Size = new Size(75, 23);
            SaveBtn.TabIndex = 2;
            SaveBtn.Text = "💾 Save Changes";
            SaveBtn.Click += SaveBtn_Click;
            // 
            // AdminHoursForm
            // 
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(700, 420);
            Controls.Add(SalonComboBox);
            Controls.Add(HoursGrid);
            Controls.Add(SaveBtn);
            Name = "AdminHoursForm";
            Text = "Manage Salon Working Hours";
            Load += AdminHoursForm_Load;
            ((System.ComponentModel.ISupportInitialize)HoursGrid).EndInit();
            ResumeLayout(false);
        }
        private DataGridViewTextBoxColumn Day;
        private DataGridViewTextBoxColumn DayName;
        private DataGridViewCheckBoxColumn IsOpen;
        private DataGridViewTextBoxColumn OpenTime;
        private DataGridViewTextBoxColumn CloseTime;
    }
}
