namespace GlamoraHairdresser.WinForms.Forms.Services
{
    partial class ServicesDashboard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.ComboBox SalonCombo;
        private System.Windows.Forms.ComboBox ServiceNameCmb;
        private System.Windows.Forms.TextBox PriceTxt;
        private System.Windows.Forms.TextBox DurationTxt;
        private System.Windows.Forms.DataGridView ServicesGrid;
        private System.Windows.Forms.Button AddBtn;
        private System.Windows.Forms.Button UpdateBtn;
        private System.Windows.Forms.Button DeleteBtn;
        private System.Windows.Forms.Button CleanBtn;
        private System.Windows.Forms.Label lblSalon;
        private System.Windows.Forms.Label lblService;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.Label lblDuration;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServicesDashboard));
            SalonCombo = new ComboBox();
            ServiceNameCmb = new ComboBox();
            PriceTxt = new TextBox();
            DurationTxt = new TextBox();
            ServicesGrid = new DataGridView();
            AddBtn = new Button();
            UpdateBtn = new Button();
            DeleteBtn = new Button();
            CleanBtn = new Button();
            lblSalon = new Label();
            lblService = new Label();
            lblPrice = new Label();
            lblDuration = new Label();
            ServicesLbl = new Label();
            ((System.ComponentModel.ISupportInitialize)ServicesGrid).BeginInit();
            SuspendLayout();
            // 
            // SalonCombo
            // 
            SalonCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            SalonCombo.Location = new Point(697, 197);
            SalonCombo.Name = "SalonCombo";
            SalonCombo.Size = new Size(300, 23);
            SalonCombo.TabIndex = 4;
            // 
            // ServiceNameCmb
            // 
            ServiceNameCmb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            ServiceNameCmb.AutoCompleteSource = AutoCompleteSource.ListItems;
            ServiceNameCmb.Location = new Point(697, 259);
            ServiceNameCmb.Name = "ServiceNameCmb";
            ServiceNameCmb.Size = new Size(300, 23);
            ServiceNameCmb.TabIndex = 5;
            // 
            // PriceTxt
            // 
            PriceTxt.Location = new Point(697, 325);
            PriceTxt.Name = "PriceTxt";
            PriceTxt.PlaceholderText = "e.g. 250";
            PriceTxt.Size = new Size(300, 23);
            PriceTxt.TabIndex = 6;
            // 
            // DurationTxt
            // 
            DurationTxt.Location = new Point(697, 395);
            DurationTxt.Name = "DurationTxt";
            DurationTxt.PlaceholderText = "e.g. 30";
            DurationTxt.Size = new Size(300, 23);
            DurationTxt.TabIndex = 7;
            // 
            // ServicesGrid
            // 
            ServicesGrid.AllowUserToAddRows = false;
            ServicesGrid.AllowUserToDeleteRows = false;
            ServicesGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            ServicesGrid.BackgroundColor = Color.FromArgb(255, 192, 192);
            ServicesGrid.Location = new Point(1, 0);
            ServicesGrid.MultiSelect = false;
            ServicesGrid.Name = "ServicesGrid";
            ServicesGrid.ReadOnly = true;
            ServicesGrid.RowHeadersVisible = false;
            ServicesGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            ServicesGrid.Size = new Size(520, 649);
            ServicesGrid.TabIndex = 12;
            // 
            // AddBtn
            // 
            AddBtn.BackColor = Color.RosyBrown;
            AddBtn.Font = new Font("Showcard Gothic", 9F);
            AddBtn.Location = new Point(530, 530);
            AddBtn.Name = "AddBtn";
            AddBtn.Size = new Size(90, 49);
            AddBtn.TabIndex = 8;
            AddBtn.Text = "Add";
            AddBtn.UseVisualStyleBackColor = false;
            // 
            // UpdateBtn
            // 
            UpdateBtn.BackColor = Color.RosyBrown;
            UpdateBtn.Font = new Font("Showcard Gothic", 9F);
            UpdateBtn.Location = new Point(652, 530);
            UpdateBtn.Name = "UpdateBtn";
            UpdateBtn.Size = new Size(90, 49);
            UpdateBtn.TabIndex = 9;
            UpdateBtn.Text = "Update";
            UpdateBtn.UseVisualStyleBackColor = false;
            // 
            // DeleteBtn
            // 
            DeleteBtn.BackColor = Color.RosyBrown;
            DeleteBtn.Font = new Font("Showcard Gothic", 9F);
            DeleteBtn.Location = new Point(775, 530);
            DeleteBtn.Name = "DeleteBtn";
            DeleteBtn.Size = new Size(90, 49);
            DeleteBtn.TabIndex = 10;
            DeleteBtn.Text = "Delete";
            DeleteBtn.UseVisualStyleBackColor = false;
            // 
            // CleanBtn
            // 
            CleanBtn.BackColor = Color.RosyBrown;
            CleanBtn.Font = new Font("Showcard Gothic", 9F);
            CleanBtn.Location = new Point(897, 530);
            CleanBtn.Name = "CleanBtn";
            CleanBtn.Size = new Size(100, 49);
            CleanBtn.TabIndex = 11;
            CleanBtn.Text = "Clear";
            CleanBtn.UseVisualStyleBackColor = false;
            // 
            // lblSalon
            // 
            lblSalon.AutoSize = true;
            lblSalon.BackColor = Color.IndianRed;
            lblSalon.Font = new Font("Showcard Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblSalon.ForeColor = Color.Cornsilk;
            lblSalon.Location = new Point(527, 200);
            lblSalon.Name = "lblSalon";
            lblSalon.Size = new Size(64, 20);
            lblSalon.TabIndex = 0;
            lblSalon.Text = "Salon:";
            // 
            // lblService
            // 
            lblService.AutoSize = true;
            lblService.BackColor = Color.IndianRed;
            lblService.Font = new Font("Showcard Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblService.ForeColor = Color.Cornsilk;
            lblService.Location = new Point(527, 262);
            lblService.Name = "lblService";
            lblService.Size = new Size(79, 20);
            lblService.TabIndex = 1;
            lblService.Text = "Service:";
            // 
            // lblPrice
            // 
            lblPrice.AutoSize = true;
            lblPrice.BackColor = Color.IndianRed;
            lblPrice.Font = new Font("Showcard Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblPrice.ForeColor = Color.Cornsilk;
            lblPrice.Location = new Point(530, 328);
            lblPrice.Name = "lblPrice";
            lblPrice.Size = new Size(61, 20);
            lblPrice.TabIndex = 2;
            lblPrice.Text = "Price:";
            // 
            // lblDuration
            // 
            lblDuration.AutoSize = true;
            lblDuration.BackColor = Color.IndianRed;
            lblDuration.Font = new Font("Showcard Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblDuration.ForeColor = Color.Cornsilk;
            lblDuration.Location = new Point(530, 398);
            lblDuration.Name = "lblDuration";
            lblDuration.Size = new Size(153, 20);
            lblDuration.TabIndex = 3;
            lblDuration.Text = "Duration (mins):";
            // 
            // ServicesLbl
            // 
            ServicesLbl.AutoSize = true;
            ServicesLbl.BackColor = Color.IndianRed;
            ServicesLbl.Font = new Font("Showcard Gothic", 48F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ServicesLbl.ForeColor = Color.Cornsilk;
            ServicesLbl.Location = new Point(607, 25);
            ServicesLbl.Name = "ServicesLbl";
            ServicesLbl.Size = new Size(319, 79);
            ServicesLbl.TabIndex = 13;
            ServicesLbl.Text = "Services";
            // 
            // ServicesDashboard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(1000, 650);
            Controls.Add(ServicesLbl);
            Controls.Add(lblSalon);
            Controls.Add(lblService);
            Controls.Add(lblPrice);
            Controls.Add(lblDuration);
            Controls.Add(SalonCombo);
            Controls.Add(ServiceNameCmb);
            Controls.Add(PriceTxt);
            Controls.Add(DurationTxt);
            Controls.Add(AddBtn);
            Controls.Add(UpdateBtn);
            Controls.Add(DeleteBtn);
            Controls.Add(CleanBtn);
            Controls.Add(ServicesGrid);
            MinimumSize = new Size(900, 600);
            Name = "ServicesDashboard";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Services Dashboard";
            ((System.ComponentModel.ISupportInitialize)ServicesGrid).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label ServicesLbl;
    }
}
