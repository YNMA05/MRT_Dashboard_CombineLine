namespace SmartMRT_DashCombine_Setup
{
    partial class UC_MRT_DashCombine_Service
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
            this.glassPanel1 = new SmartMRT_DashCombine_Setup.Controls.GlassPanel();
            this.lblServiceStatus = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnRefreshStatus = new SmartMRT_DashCombine_Setup.Controls.GlassButton();
            this.lblServiceRun = new System.Windows.Forms.Label();
            this.glassPanel2 = new SmartMRT_DashCombine_Setup.Controls.GlassPanel();
            this.btnRestart = new SmartMRT_DashCombine_Setup.Controls.GlassButton();
            this.btnStart = new SmartMRT_DashCombine_Setup.Controls.GlassButton();
            this.btnInstall = new SmartMRT_DashCombine_Setup.Controls.GlassButton();
            this.glassPanel3 = new SmartMRT_DashCombine_Setup.Controls.GlassPanel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.glassPanel1.SuspendLayout();
            this.glassPanel2.SuspendLayout();
            this.glassPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // glassPanel1
            // 
            this.glassPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.glassPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(36)))), ((int)(((byte)(71)))));
            this.glassPanel1.BorderColor = System.Drawing.Color.Transparent;
            this.glassPanel1.BorderThickness = 0F;
            this.glassPanel1.Controls.Add(this.lblServiceStatus);
            this.glassPanel1.Controls.Add(this.lblStatus);
            this.glassPanel1.Controls.Add(this.btnRefreshStatus);
            this.glassPanel1.Controls.Add(this.lblServiceRun);
            this.glassPanel1.CornerRadius = 18;
            this.glassPanel1.GlassColor = System.Drawing.Color.Transparent;
            this.glassPanel1.GlassOpacity = 180;
            this.glassPanel1.Location = new System.Drawing.Point(0, 109);
            this.glassPanel1.Name = "glassPanel1";
            this.glassPanel1.Padding = new System.Windows.Forms.Padding(20);
            this.glassPanel1.Size = new System.Drawing.Size(1012, 113);
            this.glassPanel1.TabIndex = 113;
            // 
            // lblServiceStatus
            // 
            this.lblServiceStatus.AutoSize = true;
            this.lblServiceStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblServiceStatus.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServiceStatus.ForeColor = System.Drawing.Color.White;
            this.lblServiceStatus.Location = new System.Drawing.Point(265, 42);
            this.lblServiceStatus.Name = "lblServiceStatus";
            this.lblServiceStatus.Size = new System.Drawing.Size(59, 17);
            this.lblServiceStatus.TabIndex = 3;
            this.lblServiceStatus.Text = "Running";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblStatus.Location = new System.Drawing.Point(40, 27);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(136, 25);
            this.lblStatus.TabIndex = 2;
            this.lblStatus.Text = "Service Status";
            // 
            // btnRefreshStatus
            // 
            this.btnRefreshStatus.BackColor = System.Drawing.Color.Transparent;
            this.btnRefreshStatus.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(220)))), ((int)(((byte)(255)))));
            this.btnRefreshStatus.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefreshStatus.FlatAppearance.BorderSize = 0;
            this.btnRefreshStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefreshStatus.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnRefreshStatus.ForeColor = System.Drawing.Color.White;
            this.btnRefreshStatus.GlassColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(55)))), ((int)(((byte)(65)))));
            this.btnRefreshStatus.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(85)))), ((int)(((byte)(95)))));
            this.btnRefreshStatus.Location = new System.Drawing.Point(650, 34);
            this.btnRefreshStatus.Name = "btnRefreshStatus";
            this.btnRefreshStatus.Selected = false;
            this.btnRefreshStatus.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(140)))), ((int)(((byte)(190)))));
            this.btnRefreshStatus.Size = new System.Drawing.Size(132, 33);
            this.btnRefreshStatus.TabIndex = 1;
            this.btnRefreshStatus.Text = "Refresh";
            this.btnRefreshStatus.TextColor = System.Drawing.Color.White;
            this.btnRefreshStatus.UseVisualStyleBackColor = false;
            this.btnRefreshStatus.Click += new System.EventHandler(this.btnRefreshStatus_Click);
            // 
            // lblServiceRun
            // 
            this.lblServiceRun.AutoSize = true;
            this.lblServiceRun.BackColor = System.Drawing.Color.Transparent;
            this.lblServiceRun.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServiceRun.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lblServiceRun.Location = new System.Drawing.Point(42, 52);
            this.lblServiceRun.Name = "lblServiceRun";
            this.lblServiceRun.Size = new System.Drawing.Size(153, 17);
            this.lblServiceRun.TabIndex = 0;
            this.lblServiceRun.Text = "MRT_Dashboard Service";
            // 
            // glassPanel2
            // 
            this.glassPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.glassPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(36)))), ((int)(((byte)(71)))));
            this.glassPanel2.BorderColor = System.Drawing.Color.Transparent;
            this.glassPanel2.BorderThickness = 0F;
            this.glassPanel2.Controls.Add(this.btnRestart);
            this.glassPanel2.Controls.Add(this.btnStart);
            this.glassPanel2.Controls.Add(this.btnInstall);
            this.glassPanel2.CornerRadius = 18;
            this.glassPanel2.GlassColor = System.Drawing.Color.Transparent;
            this.glassPanel2.GlassOpacity = 180;
            this.glassPanel2.Location = new System.Drawing.Point(3, 228);
            this.glassPanel2.Name = "glassPanel2";
            this.glassPanel2.Padding = new System.Windows.Forms.Padding(20);
            this.glassPanel2.Size = new System.Drawing.Size(1006, 423);
            this.glassPanel2.TabIndex = 114;
            // 
            // btnRestart
            // 
            this.btnRestart.BackColor = System.Drawing.Color.Transparent;
            this.btnRestart.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(220)))), ((int)(((byte)(255)))));
            this.btnRestart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRestart.FlatAppearance.BorderSize = 0;
            this.btnRestart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRestart.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnRestart.ForeColor = System.Drawing.Color.White;
            this.btnRestart.GlassColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(55)))), ((int)(((byte)(65)))));
            this.btnRestart.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(85)))), ((int)(((byte)(95)))));
            this.btnRestart.Location = new System.Drawing.Point(93, 237);
            this.btnRestart.Name = "btnRestart";
            this.btnRestart.Selected = false;
            this.btnRestart.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(140)))), ((int)(((byte)(190)))));
            this.btnRestart.Size = new System.Drawing.Size(759, 67);
            this.btnRestart.TabIndex = 8;
            this.btnRestart.Text = "Click to Restart Service";
            this.btnRestart.TextColor = System.Drawing.Color.White;
            this.btnRestart.UseVisualStyleBackColor = false;
            this.btnRestart.Click += new System.EventHandler(this.btnRestart_Click);
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.Color.Transparent;
            this.btnStart.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(220)))), ((int)(((byte)(255)))));
            this.btnStart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStart.FlatAppearance.BorderSize = 0;
            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStart.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnStart.ForeColor = System.Drawing.Color.White;
            this.btnStart.GlassColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(55)))), ((int)(((byte)(65)))));
            this.btnStart.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(85)))), ((int)(((byte)(95)))));
            this.btnStart.Location = new System.Drawing.Point(93, 150);
            this.btnStart.Name = "btnStart";
            this.btnStart.Selected = false;
            this.btnStart.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(140)))), ((int)(((byte)(190)))));
            this.btnStart.Size = new System.Drawing.Size(759, 67);
            this.btnStart.TabIndex = 7;
            this.btnStart.Text = "Click to Start Service";
            this.btnStart.TextColor = System.Drawing.Color.White;
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnInstall
            // 
            this.btnInstall.BackColor = System.Drawing.Color.Transparent;
            this.btnInstall.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(220)))), ((int)(((byte)(255)))));
            this.btnInstall.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInstall.FlatAppearance.BorderSize = 0;
            this.btnInstall.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInstall.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnInstall.ForeColor = System.Drawing.Color.White;
            this.btnInstall.GlassColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(55)))), ((int)(((byte)(65)))));
            this.btnInstall.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(85)))), ((int)(((byte)(95)))));
            this.btnInstall.Location = new System.Drawing.Point(93, 67);
            this.btnInstall.Name = "btnInstall";
            this.btnInstall.Selected = false;
            this.btnInstall.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(140)))), ((int)(((byte)(190)))));
            this.btnInstall.Size = new System.Drawing.Size(759, 68);
            this.btnInstall.TabIndex = 6;
            this.btnInstall.Text = "Click to Install Service";
            this.btnInstall.TextColor = System.Drawing.Color.White;
            this.btnInstall.UseVisualStyleBackColor = false;
            this.btnInstall.Click += new System.EventHandler(this.btnInstall_Click);
            // 
            // glassPanel3
            // 
            this.glassPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.glassPanel3.BackColor = System.Drawing.Color.Transparent;
            this.glassPanel3.BorderColor = System.Drawing.Color.Transparent;
            this.glassPanel3.BorderThickness = 0F;
            this.glassPanel3.Controls.Add(this.lblTitle);
            this.glassPanel3.Controls.Add(this.lblSubtitle);
            this.glassPanel3.CornerRadius = 18;
            this.glassPanel3.GlassColor = System.Drawing.Color.Transparent;
            this.glassPanel3.GlassOpacity = 180;
            this.glassPanel3.Location = new System.Drawing.Point(0, 3);
            this.glassPanel3.Name = "glassPanel3";
            this.glassPanel3.Padding = new System.Windows.Forms.Padding(20);
            this.glassPanel3.Size = new System.Drawing.Size(1009, 100);
            this.glassPanel3.TabIndex = 115;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(23, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(283, 32);
            this.lblTitle.TabIndex = 114;
            this.lblTitle.Text = "SERVICE INSTALLATION";
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.BackColor = System.Drawing.Color.Transparent;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(170)))), ((int)(((byte)(190)))));
            this.lblSubtitle.Location = new System.Drawing.Point(27, 52);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(165, 15);
            this.lblSubtitle.TabIndex = 113;
            this.lblSubtitle.Text = "Installation Service Dashboard";
            // 
            // UC_MRT_DashCombine_Service
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.glassPanel3);
            this.Controls.Add(this.glassPanel2);
            this.Controls.Add(this.glassPanel1);
            this.Name = "UC_MRT_DashCombine_Service";
            this.Size = new System.Drawing.Size(1012, 654);
            this.glassPanel1.ResumeLayout(false);
            this.glassPanel1.PerformLayout();
            this.glassPanel2.ResumeLayout(false);
            this.glassPanel3.ResumeLayout(false);
            this.glassPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private Controls.GlassPanel glassPanel1;
        private System.Windows.Forms.Label lblStatus;
        private Controls.GlassButton btnRefreshStatus;
        private System.Windows.Forms.Label lblServiceRun;
        private System.Windows.Forms.Label lblServiceStatus;
        private Controls.GlassPanel glassPanel2;
        private Controls.GlassButton btnRestart;
        private Controls.GlassButton btnStart;
        private Controls.GlassButton btnInstall;
        private Controls.GlassPanel glassPanel3;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
    }
}
