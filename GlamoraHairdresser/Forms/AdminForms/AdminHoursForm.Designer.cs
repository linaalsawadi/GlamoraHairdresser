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
            SalonComboBox = new ComboBox();
            HoursGrid = new DataGridView();
            SaveBtn = new Button();
            Day = new DataGridViewTextBoxColumn();
            DayName = new DataGridViewTextBoxColumn();
            IsOpen = new DataGridViewCheckBoxColumn();
            OpenTime = new DataGridViewTextBoxColumn();
            CloseTime = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)HoursGrid).BeginInit();
            SuspendLayout();
            // 
            // SalonComboBox
            // 
            SalonComboBox.Location = new Point(30, 30);
            SalonComboBox.Name = "SalonComboBox";
            SalonComboBox.Size = new Size(121, 23);
            SalonComboBox.TabIndex = 0;
            SalonComboBox.SelectedIndexChanged += SalonComboBox_SelectedIndexChanged;
            // 
            // HoursGrid
            // 
            HoursGrid.Columns.AddRange(new DataGridViewColumn[] { Day, DayName, IsOpen, OpenTime, CloseTime });
            HoursGrid.Location = new Point(30, 79);
            HoursGrid.Name = "HoursGrid";
            HoursGrid.Size = new Size(546, 250);
            HoursGrid.TabIndex = 1;
            // 
            // SaveBtn
            // 
            SaveBtn.Location = new Point(250, 350);
            SaveBtn.Name = "SaveBtn";
            SaveBtn.Size = new Size(75, 23);
            SaveBtn.TabIndex = 2;
            SaveBtn.Text = "💾 Save Changes";
            SaveBtn.Click += SaveBtn_Click;
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
            // AdminHoursForm
            // 
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
