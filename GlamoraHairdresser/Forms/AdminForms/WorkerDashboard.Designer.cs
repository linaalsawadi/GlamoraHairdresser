namespace GlamoraHairdresser.WinForms.Forms.WorkerForms
{
    partial class WorkerDashboard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WorkerDashboard));
            WorkerLbl = new Label();
            WorkerSalonLbl = new Label();
            WorkerEmailLbl = new Label();
            WorkerNameLbl = new Label();
            IdLbl = new Label();
            WorkerSalonTxtBox = new TextBox();
            WorkerEmailTxtBox = new TextBox();
            WorkerNameTxtBox = new TextBox();
            WorkerIdTxtBox = new TextBox();
            ClearBtn = new Button();
            DeleteBtn = new Button();
            UpdateBtn = new Button();
            AddBtn = new Button();
            DataGridViewWorker = new DataGridView();
            WorkerSkillsLbl = new Label();
            Skillsclb = new CheckedListBox();
            WorkerWorkingHoursBtn = new Button();
            label1 = new Label();
            PasstxtBox = new TextBox();
            label2 = new Label();
            WorkerPhoneTxtBox = new TextBox();
            ((System.ComponentModel.ISupportInitialize)DataGridViewWorker).BeginInit();
            SuspendLayout();
            // 
            // WorkerLbl
            // 
            WorkerLbl.AutoSize = true;
            WorkerLbl.BackColor = Color.IndianRed;
            WorkerLbl.Font = new Font("Showcard Gothic", 27.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            WorkerLbl.ForeColor = Color.Cornsilk;
            WorkerLbl.Location = new Point(612, 1);
            WorkerLbl.Name = "WorkerLbl";
            WorkerLbl.Size = new Size(201, 46);
            WorkerLbl.TabIndex = 28;
            WorkerLbl.Text = "Workers";
            // 
            // WorkerSalonLbl
            // 
            WorkerSalonLbl.AutoSize = true;
            WorkerSalonLbl.BackColor = Color.IndianRed;
            WorkerSalonLbl.Font = new Font("Showcard Gothic", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            WorkerSalonLbl.ForeColor = Color.Cornsilk;
            WorkerSalonLbl.Location = new Point(661, 199);
            WorkerSalonLbl.Name = "WorkerSalonLbl";
            WorkerSalonLbl.Size = new Size(107, 20);
            WorkerSalonLbl.TabIndex = 27;
            WorkerSalonLbl.Text = "Salon Name";
            WorkerSalonLbl.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // WorkerEmailLbl
            // 
            WorkerEmailLbl.AutoSize = true;
            WorkerEmailLbl.BackColor = Color.IndianRed;
            WorkerEmailLbl.Font = new Font("Showcard Gothic", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            WorkerEmailLbl.ForeColor = Color.Cornsilk;
            WorkerEmailLbl.Location = new Point(647, 150);
            WorkerEmailLbl.Name = "WorkerEmailLbl";
            WorkerEmailLbl.Size = new Size(132, 20);
            WorkerEmailLbl.TabIndex = 26;
            WorkerEmailLbl.Text = "Worker Email";
            // 
            // WorkerNameLbl
            // 
            WorkerNameLbl.AutoSize = true;
            WorkerNameLbl.BackColor = Color.IndianRed;
            WorkerNameLbl.Font = new Font("Showcard Gothic", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            WorkerNameLbl.ForeColor = Color.Cornsilk;
            WorkerNameLbl.Location = new Point(650, 101);
            WorkerNameLbl.Name = "WorkerNameLbl";
            WorkerNameLbl.Size = new Size(129, 20);
            WorkerNameLbl.TabIndex = 25;
            WorkerNameLbl.Text = "Worker Name";
            // 
            // IdLbl
            // 
            IdLbl.AutoSize = true;
            IdLbl.BackColor = Color.IndianRed;
            IdLbl.Font = new Font("Showcard Gothic", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            IdLbl.ForeColor = Color.Cornsilk;
            IdLbl.Location = new Point(699, 52);
            IdLbl.Name = "IdLbl";
            IdLbl.Size = new Size(27, 20);
            IdLbl.TabIndex = 24;
            IdLbl.Text = "Id";
            IdLbl.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // WorkerSalonTxtBox
            // 
            WorkerSalonTxtBox.Location = new Point(643, 223);
            WorkerSalonTxtBox.Name = "WorkerSalonTxtBox";
            WorkerSalonTxtBox.Size = new Size(147, 23);
            WorkerSalonTxtBox.TabIndex = 23;
            // 
            // WorkerEmailTxtBox
            // 
            WorkerEmailTxtBox.Location = new Point(643, 173);
            WorkerEmailTxtBox.Name = "WorkerEmailTxtBox";
            WorkerEmailTxtBox.Size = new Size(147, 23);
            WorkerEmailTxtBox.TabIndex = 22;
            // 
            // WorkerNameTxtBox
            // 
            WorkerNameTxtBox.Location = new Point(643, 124);
            WorkerNameTxtBox.Name = "WorkerNameTxtBox";
            WorkerNameTxtBox.Size = new Size(147, 23);
            WorkerNameTxtBox.TabIndex = 21;
            // 
            // WorkerIdTxtBox
            // 
            WorkerIdTxtBox.Location = new Point(643, 75);
            WorkerIdTxtBox.Name = "WorkerIdTxtBox";
            WorkerIdTxtBox.Size = new Size(147, 23);
            WorkerIdTxtBox.TabIndex = 20;
            // 
            // ClearBtn
            // 
            ClearBtn.BackColor = Color.RosyBrown;
            ClearBtn.Font = new Font("Showcard Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ClearBtn.ForeColor = Color.Cornsilk;
            ClearBtn.Location = new Point(351, 439);
            ClearBtn.Name = "ClearBtn";
            ClearBtn.Size = new Size(82, 42);
            ClearBtn.TabIndex = 19;
            ClearBtn.Text = "Clear";
            ClearBtn.UseVisualStyleBackColor = false;
            ClearBtn.Click += ClearBtn_Click;
            // 
            // DeleteBtn
            // 
            DeleteBtn.BackColor = Color.RosyBrown;
            DeleteBtn.Font = new Font("Showcard Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            DeleteBtn.ForeColor = Color.Cornsilk;
            DeleteBtn.Location = new Point(234, 439);
            DeleteBtn.Name = "DeleteBtn";
            DeleteBtn.Size = new Size(82, 42);
            DeleteBtn.TabIndex = 18;
            DeleteBtn.Text = "Delete";
            DeleteBtn.UseVisualStyleBackColor = false;
            DeleteBtn.Click += DeleteBtn_Click;
            // 
            // UpdateBtn
            // 
            UpdateBtn.BackColor = Color.RosyBrown;
            UpdateBtn.Font = new Font("Showcard Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            UpdateBtn.ForeColor = Color.Cornsilk;
            UpdateBtn.Location = new Point(122, 439);
            UpdateBtn.Name = "UpdateBtn";
            UpdateBtn.Size = new Size(82, 42);
            UpdateBtn.TabIndex = 17;
            UpdateBtn.Text = "Update";
            UpdateBtn.UseVisualStyleBackColor = false;
            UpdateBtn.Click += UpdateBtn_Click;
            // 
            // AddBtn
            // 
            AddBtn.BackColor = Color.RosyBrown;
            AddBtn.Font = new Font("Showcard Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            AddBtn.ForeColor = Color.Cornsilk;
            AddBtn.Location = new Point(13, 439);
            AddBtn.Name = "AddBtn";
            AddBtn.Size = new Size(82, 42);
            AddBtn.TabIndex = 16;
            AddBtn.Text = "Add";
            AddBtn.UseVisualStyleBackColor = false;
            AddBtn.Click += AddBtn_Click;
            // 
            // DataGridViewWorker
            // 
            DataGridViewWorker.BackgroundColor = Color.FromArgb(255, 192, 192);
            DataGridViewWorker.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGridViewWorker.Location = new Point(0, -2);
            DataGridViewWorker.Name = "DataGridViewWorker";
            DataGridViewWorker.Size = new Size(585, 435);
            DataGridViewWorker.TabIndex = 15;
            // 
            // WorkerSkillsLbl
            // 
            WorkerSkillsLbl.AutoSize = true;
            WorkerSkillsLbl.BackColor = Color.IndianRed;
            WorkerSkillsLbl.Font = new Font("Showcard Gothic", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            WorkerSkillsLbl.ForeColor = Color.Cornsilk;
            WorkerSkillsLbl.Location = new Point(643, 249);
            WorkerSkillsLbl.Name = "WorkerSkillsLbl";
            WorkerSkillsLbl.Size = new Size(136, 20);
            WorkerSkillsLbl.TabIndex = 30;
            WorkerSkillsLbl.Text = "Worker Skills";
            WorkerSkillsLbl.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Skillsclb
            // 
            Skillsclb.CheckOnClick = true;
            Skillsclb.FormattingEnabled = true;
            Skillsclb.Location = new Point(643, 272);
            Skillsclb.Name = "Skillsclb";
            Skillsclb.Size = new Size(147, 130);
            Skillsclb.TabIndex = 32;
            // 
            // WorkerWorkingHoursBtn
            // 
            WorkerWorkingHoursBtn.BackColor = Color.RosyBrown;
            WorkerWorkingHoursBtn.Font = new Font("Showcard Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            WorkerWorkingHoursBtn.ForeColor = Color.Cornsilk;
            WorkerWorkingHoursBtn.Location = new Point(466, 439);
            WorkerWorkingHoursBtn.Name = "WorkerWorkingHoursBtn";
            WorkerWorkingHoursBtn.Size = new Size(107, 42);
            WorkerWorkingHoursBtn.TabIndex = 33;
            WorkerWorkingHoursBtn.Text = "Working Hours";
            WorkerWorkingHoursBtn.UseVisualStyleBackColor = false;
            WorkerWorkingHoursBtn.Click += WorkerWorkingHoursBtn_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.IndianRed;
            label1.Font = new Font("Showcard Gothic", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Cornsilk;
            label1.Location = new Point(654, 406);
            label1.Name = "label1";
            label1.Size = new Size(123, 20);
            label1.TabIndex = 35;
            label1.Text = "Worker Pass";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // PasstxtBox
            // 
            PasstxtBox.Location = new Point(643, 429);
            PasstxtBox.Name = "PasstxtBox";
            PasstxtBox.Size = new Size(147, 23);
            PasstxtBox.TabIndex = 34;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.IndianRed;
            label2.Font = new Font("Showcard Gothic", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.Cornsilk;
            label2.Location = new Point(647, 455);
            label2.Name = "label2";
            label2.Size = new Size(139, 20);
            label2.TabIndex = 37;
            label2.Text = "Worker Phone";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // WorkerPhoneTxtBox
            // 
            WorkerPhoneTxtBox.Location = new Point(645, 478);
            WorkerPhoneTxtBox.Name = "WorkerPhoneTxtBox";
            WorkerPhoneTxtBox.Size = new Size(147, 23);
            WorkerPhoneTxtBox.TabIndex = 36;
            // 
            // WorkerDashboard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(836, 509);
            Controls.Add(label2);
            Controls.Add(WorkerPhoneTxtBox);
            Controls.Add(label1);
            Controls.Add(PasstxtBox);
            Controls.Add(WorkerWorkingHoursBtn);
            Controls.Add(Skillsclb);
            Controls.Add(WorkerSkillsLbl);
            Controls.Add(WorkerLbl);
            Controls.Add(WorkerSalonLbl);
            Controls.Add(WorkerEmailLbl);
            Controls.Add(WorkerNameLbl);
            Controls.Add(IdLbl);
            Controls.Add(WorkerSalonTxtBox);
            Controls.Add(WorkerEmailTxtBox);
            Controls.Add(WorkerNameTxtBox);
            Controls.Add(WorkerIdTxtBox);
            Controls.Add(ClearBtn);
            Controls.Add(DeleteBtn);
            Controls.Add(UpdateBtn);
            Controls.Add(AddBtn);
            Controls.Add(DataGridViewWorker);
            Name = "WorkerDashboard";
            Text = "WorkerDashboard";
            Load += WorkerDashboard_Load;
            ((System.ComponentModel.ISupportInitialize)DataGridViewWorker).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label WorkerLbl;
        private Label WorkerSalonLbl;
        private Label WorkerEmailLbl;
        private Label WorkerNameLbl;
        private Label IdLbl;
        private TextBox WorkerSalonTxtBox;
        private TextBox WorkerEmailTxtBox;
        private TextBox WorkerNameTxtBox;
        private TextBox WorkerIdTxtBox;
        private Button ClearBtn;
        private Button DeleteBtn;
        private Button UpdateBtn;
        private Button AddBtn;
        private DataGridView DataGridViewWorker;
        private Label WorkerSkillsLbl;
        private CheckedListBox Skillsclb;
        private Button WorkerWorkingHoursBtn;
        private Label label1;
        private TextBox PasstxtBox;
        private Label label2;
        private TextBox WorkerPhoneTxtBox;
    }
}