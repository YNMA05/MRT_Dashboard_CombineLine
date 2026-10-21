
namespace SmartMRT_DashCombine_Setup
{
    partial class FrmAddUser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAddUser));
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblPwd = new System.Windows.Forms.Label();
            this.lblRole = new System.Windows.Forms.Label();
            this.txtUsername = new SmartMRT_DashCombine_Setup.Controls.ModernTextBox();
            this.txtPwd = new SmartMRT_DashCombine_Setup.Controls.ModernTextBox();
            this.chkStatus = new System.Windows.Forms.CheckBox();
            this.ddlRole = new SmartMRT_DashCombine_Setup.Controls.ModernComboBox();
            this.btnShowPwd = new System.Windows.Forms.Button();
            this.lblStrength = new System.Windows.Forms.Label();
            this.progressBarPwd = new System.Windows.Forms.ProgressBar();
            this.lblPasswordStrength = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.BackColor = System.Drawing.Color.Transparent;
            this.lblUsername.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsername.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(180)))), ((int)(((byte)(210)))));
            this.lblUsername.Location = new System.Drawing.Point(58, 94);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(78, 20);
            this.lblUsername.TabIndex = 0;
            this.lblUsername.Text = "Username";
            // 
            // lblPwd
            // 
            this.lblPwd.AutoSize = true;
            this.lblPwd.BackColor = System.Drawing.Color.Transparent;
            this.lblPwd.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPwd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(180)))), ((int)(((byte)(210)))));
            this.lblPwd.Location = new System.Drawing.Point(58, 153);
            this.lblPwd.Name = "lblPwd";
            this.lblPwd.Size = new System.Drawing.Size(73, 20);
            this.lblPwd.TabIndex = 1;
            this.lblPwd.Text = "Password";
            // 
            // lblRole
            // 
            this.lblRole.AutoSize = true;
            this.lblRole.BackColor = System.Drawing.Color.Transparent;
            this.lblRole.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRole.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(180)))), ((int)(((byte)(210)))));
            this.lblRole.Location = new System.Drawing.Point(58, 254);
            this.lblRole.Name = "lblRole";
            this.lblRole.Size = new System.Drawing.Size(43, 20);
            this.lblRole.TabIndex = 2;
            this.lblRole.Text = "Role ";
            // 
            // txtUsername
            // 
            this.txtUsername.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(36)))), ((int)(((byte)(71)))));
            this.txtUsername.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsername.ForeColor = System.Drawing.Color.White;
            this.txtUsername.Location = new System.Drawing.Point(61, 117);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.ReadOnly = false;
            this.txtUsername.Size = new System.Drawing.Size(309, 34);
            this.txtUsername.TabIndex = 5;
            this.txtUsername.UseSystemPasswordChar = false;
            this.txtUsername.TextChanged += new System.EventHandler(this.txtUsername_TextChanged);
            // 
            // txtPwd
            // 
            this.txtPwd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(36)))), ((int)(((byte)(71)))));
            this.txtPwd.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPwd.ForeColor = System.Drawing.Color.White;
            this.txtPwd.Location = new System.Drawing.Point(61, 173);
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.ReadOnly = false;
            this.txtPwd.Size = new System.Drawing.Size(275, 34);
            this.txtPwd.TabIndex = 6;
            this.txtPwd.UseSystemPasswordChar = false;
            this.txtPwd.TextChanged += new System.EventHandler(this.txtPwd_TextChanged);
            // 
            // chkStatus
            // 
            this.chkStatus.AutoSize = true;
            this.chkStatus.BackColor = System.Drawing.Color.Transparent;
            this.chkStatus.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(180)))), ((int)(((byte)(210)))));
            this.chkStatus.Location = new System.Drawing.Point(359, 224);
            this.chkStatus.Name = "chkStatus";
            this.chkStatus.Size = new System.Drawing.Size(70, 24);
            this.chkStatus.TabIndex = 7;
            this.chkStatus.Text = "Active";
            this.chkStatus.UseVisualStyleBackColor = false;
            this.chkStatus.Visible = false;
            // 
            // ddlRole
            // 
            this.ddlRole.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(36)))), ((int)(((byte)(71)))));
            this.ddlRole.DataSource = null;
            this.ddlRole.DisplayMember = "";
            this.ddlRole.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlRole.ForeColor = System.Drawing.Color.White;
            this.ddlRole.Location = new System.Drawing.Point(60, 277);
            this.ddlRole.Name = "ddlRole";
            this.ddlRole.SelectedIndex = -1;
            this.ddlRole.SelectedItem = null;
            this.ddlRole.SelectedValue = null;
            this.ddlRole.Size = new System.Drawing.Size(310, 38);
            this.ddlRole.TabIndex = 8;
            this.ddlRole.ValueMember = "";
            this.ddlRole.Load += new System.EventHandler(this.ddlRole_Load);
            // 
            // btnShowPwd
            // 
            this.btnShowPwd.Location = new System.Drawing.Point(341, 178);
            this.btnShowPwd.Name = "btnShowPwd";
            this.btnShowPwd.Size = new System.Drawing.Size(28, 23);
            this.btnShowPwd.TabIndex = 10;
            this.btnShowPwd.Text = "👁\r\n";
            this.btnShowPwd.UseVisualStyleBackColor = true;
            this.btnShowPwd.Click += new System.EventHandler(this.btnShowPwd_Click);
            // 
            // lblStrength
            // 
            this.lblStrength.AutoSize = true;
            this.lblStrength.ForeColor = System.Drawing.Color.White;
            this.lblStrength.Location = new System.Drawing.Point(63, 209);
            this.lblStrength.Name = "lblStrength";
            this.lblStrength.Size = new System.Drawing.Size(0, 13);
            this.lblStrength.TabIndex = 11;
            // 
            // progressBarPwd
            // 
            this.progressBarPwd.Location = new System.Drawing.Point(61, 225);
            this.progressBarPwd.Name = "progressBarPwd";
            this.progressBarPwd.Size = new System.Drawing.Size(150, 23);
            this.progressBarPwd.TabIndex = 12;
            this.progressBarPwd.Visible = false;
            // 
            // lblPasswordStrength
            // 
            this.lblPasswordStrength.AutoSize = true;
            this.lblPasswordStrength.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPasswordStrength.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(180)))), ((int)(((byte)(210)))));
            this.lblPasswordStrength.Location = new System.Drawing.Point(60, 209);
            this.lblPasswordStrength.Name = "lblPasswordStrength";
            this.lblPasswordStrength.Size = new System.Drawing.Size(38, 13);
            this.lblPasswordStrength.TabIndex = 13;
            this.lblPasswordStrength.Text = "label1";
            this.lblPasswordStrength.Visible = false;
            // 
            // FrmAddUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(36)))), ((int)(((byte)(71)))));
            this.ClientSize = new System.Drawing.Size(459, 456);
            this.Controls.Add(this.lblPasswordStrength);
            this.Controls.Add(this.progressBarPwd);
            this.Controls.Add(this.lblStrength);
            this.Controls.Add(this.btnShowPwd);
            this.Controls.Add(this.ddlRole);
            this.Controls.Add(this.chkStatus);
            this.Controls.Add(this.txtPwd);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.lblRole);
            this.Controls.Add(this.lblPwd);
            this.Controls.Add(this.lblUsername);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmAddUser";
            this.Text = "Setup User";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmAddUser_FormClosed);
            this.Load += new System.EventHandler(this.FrmAddUser_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblPwd;
        private System.Windows.Forms.Label lblRole;
        private SmartMRT_DashCombine_Setup.Controls.ModernTextBox txtUsername;
        private SmartMRT_DashCombine_Setup.Controls.ModernTextBox txtPwd;
        private System.Windows.Forms.CheckBox chkStatus;
        private SmartMRT_DashCombine_Setup.Controls.ModernComboBox ddlRole;
        private System.Windows.Forms.Button btnShowPwd;
        private System.Windows.Forms.Label lblStrength;
        private System.Windows.Forms.ProgressBar progressBarPwd;
        private System.Windows.Forms.Label lblPasswordStrength;
    }
}