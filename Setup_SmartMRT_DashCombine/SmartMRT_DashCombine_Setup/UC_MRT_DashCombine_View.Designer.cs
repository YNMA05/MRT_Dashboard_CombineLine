using SmartMRT_DashCombine_Setup.Controls;
using System.Drawing;
using System.Windows.Forms;

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
            this.lblSelectedModule = new System.Windows.Forms.Label();
            this.cbLineNum = new System.Windows.Forms.ComboBox();
            this.pnlHeader = new SmartMRT_DashCombine_Setup.Controls.GlassPanel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.pnlModuleGlass = new SmartMRT_DashCombine_Setup.Controls.GlassPanel();
            this.flowModuleList = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlLineGlass = new SmartMRT_DashCombine_Setup.Controls.GlassPanel();
            this.flowLineList = new System.Windows.Forms.FlowLayoutPanel();
            this.lblPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.lblLine = new System.Windows.Forms.Label();
            this.pnlHeader.SuspendLayout();
            this.pnlModuleGlass.SuspendLayout();
            this.pnlLineGlass.SuspendLayout();
            this.lblPanel.SuspendLayout();
            this.SuspendLayout();
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
            // cbLineNum
            // 
            this.cbLineNum.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbLineNum.FormattingEnabled = true;
            this.cbLineNum.Location = new System.Drawing.Point(-462, -265);
            this.cbLineNum.Name = "cbLineNum";
            this.cbLineNum.Size = new System.Drawing.Size(205, 29);
            this.cbLineNum.TabIndex = 3;
            this.cbLineNum.Text = "Choose Line Number";
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.Transparent;
            this.pnlHeader.BorderColor = System.Drawing.Color.Transparent;
            this.pnlHeader.BorderThickness = 1.2F;
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Controls.Add(this.lblSubtitle);
            this.pnlHeader.CornerRadius = 18;
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.GlassColor = System.Drawing.Color.Transparent;
            this.pnlHeader.GlassOpacity = 180;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(30, 30, 10, 10);
            this.pnlHeader.Size = new System.Drawing.Size(1193, 68);
            this.pnlHeader.TabIndex = 110;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(30, 21);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(272, 32);
            this.lblTitle.TabIndex = 111;
            this.lblTitle.Text = "DASHBOARD MODULE";
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(170)))), ((int)(((byte)(190)))));
            this.lblSubtitle.Location = new System.Drawing.Point(33, 54);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(296, 15);
            this.lblSubtitle.TabIndex = 110;
            this.lblSubtitle.Text = "Select a module to view the assigned production lines  ";
            // 
            // pnlModuleGlass
            // 
            this.pnlModuleGlass.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlModuleGlass.BackColor = System.Drawing.Color.Transparent;
            this.pnlModuleGlass.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.pnlModuleGlass.BorderThickness = 1.2F;
            this.pnlModuleGlass.Controls.Add(this.flowModuleList);
            this.pnlModuleGlass.CornerRadius = 18;
            this.pnlModuleGlass.GlassColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(25)))), ((int)(((byte)(35)))));
            this.pnlModuleGlass.GlassOpacity = 45;
            this.pnlModuleGlass.Location = new System.Drawing.Point(32, 73);
            this.pnlModuleGlass.Name = "pnlModuleGlass";
            this.pnlModuleGlass.Padding = new System.Windows.Forms.Padding(10, 15, 10, 15);
            this.pnlModuleGlass.Size = new System.Drawing.Size(1109, 372);
            this.pnlModuleGlass.TabIndex = 0;
            // 
            // flowModuleList
            // 
            this.flowModuleList.AutoScroll = true;
            this.flowModuleList.BackColor = System.Drawing.Color.Transparent;
            this.flowModuleList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowModuleList.Location = new System.Drawing.Point(10, 15);
            this.flowModuleList.Name = "flowModuleList";
            this.flowModuleList.Size = new System.Drawing.Size(1089, 342);
            this.flowModuleList.TabIndex = 0;
            // 
            // pnlLineGlass
            // 
            this.pnlLineGlass.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlLineGlass.BackColor = System.Drawing.Color.Transparent;
            this.pnlLineGlass.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.pnlLineGlass.BorderThickness = 1.2F;
            this.pnlLineGlass.Controls.Add(this.flowLineList);
            this.pnlLineGlass.CornerRadius = 18;
            this.pnlLineGlass.GlassColor = System.Drawing.Color.Transparent;
            this.pnlLineGlass.GlassOpacity = 45;
            this.pnlLineGlass.Location = new System.Drawing.Point(32, 487);
            this.pnlLineGlass.Name = "pnlLineGlass";
            this.pnlLineGlass.Padding = new System.Windows.Forms.Padding(20, 15, 20, 15);
            this.pnlLineGlass.Size = new System.Drawing.Size(1109, 238);
            this.pnlLineGlass.TabIndex = 1;
            // 
            // flowLineList
            // 
            this.flowLineList.AutoScroll = true;
            this.flowLineList.BackColor = System.Drawing.Color.Transparent;
            this.flowLineList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLineList.Location = new System.Drawing.Point(20, 15);
            this.flowLineList.Name = "flowLineList";
            this.flowLineList.Size = new System.Drawing.Size(1069, 208);
            this.flowLineList.TabIndex = 0;
            this.flowLineList.Paint += new System.Windows.Forms.PaintEventHandler(this.flowLineList_Paint);
            // 
            // lblPanel
            // 
            this.lblPanel.Controls.Add(this.lblLine);
            this.lblPanel.Location = new System.Drawing.Point(34, 451);
            this.lblPanel.Name = "lblPanel";
            this.lblPanel.Size = new System.Drawing.Size(1107, 35);
            this.lblPanel.TabIndex = 112;
            // 
            // lblLine
            // 
            this.lblLine.AutoSize = true;
            this.lblLine.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLine.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblLine.Location = new System.Drawing.Point(3, 0);
            this.lblLine.Name = "lblLine";
            this.lblLine.Size = new System.Drawing.Size(127, 25);
            this.lblLine.TabIndex = 1;
            this.lblLine.Text = "All Active Line";
            // 
            // UC_MRT_DashCombine_View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(21)))), ((int)(((byte)(47)))));
            this.Controls.Add(this.lblPanel);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlModuleGlass);
            this.Controls.Add(this.pnlLineGlass);
            this.Name = "UC_MRT_DashCombine_View";
            this.Size = new System.Drawing.Size(1193, 766);
            this.Load += new System.EventHandler(this.UC_MRT_DashCombine_View_Load);
            this.Resize += new System.EventHandler(this.UC_MRT_DashCombine_View_Resize);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlModuleGlass.ResumeLayout(false);
            this.pnlLineGlass.ResumeLayout(false);
            this.lblPanel.ResumeLayout(false);
            this.lblPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private SmartMRT_DashCombine_Setup.Controls.GlassPanel pnlLineGlass;
        private SmartMRT_DashCombine_Setup.Controls.GlassPanel pnlModuleGlass;
        private System.Windows.Forms.FlowLayoutPanel flowLineList;
        private System.Windows.Forms.Label lblSelectedModule;
        private System.Windows.Forms.FlowLayoutPanel flowModuleList;
        private System.Windows.Forms.ComboBox cbLineNum;
        private GlassPanel pnlHeader;
        private Label lblTitle;
        private Label lblSubtitle;
        private FlowLayoutPanel lblPanel;
        private Label lblLine;
    }
}
