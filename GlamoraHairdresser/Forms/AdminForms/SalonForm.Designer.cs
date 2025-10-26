namespace GlamoraHairdresser.WinForms.Forms.SalonForms
{
    partial class SalonForm
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
            dataGridViewSalon = new DataGridView();
            AddBtn = new Button();
            UpdateBtn = new Button();
            DeleteBtn = new Button();
            ClearBtn = new Button();
            SalonIdTxtBox = new TextBox();
            SalonNameTxtBox = new TextBox();
            AddressTxtBox = new TextBox();
            SalonPhoneNumTxtBox = new TextBox();
            IdLbl = new Label();
            SalonNameLbl = new Label();
            SalonAddressLbl = new Label();
            SalonPhoneNumLbl = new Label();
            SalonLbl = new Label();
            WorkingHoursBtn = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewSalon).BeginInit();
            SuspendLayout();
            // 
            // dataGridViewSalon
            // 
            dataGridViewSalon.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewSalon.Location = new Point(21, 12);
            dataGridViewSalon.Name = "dataGridViewSalon";
            dataGridViewSalon.Size = new Size(560, 378);
            dataGridViewSalon.TabIndex = 0;
            // 
            // AddBtn
            // 
            AddBtn.Font = new Font("Showcard Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            AddBtn.Location = new Point(21, 396);
            AddBtn.Name = "AddBtn";
            AddBtn.Size = new Size(82, 42);
            AddBtn.TabIndex = 1;
            AddBtn.Text = "Add";
            AddBtn.UseVisualStyleBackColor = true;
            AddBtn.Click += AddBtn_Click;
            // 
            // UpdateBtn
            // 
            UpdateBtn.Font = new Font("Showcard Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            UpdateBtn.Location = new Point(184, 396);
            UpdateBtn.Name = "UpdateBtn";
            UpdateBtn.Size = new Size(82, 42);
            UpdateBtn.TabIndex = 2;
            UpdateBtn.Text = "Update";
            UpdateBtn.UseVisualStyleBackColor = true;
            UpdateBtn.Click += UpdateBtn_Click;
            // 
            // DeleteBtn
            // 
            DeleteBtn.Font = new Font("Showcard Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            DeleteBtn.Location = new Point(338, 396);
            DeleteBtn.Name = "DeleteBtn";
            DeleteBtn.Size = new Size(82, 42);
            DeleteBtn.TabIndex = 3;
            DeleteBtn.Text = "Delete";
            DeleteBtn.UseVisualStyleBackColor = true;
            DeleteBtn.Click += DeleteBtn_Click;
            // 
            // ClearBtn
            // 
            ClearBtn.Font = new Font("Showcard Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ClearBtn.Location = new Point(499, 396);
            ClearBtn.Name = "ClearBtn";
            ClearBtn.Size = new Size(82, 42);
            ClearBtn.TabIndex = 4;
            ClearBtn.Text = "Clear";
            ClearBtn.UseVisualStyleBackColor = true;
            ClearBtn.Click += ClearBtn_Click;
            // 
            // SalonIdTxtBox
            // 
            SalonIdTxtBox.Location = new Point(616, 105);
            SalonIdTxtBox.Name = "SalonIdTxtBox";
            SalonIdTxtBox.Size = new Size(136, 23);
            SalonIdTxtBox.TabIndex = 5;
            // 
            // SalonNameTxtBox
            // 
            SalonNameTxtBox.Location = new Point(616, 174);
            SalonNameTxtBox.Name = "SalonNameTxtBox";
            SalonNameTxtBox.Size = new Size(136, 23);
            SalonNameTxtBox.TabIndex = 6;
            // 
            // AddressTxtBox
            // 
            AddressTxtBox.Location = new Point(616, 246);
            AddressTxtBox.Name = "AddressTxtBox";
            AddressTxtBox.Size = new Size(136, 23);
            AddressTxtBox.TabIndex = 7;
            // 
            // SalonPhoneNumTxtBox
            // 
            SalonPhoneNumTxtBox.Location = new Point(616, 324);
            SalonPhoneNumTxtBox.Name = "SalonPhoneNumTxtBox";
            SalonPhoneNumTxtBox.Size = new Size(136, 23);
            SalonPhoneNumTxtBox.TabIndex = 8;
            // 
            // IdLbl
            // 
            IdLbl.AutoSize = true;
            IdLbl.Font = new Font("Showcard Gothic", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            IdLbl.Location = new Point(673, 81);
            IdLbl.Name = "IdLbl";
            IdLbl.Size = new Size(27, 20);
            IdLbl.TabIndex = 10;
            IdLbl.Text = "Id";
            IdLbl.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // SalonNameLbl
            // 
            SalonNameLbl.AutoSize = true;
            SalonNameLbl.Font = new Font("Showcard Gothic", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            SalonNameLbl.Location = new Point(630, 150);
            SalonNameLbl.Name = "SalonNameLbl";
            SalonNameLbl.Size = new Size(107, 20);
            SalonNameLbl.TabIndex = 11;
            SalonNameLbl.Text = "Salon Name";
            // 
            // SalonAddressLbl
            // 
            SalonAddressLbl.AutoSize = true;
            SalonAddressLbl.Font = new Font("Showcard Gothic", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            SalonAddressLbl.Location = new Point(646, 222);
            SalonAddressLbl.Name = "SalonAddressLbl";
            SalonAddressLbl.Size = new Size(81, 20);
            SalonAddressLbl.TabIndex = 12;
            SalonAddressLbl.Text = "Address";
            // 
            // SalonPhoneNumLbl
            // 
            SalonPhoneNumLbl.AutoSize = true;
            SalonPhoneNumLbl.Font = new Font("Showcard Gothic", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            SalonPhoneNumLbl.Location = new Point(605, 300);
            SalonPhoneNumLbl.Name = "SalonPhoneNumLbl";
            SalonPhoneNumLbl.Size = new Size(157, 20);
            SalonPhoneNumLbl.TabIndex = 13;
            SalonPhoneNumLbl.Text = "Salon Phone Num";
            SalonPhoneNumLbl.TextAlign = ContentAlignment.MiddleCenter;
            SalonPhoneNumLbl.Click += SalonPhoneNumLbl_Click;
            // 
            // SalonLbl
            // 
            SalonLbl.AutoSize = true;
            SalonLbl.Font = new Font("Showcard Gothic", 36F, FontStyle.Regular, GraphicsUnit.Point, 0);
            SalonLbl.Location = new Point(594, 21);
            SalonLbl.Name = "SalonLbl";
            SalonLbl.Size = new Size(194, 60);
            SalonLbl.TabIndex = 14;
            SalonLbl.Text = "Salons";
            // 
            // WorkingHoursBtn
            // 
            WorkingHoursBtn.Font = new Font("Showcard Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            WorkingHoursBtn.Location = new Point(614, 390);
            WorkingHoursBtn.Name = "WorkingHoursBtn";
            WorkingHoursBtn.Size = new Size(138, 54);
            WorkingHoursBtn.TabIndex = 15;
            WorkingHoursBtn.Text = "Working Hours";
            WorkingHoursBtn.UseVisualStyleBackColor = true;
            WorkingHoursBtn.Click += WorkingHoursBtn_Click;
            // 
            // SalonForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(WorkingHoursBtn);
            Controls.Add(SalonLbl);
            Controls.Add(SalonPhoneNumLbl);
            Controls.Add(SalonAddressLbl);
            Controls.Add(SalonNameLbl);
            Controls.Add(IdLbl);
            Controls.Add(SalonPhoneNumTxtBox);
            Controls.Add(AddressTxtBox);
            Controls.Add(SalonNameTxtBox);
            Controls.Add(SalonIdTxtBox);
            Controls.Add(ClearBtn);
            Controls.Add(DeleteBtn);
            Controls.Add(UpdateBtn);
            Controls.Add(AddBtn);
            Controls.Add(dataGridViewSalon);
            Name = "SalonForm";
            Text = "SalonForm";
            Load += SalonForm_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridViewSalon).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridViewSalon;
        private Button AddBtn;
        private Button UpdateBtn;
        private Button DeleteBtn;
        private Button ClearBtn;
        private TextBox SalonIdTxtBox;
        private TextBox SalonNameTxtBox;
        private TextBox AddressTxtBox;
        private TextBox SalonPhoneNumTxtBox;
        private TextBox textBox5;
        private Label IdLbl;
        private Label SalonNameLbl;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label SalonAddressLbl;
        private Label SalonPhoneNumLbl;
        private Label SalonLbl;
        private Button WorkingHoursBtn;
    }
}