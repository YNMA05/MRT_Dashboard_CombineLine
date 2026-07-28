namespace SmartMRT_DashCombine_Setup
{
    partial class UC_MRT_DashCombine_View
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
            this.cbLineNum = new System.Windows.Forms.ComboBox();
            this.flowModuleList = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLineList = new System.Windows.Forms.FlowLayoutPanel();
            this.lblSelectedModule = new System.Windows.Forms.Label();
            this.flowLineList.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbLineNum
            // 
            this.cbLineNum.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbLineNum.FormattingEnabled = true;
            this.cbLineNum.Location = new System.Drawing.Point(68, 59);
            this.cbLineNum.Name = "cbLineNum";
            this.cbLineNum.Size = new System.Drawing.Size(205, 29);
            this.cbLineNum.TabIndex = 0;
            this.cbLineNum.Text = "Choose Line Number";
            this.cbLineNum.SelectedIndexChanged += new System.EventHandler(this.cbLineNum_SelectedIndexChanged);
            // 
            // flowModuleList
            // 
            this.flowModuleList.Location = new System.Drawing.Point(78, 100);
            this.flowModuleList.Name = "flowModuleList";
            this.flowModuleList.Size = new System.Drawing.Size(1114, 324);
            this.flowModuleList.TabIndex = 1;
            // 
            // flowLineList
            // 
            this.flowLineList.Controls.Add(this.lblSelectedModule);
            this.flowLineList.Location = new System.Drawing.Point(77, 445);
            this.flowLineList.Name = "flowLineList";
            this.flowLineList.Size = new System.Drawing.Size(1114, 245);
            this.flowLineList.TabIndex = 2;
            // 
            // lblSelectedModule
            // 
            this.lblSelectedModule.AutoSize = true;
            this.lblSelectedModule.ForeColor = System.Drawing.Color.White;
            this.lblSelectedModule.Location = new System.Drawing.Point(3, 0);
            this.lblSelectedModule.Name = "lblSelectedModule";
            this.lblSelectedModule.Size = new System.Drawing.Size(94, 13);
            this.lblSelectedModule.TabIndex = 3;
            this.lblSelectedModule.Text = "lblSelectedModule";
            // 
            // UC_MRT_DashCombine_View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.Controls.Add(this.flowLineList);
            this.Controls.Add(this.flowModuleList);
            this.Controls.Add(this.cbLineNum);
            this.Name = "UC_MRT_DashCombine_View";
            this.Size = new System.Drawing.Size(1268, 766);
            this.Load += new System.EventHandler(this.UC_MRT_DashCombine_View_Load);
            this.flowLineList.ResumeLayout(false);
            this.flowLineList.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbLineNum;
        private System.Windows.Forms.FlowLayoutPanel flowModuleList;
        private System.Windows.Forms.FlowLayoutPanel flowLineList;
        private System.Windows.Forms.Label lblSelectedModule;
    }
}
