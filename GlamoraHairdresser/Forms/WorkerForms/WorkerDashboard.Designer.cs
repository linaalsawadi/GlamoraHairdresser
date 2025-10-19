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
            ((System.ComponentModel.ISupportInitialize)DataGridViewWorker).BeginInit();
            SuspendLayout();
            // 
            // WorkerLbl
            // 
            WorkerLbl.AutoSize = true;
            WorkerLbl.Font = new Font("Showcard Gothic", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            WorkerLbl.Location = new Point(625, 9);
            WorkerLbl.Name = "WorkerLbl";
            WorkerLbl.Size = new Size(147, 33);
            WorkerLbl.TabIndex = 28;
            WorkerLbl.Text = "Workers";
            // 
            // WorkerSalonLbl
            // 
            WorkerSalonLbl.AutoSize = true;
            WorkerSalonLbl.Font = new Font("Segoe UI", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            WorkerSalonLbl.Location = new Point(652, 372);
            WorkerSalonLbl.Name = "WorkerSalonLbl";
            WorkerSalonLbl.Size = new Size(96, 21);
            WorkerSalonLbl.TabIndex = 27;
            WorkerSalonLbl.Text = "Salon Name";
            WorkerSalonLbl.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // WorkerEmailLbl
            // 
            WorkerEmailLbl.AutoSize = true;
            WorkerEmailLbl.Font = new Font("Segoe UI", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            WorkerEmailLbl.Location = new Point(650, 154);
            WorkerEmailLbl.Name = "WorkerEmailLbl";
            WorkerEmailLbl.Size = new Size(105, 21);
            WorkerEmailLbl.TabIndex = 26;
            WorkerEmailLbl.Text = "Worker Email";
            // 
            // WorkerNameLbl
            // 
            WorkerNameLbl.AutoSize = true;
            WorkerNameLbl.Font = new Font("Segoe UI", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            WorkerNameLbl.Location = new Point(650, 103);
            WorkerNameLbl.Name = "WorkerNameLbl";
            WorkerNameLbl.Size = new Size(109, 21);
            WorkerNameLbl.TabIndex = 25;
            WorkerNameLbl.Text = "Worker Name";
            // 
            // IdLbl
            // 
            IdLbl.AutoSize = true;
            IdLbl.Font = new Font("Segoe UI", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            IdLbl.Location = new Point(692, 52);
            IdLbl.Name = "IdLbl";
            IdLbl.Size = new Size(23, 21);
            IdLbl.TabIndex = 24;
            IdLbl.Text = "Id";
            IdLbl.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // WorkerSalonTxtBox
            // 
            WorkerSalonTxtBox.Location = new Point(625, 396);
            WorkerSalonTxtBox.Name = "WorkerSalonTxtBox";
            WorkerSalonTxtBox.Size = new Size(147, 23);
            WorkerSalonTxtBox.TabIndex = 23;
            // 
            // WorkerEmailTxtBox
            // 
            WorkerEmailTxtBox.Location = new Point(625, 178);
            WorkerEmailTxtBox.Name = "WorkerEmailTxtBox";
            WorkerEmailTxtBox.Size = new Size(147, 23);
            WorkerEmailTxtBox.TabIndex = 22;
            // 
            // WorkerNameTxtBox
            // 
            WorkerNameTxtBox.Location = new Point(625, 127);
            WorkerNameTxtBox.Name = "WorkerNameTxtBox";
            WorkerNameTxtBox.Size = new Size(147, 23);
            WorkerNameTxtBox.TabIndex = 21;
            // 
            // WorkerIdTxtBox
            // 
            WorkerIdTxtBox.Location = new Point(625, 76);
            WorkerIdTxtBox.Name = "WorkerIdTxtBox";
            WorkerIdTxtBox.Size = new Size(147, 23);
            WorkerIdTxtBox.TabIndex = 20;
            // 
            // ClearBtn
            // 
            ClearBtn.Location = new Point(503, 396);
            ClearBtn.Name = "ClearBtn";
            ClearBtn.Size = new Size(82, 42);
            ClearBtn.TabIndex = 19;
            ClearBtn.Text = "Clear";
            ClearBtn.UseVisualStyleBackColor = true;
            ClearBtn.Click += ClearBtn_Click;
            // 
            // DeleteBtn
            // 
            DeleteBtn.Location = new Point(342, 396);
            DeleteBtn.Name = "DeleteBtn";
            DeleteBtn.Size = new Size(82, 42);
            DeleteBtn.TabIndex = 18;
            DeleteBtn.Text = "Delete";
            DeleteBtn.UseVisualStyleBackColor = true;
            DeleteBtn.Click += DeleteBtn_Click;
            // 
            // UpdateBtn
            // 
            UpdateBtn.Location = new Point(188, 396);
            UpdateBtn.Name = "UpdateBtn";
            UpdateBtn.Size = new Size(82, 42);
            UpdateBtn.TabIndex = 17;
            UpdateBtn.Text = "Update";
            UpdateBtn.UseVisualStyleBackColor = true;
            UpdateBtn.Click += UpdateBtn_Click;
            // 
            // AddBtn
            // 
            AddBtn.Location = new Point(25, 396);
            AddBtn.Name = "AddBtn";
            AddBtn.Size = new Size(82, 42);
            AddBtn.TabIndex = 16;
            AddBtn.Text = "Add";
            AddBtn.UseVisualStyleBackColor = true;
            AddBtn.Click += AddBtn_Click;
            // 
            // DataGridViewWorker
            // 
            DataGridViewWorker.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGridViewWorker.Location = new Point(25, 12);
            DataGridViewWorker.Name = "DataGridViewWorker";
            DataGridViewWorker.Size = new Size(560, 378);
            DataGridViewWorker.TabIndex = 15;
            // 
            // WorkerSkillsLbl
            // 
            WorkerSkillsLbl.AutoSize = true;
            WorkerSkillsLbl.Font = new Font("Segoe UI", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            WorkerSkillsLbl.Location = new Point(652, 210);
            WorkerSkillsLbl.Name = "WorkerSkillsLbl";
            WorkerSkillsLbl.Size = new Size(100, 21);
            WorkerSkillsLbl.TabIndex = 30;
            WorkerSkillsLbl.Text = "Worker Skills";
            WorkerSkillsLbl.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Skillsclb
            // 
            Skillsclb.CheckOnClick = true;
            Skillsclb.FormattingEnabled = true;
            Skillsclb.Location = new Point(625, 234);
            Skillsclb.Name = "Skillsclb";
            Skillsclb.Size = new Size(147, 130);
            Skillsclb.TabIndex = 32;
            Skillsclb.SelectedIndexChanged += Skillsclb_SelectedIndexChanged;
            // 
            // WorkerDashboard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
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
    }
}