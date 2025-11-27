namespace GlamoraHairdresser.WinForms.Forms.AdminForms
{
    partial class CustomerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomerForm));
            txtId = new TextBox();
            txtName = new TextBox();
            txtEmail = new TextBox();
            txtPhone = new TextBox();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            btnRefresh = new Button();
            btnClose = new Button();
            label1 = new Label();
            Name = new Label();
            label3 = new Label();
            label4 = new Label();
            dgvCustomers = new DataGridView();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvCustomers).BeginInit();
            SuspendLayout();
            // 
            // txtId
            // 
            txtId.Location = new Point(620, 163);
            txtId.Multiline = true;
            txtId.Name = "txtId";
            txtId.Size = new Size(122, 35);
            txtId.TabIndex = 0;
            // 
            // txtName
            // 
            txtName.Location = new Point(620, 237);
            txtName.Multiline = true;
            txtName.Name = "txtName";
            txtName.Size = new Size(122, 35);
            txtName.TabIndex = 1;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(620, 316);
            txtEmail.Multiline = true;
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(122, 35);
            txtEmail.TabIndex = 2;
            // 
            // txtPhone
            // 
            txtPhone.Location = new Point(620, 394);
            txtPhone.Multiline = true;
            txtPhone.Name = "txtPhone";
            txtPhone.Size = new Size(122, 35);
            txtPhone.TabIndex = 3;
            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.RosyBrown;
            btnAdd.Font = new Font("Showcard Gothic", 9F);
            btnAdd.ForeColor = Color.Cornsilk;
            btnAdd.Location = new Point(2, 394);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(94, 44);
            btnAdd.TabIndex = 0;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = false;
            // 
            // btnUpdate
            // 
            btnUpdate.BackColor = Color.RosyBrown;
            btnUpdate.Font = new Font("Showcard Gothic", 9F);
            btnUpdate.ForeColor = Color.Cornsilk;
            btnUpdate.Location = new Point(120, 394);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(94, 44);
            btnUpdate.TabIndex = 4;
            btnUpdate.Text = "Update";
            btnUpdate.UseVisualStyleBackColor = false;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.RosyBrown;
            btnDelete.Font = new Font("Showcard Gothic", 9F);
            btnDelete.ForeColor = Color.Cornsilk;
            btnDelete.Location = new Point(240, 394);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(94, 44);
            btnDelete.TabIndex = 5;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = false;
            // 
            // btnRefresh
            // 
            btnRefresh.BackColor = Color.RosyBrown;
            btnRefresh.Font = new Font("Showcard Gothic", 9F);
            btnRefresh.ForeColor = Color.Cornsilk;
            btnRefresh.Location = new Point(352, 394);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(94, 44);
            btnRefresh.TabIndex = 6;
            btnRefresh.Text = "Refresh";
            btnRefresh.UseVisualStyleBackColor = false;
            // 
            // btnClose
            // 
            btnClose.BackColor = Color.RosyBrown;
            btnClose.Font = new Font("Showcard Gothic", 9F);
            btnClose.ForeColor = Color.Cornsilk;
            btnClose.Location = new Point(464, 394);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(94, 44);
            btnClose.TabIndex = 7;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            label1.BackColor = Color.IndianRed;
            label1.Enabled = false;
            label1.Font = new Font("Showcard Gothic", 9F);
            label1.ForeColor = Color.Cornsilk;
            label1.Location = new Point(620, 137);
            label1.Name = "label1";
            label1.Size = new Size(122, 23);
            label1.TabIndex = 8;
            label1.Text = "Customer ID";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Name
            // 
            Name.BackColor = Color.IndianRed;
            Name.Enabled = false;
            Name.Font = new Font("Showcard Gothic", 9F);
            Name.ForeColor = Color.Cornsilk;
            Name.Location = new Point(620, 211);
            Name.Name = "Name";
            Name.Size = new Size(122, 23);
            Name.TabIndex = 9;
            Name.Text = "Customer Name";
            Name.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            label3.BackColor = Color.IndianRed;
            label3.Enabled = false;
            label3.Font = new Font("Showcard Gothic", 9F);
            label3.ForeColor = Color.Cornsilk;
            label3.Location = new Point(620, 290);
            label3.Name = "label3";
            label3.Size = new Size(122, 23);
            label3.TabIndex = 10;
            label3.Text = "Customer Email";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            label4.BackColor = Color.IndianRed;
            label4.Enabled = false;
            label4.Font = new Font("Showcard Gothic", 9F);
            label4.ForeColor = Color.Cornsilk;
            label4.Location = new Point(620, 368);
            label4.Name = "label4";
            label4.Size = new Size(122, 23);
            label4.TabIndex = 11;
            label4.Text = "Customer Phone";
            label4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // dgvCustomers
            // 
            dgvCustomers.BackgroundColor = Color.FromArgb(255, 192, 192);
            dgvCustomers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCustomers.Location = new Point(2, 2);
            dgvCustomers.Name = "dgvCustomers";
            dgvCustomers.Size = new Size(556, 386);
            dgvCustomers.TabIndex = 12;
            // 
            // label2
            // 
            label2.BackColor = Color.IndianRed;
            label2.Enabled = false;
            label2.Font = new Font("Showcard Gothic", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.Cornsilk;
            label2.Location = new Point(564, 21);
            label2.Name = "label2";
            label2.Size = new Size(224, 90);
            label2.TabIndex = 13;
            label2.Text = "Customers";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // CustomerForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(800, 450);
            Controls.Add(label2);
            Controls.Add(dgvCustomers);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(Name);
            Controls.Add(label1);
            Controls.Add(btnClose);
            Controls.Add(btnRefresh);
            Controls.Add(btnDelete);
            Controls.Add(btnUpdate);
            Controls.Add(btnAdd);
            Controls.Add(txtPhone);
            Controls.Add(txtEmail);
            Controls.Add(txtName);
            Controls.Add(txtId);
            Text = "CustomerForm";
            ((System.ComponentModel.ISupportInitialize)dgvCustomers).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtId;
        private TextBox txtName;
        private TextBox txtEmail;
        private TextBox txtPhone;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnRefresh;
        private Button btnClose;
        private Label label1;
        private Label Name;
        private Label label3;
        private Label label4;
        private DataGridView dgvCustomers;
        private Label label2;
    }
}