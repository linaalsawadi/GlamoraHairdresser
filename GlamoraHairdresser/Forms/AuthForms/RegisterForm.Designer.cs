namespace GlamoraHairdresser.WinForms.Forms.AuthForms
{
    partial class RegisterForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegisterForm));
            RegPassLbl = new Label();
            RegEmailLbl = new Label();
            RegEmailTxtBox = new TextBox();
            RegPassTxtBox = new TextBox();
            RegNameLbl = new Label();
            RegNameTxtBox = new TextBox();
            CusRegisterBtn = new Button();
            SuspendLayout();
            // 
            // RegPassLbl
            // 
            RegPassLbl.AutoSize = true;
            RegPassLbl.BackColor = Color.RosyBrown;
            RegPassLbl.Font = new Font("Simplified Arabic Fixed", 18F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            RegPassLbl.ForeColor = Color.Cornsilk;
            RegPassLbl.Location = new Point(213, 253);
            RegPassLbl.Name = "RegPassLbl";
            RegPassLbl.Size = new Size(132, 28);
            RegPassLbl.TabIndex = 4;
            RegPassLbl.Text = "Password";
            // 
            // RegEmailLbl
            // 
            RegEmailLbl.AutoSize = true;
            RegEmailLbl.BackColor = Color.RosyBrown;
            RegEmailLbl.Font = new Font("Simplified Arabic Fixed", 18F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            RegEmailLbl.ForeColor = SystemColors.ButtonHighlight;
            RegEmailLbl.Location = new Point(213, 164);
            RegEmailLbl.Name = "RegEmailLbl";
            RegEmailLbl.Size = new Size(87, 28);
            RegEmailLbl.TabIndex = 3;
            RegEmailLbl.Text = "Email";
            // 
            // RegEmailTxtBox
            // 
            RegEmailTxtBox.Location = new Point(397, 151);
            RegEmailTxtBox.Multiline = true;
            RegEmailTxtBox.Name = "RegEmailTxtBox";
            RegEmailTxtBox.Size = new Size(226, 41);
            RegEmailTxtBox.TabIndex = 5;
            // 
            // RegPassTxtBox
            // 
            RegPassTxtBox.Location = new Point(397, 240);
            RegPassTxtBox.Multiline = true;
            RegPassTxtBox.Name = "RegPassTxtBox";
            RegPassTxtBox.Size = new Size(226, 41);
            RegPassTxtBox.TabIndex = 6;
            // 
            // RegNameLbl
            // 
            RegNameLbl.AutoSize = true;
            RegNameLbl.BackColor = Color.RosyBrown;
            RegNameLbl.Font = new Font("Simplified Arabic Fixed", 18F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            RegNameLbl.ForeColor = Color.Snow;
            RegNameLbl.Location = new Point(213, 73);
            RegNameLbl.Name = "RegNameLbl";
            RegNameLbl.Size = new Size(72, 28);
            RegNameLbl.TabIndex = 7;
            RegNameLbl.Text = "Name";
            // 
            // RegNameTxtBox
            // 
            RegNameTxtBox.Location = new Point(397, 60);
            RegNameTxtBox.Multiline = true;
            RegNameTxtBox.Name = "RegNameTxtBox";
            RegNameTxtBox.Size = new Size(226, 41);
            RegNameTxtBox.TabIndex = 8;
            // 
            // CusRegisterBtn
            // 
            CusRegisterBtn.BackColor = Color.IndianRed;
            CusRegisterBtn.Font = new Font("Showcard Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            CusRegisterBtn.ForeColor = SystemColors.Control;
            CusRegisterBtn.Location = new Point(397, 354);
            CusRegisterBtn.Name = "CusRegisterBtn";
            CusRegisterBtn.Size = new Size(79, 41);
            CusRegisterBtn.TabIndex = 9;
            CusRegisterBtn.Text = "Register";
            CusRegisterBtn.UseVisualStyleBackColor = false;
            CusRegisterBtn.Click += CusRegisterBtn_Click;
            // 
            // RegisterForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(800, 450);
            Controls.Add(CusRegisterBtn);
            Controls.Add(RegNameTxtBox);
            Controls.Add(RegNameLbl);
            Controls.Add(RegPassTxtBox);
            Controls.Add(RegEmailTxtBox);
            Controls.Add(RegPassLbl);
            Controls.Add(RegEmailLbl);
            Name = "RegisterForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label RegPassLbl;
        private Label RegEmailLbl;
        private TextBox RegEmailTxtBox;
        private TextBox RegPassTxtBox;
        private Label RegNameLbl;
        private TextBox RegNameTxtBox;
        private Button CusRegisterBtn;
    }
}