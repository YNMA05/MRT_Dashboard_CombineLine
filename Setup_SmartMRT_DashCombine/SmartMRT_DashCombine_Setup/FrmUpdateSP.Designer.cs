namespace SmartMRT_DashCombine_Setup
{
    partial class FrmUpdateSP
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmUpdateSP));
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblProdList = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.rtbScript = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.chkActive = new System.Windows.Forms.CheckBox();
            this.lblProdName = new System.Windows.Forms.Label();
            this.chkNewSP = new System.Windows.Forms.CheckBox();
            this.chkNew = new System.Windows.Forms.Label();
            this.lblRemark = new System.Windows.Forms.Label();
            this.progressBarDeploy = new System.Windows.Forms.ProgressBar();
            this.lblDeployProgress = new System.Windows.Forms.Label();
            this.txtRemark = new SmartMRT_DashCombine_Setup.Controls.ModernTextBox();
            this.btnUpdateAll = new SmartMRT_DashCombine_Setup.Controls.GlassButton();
            this.btnUpdateScript = new SmartMRT_DashCombine_Setup.Controls.GlassButton();
            this.btnEdit = new SmartMRT_DashCombine_Setup.Controls.GlassButton();
            this.btnNew = new SmartMRT_DashCombine_Setup.Controls.GlassButton();
            this.txtName = new SmartMRT_DashCombine_Setup.Controls.ModernTextBox();
            this.dateModified = new SmartMRT_DashCombine_Setup.Controls.GlassDatePicker(this.components);
            this.txtVersion = new SmartMRT_DashCombine_Setup.Controls.ModernTextBox();
            this.ddlListSP = new SmartMRT_DashCombine_Setup.Controls.ModernComboBox();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.LightSkyBlue;
            this.lblTitle.Location = new System.Drawing.Point(12, 6);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(280, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Stored Procedure Management";
            // 
            // lblProdList
            // 
            this.lblProdList.AutoSize = true;
            this.lblProdList.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProdList.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblProdList.Location = new System.Drawing.Point(39, 40);
            this.lblProdList.Name = "lblProdList";
            this.lblProdList.Size = new System.Drawing.Size(88, 15);
            this.lblProdList.TabIndex = 1;
            this.lblProdList.Text = "Procedure List :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label3.Location = new System.Drawing.Point(39, 215);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Version :";
            // 
            // rtbScript
            // 
            this.rtbScript.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(30)))), ((int)(((byte)(40)))));
            this.rtbScript.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbScript.ForeColor = System.Drawing.Color.Gainsboro;
            this.rtbScript.Location = new System.Drawing.Point(40, 302);
            this.rtbScript.Name = "rtbScript";
            this.rtbScript.ReadOnly = true;
            this.rtbScript.Size = new System.Drawing.Size(660, 387);
            this.rtbScript.TabIndex = 5;
            this.rtbScript.Text = "";
            this.rtbScript.WordWrap = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.Location = new System.Drawing.Point(147, 215);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "Status :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label4.Location = new System.Drawing.Point(39, 159);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(140, 15);
            this.label4.TabIndex = 8;
            this.label4.Text = "Date Created / Modified :";
            // 
            // chkActive
            // 
            this.chkActive.AutoSize = true;
            this.chkActive.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.chkActive.Location = new System.Drawing.Point(150, 233);
            this.chkActive.Name = "chkActive";
            this.chkActive.Size = new System.Drawing.Size(56, 17);
            this.chkActive.TabIndex = 10;
            this.chkActive.Text = "Active";
            this.chkActive.UseVisualStyleBackColor = true;
            // 
            // lblProdName
            // 
            this.lblProdName.AutoSize = true;
            this.lblProdName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProdName.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblProdName.Location = new System.Drawing.Point(39, 101);
            this.lblProdName.Name = "lblProdName";
            this.lblProdName.Size = new System.Drawing.Size(102, 15);
            this.lblProdName.TabIndex = 12;
            this.lblProdName.Text = "Procedure Name :";
            // 
            // chkNewSP
            // 
            this.chkNewSP.AutoSize = true;
            this.chkNewSP.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.chkNewSP.Location = new System.Drawing.Point(336, 76);
            this.chkNewSP.Name = "chkNewSP";
            this.chkNewSP.Size = new System.Drawing.Size(134, 17);
            this.chkNewSP.TabIndex = 16;
            this.chkNewSP.Text = "New Stored Procedure";
            this.chkNewSP.UseVisualStyleBackColor = true;
            this.chkNewSP.CheckedChanged += new System.EventHandler(this.chkNewSP_CheckedChanged);
            // 
            // chkNew
            // 
            this.chkNew.AutoSize = true;
            this.chkNew.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkNew.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.chkNew.Location = new System.Drawing.Point(333, 58);
            this.chkNew.Name = "chkNew";
            this.chkNew.Size = new System.Drawing.Size(74, 15);
            this.chkNew.TabIndex = 15;
            this.chkNew.Text = "Create New :";
            // 
            // lblRemark
            // 
            this.lblRemark.AutoSize = true;
            this.lblRemark.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRemark.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblRemark.Location = new System.Drawing.Point(333, 101);
            this.lblRemark.Name = "lblRemark";
            this.lblRemark.Size = new System.Drawing.Size(53, 15);
            this.lblRemark.TabIndex = 21;
            this.lblRemark.Text = "Remark :";
            // 
            // progressBarDeploy
            // 
            this.progressBarDeploy.Location = new System.Drawing.Point(541, 244);
            this.progressBarDeploy.Name = "progressBarDeploy";
            this.progressBarDeploy.Size = new System.Drawing.Size(159, 23);
            this.progressBarDeploy.TabIndex = 23;
            this.progressBarDeploy.Visible = false;
            // 
            // lblDeployProgress
            // 
            this.lblDeployProgress.AutoSize = true;
            this.lblDeployProgress.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDeployProgress.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblDeployProgress.Location = new System.Drawing.Point(540, 226);
            this.lblDeployProgress.Name = "lblDeployProgress";
            this.lblDeployProgress.Size = new System.Drawing.Size(0, 15);
            this.lblDeployProgress.TabIndex = 24;
            // 
            // txtRemark
            // 
            this.txtRemark.BackColor = System.Drawing.Color.Transparent;
            this.txtRemark.Location = new System.Drawing.Point(336, 119);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.ReadOnly = false;
            this.txtRemark.Size = new System.Drawing.Size(364, 35);
            this.txtRemark.TabIndex = 22;
            this.txtRemark.UseSystemPasswordChar = false;
            // 
            // btnUpdateAll
            // 
            this.btnUpdateAll.BackColor = System.Drawing.Color.Transparent;
            this.btnUpdateAll.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(220)))), ((int)(((byte)(255)))));
            this.btnUpdateAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUpdateAll.FlatAppearance.BorderSize = 0;
            this.btnUpdateAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdateAll.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnUpdateAll.ForeColor = System.Drawing.Color.White;
            this.btnUpdateAll.GlassColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(55)))), ((int)(((byte)(65)))));
            this.btnUpdateAll.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(85)))), ((int)(((byte)(95)))));
            this.btnUpdateAll.Location = new System.Drawing.Point(543, 177);
            this.btnUpdateAll.Name = "btnUpdateAll";
            this.btnUpdateAll.Selected = false;
            this.btnUpdateAll.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(140)))), ((int)(((byte)(190)))));
            this.btnUpdateAll.Size = new System.Drawing.Size(157, 44);
            this.btnUpdateAll.TabIndex = 20;
            this.btnUpdateAll.Text = "Update To Line PC";
            this.btnUpdateAll.TextColor = System.Drawing.Color.White;
            this.btnUpdateAll.UseVisualStyleBackColor = false;
            this.btnUpdateAll.Click += new System.EventHandler(this.btnUpdateAll_Click);
            // 
            // btnUpdateScript
            // 
            this.btnUpdateScript.BackColor = System.Drawing.Color.Transparent;
            this.btnUpdateScript.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(220)))), ((int)(((byte)(255)))));
            this.btnUpdateScript.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUpdateScript.FlatAppearance.BorderSize = 0;
            this.btnUpdateScript.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdateScript.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnUpdateScript.ForeColor = System.Drawing.Color.White;
            this.btnUpdateScript.GlassColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(55)))), ((int)(((byte)(65)))));
            this.btnUpdateScript.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(85)))), ((int)(((byte)(95)))));
            this.btnUpdateScript.Location = new System.Drawing.Point(336, 177);
            this.btnUpdateScript.Name = "btnUpdateScript";
            this.btnUpdateScript.Selected = false;
            this.btnUpdateScript.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(140)))), ((int)(((byte)(190)))));
            this.btnUpdateScript.Size = new System.Drawing.Size(157, 44);
            this.btnUpdateScript.TabIndex = 19;
            this.btnUpdateScript.Text = "Update Script Only";
            this.btnUpdateScript.TextColor = System.Drawing.Color.White;
            this.btnUpdateScript.UseVisualStyleBackColor = false;
            this.btnUpdateScript.Click += new System.EventHandler(this.btnUpdateScript_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.BackColor = System.Drawing.Color.Transparent;
            this.btnEdit.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(220)))), ((int)(((byte)(255)))));
            this.btnEdit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEdit.FlatAppearance.BorderSize = 0;
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnEdit.ForeColor = System.Drawing.Color.White;
            this.btnEdit.GlassColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(55)))), ((int)(((byte)(65)))));
            this.btnEdit.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(85)))), ((int)(((byte)(95)))));
            this.btnEdit.Location = new System.Drawing.Point(42, 273);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Selected = false;
            this.btnEdit.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(140)))), ((int)(((byte)(190)))));
            this.btnEdit.Size = new System.Drawing.Size(660, 23);
            this.btnEdit.TabIndex = 18;
            this.btnEdit.Text = "Edit Script";
            this.btnEdit.TextColor = System.Drawing.Color.White;
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnNew
            // 
            this.btnNew.BackColor = System.Drawing.Color.Transparent;
            this.btnNew.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(220)))), ((int)(((byte)(255)))));
            this.btnNew.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNew.Enabled = false;
            this.btnNew.FlatAppearance.BorderSize = 0;
            this.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNew.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnNew.ForeColor = System.Drawing.Color.White;
            this.btnNew.GlassColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(55)))), ((int)(((byte)(65)))));
            this.btnNew.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(85)))), ((int)(((byte)(95)))));
            this.btnNew.Location = new System.Drawing.Point(504, 49);
            this.btnNew.Name = "btnNew";
            this.btnNew.Selected = false;
            this.btnNew.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(140)))), ((int)(((byte)(190)))));
            this.btnNew.Size = new System.Drawing.Size(157, 44);
            this.btnNew.TabIndex = 14;
            this.btnNew.Text = "Save";
            this.btnNew.TextColor = System.Drawing.Color.White;
            this.btnNew.UseVisualStyleBackColor = false;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.Color.Transparent;
            this.txtName.Location = new System.Drawing.Point(42, 119);
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = false;
            this.txtName.Size = new System.Drawing.Size(247, 35);
            this.txtName.TabIndex = 13;
            this.txtName.UseSystemPasswordChar = false;
            // 
            // dateModified
            // 
            this.dateModified.DateText = "08 10 2026";
            this.dateModified.DateValue = new System.DateTime(2026, 10, 8, 0, 0, 0, 0);
            this.dateModified.Location = new System.Drawing.Point(42, 177);
            this.dateModified.Name = "dateModified";
            this.dateModified.Size = new System.Drawing.Size(235, 35);
            this.dateModified.TabIndex = 11;
            // 
            // txtVersion
            // 
            this.txtVersion.BackColor = System.Drawing.Color.Transparent;
            this.txtVersion.Location = new System.Drawing.Point(42, 233);
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.ReadOnly = false;
            this.txtVersion.Size = new System.Drawing.Size(82, 35);
            this.txtVersion.TabIndex = 4;
            this.txtVersion.UseSystemPasswordChar = false;
            // 
            // ddlListSP
            // 
            this.ddlListSP.BackColor = System.Drawing.Color.Transparent;
            this.ddlListSP.DataSource = null;
            this.ddlListSP.DisplayMember = "";
            this.ddlListSP.Location = new System.Drawing.Point(42, 58);
            this.ddlListSP.Name = "ddlListSP";
            this.ddlListSP.SelectedIndex = -1;
            this.ddlListSP.SelectedItem = null;
            this.ddlListSP.SelectedValue = null;
            this.ddlListSP.Size = new System.Drawing.Size(247, 38);
            this.ddlListSP.TabIndex = 3;
            this.ddlListSP.ValueMember = "";
            this.ddlListSP.SelectedIndexChanged += new System.EventHandler(this.ddlListSP_SelectedIndexChanged);
            this.ddlListSP.Load += new System.EventHandler(this.ddlListSP_Load);
            // 
            // FrmUpdateSP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(36)))), ((int)(((byte)(71)))));
            this.ClientSize = new System.Drawing.Size(733, 701);
            this.Controls.Add(this.lblDeployProgress);
            this.Controls.Add(this.progressBarDeploy);
            this.Controls.Add(this.txtRemark);
            this.Controls.Add(this.lblRemark);
            this.Controls.Add(this.btnUpdateAll);
            this.Controls.Add(this.btnUpdateScript);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.chkNewSP);
            this.Controls.Add(this.chkNew);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblProdName);
            this.Controls.Add(this.dateModified);
            this.Controls.Add(this.chkActive);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rtbScript);
            this.Controls.Add(this.txtVersion);
            this.Controls.Add(this.ddlListSP);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblProdList);
            this.Controls.Add(this.lblTitle);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmUpdateSP";
            this.Text = "FrmUpdateSP";
            this.Load += new System.EventHandler(this.FrmUpdateSP_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblProdList;
        private System.Windows.Forms.Label label3;
        private Controls.ModernComboBox ddlListSP;
        private Controls.ModernTextBox txtVersion;
        private System.Windows.Forms.RichTextBox rtbScript;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkActive;
        private Controls.GlassDatePicker dateModified;
        private System.Windows.Forms.Label lblProdName;
        private Controls.ModernTextBox txtName;
        private Controls.GlassButton btnNew;
        private System.Windows.Forms.CheckBox chkNewSP;
        private System.Windows.Forms.Label chkNew;
        private Controls.GlassButton btnEdit;
        private Controls.GlassButton btnUpdateScript;
        private Controls.GlassButton btnUpdateAll;
        private Controls.ModernTextBox txtRemark;
        private System.Windows.Forms.Label lblRemark;
        private System.Windows.Forms.ProgressBar progressBarDeploy;
        private System.Windows.Forms.Label lblDeployProgress;
    }
}