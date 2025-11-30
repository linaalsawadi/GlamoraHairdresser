namespace GlamoraHairdresser.WinForms.Forms.WorkerForms
{
    partial class WorkerDailyScheduleForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            datePicker = new DateTimePicker();
            btnLoad = new Button();
            btnBack = new Button();
            dgvSchedule = new DataGridView();
            lblMessage = new Label();

            SuspendLayout();

            // ================================
            // Date Picker
            // ================================
            datePicker.Format = DateTimePickerFormat.Short;
            datePicker.Location = new Point(20, 20);
            datePicker.Width = 150;
            datePicker.Font = new Font("Segoe UI", 11, FontStyle.Bold);

            // ================================
            // Load Button
            // ================================
            btnLoad.Text = "Load";
            btnLoad.Location = new Point(180, 20);
            btnLoad.Width = 90;
            btnLoad.Height = 32;
            btnLoad.BackColor = Color.IndianRed;
            btnLoad.ForeColor = Color.White;
            btnLoad.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnLoad.FlatStyle = FlatStyle.Flat;
            btnLoad.Click += btnLoad_Click;

            // ================================
            // Back Button
            // ================================
            btnBack.Text = "Back";
            btnBack.Location = new Point(20, 390);
            btnBack.Width = 100;
            btnBack.Height = 35;
            btnBack.BackColor = Color.Black;
            btnBack.ForeColor = Color.White;
            btnBack.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.Click += btnBack_Click;

            // ================================
            // DataGridView
            // ================================
            dgvSchedule.Location = new Point(20, 70);
            dgvSchedule.Size = new Size(760, 300);
            dgvSchedule.BackgroundColor = Color.FromArgb(255, 192, 192);
            dgvSchedule.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSchedule.ReadOnly = true;
            dgvSchedule.AllowUserToAddRows = false;
            dgvSchedule.AllowUserToDeleteRows = false;
            dgvSchedule.AutoGenerateColumns = true;
            dgvSchedule.GridColor = Color.DarkRed;

            // ================================
            // Message Label
            // ================================
            lblMessage.Dock = DockStyle.Bottom;      // ⭐ أفضل مكان
            lblMessage.Height = 40;
            lblMessage.ForeColor = Color.Maroon;
            lblMessage.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblMessage.TextAlign = ContentAlignment.MiddleCenter;
            lblMessage.BackColor = Color.Transparent;
            lblMessage.Text = "";                   // فارغ بالبداية

            // ================================
            // Form Properties
            // ================================
            ClientSize = new Size(800, 450);
            Controls.Add(datePicker);
            Controls.Add(btnLoad);
            Controls.Add(btnBack);
            Controls.Add(dgvSchedule);
            Controls.Add(lblMessage);
            Text = "Daily Schedule";
            BackColor = Color.White;

            ResumeLayout(false);
        }

        private DateTimePicker datePicker;
        private Button btnLoad;
        private Button btnBack;
        private DataGridView dgvSchedule;
        private Label lblMessage;
    }
}
