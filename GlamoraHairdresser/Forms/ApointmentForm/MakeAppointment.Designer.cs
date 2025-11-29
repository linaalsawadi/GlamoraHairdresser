namespace GlamoraHairdresser.WinForms.Forms.ApointmentForm
{
    partial class MakeAppointment
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MakeAppointment));
            datePicker = new DateTimePicker();
            salonCmb = new ComboBox();
            serviceCmb = new ComboBox();
            workerCmb = new ComboBox();
            timeSlotsList = new ListBox();
            bookBtn = new Button();
            BackBtn = new Button();
            BookLogoutBtn = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            SuspendLayout();
            // 
            // datePicker
            // 
            datePicker.CalendarTitleBackColor = SystemColors.ControlText;
            datePicker.CalendarTitleForeColor = Color.AliceBlue;
            datePicker.Location = new Point(304, 62);
            datePicker.Name = "datePicker";
            datePicker.Size = new Size(191, 23);
            datePicker.TabIndex = 0;
            // 
            // salonCmb
            // 
            salonCmb.BackColor = Color.FromArgb(255, 192, 192);
            salonCmb.FormattingEnabled = true;
            salonCmb.Location = new Point(411, 114);
            salonCmb.Name = "salonCmb";
            salonCmb.Size = new Size(238, 23);
            salonCmb.TabIndex = 1;
            // 
            // serviceCmb
            // 
            serviceCmb.BackColor = Color.FromArgb(255, 192, 192);
            serviceCmb.FormattingEnabled = true;
            serviceCmb.Location = new Point(411, 156);
            serviceCmb.Name = "serviceCmb";
            serviceCmb.Size = new Size(238, 23);
            serviceCmb.TabIndex = 2;
            // 
            // workerCmb
            // 
            workerCmb.BackColor = Color.FromArgb(255, 192, 192);
            workerCmb.FormattingEnabled = true;
            workerCmb.Location = new Point(411, 201);
            workerCmb.Name = "workerCmb";
            workerCmb.Size = new Size(238, 23);
            workerCmb.TabIndex = 3;
            // 
            // timeSlotsList
            // 
            timeSlotsList.BackColor = Color.FromArgb(255, 192, 192);
            timeSlotsList.ForeColor = Color.BlanchedAlmond;
            timeSlotsList.FormattingEnabled = true;
            timeSlotsList.ItemHeight = 15;
            timeSlotsList.Location = new Point(311, 261);
            timeSlotsList.Name = "timeSlotsList";
            timeSlotsList.Size = new Size(180, 184);
            timeSlotsList.TabIndex = 4;
            // 
            // bookBtn
            // 
            bookBtn.BackColor = Color.RosyBrown;
            bookBtn.Font = new Font("Showcard Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            bookBtn.ForeColor = Color.Cornsilk;
            bookBtn.Location = new Point(645, 378);
            bookBtn.Name = "bookBtn";
            bookBtn.Size = new Size(133, 54);
            bookBtn.TabIndex = 5;
            bookBtn.Text = "Book";
            bookBtn.UseVisualStyleBackColor = false;
            // 
            // BackBtn
            // 
            BackBtn.BackColor = SystemColors.ControlText;
            BackBtn.Font = new Font("Berlin Sans FB Demi", 20.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            BackBtn.ForeColor = Color.Cornsilk;
            BackBtn.Location = new Point(134, 393);
            BackBtn.Name = "BackBtn";
            BackBtn.Size = new Size(118, 45);
            BackBtn.TabIndex = 16;
            BackBtn.Text = "Back";
            BackBtn.UseVisualStyleBackColor = false;
            BackBtn.Click += BackBtn_Click;
            // 
            // BookLogoutBtn
            // 
            BookLogoutBtn.BackColor = SystemColors.ControlText;
            BookLogoutBtn.Font = new Font("Berlin Sans FB Demi", 20.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            BookLogoutBtn.ForeColor = Color.Cornsilk;
            BookLogoutBtn.Location = new Point(10, 393);
            BookLogoutBtn.Name = "BookLogoutBtn";
            BookLogoutBtn.Size = new Size(118, 45);
            BookLogoutBtn.TabIndex = 15;
            BookLogoutBtn.Text = "Logout";
            BookLogoutBtn.UseVisualStyleBackColor = false;
            BookLogoutBtn.Click += BookLogoutBtn_Click;
            // 
            // label1
            // 
            label1.BackColor = Color.IndianRed;
            label1.Enabled = false;
            label1.Font = new Font("Showcard Gothic", 12F);
            label1.ForeColor = Color.Cornsilk;
            label1.Location = new Point(139, 114);
            label1.Name = "label1";
            label1.Size = new Size(215, 23);
            label1.TabIndex = 17;
            label1.Text = "Choose Salon";
            // 
            // label2
            // 
            label2.BackColor = Color.IndianRed;
            label2.Enabled = false;
            label2.Font = new Font("Showcard Gothic", 12F);
            label2.ForeColor = Color.Cornsilk;
            label2.Location = new Point(139, 161);
            label2.Name = "label2";
            label2.Size = new Size(215, 23);
            label2.TabIndex = 18;
            label2.Text = "Choose Service";
            // 
            // label3
            // 
            label3.BackColor = Color.IndianRed;
            label3.Enabled = false;
            label3.Font = new Font("Showcard Gothic", 12F);
            label3.ForeColor = Color.Cornsilk;
            label3.Location = new Point(139, 206);
            label3.Name = "label3";
            label3.Size = new Size(215, 23);
            label3.TabIndex = 19;
            label3.Text = "Choose HairDresser";
            // 
            // label4
            // 
            label4.BackColor = Color.IndianRed;
            label4.Enabled = false;
            label4.Font = new Font("Showcard Gothic", 12F);
            label4.ForeColor = Color.Cornsilk;
            label4.Location = new Point(292, 22);
            label4.Name = "label4";
            label4.Size = new Size(215, 23);
            label4.TabIndex = 20;
            label4.Text = "Choose Salon";
            // 
            // MakeAppointment
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(800, 450);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(BackBtn);
            Controls.Add(BookLogoutBtn);
            Controls.Add(bookBtn);
            Controls.Add(timeSlotsList);
            Controls.Add(workerCmb);
            Controls.Add(serviceCmb);
            Controls.Add(salonCmb);
            Controls.Add(datePicker);
            Name = "MakeAppointment";
            Text = "MakeAppointment";
            ResumeLayout(false);
        }

        #endregion

        private DateTimePicker datePicker;
        private ComboBox salonCmb;
        private ComboBox serviceCmb;
        private ComboBox workerCmb;
        private ListBox timeSlotsList;
        private Button bookBtn;
        private Button BackBtn;
        private Button BookLogoutBtn;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
    }
}