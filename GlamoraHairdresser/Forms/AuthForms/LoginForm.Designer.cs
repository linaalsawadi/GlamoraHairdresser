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
            SuspendLayout();
            // 
            // EmailLabel
            // 
            EmailLabel.AutoSize = true;
            EmailLabel.Font = new Font("Simplified Arabic Fixed", 18F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            EmailLabel.Location = new Point(188, 105);
            EmailLabel.Name = "EmailLabel";
            EmailLabel.Size = new Size(87, 28);
            EmailLabel.TabIndex = 0;
            EmailLabel.Text = "Email";
            // 
            // PassLabel
            // 
            PassLabel.AutoSize = true;
            PassLabel.Font = new Font("Simplified Arabic Fixed", 18F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
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
            RegisterLabel.Font = new Font("Simplified Arabic Fixed", 9.75F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            RegisterLabel.Location = new Point(202, 275);
            RegisterLabel.Name = "RegisterLabel";
            RegisterLabel.Size = new Size(314, 18);
            RegisterLabel.TabIndex = 6;
            RegisterLabel.Text = "If you don't have an accout before";
            // 
            // RegisterBtn
            // 
            RegisterBtn.Location = new Point(321, 306);
            RegisterBtn.Name = "RegisterBtn";
            RegisterBtn.Size = new Size(79, 41);
            RegisterBtn.TabIndex = 7;
            RegisterBtn.Text = "Register";
            RegisterBtn.UseVisualStyleBackColor = true;
            // 
            // LoginBtn
            // 
            LoginBtn.Location = new Point(579, 138);
            LoginBtn.Name = "LoginBtn";
            LoginBtn.Size = new Size(73, 36);
            LoginBtn.TabIndex = 8;
            LoginBtn.Text = "Login";
            LoginBtn.UseVisualStyleBackColor = true;
            LoginBtn.Click += LoginBtn_Click;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
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
    }
}