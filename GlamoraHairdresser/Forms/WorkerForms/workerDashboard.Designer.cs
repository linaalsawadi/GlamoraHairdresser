namespace GlamoraHairdresser.WinForms.Forms.WorkerForms
{
    partial class workerDashboard
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
            ProfileBtn = new Button();
            AppointmetBtn = new Button();
            onayBtn = new Button();
            SuspendLayout();
            // 
            // ProfileBtn
            // 
            ProfileBtn.BackColor = Color.FromArgb(255, 192, 192);
            ProfileBtn.Font = new Font("Kristen ITC", 27.75F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            ProfileBtn.Location = new Point(266, 79);
            ProfileBtn.Name = "ProfileBtn";
            ProfileBtn.Size = new Size(285, 78);
            ProfileBtn.TabIndex = 9;
            ProfileBtn.Text = "Profile";
            ProfileBtn.UseVisualStyleBackColor = false;
            // 
            // AppointmetBtn
            // 
            AppointmetBtn.BackColor = Color.FromArgb(255, 224, 192);
            AppointmetBtn.Font = new Font("Kristen ITC", 27.75F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            AppointmetBtn.Location = new Point(266, 186);
            AppointmetBtn.Name = "AppointmetBtn";
            AppointmetBtn.Size = new Size(285, 78);
            AppointmetBtn.TabIndex = 10;
            AppointmetBtn.Text = "Appointmet";
            AppointmetBtn.UseVisualStyleBackColor = false;
            // 
            // onayBtn
            // 
            onayBtn.BackColor = Color.FromArgb(255, 192, 255);
            onayBtn.Font = new Font("Kristen ITC", 27.75F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            onayBtn.Location = new Point(266, 301);
            onayBtn.Name = "onayBtn";
            onayBtn.Size = new Size(285, 78);
            onayBtn.TabIndex = 11;
            onayBtn.Text = "onay";
            onayBtn.UseVisualStyleBackColor = false;
            // 
            // workerDashboard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(onayBtn);
            Controls.Add(AppointmetBtn);
            Controls.Add(ProfileBtn);
            Name = "workerDashboard";
            Text = "workerDashboard";
            ResumeLayout(false);
        }

        #endregion

        private Button ProfileBtn;
        private Button AppointmetBtn;
        private Button onayBtn;
    }
}