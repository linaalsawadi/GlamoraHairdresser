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
            datePicker = new DateTimePicker();
            salonCmb = new ComboBox();
            serviceCmb = new ComboBox();
            workerCmb = new ComboBox();
            timeSlotsList = new ListBox();
            bookBtn = new Button();
            BackBtn = new Button();
            BookLogoutBtn = new Button();
            SuspendLayout();
            // 
            // datePicker
            // 
            datePicker.Location = new Point(38, 65);
            datePicker.Name = "datePicker";
            datePicker.Size = new Size(200, 23);
            datePicker.TabIndex = 0;
            // 
            // salonCmb
            // 
            salonCmb.FormattingEnabled = true;
            salonCmb.Location = new Point(90, 110);
            salonCmb.Name = "salonCmb";
            salonCmb.Size = new Size(121, 23);
            salonCmb.TabIndex = 1;
            // 
            // serviceCmb
            // 
            serviceCmb.FormattingEnabled = true;
            serviceCmb.Location = new Point(90, 139);
            serviceCmb.Name = "serviceCmb";
            serviceCmb.Size = new Size(121, 23);
            serviceCmb.TabIndex = 2;
            // 
            // workerCmb
            // 
            workerCmb.FormattingEnabled = true;
            workerCmb.Location = new Point(90, 168);
            workerCmb.Name = "workerCmb";
            workerCmb.Size = new Size(121, 23);
            workerCmb.TabIndex = 3;
            // 
            // timeSlotsList
            // 
            timeSlotsList.FormattingEnabled = true;
            timeSlotsList.ItemHeight = 15;
            timeSlotsList.Location = new Point(91, 226);
            timeSlotsList.Name = "timeSlotsList";
            timeSlotsList.Size = new Size(120, 94);
            timeSlotsList.TabIndex = 4;
            // 
            // bookBtn
            // 
            bookBtn.Location = new Point(322, 337);
            bookBtn.Name = "bookBtn";
            bookBtn.Size = new Size(75, 23);
            bookBtn.TabIndex = 5;
            bookBtn.Text = "Book";
            bookBtn.UseVisualStyleBackColor = true;
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
            // MakeAppointment
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
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
    }
}