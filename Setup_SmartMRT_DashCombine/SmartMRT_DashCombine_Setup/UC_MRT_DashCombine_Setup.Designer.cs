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
            this.lblLine = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.flowLineList = new System.Windows.Forms.FlowLayoutPanel();
            this.txtLine = new System.Windows.Forms.TextBox();
            this.txtIpAddress = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkStatus = new System.Windows.Forms.CheckBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblLine
            // 
            this.lblLine.AutoSize = true;
            this.lblLine.BackColor = System.Drawing.Color.Transparent;
            this.lblLine.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLine.ForeColor = System.Drawing.Color.White;
            this.lblLine.Location = new System.Drawing.Point(57, 48);
            this.lblLine.Name = "lblLine";
            this.lblLine.Size = new System.Drawing.Size(50, 20);
            this.lblLine.TabIndex = 9;
            this.lblLine.Text = "LINE : ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(57, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 20);
            this.label1.TabIndex = 10;
            this.label1.Text = "IP ADDRESS : ";
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(477, 58);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(183, 23);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // flowLineList
            // 
            this.flowLineList.AutoScroll = true;
            this.flowLineList.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLineList.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLineList.Location = new System.Drawing.Point(0, 221);
            this.flowLineList.Name = "flowLineList";
            this.flowLineList.Padding = new System.Windows.Forms.Padding(10);
            this.flowLineList.Size = new System.Drawing.Size(1045, 426);
            this.flowLineList.TabIndex = 12;
            this.flowLineList.WrapContents = false;
            // 
            // txtLine
            // 
            this.txtLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLine.Location = new System.Drawing.Point(184, 48);
            this.txtLine.Name = "txtLine";
            this.txtLine.Size = new System.Drawing.Size(251, 24);
            this.txtLine.TabIndex = 13;
            // 
            // txtIpAddress
            // 
            this.txtIpAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIpAddress.Location = new System.Drawing.Point(184, 89);
            this.txtIpAddress.Name = "txtIpAddress";
            this.txtIpAddress.Size = new System.Drawing.Size(251, 24);
            this.txtIpAddress.TabIndex = 14;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(61, 165);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(286, 20);
            this.textBox1.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(57, 133);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 20);
            this.label2.TabIndex = 16;
            this.label2.Text = "STATUS : ";
            // 
            // chkStatus
            // 
            this.chkStatus.AutoSize = true;
            this.chkStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkStatus.Location = new System.Drawing.Point(184, 133);
            this.chkStatus.Name = "chkStatus";
            this.chkStatus.Size = new System.Drawing.Size(15, 14);
            this.chkStatus.TabIndex = 17;
            this.chkStatus.UseVisualStyleBackColor = true;
            this.chkStatus.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.White;
            this.btnClear.Location = new System.Drawing.Point(477, 84);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(183, 23);
            this.btnClear.TabIndex = 12;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // UC_MRT_DashCombine_Setup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(21)))), ((int)(((byte)(47)))));
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.chkStatus);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.txtIpAddress);
            this.Controls.Add(this.txtLine);
            this.Controls.Add(this.flowLineList);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblLine);
            this.Name = "UC_MRT_DashCombine_Setup";
            this.Size = new System.Drawing.Size(1045, 647);
            this.Load += new System.EventHandler(this.UC_MRT_DashCombine_Setup_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblLine;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.FlowLayoutPanel flowLineList;
        private System.Windows.Forms.TextBox txtLine;
        private System.Windows.Forms.TextBox txtIpAddress;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkStatus;
        private System.Windows.Forms.Button btnClear;
    }
}
