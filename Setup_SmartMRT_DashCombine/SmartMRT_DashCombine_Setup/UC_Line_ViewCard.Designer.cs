namespace SmartMRT_DashCombine_Setup
{
    partial class UC_Line_ViewCard
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
            this.lblLine = new System.Windows.Forms.Label();
            this.lblIPAddress = new System.Windows.Forms.Label();
            this.chkSelected = new System.Windows.Forms.CheckBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblLine
            // 
            this.lblLine.AutoSize = true;
            this.lblLine.ForeColor = System.Drawing.Color.White;
            this.lblLine.Location = new System.Drawing.Point(25, 14);
            this.lblLine.Name = "lblLine";
            this.lblLine.Size = new System.Drawing.Size(37, 13);
            this.lblLine.TabIndex = 0;
            this.lblLine.Text = "lblLine";
            // 
            // lblIPAddress
            // 
            this.lblIPAddress.AutoSize = true;
            this.lblIPAddress.ForeColor = System.Drawing.Color.White;
            this.lblIPAddress.Location = new System.Drawing.Point(25, 42);
            this.lblIPAddress.Name = "lblIPAddress";
            this.lblIPAddress.Size = new System.Drawing.Size(62, 13);
            this.lblIPAddress.TabIndex = 1;
            this.lblIPAddress.Text = "lblPAddress";
            // 
            // chkSelected
            // 
            this.chkSelected.AutoSize = true;
            this.chkSelected.ForeColor = System.Drawing.Color.White;
            this.chkSelected.Location = new System.Drawing.Point(28, 73);
            this.chkSelected.Name = "chkSelected";
            this.chkSelected.Size = new System.Drawing.Size(61, 17);
            this.chkSelected.TabIndex = 2;
            this.chkSelected.Text = "Include";
            this.chkSelected.UseVisualStyleBackColor = true;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(202, 14);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(35, 13);
            this.lblStatus.TabIndex = 3;
            this.lblStatus.Text = "label1";
            // 
            // UC_Line_ViewCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.chkSelected);
            this.Controls.Add(this.lblIPAddress);
            this.Controls.Add(this.lblLine);
            this.Name = "UC_Line_ViewCard";
            this.Size = new System.Drawing.Size(317, 107);
            this.Load += new System.EventHandler(this.UC_Line_ViewCard_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblLine;
        private System.Windows.Forms.Label lblIPAddress;
        private System.Windows.Forms.CheckBox chkSelected;
        private System.Windows.Forms.Label lblStatus;
    }
}
