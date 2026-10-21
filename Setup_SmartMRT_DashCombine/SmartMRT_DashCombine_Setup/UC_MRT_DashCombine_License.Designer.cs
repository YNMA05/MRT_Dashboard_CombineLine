namespace SmartMRT_DashCombine_Setup
{
    partial class UC_MRT_DashCombine_License
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.dgvLicenseDetail = new System.Windows.Forms.DataGridView();
            this.panelLicenseDetail = new System.Windows.Forms.FlowLayoutPanel();
            this.btnCheckLicense = new SmartMRT_DashCombine_Setup.Controls.GlassButton();
            this.panelBottom = new SmartMRT_DashCombine_Setup.Controls.GlassPanel();
            this.label9 = new System.Windows.Forms.Label();
            this.btnInstallLicense = new SmartMRT_DashCombine_Setup.Controls.GlassButton();
            this.txtLicensePath = new SmartMRT_DashCombine_Setup.Controls.ModernTextBox();
            this.btnBrowseLicense = new SmartMRT_DashCombine_Setup.Controls.GlassButton();
            this.panelGenerateKey = new SmartMRT_DashCombine_Setup.Controls.GlassPanel();
            this.lblStatusResult = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblLicenseStatus = new System.Windows.Forms.Label();
            this.btnGenerateKey = new SmartMRT_DashCombine_Setup.Controls.GlassButton();
            this.glassDatePicker2 = new SmartMRT_DashCombine_Setup.Controls.GlassDatePicker(this.components);
            this.glassDatePicker1 = new SmartMRT_DashCombine_Setup.Controls.GlassDatePicker(this.components);
            this.txtMaxPage = new SmartMRT_DashCombine_Setup.Controls.ModernTextBox();
            this.txtMaxLine = new SmartMRT_DashCombine_Setup.Controls.ModernTextBox();
            this.txtHash = new SmartMRT_DashCombine_Setup.Controls.ModernTextBox();
            this.txtCompany = new SmartMRT_DashCombine_Setup.Controls.ModernTextBox();
            this.ddlDashGroup = new SmartMRT_DashCombine_Setup.Controls.ModernComboBox();
            this.glassPanelProduct = new SmartMRT_DashCombine_Setup.Controls.GlassPanel();
            this.dgvLicenseProduct = new System.Windows.Forms.DataGridView();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnTrial = new SmartMRT_DashCombine_Setup.Controls.GlassButton();
            this.btnFull = new SmartMRT_DashCombine_Setup.Controls.GlassButton();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCompany = new System.Windows.Forms.Label();
            this.lblLicenseDetail = new System.Windows.Forms.Label();
            this.glassPanelSignature = new SmartMRT_DashCombine_Setup.Controls.GlassPanel();
            this.lblLicenseAbout = new System.Windows.Forms.Label();
            this.modernTxtDays = new SmartMRT_DashCombine_Setup.Controls.ModernTextBox();
            this.lblRemainingDay = new System.Windows.Forms.Label();
            this.glassDatePicker3 = new SmartMRT_DashCombine_Setup.Controls.GlassDatePicker(this.components);
            this.label10 = new System.Windows.Forms.Label();
            this.lblLicenseSignature = new System.Windows.Forms.Label();
            this.modernTextSign = new SmartMRT_DashCombine_Setup.Controls.ModernTextBox();
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLicenseDetail)).BeginInit();
            this.panelLicenseDetail.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.panelGenerateKey.SuspendLayout();
            this.glassPanelProduct.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLicenseProduct)).BeginInit();
            this.glassPanelSignature.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Controls.Add(this.lblSubtitle);
            this.pnlHeader.Location = new System.Drawing.Point(3, 3);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1230, 81);
            this.pnlHeader.TabIndex = 110;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(14, 25);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(186, 32);
            this.lblTitle.TabIndex = 118;
            this.lblTitle.Text = "LICENSE SETUP";
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(170)))), ((int)(((byte)(190)))));
            this.lblSubtitle.Location = new System.Drawing.Point(17, 57);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(222, 15);
            this.lblSubtitle.TabIndex = 117;
            this.lblSubtitle.Text = "Configure  SmartMRT Dashboard License";
            // 
            // dgvLicenseDetail
            // 
            this.dgvLicenseDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLicenseDetail.Location = new System.Drawing.Point(3, 42);
            this.dgvLicenseDetail.Name = "dgvLicenseDetail";
            this.dgvLicenseDetail.RowHeadersWidth = 45;
            this.dgvLicenseDetail.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dgvLicenseDetail.Size = new System.Drawing.Size(624, 357);
            this.dgvLicenseDetail.TabIndex = 127;
            this.dgvLicenseDetail.Visible = false;
            this.dgvLicenseDetail.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLicenseDetail_CellContentClick);
            // 
            // panelLicenseDetail
            // 
            this.panelLicenseDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelLicenseDetail.AutoScroll = true;
            this.panelLicenseDetail.Controls.Add(this.btnCheckLicense);
            this.panelLicenseDetail.Controls.Add(this.dgvLicenseDetail);
            this.panelLicenseDetail.Location = new System.Drawing.Point(593, 251);
            this.panelLicenseDetail.Name = "panelLicenseDetail";
            this.panelLicenseDetail.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.panelLicenseDetail.Size = new System.Drawing.Size(640, 454);
            this.panelLicenseDetail.TabIndex = 130;
            this.panelLicenseDetail.Visible = false;
            // 
            // btnCheckLicense
            // 
            this.btnCheckLicense.BackColor = System.Drawing.Color.Transparent;
            this.btnCheckLicense.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(220)))), ((int)(((byte)(255)))));
            this.btnCheckLicense.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCheckLicense.FlatAppearance.BorderSize = 0;
            this.btnCheckLicense.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCheckLicense.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCheckLicense.ForeColor = System.Drawing.Color.White;
            this.btnCheckLicense.GlassColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(55)))), ((int)(((byte)(65)))));
            this.btnCheckLicense.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(85)))), ((int)(((byte)(95)))));
            this.btnCheckLicense.Location = new System.Drawing.Point(3, 13);
            this.btnCheckLicense.Name = "btnCheckLicense";
            this.btnCheckLicense.Selected = false;
            this.btnCheckLicense.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(140)))), ((int)(((byte)(190)))));
            this.btnCheckLicense.Size = new System.Drawing.Size(183, 23);
            this.btnCheckLicense.TabIndex = 128;
            this.btnCheckLicense.Text = "Show License Detail";
            this.btnCheckLicense.TextColor = System.Drawing.Color.White;
            this.btnCheckLicense.UseVisualStyleBackColor = false;
            this.btnCheckLicense.Click += new System.EventHandler(this.btnCheckLicense_Click);
            // 
            // panelBottom
            // 
            this.panelBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(36)))), ((int)(((byte)(71)))));
            this.panelBottom.BorderColor = System.Drawing.Color.Transparent;
            this.panelBottom.BorderThickness = 1.2F;
            this.panelBottom.Controls.Add(this.label9);
            this.panelBottom.Controls.Add(this.btnInstallLicense);
            this.panelBottom.Controls.Add(this.txtLicensePath);
            this.panelBottom.Controls.Add(this.btnBrowseLicense);
            this.panelBottom.CornerRadius = 18;
            this.panelBottom.GlassColor = System.Drawing.Color.Transparent;
            this.panelBottom.GlassOpacity = 180;
            this.panelBottom.Location = new System.Drawing.Point(596, 87);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Padding = new System.Windows.Forms.Padding(20);
            this.panelBottom.Size = new System.Drawing.Size(624, 156);
            this.panelBottom.TabIndex = 161;
            this.panelBottom.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Segoe Print", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(170)))), ((int)(((byte)(190)))));
            this.label9.Location = new System.Drawing.Point(13, 5);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(101, 28);
            this.label9.TabIndex = 188;
            this.label9.Text = "File Upload";
            // 
            // btnInstallLicense
            // 
            this.btnInstallLicense.BackColor = System.Drawing.Color.Transparent;
            this.btnInstallLicense.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(220)))), ((int)(((byte)(255)))));
            this.btnInstallLicense.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInstallLicense.FlatAppearance.BorderSize = 0;
            this.btnInstallLicense.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInstallLicense.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnInstallLicense.ForeColor = System.Drawing.Color.White;
            this.btnInstallLicense.GlassColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(55)))), ((int)(((byte)(65)))));
            this.btnInstallLicense.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(85)))), ((int)(((byte)(95)))));
            this.btnInstallLicense.Location = new System.Drawing.Point(189, 108);
            this.btnInstallLicense.Name = "btnInstallLicense";
            this.btnInstallLicense.Selected = false;
            this.btnInstallLicense.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(140)))), ((int)(((byte)(190)))));
            this.btnInstallLicense.Size = new System.Drawing.Size(155, 24);
            this.btnInstallLicense.TabIndex = 161;
            this.btnInstallLicense.Text = "Install License";
            this.btnInstallLicense.TextColor = System.Drawing.Color.White;
            this.btnInstallLicense.UseVisualStyleBackColor = false;
            this.btnInstallLicense.Click += new System.EventHandler(this.btnInstallLicense_Click);
            // 
            // txtLicensePath
            // 
            this.txtLicensePath.BackColor = System.Drawing.Color.Transparent;
            this.txtLicensePath.Location = new System.Drawing.Point(9, 59);
            this.txtLicensePath.Name = "txtLicensePath";
            this.txtLicensePath.ReadOnly = false;
            this.txtLicensePath.Size = new System.Drawing.Size(583, 32);
            this.txtLicensePath.TabIndex = 160;
            this.txtLicensePath.UseSystemPasswordChar = false;
            // 
            // btnBrowseLicense
            // 
            this.btnBrowseLicense.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseLicense.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(220)))), ((int)(((byte)(255)))));
            this.btnBrowseLicense.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBrowseLicense.FlatAppearance.BorderSize = 0;
            this.btnBrowseLicense.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseLicense.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnBrowseLicense.ForeColor = System.Drawing.Color.White;
            this.btnBrowseLicense.GlassColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(55)))), ((int)(((byte)(65)))));
            this.btnBrowseLicense.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(85)))), ((int)(((byte)(95)))));
            this.btnBrowseLicense.Location = new System.Drawing.Point(28, 109);
            this.btnBrowseLicense.Name = "btnBrowseLicense";
            this.btnBrowseLicense.Selected = false;
            this.btnBrowseLicense.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(140)))), ((int)(((byte)(190)))));
            this.btnBrowseLicense.Size = new System.Drawing.Size(155, 24);
            this.btnBrowseLicense.TabIndex = 159;
            this.btnBrowseLicense.Text = "Browse License...";
            this.btnBrowseLicense.TextColor = System.Drawing.Color.White;
            this.btnBrowseLicense.UseVisualStyleBackColor = false;
            this.btnBrowseLicense.Click += new System.EventHandler(this.btnBrowseLicense_Click);
            // 
            // panelGenerateKey
            // 
            this.panelGenerateKey.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(36)))), ((int)(((byte)(71)))));
            this.panelGenerateKey.BorderColor = System.Drawing.Color.Transparent;
            this.panelGenerateKey.BorderThickness = 1.2F;
            this.panelGenerateKey.Controls.Add(this.lblStatusResult);
            this.panelGenerateKey.Controls.Add(this.lblStatus);
            this.panelGenerateKey.Controls.Add(this.lblLicenseStatus);
            this.panelGenerateKey.Controls.Add(this.btnGenerateKey);
            this.panelGenerateKey.Controls.Add(this.glassDatePicker2);
            this.panelGenerateKey.Controls.Add(this.glassDatePicker1);
            this.panelGenerateKey.Controls.Add(this.txtMaxPage);
            this.panelGenerateKey.Controls.Add(this.txtMaxLine);
            this.panelGenerateKey.Controls.Add(this.txtHash);
            this.panelGenerateKey.Controls.Add(this.txtCompany);
            this.panelGenerateKey.Controls.Add(this.ddlDashGroup);
            this.panelGenerateKey.Controls.Add(this.glassPanelProduct);
            this.panelGenerateKey.Controls.Add(this.label8);
            this.panelGenerateKey.Controls.Add(this.label7);
            this.panelGenerateKey.Controls.Add(this.btnTrial);
            this.panelGenerateKey.Controls.Add(this.btnFull);
            this.panelGenerateKey.Controls.Add(this.label6);
            this.panelGenerateKey.Controls.Add(this.label4);
            this.panelGenerateKey.Controls.Add(this.label5);
            this.panelGenerateKey.Controls.Add(this.label2);
            this.panelGenerateKey.Controls.Add(this.label3);
            this.panelGenerateKey.Controls.Add(this.label1);
            this.panelGenerateKey.Controls.Add(this.lblCompany);
            this.panelGenerateKey.Controls.Add(this.lblLicenseDetail);
            this.panelGenerateKey.CornerRadius = 18;
            this.panelGenerateKey.GlassColor = System.Drawing.Color.Transparent;
            this.panelGenerateKey.GlassOpacity = 180;
            this.panelGenerateKey.Location = new System.Drawing.Point(3, 249);
            this.panelGenerateKey.Name = "panelGenerateKey";
            this.panelGenerateKey.Padding = new System.Windows.Forms.Padding(20);
            this.panelGenerateKey.Size = new System.Drawing.Size(587, 430);
            this.panelGenerateKey.TabIndex = 160;
            this.panelGenerateKey.Visible = false;
            this.panelGenerateKey.Paint += new System.Windows.Forms.PaintEventHandler(this.panelGenerateKey_Paint);
            // 
            // lblStatusResult
            // 
            this.lblStatusResult.AutoSize = true;
            this.lblStatusResult.BackColor = System.Drawing.Color.Transparent;
            this.lblStatusResult.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStatusResult.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(170)))), ((int)(((byte)(190)))));
            this.lblStatusResult.Location = new System.Drawing.Point(103, 360);
            this.lblStatusResult.Name = "lblStatusResult";
            this.lblStatusResult.Size = new System.Drawing.Size(71, 15);
            this.lblStatusResult.TabIndex = 211;
            this.lblStatusResult.Text = "NOT ACTIVE";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblStatus.ForeColor = System.Drawing.Color.LightYellow;
            this.lblStatus.Location = new System.Drawing.Point(15, 391);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(66, 13);
            this.lblStatus.TabIndex = 189;
            this.lblStatus.Text = "Label Status";
            this.lblStatus.Visible = false;
            // 
            // lblLicenseStatus
            // 
            this.lblLicenseStatus.AutoSize = true;
            this.lblLicenseStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblLicenseStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblLicenseStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(170)))), ((int)(((byte)(190)))));
            this.lblLicenseStatus.Location = new System.Drawing.Point(10, 360);
            this.lblLicenseStatus.Name = "lblLicenseStatus";
            this.lblLicenseStatus.Size = new System.Drawing.Size(87, 15);
            this.lblLicenseStatus.TabIndex = 210;
            this.lblLicenseStatus.Text = "License Status :";
            // 
            // btnGenerateKey
            // 
            this.btnGenerateKey.BackColor = System.Drawing.Color.Transparent;
            this.btnGenerateKey.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(220)))), ((int)(((byte)(255)))));
            this.btnGenerateKey.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGenerateKey.FlatAppearance.BorderSize = 0;
            this.btnGenerateKey.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerateKey.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnGenerateKey.ForeColor = System.Drawing.Color.White;
            this.btnGenerateKey.GlassColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(55)))), ((int)(((byte)(65)))));
            this.btnGenerateKey.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(85)))), ((int)(((byte)(95)))));
            this.btnGenerateKey.Location = new System.Drawing.Point(408, 375);
            this.btnGenerateKey.Name = "btnGenerateKey";
            this.btnGenerateKey.Selected = false;
            this.btnGenerateKey.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(140)))), ((int)(((byte)(190)))));
            this.btnGenerateKey.Size = new System.Drawing.Size(168, 26);
            this.btnGenerateKey.TabIndex = 209;
            this.btnGenerateKey.Text = "Export License Request";
            this.btnGenerateKey.TextColor = System.Drawing.Color.White;
            this.btnGenerateKey.UseVisualStyleBackColor = false;
            this.btnGenerateKey.Click += new System.EventHandler(this.btnGenerateKey_Click);
            // 
            // glassDatePicker2
            // 
            this.glassDatePicker2.DateText = "02 10 2026";
            this.glassDatePicker2.DateValue = new System.DateTime(2026, 10, 2, 0, 0, 0, 0);
            this.glassDatePicker2.Location = new System.Drawing.Point(304, 300);
            this.glassDatePicker2.Name = "glassDatePicker2";
            this.glassDatePicker2.Size = new System.Drawing.Size(247, 37);
            this.glassDatePicker2.TabIndex = 208;
            this.glassDatePicker2.Load += new System.EventHandler(this.glassDatePicker2_Load);
            // 
            // glassDatePicker1
            // 
            this.glassDatePicker1.DateText = "02 10 2026";
            this.glassDatePicker1.DateValue = new System.DateTime(2026, 10, 2, 0, 0, 0, 0);
            this.glassDatePicker1.Location = new System.Drawing.Point(13, 300);
            this.glassDatePicker1.Name = "glassDatePicker1";
            this.glassDatePicker1.Size = new System.Drawing.Size(247, 37);
            this.glassDatePicker1.TabIndex = 207;
            this.glassDatePicker1.Load += new System.EventHandler(this.glassDatePicker1_Load);
            // 
            // txtMaxPage
            // 
            this.txtMaxPage.BackColor = System.Drawing.Color.Transparent;
            this.txtMaxPage.Enabled = false;
            this.txtMaxPage.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.txtMaxPage.Location = new System.Drawing.Point(304, 227);
            this.txtMaxPage.Name = "txtMaxPage";
            this.txtMaxPage.ReadOnly = false;
            this.txtMaxPage.Size = new System.Drawing.Size(271, 28);
            this.txtMaxPage.TabIndex = 206;
            this.txtMaxPage.UseSystemPasswordChar = false;
            // 
            // txtMaxLine
            // 
            this.txtMaxLine.BackColor = System.Drawing.Color.Transparent;
            this.txtMaxLine.Enabled = false;
            this.txtMaxLine.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.txtMaxLine.Location = new System.Drawing.Point(304, 165);
            this.txtMaxLine.Name = "txtMaxLine";
            this.txtMaxLine.ReadOnly = false;
            this.txtMaxLine.Size = new System.Drawing.Size(271, 28);
            this.txtMaxLine.TabIndex = 205;
            this.txtMaxLine.UseSystemPasswordChar = false;
            // 
            // txtHash
            // 
            this.txtHash.BackColor = System.Drawing.Color.Transparent;
            this.txtHash.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.txtHash.Location = new System.Drawing.Point(304, 41);
            this.txtHash.Name = "txtHash";
            this.txtHash.ReadOnly = false;
            this.txtHash.Size = new System.Drawing.Size(271, 28);
            this.txtHash.TabIndex = 204;
            this.txtHash.UseSystemPasswordChar = false;
            // 
            // txtCompany
            // 
            this.txtCompany.BackColor = System.Drawing.Color.Transparent;
            this.txtCompany.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCompany.ForeColor = System.Drawing.Color.Black;
            this.txtCompany.Location = new System.Drawing.Point(12, 41);
            this.txtCompany.Name = "txtCompany";
            this.txtCompany.ReadOnly = false;
            this.txtCompany.Size = new System.Drawing.Size(271, 28);
            this.txtCompany.TabIndex = 203;
            this.txtCompany.UseSystemPasswordChar = false;
            // 
            // ddlDashGroup
            // 
            this.ddlDashGroup.BackColor = System.Drawing.Color.Transparent;
            this.ddlDashGroup.DataSource = null;
            this.ddlDashGroup.DisplayMember = "";
            this.ddlDashGroup.Location = new System.Drawing.Point(304, 98);
            this.ddlDashGroup.Name = "ddlDashGroup";
            this.ddlDashGroup.SelectedIndex = -1;
            this.ddlDashGroup.SelectedItem = null;
            this.ddlDashGroup.SelectedValue = null;
            this.ddlDashGroup.Size = new System.Drawing.Size(271, 38);
            this.ddlDashGroup.TabIndex = 202;
            this.ddlDashGroup.ValueMember = "";
            this.ddlDashGroup.SelectedIndexChanged += new System.EventHandler(this.ddlDashGroup_SelectedIndexChanged);
            this.ddlDashGroup.Load += new System.EventHandler(this.ddlDashGroup_Load);
            // 
            // glassPanelProduct
            // 
            this.glassPanelProduct.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(36)))), ((int)(((byte)(71)))));
            this.glassPanelProduct.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(75)))), ((int)(((byte)(122)))));
            this.glassPanelProduct.BorderThickness = 1.2F;
            this.glassPanelProduct.Controls.Add(this.dgvLicenseProduct);
            this.glassPanelProduct.CornerRadius = 18;
            this.glassPanelProduct.GlassColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(36)))), ((int)(((byte)(71)))));
            this.glassPanelProduct.GlassOpacity = 180;
            this.glassPanelProduct.Location = new System.Drawing.Point(12, 165);
            this.glassPanelProduct.Name = "glassPanelProduct";
            this.glassPanelProduct.Padding = new System.Windows.Forms.Padding(20);
            this.glassPanelProduct.Size = new System.Drawing.Size(266, 105);
            this.glassPanelProduct.TabIndex = 201;
            // 
            // dgvLicenseProduct
            // 
            this.dgvLicenseProduct.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLicenseProduct.Location = new System.Drawing.Point(23, 9);
            this.dgvLicenseProduct.Name = "dgvLicenseProduct";
            this.dgvLicenseProduct.Size = new System.Drawing.Size(224, 88);
            this.dgvLicenseProduct.TabIndex = 150;
            this.dgvLicenseProduct.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLicenseProduct_CellContentClick);
            this.dgvLicenseProduct.SelectionChanged += new System.EventHandler(this.dgvLicenseProduct_SelectionChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(170)))), ((int)(((byte)(190)))));
            this.label8.Location = new System.Drawing.Point(314, 211);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(102, 15);
            this.label8.TabIndex = 200;
            this.label8.Text = "Maximum Pages :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(170)))), ((int)(((byte)(190)))));
            this.label7.Location = new System.Drawing.Point(314, 85);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(106, 15);
            this.label7.TabIndex = 199;
            this.label7.Text = "Dashboard Group :";
            // 
            // btnTrial
            // 
            this.btnTrial.BackColor = System.Drawing.Color.Transparent;
            this.btnTrial.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(220)))), ((int)(((byte)(255)))));
            this.btnTrial.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTrial.FlatAppearance.BorderSize = 0;
            this.btnTrial.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTrial.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnTrial.ForeColor = System.Drawing.Color.White;
            this.btnTrial.GlassColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(55)))), ((int)(((byte)(65)))));
            this.btnTrial.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(85)))), ((int)(((byte)(95)))));
            this.btnTrial.Location = new System.Drawing.Point(124, 100);
            this.btnTrial.Name = "btnTrial";
            this.btnTrial.Selected = false;
            this.btnTrial.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(140)))), ((int)(((byte)(190)))));
            this.btnTrial.Size = new System.Drawing.Size(108, 30);
            this.btnTrial.TabIndex = 198;
            this.btnTrial.Text = "TRIAL";
            this.btnTrial.TextColor = System.Drawing.Color.White;
            this.btnTrial.UseVisualStyleBackColor = false;
            this.btnTrial.Click += new System.EventHandler(this.btnTrial_Click);
            // 
            // btnFull
            // 
            this.btnFull.BackColor = System.Drawing.Color.Transparent;
            this.btnFull.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(220)))), ((int)(((byte)(255)))));
            this.btnFull.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFull.FlatAppearance.BorderSize = 0;
            this.btnFull.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFull.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnFull.ForeColor = System.Drawing.Color.White;
            this.btnFull.GlassColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(55)))), ((int)(((byte)(65)))));
            this.btnFull.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(85)))), ((int)(((byte)(95)))));
            this.btnFull.Location = new System.Drawing.Point(12, 100);
            this.btnFull.Name = "btnFull";
            this.btnFull.Selected = false;
            this.btnFull.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(140)))), ((int)(((byte)(190)))));
            this.btnFull.Size = new System.Drawing.Size(108, 30);
            this.btnFull.TabIndex = 197;
            this.btnFull.Text = "FULL";
            this.btnFull.TextColor = System.Drawing.Color.White;
            this.btnFull.UseVisualStyleBackColor = false;
            this.btnFull.Click += new System.EventHandler(this.btnFull_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(170)))), ((int)(((byte)(190)))));
            this.label6.Location = new System.Drawing.Point(314, 285);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 15);
            this.label6.TabIndex = 196;
            this.label6.Text = "Issued Date :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(170)))), ((int)(((byte)(190)))));
            this.label4.Location = new System.Drawing.Point(17, 285);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 15);
            this.label4.TabIndex = 195;
            this.label4.Text = "Expiry Date :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(170)))), ((int)(((byte)(190)))));
            this.label5.Location = new System.Drawing.Point(314, 150);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 15);
            this.label5.TabIndex = 194;
            this.label5.Text = "Maximum Lines :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(170)))), ((int)(((byte)(190)))));
            this.label2.Location = new System.Drawing.Point(17, 150);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 15);
            this.label2.TabIndex = 193;
            this.label2.Text = "Product :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(170)))), ((int)(((byte)(190)))));
            this.label3.Location = new System.Drawing.Point(19, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 15);
            this.label3.TabIndex = 192;
            this.label3.Text = "License Type :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(170)))), ((int)(((byte)(190)))));
            this.label1.Location = new System.Drawing.Point(314, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 15);
            this.label1.TabIndex = 191;
            this.label1.Text = "Hash ID :";
            // 
            // lblCompany
            // 
            this.lblCompany.AutoSize = true;
            this.lblCompany.BackColor = System.Drawing.Color.Transparent;
            this.lblCompany.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCompany.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(170)))), ((int)(((byte)(190)))));
            this.lblCompany.Location = new System.Drawing.Point(17, 27);
            this.lblCompany.Name = "lblCompany";
            this.lblCompany.Size = new System.Drawing.Size(100, 15);
            this.lblCompany.TabIndex = 190;
            this.lblCompany.Text = "Company Name :";
            // 
            // lblLicenseDetail
            // 
            this.lblLicenseDetail.AutoSize = true;
            this.lblLicenseDetail.BackColor = System.Drawing.Color.Transparent;
            this.lblLicenseDetail.Font = new System.Drawing.Font("Segoe Print", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLicenseDetail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(170)))), ((int)(((byte)(190)))));
            this.lblLicenseDetail.Location = new System.Drawing.Point(12, 2);
            this.lblLicenseDetail.Name = "lblLicenseDetail";
            this.lblLicenseDetail.Size = new System.Drawing.Size(114, 28);
            this.lblLicenseDetail.TabIndex = 188;
            this.lblLicenseDetail.Text = "Information ";
            // 
            // glassPanelSignature
            // 
            this.glassPanelSignature.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(36)))), ((int)(((byte)(71)))));
            this.glassPanelSignature.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(75)))), ((int)(((byte)(122)))));
            this.glassPanelSignature.BorderThickness = 1.2F;
            this.glassPanelSignature.Controls.Add(this.lblLicenseAbout);
            this.glassPanelSignature.Controls.Add(this.modernTxtDays);
            this.glassPanelSignature.Controls.Add(this.lblRemainingDay);
            this.glassPanelSignature.Controls.Add(this.glassDatePicker3);
            this.glassPanelSignature.Controls.Add(this.label10);
            this.glassPanelSignature.Controls.Add(this.lblLicenseSignature);
            this.glassPanelSignature.Controls.Add(this.modernTextSign);
            this.glassPanelSignature.CornerRadius = 18;
            this.glassPanelSignature.GlassColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(36)))), ((int)(((byte)(71)))));
            this.glassPanelSignature.GlassOpacity = 180;
            this.glassPanelSignature.Location = new System.Drawing.Point(6, 87);
            this.glassPanelSignature.Name = "glassPanelSignature";
            this.glassPanelSignature.Padding = new System.Windows.Forms.Padding(20);
            this.glassPanelSignature.Size = new System.Drawing.Size(584, 156);
            this.glassPanelSignature.TabIndex = 159;
            this.glassPanelSignature.Visible = false;
            this.glassPanelSignature.Paint += new System.Windows.Forms.PaintEventHandler(this.glassPanelSignature_Paint);
            // 
            // lblLicenseAbout
            // 
            this.lblLicenseAbout.AutoSize = true;
            this.lblLicenseAbout.Font = new System.Drawing.Font("Segoe Print", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLicenseAbout.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(170)))), ((int)(((byte)(190)))));
            this.lblLicenseAbout.Location = new System.Drawing.Point(9, 5);
            this.lblLicenseAbout.Name = "lblLicenseAbout";
            this.lblLicenseAbout.Size = new System.Drawing.Size(157, 28);
            this.lblLicenseAbout.TabIndex = 188;
            this.lblLicenseAbout.Text = "License Activation";
            // 
            // modernTxtDays
            // 
            this.modernTxtDays.BackColor = System.Drawing.Color.Transparent;
            this.modernTxtDays.Enabled = false;
            this.modernTxtDays.Location = new System.Drawing.Point(326, 112);
            this.modernTxtDays.Name = "modernTxtDays";
            this.modernTxtDays.ReadOnly = false;
            this.modernTxtDays.Size = new System.Drawing.Size(244, 30);
            this.modernTxtDays.TabIndex = 186;
            this.modernTxtDays.UseSystemPasswordChar = false;
            // 
            // lblRemainingDay
            // 
            this.lblRemainingDay.AutoSize = true;
            this.lblRemainingDay.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblRemainingDay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(170)))), ((int)(((byte)(190)))));
            this.lblRemainingDay.Location = new System.Drawing.Point(324, 95);
            this.lblRemainingDay.Name = "lblRemainingDay";
            this.lblRemainingDay.Size = new System.Drawing.Size(106, 15);
            this.lblRemainingDay.TabIndex = 185;
            this.lblRemainingDay.Text = "Remaining Day(s) :";
            // 
            // glassDatePicker3
            // 
            this.glassDatePicker3.DateText = "02 10 2026";
            this.glassDatePicker3.DateValue = new System.DateTime(2026, 10, 2, 0, 0, 0, 0);
            this.glassDatePicker3.Enabled = false;
            this.glassDatePicker3.Location = new System.Drawing.Point(12, 108);
            this.glassDatePicker3.Name = "glassDatePicker3";
            this.glassDatePicker3.Size = new System.Drawing.Size(247, 35);
            this.glassDatePicker3.TabIndex = 184;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(170)))), ((int)(((byte)(190)))));
            this.label10.Location = new System.Drawing.Point(16, 93);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(72, 15);
            this.label10.TabIndex = 183;
            this.label10.Text = "Expiry Date :";
            // 
            // lblLicenseSignature
            // 
            this.lblLicenseSignature.AutoSize = true;
            this.lblLicenseSignature.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLicenseSignature.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(170)))), ((int)(((byte)(190)))));
            this.lblLicenseSignature.Location = new System.Drawing.Point(16, 39);
            this.lblLicenseSignature.Name = "lblLicenseSignature";
            this.lblLicenseSignature.Size = new System.Drawing.Size(73, 17);
            this.lblLicenseSignature.TabIndex = 160;
            this.lblLicenseSignature.Text = "Signature :";
            // 
            // modernTextSign
            // 
            this.modernTextSign.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.modernTextSign.BackColor = System.Drawing.Color.Transparent;
            this.modernTextSign.Enabled = false;
            this.modernTextSign.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.modernTextSign.Location = new System.Drawing.Point(10, 59);
            this.modernTextSign.Name = "modernTextSign";
            this.modernTextSign.ReadOnly = false;
            this.modernTextSign.Size = new System.Drawing.Size(565, 30);
            this.modernTextSign.TabIndex = 159;
            this.modernTextSign.UseSystemPasswordChar = false;
            this.modernTextSign.Load += new System.EventHandler(this.modernTextSign_Load);
            // 
            // UC_MRT_DashCombine_License
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(21)))), ((int)(((byte)(47)))));
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.panelGenerateKey);
            this.Controls.Add(this.glassPanelSignature);
            this.Controls.Add(this.panelLicenseDetail);
            this.Controls.Add(this.pnlHeader);
            this.Name = "UC_MRT_DashCombine_License";
            this.Size = new System.Drawing.Size(1223, 686);
            this.Load += new System.EventHandler(this.UC_MRT_DashCombine_License_Load);
            this.Resize += new System.EventHandler(this.UC_MRT_DashCombine_License_Resize);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLicenseDetail)).EndInit();
            this.panelLicenseDetail.ResumeLayout(false);
            this.panelBottom.ResumeLayout(false);
            this.panelBottom.PerformLayout();
            this.panelGenerateKey.ResumeLayout(false);
            this.panelGenerateKey.PerformLayout();
            this.glassPanelProduct.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLicenseProduct)).EndInit();
            this.glassPanelSignature.ResumeLayout(false);
            this.glassPanelSignature.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.DataGridView dgvLicenseDetail;
        private System.Windows.Forms.FlowLayoutPanel panelLicenseDetail;
        private Controls.GlassButton btnCheckLicense;
        private Controls.GlassPanel glassPanelSignature;
        private System.Windows.Forms.Label lblLicenseSignature;
        private Controls.ModernTextBox modernTextSign;
        private Controls.GlassPanel panelGenerateKey;
        private Controls.GlassPanel panelBottom;
        private System.Windows.Forms.Label label9;
        private Controls.GlassButton btnInstallLicense;
        private Controls.ModernTextBox txtLicensePath;
        private Controls.GlassButton btnBrowseLicense;
        private Controls.GlassDatePicker glassDatePicker3;
        private System.Windows.Forms.Label label10;
        private Controls.ModernTextBox modernTxtDays;
        private System.Windows.Forms.Label lblRemainingDay;
        private System.Windows.Forms.Label lblLicenseAbout;
        private System.Windows.Forms.Label lblLicenseDetail;
        private System.Windows.Forms.Label lblStatusResult;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblLicenseStatus;
        private Controls.GlassButton btnGenerateKey;
        private Controls.GlassDatePicker glassDatePicker2;
        private Controls.GlassDatePicker glassDatePicker1;
        private Controls.ModernTextBox txtMaxPage;
        private Controls.ModernTextBox txtMaxLine;
        private Controls.ModernTextBox txtHash;
        private Controls.ModernTextBox txtCompany;
        private Controls.ModernComboBox ddlDashGroup;
        private Controls.GlassPanel glassPanelProduct;
        private System.Windows.Forms.DataGridView dgvLicenseProduct;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private Controls.GlassButton btnTrial;
        private Controls.GlassButton btnFull;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCompany;
    }
}
