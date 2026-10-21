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
            this.lblSql = new System.Windows.Forms.Label();
            this.lblMotor = new System.Windows.Forms.Label();
            this.lblSp = new System.Windows.Forms.Label();
            this.lblSpStatus = new System.Windows.Forms.Label();
            this.btnSp = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblLine
            // 
            this.lblLine.AutoSize = true;
            this.lblLine.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLine.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(179)))), ((int)(((byte)(199)))));
            this.lblLine.Location = new System.Drawing.Point(15, 16);
            this.lblLine.Name = "lblLine";
            this.lblLine.Size = new System.Drawing.Size(50, 17);
            this.lblLine.TabIndex = 0;
            this.lblLine.Text = "lblLine";
            this.lblLine.Click += new System.EventHandler(this.lblLine_Click);
            // 
            // lblIpAddress
            // 
            this.lblIpAddress.AutoSize = true;
            this.lblIpAddress.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIpAddress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(179)))), ((int)(((byte)(199)))));
            this.lblIpAddress.Location = new System.Drawing.Point(15, 36);
            this.lblIpAddress.Name = "lblIpAddress";
            this.lblIpAddress.Size = new System.Drawing.Size(36, 17);
            this.lblIpAddress.TabIndex = 1;
            this.lblIpAddress.Text = "lblIP";
            // 
            // lblMotorStatus
            // 
            this.lblMotorStatus.AutoSize = true;
            this.lblMotorStatus.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMotorStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(179)))), ((int)(((byte)(199)))));
            this.lblMotorStatus.Location = new System.Drawing.Point(120, 112);
            this.lblMotorStatus.Name = "lblMotorStatus";
            this.lblMotorStatus.Size = new System.Drawing.Size(100, 17);
            this.lblMotorStatus.TabIndex = 2;
            this.lblMotorStatus.Text = "lblMotorStatus";
            this.lblMotorStatus.Click += new System.EventHandler(this.lblMotorStatus_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(235, 100);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(106, 34);
            this.btnEdit.TabIndex = 4;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(235, 16);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(106, 34);
            this.btnDelete.TabIndex = 5;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // lblDatabaseStatus
            // 
            this.lblDatabaseStatus.AutoSize = true;
            this.lblDatabaseStatus.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDatabaseStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(179)))), ((int)(((byte)(199)))));
            this.lblDatabaseStatus.Location = new System.Drawing.Point(120, 66);
            this.lblDatabaseStatus.Name = "lblDatabaseStatus";
            this.lblDatabaseStatus.Size = new System.Drawing.Size(81, 17);
            this.lblDatabaseStatus.TabIndex = 6;
            this.lblDatabaseStatus.Text = "lblDatabase";
            this.lblDatabaseStatus.Click += new System.EventHandler(this.lblDatabaseStatus_Click);
            // 
            // chkActive
            // 
            this.chkActive.AutoSize = true;
            this.chkActive.Enabled = false;
            this.chkActive.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkActive.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(179)))), ((int)(((byte)(199)))));
            this.chkActive.Location = new System.Drawing.Point(132, 21);
            this.chkActive.Name = "chkActive";
            this.chkActive.Size = new System.Drawing.Size(69, 24);
            this.chkActive.TabIndex = 7;
            this.chkActive.Text = "Active";
            this.chkActive.UseVisualStyleBackColor = true;
            this.chkActive.CheckedChanged += new System.EventHandler(this.chkActive_CheckedChanged_1);
            // 
            // lblSql
            // 
            this.lblSql.AutoSize = true;
            this.lblSql.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSql.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(179)))), ((int)(((byte)(199)))));
            this.lblSql.Location = new System.Drawing.Point(15, 68);
            this.lblSql.Name = "lblSql";
            this.lblSql.Size = new System.Drawing.Size(99, 15);
            this.lblSql.TabIndex = 8;
            this.lblSql.Text = "SQL Connection :";
            // 
            // lblMotor
            // 
            this.lblMotor.AutoSize = true;
            this.lblMotor.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMotor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(179)))), ((int)(((byte)(199)))));
            this.lblMotor.Location = new System.Drawing.Point(15, 114);
            this.lblMotor.Name = "lblMotor";
            this.lblMotor.Size = new System.Drawing.Size(94, 15);
            this.lblMotor.TabIndex = 9;
            this.lblMotor.Text = "Motor Running :";
            // 
            // lblSp
            // 
            this.lblSp.AutoSize = true;
            this.lblSp.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(179)))), ((int)(((byte)(199)))));
            this.lblSp.Location = new System.Drawing.Point(15, 92);
            this.lblSp.Name = "lblSp";
            this.lblSp.Size = new System.Drawing.Size(104, 15);
            this.lblSp.TabIndex = 11;
            this.lblSp.Text = "Stored Procedure :";
            // 
            // lblSpStatus
            // 
            this.lblSpStatus.AutoSize = true;
            this.lblSpStatus.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSpStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(179)))), ((int)(((byte)(199)))));
            this.lblSpStatus.Location = new System.Drawing.Point(120, 90);
            this.lblSpStatus.Name = "lblSpStatus";
            this.lblSpStatus.Size = new System.Drawing.Size(77, 17);
            this.lblSpStatus.TabIndex = 10;
            this.lblSpStatus.Text = "lblSpStatus";
            // 
            // btnSp
            // 
            this.btnSp.Location = new System.Drawing.Point(235, 60);
            this.btnSp.Name = "btnSp";
            this.btnSp.Size = new System.Drawing.Size(106, 34);
            this.btnSp.TabIndex = 12;
            this.btnSp.Text = "Add Stored Procedure";
            this.btnSp.UseVisualStyleBackColor = true;
            this.btnSp.Click += new System.EventHandler(this.btnSp_Click);
            // 
            // UC_LineCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.btnSp);
            this.Controls.Add(this.lblSp);
            this.Controls.Add(this.lblSpStatus);
            this.Controls.Add(this.lblMotor);
            this.Controls.Add(this.lblSql);
            this.Controls.Add(this.chkActive);
            this.Controls.Add(this.lblDatabaseStatus);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.lblMotorStatus);
            this.Controls.Add(this.lblIpAddress);
            this.Controls.Add(this.lblLine);
            this.ForeColor = System.Drawing.Color.Transparent;
            this.Name = "UC_LineCard";
            this.Size = new System.Drawing.Size(355, 149);
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
        private System.Windows.Forms.Label lblSql;
        private System.Windows.Forms.Label lblMotor;
        private System.Windows.Forms.Label lblSp;
        private System.Windows.Forms.Label lblSpStatus;
        private System.Windows.Forms.Button btnSp;
    }
}
