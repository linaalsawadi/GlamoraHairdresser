namespace GlamoraHairdresser.WinForms.Forms.AuthForms
{
    partial class LoginForm
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            EmailLabel = new Label();
            PassLabel = new Label();
            EmailTxtBox = new TextBox();
            PassTxtBox = new TextBox();
            imageList1 = new ImageList(components);
            RegisterLabel = new Label();
            RegisterBtn = new Button();
            LoginBtn = new Button();
            ShowPassChkBox = new CheckBox();
            SuspendLayout();
            // 
            // EmailLabel
            // 
            EmailLabel.AutoSize = true;
            EmailLabel.BackColor = Color.RosyBrown;
            EmailLabel.Font = new Font("Simplified Arabic Fixed", 18F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            EmailLabel.ForeColor = SystemColors.ControlLightLight;
            EmailLabel.Location = new Point(188, 105);
            EmailLabel.Name = "EmailLabel";
            EmailLabel.Size = new Size(87, 28);
            EmailLabel.TabIndex = 0;
            EmailLabel.Text = "Email";
            // 
            // PassLabel
            // 
            PassLabel.AutoSize = true;
            PassLabel.BackColor = Color.RosyBrown;
            PassLabel.Font = new Font("Simplified Arabic Fixed", 18F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            PassLabel.ForeColor = SystemColors.ControlLightLight;
            PassLabel.Location = new Point(188, 181);
            PassLabel.Name = "PassLabel";
            PassLabel.Size = new Size(132, 28);
            PassLabel.TabIndex = 2;
            PassLabel.Text = "Password";
            // 
            // EmailTxtBox
            // 
            EmailTxtBox.Location = new Point(339, 92);
            EmailTxtBox.Multiline = true;
            EmailTxtBox.Name = "EmailTxtBox";
            EmailTxtBox.Size = new Size(226, 41);
            EmailTxtBox.TabIndex = 3;
            // 
            // PassTxtBox
            // 
            PassTxtBox.Location = new Point(339, 181);
            PassTxtBox.Multiline = true;
            PassTxtBox.Name = "PassTxtBox";
            PassTxtBox.Size = new Size(226, 41);
            PassTxtBox.TabIndex = 4;
            // 
            // imageList1
            // 
            imageList1.ColorDepth = ColorDepth.Depth32Bit;
            imageList1.ImageStream = (ImageListStreamer)resources.GetObject("imageList1.ImageStream");
            imageList1.TransparentColor = Color.Transparent;
            imageList1.Images.SetKeyName(0, "Background.jpg");
            // 
            // RegisterLabel
            // 
            RegisterLabel.AutoSize = true;
            RegisterLabel.BackColor = Color.LavenderBlush;
            RegisterLabel.Font = new Font("Simplified Arabic Fixed", 9.75F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            RegisterLabel.Location = new Point(233, 275);
            RegisterLabel.Name = "RegisterLabel";
            RegisterLabel.Size = new Size(314, 18);
            RegisterLabel.TabIndex = 6;
            RegisterLabel.Text = "If you don't have an accout before";
            // 
            // RegisterBtn
            // 
            RegisterBtn.BackColor = Color.IndianRed;
            RegisterBtn.Font = new Font("Showcard Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            RegisterBtn.ForeColor = Color.Cornsilk;
            RegisterBtn.Location = new Point(295, 307);
            RegisterBtn.Name = "RegisterBtn";
            RegisterBtn.Size = new Size(79, 41);
            RegisterBtn.TabIndex = 7;
            RegisterBtn.Text = "Register";
            RegisterBtn.UseVisualStyleBackColor = false;
            RegisterBtn.Click += RegisterBtn_Click;
            // 
            // LoginBtn
            // 
            LoginBtn.BackColor = Color.IndianRed;
            LoginBtn.Font = new Font("Showcard Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            LoginBtn.ForeColor = Color.Cornsilk;
            LoginBtn.Location = new Point(380, 307);
            LoginBtn.Name = "LoginBtn";
            LoginBtn.Size = new Size(79, 41);
            LoginBtn.TabIndex = 8;
            LoginBtn.Text = "Login";
            LoginBtn.UseVisualStyleBackColor = false;
            LoginBtn.Click += LoginBtn_Click;
            // 
            // ShowPassChkBox
            // 
            ShowPassChkBox.AutoSize = true;
            ShowPassChkBox.BackColor = SystemColors.ControlLightLight;
            ShowPassChkBox.ForeColor = Color.Coral;
            ShowPassChkBox.Location = new Point(584, 195);
            ShowPassChkBox.Name = "ShowPassChkBox";
            ShowPassChkBox.Size = new Size(108, 19);
            ShowPassChkBox.TabIndex = 9;
            ShowPassChkBox.Text = "Show Password";
            ShowPassChkBox.UseVisualStyleBackColor = false;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(800, 450);
            Controls.Add(ShowPassChkBox);
            Controls.Add(LoginBtn);
            Controls.Add(RegisterBtn);
            Controls.Add(RegisterLabel);
            Controls.Add(PassTxtBox);
            Controls.Add(EmailTxtBox);
            Controls.Add(PassLabel);
            Controls.Add(EmailLabel);
            Name = "LoginForm";
            Text = "LoginForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label EmailLabel;
        private Label PassLabel;
        private TextBox EmailTxtBox;
        private TextBox PassTxtBox;
        private ImageList imageList1;
        private Label RegisterLabel;
        private Button RegisterBtn;
        private Button LoginBtn;
        private CheckBox ShowPassChkBox;
    }
}