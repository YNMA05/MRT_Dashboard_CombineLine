namespace SmartMRT_DashCombine_Setup
{
    partial class UC_LineCard
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
            this.lblLine = new System.Windows.Forms.Label();
            this.lblIpAddress = new System.Windows.Forms.Label();
            this.lblMotorStatus = new System.Windows.Forms.Label();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.lblDatabaseStatus = new System.Windows.Forms.Label();
            this.chkActive = new System.Windows.Forms.CheckBox();
            this.statusTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // lblLine
            // 
            this.lblLine.AutoSize = true;
            this.lblLine.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLine.ForeColor = System.Drawing.Color.Black;
            this.lblLine.Location = new System.Drawing.Point(11, 18);
            this.lblLine.Name = "lblLine";
            this.lblLine.Size = new System.Drawing.Size(53, 20);
            this.lblLine.TabIndex = 0;
            this.lblLine.Text = "lblLine";
            this.lblLine.Click += new System.EventHandler(this.lblLine_Click);
            // 
            // lblIpAddress
            // 
            this.lblIpAddress.AutoSize = true;
            this.lblIpAddress.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIpAddress.ForeColor = System.Drawing.Color.Black;
            this.lblIpAddress.Location = new System.Drawing.Point(11, 49);
            this.lblIpAddress.Name = "lblIpAddress";
            this.lblIpAddress.Size = new System.Drawing.Size(38, 20);
            this.lblIpAddress.TabIndex = 1;
            this.lblIpAddress.Text = "lblIP";
            // 
            // lblMotorStatus
            // 
            this.lblMotorStatus.AutoSize = true;
            this.lblMotorStatus.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMotorStatus.ForeColor = System.Drawing.Color.Black;
            this.lblMotorStatus.Location = new System.Drawing.Point(256, 18);
            this.lblMotorStatus.Name = "lblMotorStatus";
            this.lblMotorStatus.Size = new System.Drawing.Size(107, 20);
            this.lblMotorStatus.TabIndex = 2;
            this.lblMotorStatus.Text = "lblMotorStatus";
            this.lblMotorStatus.Click += new System.EventHandler(this.lblMotorStatus_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(95, 124);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(106, 23);
            this.btnEdit.TabIndex = 4;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(217, 124);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(101, 23);
            this.btnDelete.TabIndex = 5;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // lblDatabaseStatus
            // 
            this.lblDatabaseStatus.AutoSize = true;
            this.lblDatabaseStatus.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDatabaseStatus.ForeColor = System.Drawing.Color.Black;
            this.lblDatabaseStatus.Location = new System.Drawing.Point(256, 42);
            this.lblDatabaseStatus.Name = "lblDatabaseStatus";
            this.lblDatabaseStatus.Size = new System.Drawing.Size(89, 20);
            this.lblDatabaseStatus.TabIndex = 6;
            this.lblDatabaseStatus.Text = "lblDatabase";
            // 
            // chkActive
            // 
            this.chkActive.AutoSize = true;
            this.chkActive.Enabled = false;
            this.chkActive.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkActive.ForeColor = System.Drawing.Color.Black;
            this.chkActive.Location = new System.Drawing.Point(14, 85);
            this.chkActive.Name = "chkActive";
            this.chkActive.Size = new System.Drawing.Size(69, 24);
            this.chkActive.TabIndex = 7;
            this.chkActive.Text = "Active";
            this.chkActive.UseVisualStyleBackColor = true;
            this.chkActive.CheckedChanged += new System.EventHandler(this.chkActive_CheckedChanged_1);
            // 
            // UC_LineCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.Controls.Add(this.chkActive);
            this.Controls.Add(this.lblDatabaseStatus);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.lblMotorStatus);
            this.Controls.Add(this.lblIpAddress);
            this.Controls.Add(this.lblLine);
            this.Name = "UC_LineCard";
            this.Size = new System.Drawing.Size(425, 162);
            this.Load += new System.EventHandler(this.UC_LineCard_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblLine;
        private System.Windows.Forms.Label lblIpAddress;
        private System.Windows.Forms.Label lblMotorStatus;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label lblDatabaseStatus;
        private System.Windows.Forms.CheckBox chkActive;
        private System.Windows.Forms.Timer statusTimer;
    }
}
