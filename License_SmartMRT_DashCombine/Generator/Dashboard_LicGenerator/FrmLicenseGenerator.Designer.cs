
namespace Dashboard_LicGenerator
{
    partial class FrmLicenseGenerator
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblPackage = new System.Windows.Forms.Label();
            this.lblLicenseGroup = new System.Windows.Forms.Label();
            this.lblDashboard = new System.Windows.Forms.Label();
            this.lblMaxLines = new System.Windows.Forms.Label();
            this.lblMaxPages = new System.Windows.Forms.Label();
            this.ddlPackage = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtRequestPackage = new System.Windows.Forms.TextBox();
            this.lblRequestPackage = new System.Windows.Forms.Label();
            this.txtHardwareHash = new System.Windows.Forms.TextBox();
            this.lblHardwareHash = new System.Windows.Forms.Label();
            this.txtCompany = new System.Windows.Forms.TextBox();
            this.lblCompany = new System.Windows.Forms.Label();
            this.txtLicenseFiles = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblIssueDate = new System.Windows.Forms.Label();
            this.lblExpiryDate = new System.Windows.Forms.Label();
            this.btnGenerateLicense = new System.Windows.Forms.Button();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.btnLicenseReq = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubTitle = new System.Windows.Forms.Label();
            this.txtDateIssuedCustomer = new System.Windows.Forms.TextBox();
            this.lblLicenseIssued = new System.Windows.Forms.Label();
            this.lblExpiryStatus = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.ddlPackage);
            this.panel1.Controls.Add(this.lblMaxPages);
            this.panel1.Controls.Add(this.lblMaxLines);
            this.panel1.Controls.Add(this.lblDashboard);
            this.panel1.Controls.Add(this.lblLicenseGroup);
            this.panel1.Controls.Add(this.lblPackage);
            this.panel1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(12, 274);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(356, 117);
            this.panel1.TabIndex = 8;
            // 
            // lblPackage
            // 
            this.lblPackage.AutoSize = true;
            this.lblPackage.Location = new System.Drawing.Point(3, 24);
            this.lblPackage.Name = "lblPackage";
            this.lblPackage.Size = new System.Drawing.Size(51, 15);
            this.lblPackage.TabIndex = 0;
            this.lblPackage.Text = "Package";
            // 
            // lblLicenseGroup
            // 
            this.lblLicenseGroup.AutoSize = true;
            this.lblLicenseGroup.Location = new System.Drawing.Point(4, 68);
            this.lblLicenseGroup.Name = "lblLicenseGroup";
            this.lblLicenseGroup.Size = new System.Drawing.Size(82, 15);
            this.lblLicenseGroup.TabIndex = 1;
            this.lblLicenseGroup.Text = "License Group";
            // 
            // lblDashboard
            // 
            this.lblDashboard.AutoSize = true;
            this.lblDashboard.Location = new System.Drawing.Point(173, 68);
            this.lblDashboard.Name = "lblDashboard";
            this.lblDashboard.Size = new System.Drawing.Size(64, 15);
            this.lblDashboard.TabIndex = 2;
            this.lblDashboard.Text = "Dashboard";
            // 
            // lblMaxLines
            // 
            this.lblMaxLines.AutoSize = true;
            this.lblMaxLines.Location = new System.Drawing.Point(174, 87);
            this.lblMaxLines.Name = "lblMaxLines";
            this.lblMaxLines.Size = new System.Drawing.Size(63, 15);
            this.lblMaxLines.TabIndex = 3;
            this.lblMaxLines.Text = "Max Lines ";
            // 
            // lblMaxPages
            // 
            this.lblMaxPages.AutoSize = true;
            this.lblMaxPages.Location = new System.Drawing.Point(4, 87);
            this.lblMaxPages.Name = "lblMaxPages";
            this.lblMaxPages.Size = new System.Drawing.Size(64, 15);
            this.lblMaxPages.TabIndex = 4;
            this.lblMaxPages.Text = "Max Pages";
            // 
            // ddlPackage
            // 
            this.ddlPackage.FormattingEnabled = true;
            this.ddlPackage.Location = new System.Drawing.Point(3, 42);
            this.ddlPackage.Name = "ddlPackage";
            this.ddlPackage.Size = new System.Drawing.Size(345, 23);
            this.ddlPackage.TabIndex = 5;
            this.ddlPackage.SelectedIndexChanged += new System.EventHandler(this.ddlPackage_SelectedIndexChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblExpiryStatus);
            this.panel2.Controls.Add(this.dateTimePicker2);
            this.panel2.Controls.Add(this.dateTimePicker1);
            this.panel2.Controls.Add(this.btnGenerateLicense);
            this.panel2.Controls.Add(this.lblExpiryDate);
            this.panel2.Controls.Add(this.lblIssueDate);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel2.Location = new System.Drawing.Point(12, 397);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(357, 133);
            this.panel2.TabIndex = 9;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.txtDateIssuedCustomer);
            this.panel3.Controls.Add(this.lblLicenseIssued);
            this.panel3.Controls.Add(this.btnLicenseReq);
            this.panel3.Controls.Add(this.txtRequestPackage);
            this.panel3.Controls.Add(this.lblRequestPackage);
            this.panel3.Controls.Add(this.txtHardwareHash);
            this.panel3.Controls.Add(this.lblHardwareHash);
            this.panel3.Controls.Add(this.txtCompany);
            this.panel3.Controls.Add(this.lblCompany);
            this.panel3.Controls.Add(this.txtLicenseFiles);
            this.panel3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel3.Location = new System.Drawing.Point(12, 48);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(356, 225);
            this.panel3.TabIndex = 10;
            // 
            // txtRequestPackage
            // 
            this.txtRequestPackage.Enabled = false;
            this.txtRequestPackage.Location = new System.Drawing.Point(4, 155);
            this.txtRequestPackage.Name = "txtRequestPackage";
            this.txtRequestPackage.Size = new System.Drawing.Size(348, 23);
            this.txtRequestPackage.TabIndex = 15;
            // 
            // lblRequestPackage
            // 
            this.lblRequestPackage.AutoSize = true;
            this.lblRequestPackage.Location = new System.Drawing.Point(4, 139);
            this.lblRequestPackage.Name = "lblRequestPackage";
            this.lblRequestPackage.Size = new System.Drawing.Size(109, 15);
            this.lblRequestPackage.TabIndex = 14;
            this.lblRequestPackage.Text = "Requested Package";
            // 
            // txtHardwareHash
            // 
            this.txtHardwareHash.Enabled = false;
            this.txtHardwareHash.Location = new System.Drawing.Point(4, 113);
            this.txtHardwareHash.Name = "txtHardwareHash";
            this.txtHardwareHash.Size = new System.Drawing.Size(348, 23);
            this.txtHardwareHash.TabIndex = 13;
            // 
            // lblHardwareHash
            // 
            this.lblHardwareHash.AutoSize = true;
            this.lblHardwareHash.Location = new System.Drawing.Point(4, 97);
            this.lblHardwareHash.Name = "lblHardwareHash";
            this.lblHardwareHash.Size = new System.Drawing.Size(88, 15);
            this.lblHardwareHash.TabIndex = 12;
            this.lblHardwareHash.Text = "Hardware Hash";
            // 
            // txtCompany
            // 
            this.txtCompany.Enabled = false;
            this.txtCompany.Location = new System.Drawing.Point(4, 73);
            this.txtCompany.Name = "txtCompany";
            this.txtCompany.Size = new System.Drawing.Size(348, 23);
            this.txtCompany.TabIndex = 11;
            // 
            // lblCompany
            // 
            this.lblCompany.AutoSize = true;
            this.lblCompany.Location = new System.Drawing.Point(4, 57);
            this.lblCompany.Name = "lblCompany";
            this.lblCompany.Size = new System.Drawing.Size(59, 15);
            this.lblCompany.TabIndex = 10;
            this.lblCompany.Text = "Company";
            // 
            // txtLicenseFiles
            // 
            this.txtLicenseFiles.Enabled = false;
            this.txtLicenseFiles.Location = new System.Drawing.Point(4, 32);
            this.txtLicenseFiles.Name = "txtLicenseFiles";
            this.txtLicenseFiles.Size = new System.Drawing.Size(348, 23);
            this.txtLicenseFiles.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(260, 11);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 15);
            this.label6.TabIndex = 6;
            this.label6.Text = "License To Issue";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(260, 6);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(83, 15);
            this.label7.TabIndex = 7;
            this.label7.Text = "License Period";
            // 
            // lblIssueDate
            // 
            this.lblIssueDate.AutoSize = true;
            this.lblIssueDate.Location = new System.Drawing.Point(39, 34);
            this.lblIssueDate.Name = "lblIssueDate";
            this.lblIssueDate.Size = new System.Drawing.Size(60, 15);
            this.lblIssueDate.TabIndex = 8;
            this.lblIssueDate.Text = "Issue Date";
            // 
            // lblExpiryDate
            // 
            this.lblExpiryDate.AutoSize = true;
            this.lblExpiryDate.Location = new System.Drawing.Point(39, 62);
            this.lblExpiryDate.Name = "lblExpiryDate";
            this.lblExpiryDate.Size = new System.Drawing.Size(66, 15);
            this.lblExpiryDate.TabIndex = 9;
            this.lblExpiryDate.Text = "Expiry Date";
            // 
            // btnGenerateLicense
            // 
            this.btnGenerateLicense.Location = new System.Drawing.Point(206, 102);
            this.btnGenerateLicense.Name = "btnGenerateLicense";
            this.btnGenerateLicense.Size = new System.Drawing.Size(140, 23);
            this.btnGenerateLicense.TabIndex = 10;
            this.btnGenerateLicense.Text = "Generate License";
            this.btnGenerateLicense.UseVisualStyleBackColor = true;
            this.btnGenerateLicense.Click += new System.EventHandler(this.btnGenerateLicense_Click);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Enabled = false;
            this.dateTimePicker1.Location = new System.Drawing.Point(113, 28);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 23);
            this.dateTimePicker1.TabIndex = 11;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(113, 56);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(200, 23);
            this.dateTimePicker2.TabIndex = 12;
            // 
            // btnLicenseReq
            // 
            this.btnLicenseReq.Location = new System.Drawing.Point(6, 6);
            this.btnLicenseReq.Name = "btnLicenseReq";
            this.btnLicenseReq.Size = new System.Drawing.Size(131, 23);
            this.btnLicenseReq.TabIndex = 16;
            this.btnLicenseReq.Text = "License Request";
            this.btnLicenseReq.UseVisualStyleBackColor = true;
            this.btnLicenseReq.Click += new System.EventHandler(this.btnLicenseReq_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(73, 5);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(229, 21);
            this.lblTitle.TabIndex = 11;
            this.lblTitle.Text = "SmartMRT License Generator";
            // 
            // lblSubTitle
            // 
            this.lblSubTitle.AutoSize = true;
            this.lblSubTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubTitle.Location = new System.Drawing.Point(94, 26);
            this.lblSubTitle.Name = "lblSubTitle";
            this.lblSubTitle.Size = new System.Drawing.Size(203, 15);
            this.lblSubTitle.TabIndex = 12;
            this.lblSubTitle.Text = "License Key for SmartMRT Dashboard";
            // 
            // txtDateIssuedCustomer
            // 
            this.txtDateIssuedCustomer.Enabled = false;
            this.txtDateIssuedCustomer.Location = new System.Drawing.Point(4, 197);
            this.txtDateIssuedCustomer.Name = "txtDateIssuedCustomer";
            this.txtDateIssuedCustomer.Size = new System.Drawing.Size(348, 23);
            this.txtDateIssuedCustomer.TabIndex = 18;
            // 
            // lblLicenseIssued
            // 
            this.lblLicenseIssued.AutoSize = true;
            this.lblLicenseIssued.Location = new System.Drawing.Point(4, 181);
            this.lblLicenseIssued.Name = "lblLicenseIssued";
            this.lblLicenseIssued.Size = new System.Drawing.Size(153, 15);
            this.lblLicenseIssued.TabIndex = 17;
            this.lblLicenseIssued.Text = "Date Issued From Customer";
            // 
            // lblExpiryStatus
            // 
            this.lblExpiryStatus.AutoSize = true;
            this.lblExpiryStatus.Location = new System.Drawing.Point(4, 110);
            this.lblExpiryStatus.Name = "lblExpiryStatus";
            this.lblExpiryStatus.Size = new System.Drawing.Size(38, 15);
            this.lblExpiryStatus.TabIndex = 13;
            this.lblExpiryStatus.Text = "label1";
            // 
            // FrmLicenseGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(381, 542);
            this.Controls.Add(this.lblSubTitle);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "FrmLicenseGenerator";
            this.Text = "SmartMRT License Generator";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox ddlPackage;
        private System.Windows.Forms.Label lblMaxPages;
        private System.Windows.Forms.Label lblMaxLines;
        private System.Windows.Forms.Label lblDashboard;
        private System.Windows.Forms.Label lblLicenseGroup;
        private System.Windows.Forms.Label lblPackage;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button btnGenerateLicense;
        private System.Windows.Forms.Label lblExpiryDate;
        private System.Windows.Forms.Label lblIssueDate;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txtRequestPackage;
        private System.Windows.Forms.Label lblRequestPackage;
        private System.Windows.Forms.TextBox txtHardwareHash;
        private System.Windows.Forms.Label lblHardwareHash;
        private System.Windows.Forms.TextBox txtCompany;
        private System.Windows.Forms.Label lblCompany;
        private System.Windows.Forms.TextBox txtLicenseFiles;
        private System.Windows.Forms.Button btnLicenseReq;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubTitle;
        private System.Windows.Forms.TextBox txtDateIssuedCustomer;
        private System.Windows.Forms.Label lblLicenseIssued;
        private System.Windows.Forms.Label lblExpiryStatus;
    }
}