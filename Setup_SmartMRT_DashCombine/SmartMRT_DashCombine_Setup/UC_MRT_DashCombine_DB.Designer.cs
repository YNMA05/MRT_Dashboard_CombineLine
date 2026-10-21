using SmartMRT_DashCombine_Setup.Controls;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SmartMRT_DashCombine_Setup
{
    partial class UC_MRT_DashCombine_DB
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.pnlGlass = new SmartMRT_DashCombine_Setup.Controls.GlassPanel();
            this.rbutton2 = new System.Windows.Forms.RadioButton();
            this.rButton1 = new System.Windows.Forms.RadioButton();
            this.lblDbLoc = new System.Windows.Forms.Label();
            this.btnUpdateIP = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDb = new SmartMRT_DashCombine_Setup.Controls.ModernTextBox();
            this.txtInstance = new SmartMRT_DashCombine_Setup.Controls.ModernTextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnTestConnection = new System.Windows.Forms.Button();
            this.txtPwd = new SmartMRT_DashCombine_Setup.Controls.ModernTextBox();
            this.txtUsername = new SmartMRT_DashCombine_Setup.Controls.ModernTextBox();
            this.txtIP = new SmartMRT_DashCombine_Setup.Controls.ModernTextBox();
            this.btnCreateTable = new System.Windows.Forms.Button();
            this.btnCreateDb = new System.Windows.Forms.Button();
            this.lblConnectionStatus = new System.Windows.Forms.Label();
            this.pnlGlass.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(39, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(299, 32);
            this.lblTitle.TabIndex = 106;
            this.lblTitle.Text = "DATABASE CONNECTION";
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(170)))), ((int)(((byte)(190)))));
            this.lblSubtitle.Location = new System.Drawing.Point(43, 52);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(291, 15);
            this.lblSubtitle.TabIndex = 105;
            this.lblSubtitle.Text = "Configure the database used by SmartMRT Dashboard";
            // 
            // pnlGlass
            // 
            this.pnlGlass.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlGlass.BackColor = System.Drawing.Color.Transparent;
            this.pnlGlass.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.pnlGlass.BorderThickness = 1.2F;
            this.pnlGlass.Controls.Add(this.rbutton2);
            this.pnlGlass.Controls.Add(this.rButton1);
            this.pnlGlass.Controls.Add(this.lblDbLoc);
            this.pnlGlass.Controls.Add(this.btnUpdateIP);
            this.pnlGlass.Controls.Add(this.label5);
            this.pnlGlass.Controls.Add(this.label4);
            this.pnlGlass.Controls.Add(this.label3);
            this.pnlGlass.Controls.Add(this.label2);
            this.pnlGlass.Controls.Add(this.label1);
            this.pnlGlass.Controls.Add(this.txtDb);
            this.pnlGlass.Controls.Add(this.txtInstance);
            this.pnlGlass.Controls.Add(this.btnSave);
            this.pnlGlass.Controls.Add(this.btnTestConnection);
            this.pnlGlass.Controls.Add(this.txtPwd);
            this.pnlGlass.Controls.Add(this.txtUsername);
            this.pnlGlass.Controls.Add(this.txtIP);
            this.pnlGlass.Controls.Add(this.btnCreateTable);
            this.pnlGlass.Controls.Add(this.btnCreateDb);
            this.pnlGlass.Controls.Add(this.lblConnectionStatus);
            this.pnlGlass.CornerRadius = 18;
            this.pnlGlass.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlGlass.GlassColor = System.Drawing.Color.Transparent;
            this.pnlGlass.GlassOpacity = 90;
            this.pnlGlass.Location = new System.Drawing.Point(45, 79);
            this.pnlGlass.Name = "pnlGlass";
            this.pnlGlass.Padding = new System.Windows.Forms.Padding(25);
            this.pnlGlass.Size = new System.Drawing.Size(1087, 581);
            this.pnlGlass.TabIndex = 0;
            // 
            // rbutton2
            // 
            this.rbutton2.AutoSize = true;
            this.rbutton2.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbutton2.ForeColor = System.Drawing.Color.White;
            this.rbutton2.Location = new System.Drawing.Point(258, 76);
            this.rbutton2.Name = "rbutton2";
            this.rbutton2.Size = new System.Drawing.Size(156, 24);
            this.rbutton2.TabIndex = 113;
            this.rbutton2.TabStop = true;
            this.rbutton2.Text = "Another PC / Server";
            this.rbutton2.UseVisualStyleBackColor = true;
            this.rbutton2.CheckedChanged += new System.EventHandler(this.rbutton2_CheckedChanged);
            // 
            // rButton1
            // 
            this.rButton1.AutoSize = true;
            this.rButton1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rButton1.ForeColor = System.Drawing.Color.White;
            this.rButton1.Location = new System.Drawing.Point(258, 49);
            this.rButton1.Name = "rButton1";
            this.rButton1.Size = new System.Drawing.Size(74, 24);
            this.rButton1.TabIndex = 112;
            this.rButton1.TabStop = true;
            this.rButton1.Text = "This PC";
            this.rButton1.UseVisualStyleBackColor = true;
            this.rButton1.CheckedChanged += new System.EventHandler(this.rButton1_CheckedChanged);
            // 
            // lblDbLoc
            // 
            this.lblDbLoc.AutoSize = true;
            this.lblDbLoc.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblDbLoc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(170)))), ((int)(((byte)(190)))));
            this.lblDbLoc.Location = new System.Drawing.Point(235, 25);
            this.lblDbLoc.Name = "lblDbLoc";
            this.lblDbLoc.Size = new System.Drawing.Size(153, 19);
            this.lblDbLoc.TabIndex = 111;
            this.lblDbLoc.Text = "DATABASE LOCATION";
            // 
            // btnUpdateIP
            // 
            this.btnUpdateIP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnUpdateIP.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnUpdateIP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdateIP.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdateIP.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnUpdateIP.Location = new System.Drawing.Point(528, 136);
            this.btnUpdateIP.Name = "btnUpdateIP";
            this.btnUpdateIP.Size = new System.Drawing.Size(207, 36);
            this.btnUpdateIP.TabIndex = 110;
            this.btnUpdateIP.Text = "Update IP";
            this.btnUpdateIP.UseVisualStyleBackColor = false;
            this.btnUpdateIP.Visible = false;
            this.btnUpdateIP.Click += new System.EventHandler(this.btnUpdateIP_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(170)))), ((int)(((byte)(190)))));
            this.label5.Location = new System.Drawing.Point(562, 313);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 19);
            this.label5.TabIndex = 109;
            this.label5.Text = "PASSWORD";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(170)))), ((int)(((byte)(190)))));
            this.label4.Location = new System.Drawing.Point(562, 227);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 19);
            this.label4.TabIndex = 108;
            this.label4.Text = "USERNAME";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(170)))), ((int)(((byte)(190)))));
            this.label3.Location = new System.Drawing.Point(230, 312);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 19);
            this.label3.TabIndex = 107;
            this.label3.Text = "SQL INSTANCE";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(170)))), ((int)(((byte)(190)))));
            this.label2.Location = new System.Drawing.Point(230, 227);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 19);
            this.label2.TabIndex = 106;
            this.label2.Text = "DATABASE NAME";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(170)))), ((int)(((byte)(190)))));
            this.label1.Location = new System.Drawing.Point(230, 116);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 19);
            this.label1.TabIndex = 105;
            this.label1.Text = "SERVER";
            // 
            // txtDb
            // 
            this.txtDb.BackColor = System.Drawing.Color.Transparent;
            this.txtDb.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDb.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(180)))), ((int)(((byte)(200)))));
            this.txtDb.Location = new System.Drawing.Point(233, 247);
            this.txtDb.Name = "txtDb";
            this.txtDb.ReadOnly = false;
            this.txtDb.Size = new System.Drawing.Size(274, 34);
            this.txtDb.TabIndex = 26;
            this.txtDb.UseSystemPasswordChar = false;
            // 
            // txtInstance
            // 
            this.txtInstance.BackColor = System.Drawing.Color.Transparent;
            this.txtInstance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInstance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(180)))), ((int)(((byte)(200)))));
            this.txtInstance.Location = new System.Drawing.Point(233, 332);
            this.txtInstance.Name = "txtInstance";
            this.txtInstance.ReadOnly = false;
            this.txtInstance.Size = new System.Drawing.Size(274, 34);
            this.txtInstance.TabIndex = 24;
            this.txtInstance.UseSystemPasswordChar = false;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnSave.Location = new System.Drawing.Point(565, 486);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(274, 35);
            this.btnSave.TabIndex = 23;
            this.btnSave.Text = "Save Setup";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnTestConnection
            // 
            this.btnTestConnection.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnTestConnection.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnTestConnection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTestConnection.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTestConnection.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnTestConnection.Location = new System.Drawing.Point(565, 444);
            this.btnTestConnection.Name = "btnTestConnection";
            this.btnTestConnection.Size = new System.Drawing.Size(274, 36);
            this.btnTestConnection.TabIndex = 22;
            this.btnTestConnection.Text = "Test Connection ";
            this.btnTestConnection.UseVisualStyleBackColor = false;
            this.btnTestConnection.Click += new System.EventHandler(this.btnTestConnection_Click);
            // 
            // txtPwd
            // 
            this.txtPwd.BackColor = System.Drawing.Color.Transparent;
            this.txtPwd.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPwd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(180)))), ((int)(((byte)(200)))));
            this.txtPwd.Location = new System.Drawing.Point(565, 333);
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.ReadOnly = false;
            this.txtPwd.Size = new System.Drawing.Size(274, 34);
            this.txtPwd.TabIndex = 20;
            this.txtPwd.UseSystemPasswordChar = true;
            // 
            // txtUsername
            // 
            this.txtUsername.BackColor = System.Drawing.Color.Transparent;
            this.txtUsername.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsername.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(180)))), ((int)(((byte)(200)))));
            this.txtUsername.Location = new System.Drawing.Point(565, 247);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.ReadOnly = false;
            this.txtUsername.Size = new System.Drawing.Size(274, 34);
            this.txtUsername.TabIndex = 18;
            this.txtUsername.UseSystemPasswordChar = false;
            // 
            // txtIP
            // 
            this.txtIP.AutoSize = true;
            this.txtIP.BackColor = System.Drawing.Color.Transparent;
            this.txtIP.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(180)))), ((int)(((byte)(200)))));
            this.txtIP.Location = new System.Drawing.Point(233, 136);
            this.txtIP.Margin = new System.Windows.Forms.Padding(0);
            this.txtIP.Name = "txtIP";
            this.txtIP.ReadOnly = false;
            this.txtIP.Size = new System.Drawing.Size(274, 34);
            this.txtIP.TabIndex = 16;
            this.txtIP.UseSystemPasswordChar = false;
            // 
            // btnCreateTable
            // 
            this.btnCreateTable.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))));
            this.btnCreateTable.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnCreateTable.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateTable.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnCreateTable.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(220)))), ((int)(((byte)(240)))));
            this.btnCreateTable.Location = new System.Drawing.Point(233, 486);
            this.btnCreateTable.Name = "btnCreateTable";
            this.btnCreateTable.Size = new System.Drawing.Size(274, 35);
            this.btnCreateTable.TabIndex = 15;
            this.btnCreateTable.Text = "Create Dashboard Table";
            this.btnCreateTable.UseVisualStyleBackColor = false;
            this.btnCreateTable.Click += new System.EventHandler(this.btnCreateTable_Click);
            // 
            // btnCreateDb
            // 
            this.btnCreateDb.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))));
            this.btnCreateDb.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnCreateDb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateDb.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateDb.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(220)))), ((int)(((byte)(240)))));
            this.btnCreateDb.Location = new System.Drawing.Point(233, 444);
            this.btnCreateDb.Name = "btnCreateDb";
            this.btnCreateDb.Size = new System.Drawing.Size(274, 36);
            this.btnCreateDb.TabIndex = 14;
            this.btnCreateDb.Text = "Create Dashboard Database";
            this.btnCreateDb.UseVisualStyleBackColor = false;
            this.btnCreateDb.Click += new System.EventHandler(this.btnCreateDb_Click);
            // 
            // lblConnectionStatus
            // 
            this.lblConnectionStatus.AutoSize = true;
            this.lblConnectionStatus.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.lblConnectionStatus.ForeColor = System.Drawing.Color.LightYellow;
            this.lblConnectionStatus.Location = new System.Drawing.Point(707, 261);
            this.lblConnectionStatus.Name = "lblConnectionStatus";
            this.lblConnectionStatus.Size = new System.Drawing.Size(0, 20);
            this.lblConnectionStatus.TabIndex = 101;
            // 
            // UC_MRT_DashCombine_DB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(21)))), ((int)(((byte)(47)))));
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblSubtitle);
            this.Controls.Add(this.pnlGlass);
            this.Name = "UC_MRT_DashCombine_DB";
            this.Size = new System.Drawing.Size(1193, 675);
            this.pnlGlass.ResumeLayout(false);
            this.pnlGlass.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        //private GlassPanel pnlGlass;

        #endregion
        private SmartMRT_DashCombine_Setup.Controls.GlassPanel pnlGlass;
        private Label lblConnectionStatus;
        private SmartMRT_DashCombine_Setup.Controls.ModernTextBox txtDb;
        private SmartMRT_DashCombine_Setup.Controls.ModernTextBox txtInstance;
        private Button btnSave;
        private Button btnTestConnection;
        private SmartMRT_DashCombine_Setup.Controls.ModernTextBox txtPwd;
        private SmartMRT_DashCombine_Setup.Controls.ModernTextBox txtUsername;
        private SmartMRT_DashCombine_Setup.Controls.ModernTextBox txtIP;
        private Button btnCreateTable;
        private Button btnCreateDb;
        private Label label5;
        private Label label4;
        private Label lblTitle;
        private Label lblSubtitle;
        private Label label3;
        private Label label2;
        private Label label1;
        private Button btnUpdateIP;
        private Label lblDbLoc;
        private RadioButton rbutton2;
        private RadioButton rButton1;
    }
}
