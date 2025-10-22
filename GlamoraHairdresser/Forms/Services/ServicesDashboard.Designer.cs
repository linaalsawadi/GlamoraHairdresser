namespace GlamoraHairdresser.WinForms.Forms.Services
{
    partial class ServicesDashboard
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
            ServiceNameCmb = new ComboBox();
            SalonCombo = new ComboBox();
            AddBtn = new Button();
            UpdateBtn = new Button();
            DeleteBtn = new Button();
            button4 = new Button();
            ServicesGrid = new DataGridView();
            PriceTxt = new TextBox();
            DurationTxt = new TextBox();
            PriceLbl = new Label();
            DurationLbl = new Label();
            ((System.ComponentModel.ISupportInitialize)ServicesGrid).BeginInit();
            SuspendLayout();
            // 
            // ServiceNameCmb
            // 
            ServiceNameCmb.FormattingEnabled = true;
            ServiceNameCmb.Location = new Point(478, 83);
            ServiceNameCmb.Name = "ServiceNameCmb";
            ServiceNameCmb.Size = new Size(121, 23);
            ServiceNameCmb.TabIndex = 0;
            // 
            // SalonCombo
            // 
            SalonCombo.FormattingEnabled = true;
            SalonCombo.Location = new Point(336, 83);
            SalonCombo.Name = "SalonCombo";
            SalonCombo.Size = new Size(121, 23);
            SalonCombo.TabIndex = 1;
            // 
            // AddBtn
            // 
            AddBtn.Font = new Font("Showcard Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            AddBtn.Location = new Point(24, 398);
            AddBtn.Name = "AddBtn";
            AddBtn.Size = new Size(87, 33);
            AddBtn.TabIndex = 2;
            AddBtn.Text = "Add";
            AddBtn.UseVisualStyleBackColor = true;
            // 
            // UpdateBtn
            // 
            UpdateBtn.Font = new Font("Showcard Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            UpdateBtn.Location = new Point(160, 398);
            UpdateBtn.Name = "UpdateBtn";
            UpdateBtn.Size = new Size(87, 33);
            UpdateBtn.TabIndex = 3;
            UpdateBtn.Text = "Update";
            UpdateBtn.UseVisualStyleBackColor = true;
            // 
            // DeleteBtn
            // 
            DeleteBtn.Font = new Font("Showcard Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            DeleteBtn.Location = new Point(160, 368);
            DeleteBtn.Name = "DeleteBtn";
            DeleteBtn.Size = new Size(79, 33);
            DeleteBtn.TabIndex = 4;
            DeleteBtn.Text = "Delete";
            DeleteBtn.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            button4.Location = new Point(414, 405);
            button4.Name = "button4";
            button4.Size = new Size(75, 23);
            button4.TabIndex = 5;
            button4.Text = "button4";
            button4.UseVisualStyleBackColor = true;
            // 
            // ServicesGrid
            // 
            ServicesGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            ServicesGrid.Location = new Point(12, 12);
            ServicesGrid.Name = "ServicesGrid";
            ServicesGrid.Size = new Size(307, 367);
            ServicesGrid.TabIndex = 6;
            // 
            // PriceTxt
            // 
            PriceTxt.Location = new Point(336, 208);
            PriceTxt.Multiline = true;
            PriceTxt.Name = "PriceTxt";
            PriceTxt.Size = new Size(121, 71);
            PriceTxt.TabIndex = 7;
            // 
            // DurationTxt
            // 
            DurationTxt.Location = new Point(478, 208);
            DurationTxt.Multiline = true;
            DurationTxt.Name = "DurationTxt";
            DurationTxt.Size = new Size(121, 71);
            DurationTxt.TabIndex = 8;
            // 
            // PriceLbl
            // 
            PriceLbl.AutoSize = true;
            PriceLbl.Font = new Font("Showcard Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            PriceLbl.Location = new Point(366, 185);
            PriceLbl.Name = "PriceLbl";
            PriceLbl.Size = new Size(56, 20);
            PriceLbl.TabIndex = 9;
            PriceLbl.Text = "Price";
            // 
            // DurationLbl
            // 
            DurationLbl.AutoSize = true;
            DurationLbl.Font = new Font("Showcard Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            DurationLbl.Location = new Point(494, 185);
            DurationLbl.Name = "DurationLbl";
            DurationLbl.Size = new Size(92, 20);
            DurationLbl.TabIndex = 10;
            DurationLbl.Text = "Duration";
            // 
            // ServicesDashboard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(DurationLbl);
            Controls.Add(PriceLbl);
            Controls.Add(DurationTxt);
            Controls.Add(PriceTxt);
            Controls.Add(ServicesGrid);
            Controls.Add(button4);
            Controls.Add(DeleteBtn);
            Controls.Add(UpdateBtn);
            Controls.Add(AddBtn);
            Controls.Add(SalonCombo);
            Controls.Add(ServiceNameCmb);
            Name = "ServicesDashboard";
            Text = "ServicesDashboard";
            Load += ServicesDashboard_Load;
            ((System.ComponentModel.ISupportInitialize)ServicesGrid).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox ServiceNameCmb;
        private ComboBox SalonCombo;
        private Button AddBtn;
        private Button UpdateBtn;
        private Button DeleteBtn;
        private Button button4;
        private DataGridView ServicesGrid;
        private TextBox PriceTxt;
        private TextBox DurationTxt;
        private Label PriceLbl;
        private Label DurationLbl;
    }
}