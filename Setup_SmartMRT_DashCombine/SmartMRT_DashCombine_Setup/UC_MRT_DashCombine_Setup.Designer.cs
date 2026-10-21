namespace SmartMRT_DashCombine_Setup
{
    partial class UC_MRT_DashCombine_Setup
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
            this.flowLineList = new System.Windows.Forms.FlowLayoutPanel();
            this.textBox1 = new SmartMRT_DashCombine_Setup.Controls.ModernTextBox();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.chkMaster = new System.Windows.Forms.CheckBox();
            this.txtMaster = new SmartMRT_DashCombine_Setup.Controls.ModernTextBox();
            this.lblMaster = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.chkStatus = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtIpAddress = new SmartMRT_DashCombine_Setup.Controls.ModernTextBox();
            this.txtLine = new SmartMRT_DashCombine_Setup.Controls.ModernTextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblLine = new System.Windows.Forms.Label();
            this.flowLineList.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLineList
            // 
            this.flowLineList.AutoScroll = true;
            this.flowLineList.Controls.Add(this.textBox1);
            this.flowLineList.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLineList.Location = new System.Drawing.Point(0, 0);
            this.flowLineList.Name = "flowLineList";
            this.flowLineList.Padding = new System.Windows.Forms.Padding(5);
            this.flowLineList.Size = new System.Drawing.Size(1193, 675);
            this.flowLineList.TabIndex = 12;
            this.flowLineList.Paint += new System.Windows.Forms.PaintEventHandler(this.flowLineList_Paint);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.Transparent;
            this.textBox1.Location = new System.Drawing.Point(8, 8);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = false;
            this.textBox1.Size = new System.Drawing.Size(437, 38);
            this.textBox1.TabIndex = 0;
            this.textBox1.UseSystemPasswordChar = false;
            // 
            // pnlHeader
            // 
            this.pnlHeader.Controls.Add(this.label4);
            this.pnlHeader.Controls.Add(this.label3);
            this.pnlHeader.Controls.Add(this.chkMaster);
            this.pnlHeader.Controls.Add(this.txtMaster);
            this.pnlHeader.Controls.Add(this.lblMaster);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Controls.Add(this.lblSubtitle);
            this.pnlHeader.Controls.Add(this.btnClear);
            this.pnlHeader.Controls.Add(this.chkStatus);
            this.pnlHeader.Controls.Add(this.label2);
            this.pnlHeader.Controls.Add(this.txtIpAddress);
            this.pnlHeader.Controls.Add(this.txtLine);
            this.pnlHeader.Controls.Add(this.btnSave);
            this.pnlHeader.Controls.Add(this.label1);
            this.pnlHeader.Controls.Add(this.lblLine);
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1193, 188);
            this.pnlHeader.TabIndex = 109;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(170)))), ((int)(((byte)(190)))));
            this.label4.Location = new System.Drawing.Point(533, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 15);
            this.label4.TabIndex = 123;
            this.label4.Text = "LINE MASTER : ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(373, 142);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(137, 15);
            this.label3.TabIndex = 122;
            this.label3.Text = "*Tick if line is standalone";
            // 
            // chkMaster
            // 
            this.chkMaster.AutoSize = true;
            this.chkMaster.Checked = true;
            this.chkMaster.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMaster.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkMaster.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(170)))), ((int)(((byte)(190)))));
            this.chkMaster.Location = new System.Drawing.Point(458, 122);
            this.chkMaster.Name = "chkMaster";
            this.chkMaster.Size = new System.Drawing.Size(69, 21);
            this.chkMaster.TabIndex = 121;
            this.chkMaster.Text = "Master";
            this.chkMaster.UseVisualStyleBackColor = true;
            this.chkMaster.CheckedChanged += new System.EventHandler(this.chkMaster_CheckedChanged);
            // 
            // txtMaster
            // 
            this.txtMaster.BackColor = System.Drawing.Color.Transparent;
            this.txtMaster.Enabled = false;
            this.txtMaster.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaster.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(170)))), ((int)(((byte)(190)))));
            this.txtMaster.Location = new System.Drawing.Point(533, 117);
            this.txtMaster.Name = "txtMaster";
            this.txtMaster.ReadOnly = false;
            this.txtMaster.Size = new System.Drawing.Size(102, 34);
            this.txtMaster.TabIndex = 120;
            this.txtMaster.UseSystemPasswordChar = false;
            // 
            // lblMaster
            // 
            this.lblMaster.AutoSize = true;
            this.lblMaster.BackColor = System.Drawing.Color.Transparent;
            this.lblMaster.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaster.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(170)))), ((int)(((byte)(190)))));
            this.lblMaster.Location = new System.Drawing.Point(373, 125);
            this.lblMaster.Name = "lblMaster";
            this.lblMaster.Size = new System.Drawing.Size(87, 15);
            this.lblMaster.TabIndex = 119;
            this.lblMaster.Text = "LINE MASTER : ";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(34, 26);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(145, 32);
            this.lblTitle.TabIndex = 118;
            this.lblTitle.Text = "LINE SETUP";
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(170)))), ((int)(((byte)(190)))));
            this.lblSubtitle.Location = new System.Drawing.Point(43, 58);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(275, 15);
            this.lblSubtitle.TabIndex = 117;
            this.lblSubtitle.Text = "Configure and monitor SmartMRT production lines";
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.White;
            this.btnClear.Location = new System.Drawing.Point(664, 86);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(183, 23);
            this.btnClear.TabIndex = 112;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            // 
            // chkStatus
            // 
            this.chkStatus.AutoSize = true;
            this.chkStatus.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(170)))), ((int)(((byte)(190)))));
            this.chkStatus.Location = new System.Drawing.Point(425, 91);
            this.chkStatus.Name = "chkStatus";
            this.chkStatus.Size = new System.Drawing.Size(64, 21);
            this.chkStatus.TabIndex = 116;
            this.chkStatus.Text = "Active";
            this.chkStatus.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(170)))), ((int)(((byte)(190)))));
            this.label2.Location = new System.Drawing.Point(375, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 15);
            this.label2.TabIndex = 115;
            this.label2.Text = "STATUS : ";
            // 
            // txtIpAddress
            // 
            this.txtIpAddress.BackColor = System.Drawing.Color.Transparent;
            this.txtIpAddress.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIpAddress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(170)))), ((int)(((byte)(190)))));
            this.txtIpAddress.Location = new System.Drawing.Point(154, 117);
            this.txtIpAddress.Name = "txtIpAddress";
            this.txtIpAddress.ReadOnly = false;
            this.txtIpAddress.Size = new System.Drawing.Size(196, 34);
            this.txtIpAddress.TabIndex = 114;
            this.txtIpAddress.UseSystemPasswordChar = false;
            // 
            // txtLine
            // 
            this.txtLine.BackColor = System.Drawing.Color.Transparent;
            this.txtLine.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLine.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(170)))), ((int)(((byte)(190)))));
            this.txtLine.Location = new System.Drawing.Point(46, 117);
            this.txtLine.Name = "txtLine";
            this.txtLine.ReadOnly = false;
            this.txtLine.Size = new System.Drawing.Size(102, 34);
            this.txtLine.TabIndex = 113;
            this.txtLine.UseSystemPasswordChar = false;
            this.txtLine.Load += new System.EventHandler(this.txtLine_Load);
            this.txtLine.TextChanged += new System.EventHandler(this.txtLine_TextChanged);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(664, 125);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(183, 23);
            this.btnSave.TabIndex = 111;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(170)))), ((int)(((byte)(190)))));
            this.label1.Location = new System.Drawing.Point(151, 94);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 15);
            this.label1.TabIndex = 110;
            this.label1.Text = "IP ADDRESS : ";
            // 
            // lblLine
            // 
            this.lblLine.AutoSize = true;
            this.lblLine.BackColor = System.Drawing.Color.Transparent;
            this.lblLine.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLine.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(170)))), ((int)(((byte)(190)))));
            this.lblLine.Location = new System.Drawing.Point(44, 94);
            this.lblLine.Name = "lblLine";
            this.lblLine.Size = new System.Drawing.Size(40, 15);
            this.lblLine.TabIndex = 109;
            this.lblLine.Text = "LINE : ";
            // 
            // UC_MRT_DashCombine_Setup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(21)))), ((int)(((byte)(47)))));
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.flowLineList);
            this.Name = "UC_MRT_DashCombine_Setup";
            this.Size = new System.Drawing.Size(1193, 675);
            this.Load += new System.EventHandler(this.UC_MRT_DashCombine_Setup_Load);
            this.Resize += new System.EventHandler(this.UC_MRT_DashCombine_Setup_Resize);
            this.flowLineList.ResumeLayout(false);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.FlowLayoutPanel flowLineList;
        private SmartMRT_DashCombine_Setup.Controls.ModernTextBox textBox1;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.CheckBox chkStatus;
        private System.Windows.Forms.Label label2;
        private Controls.ModernTextBox txtIpAddress;
        private Controls.ModernTextBox txtLine;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblLine;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkMaster;
        private Controls.ModernTextBox txtMaster;
        private System.Windows.Forms.Label lblMaster;
        private System.Windows.Forms.Label label4;
    }
}
