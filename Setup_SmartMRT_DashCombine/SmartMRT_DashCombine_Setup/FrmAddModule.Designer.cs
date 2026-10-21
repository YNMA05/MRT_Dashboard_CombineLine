namespace SmartMRT_DashCombine_Setup
{
    partial class FrmAddModule
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAddModule));
            this.glassPanelModule = new SmartMRT_DashCombine_Setup.Controls.GlassPanel();
            this.dgvModule = new System.Windows.Forms.DataGridView();
            this.glassPanelModule.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvModule)).BeginInit();
            this.SuspendLayout();
            // 
            // glassPanelModule
            // 
            this.glassPanelModule.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(36)))), ((int)(((byte)(71)))));
            this.glassPanelModule.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(75)))), ((int)(((byte)(122)))));
            this.glassPanelModule.BorderThickness = 1.2F;
            this.glassPanelModule.Controls.Add(this.dgvModule);
            this.glassPanelModule.CornerRadius = 18;
            this.glassPanelModule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.glassPanelModule.GlassColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(36)))), ((int)(((byte)(71)))));
            this.glassPanelModule.GlassOpacity = 180;
            this.glassPanelModule.Location = new System.Drawing.Point(0, 0);
            this.glassPanelModule.Name = "glassPanelModule";
            this.glassPanelModule.Padding = new System.Windows.Forms.Padding(20);
            this.glassPanelModule.Size = new System.Drawing.Size(955, 659);
            this.glassPanelModule.TabIndex = 0;
            // 
            // dgvModule
            // 
            this.dgvModule.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvModule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvModule.Location = new System.Drawing.Point(20, 20);
            this.dgvModule.Name = "dgvModule";
            this.dgvModule.Size = new System.Drawing.Size(915, 619);
            this.dgvModule.TabIndex = 0;
            // 
            // FrmAddModule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(36)))), ((int)(((byte)(71)))));
            this.ClientSize = new System.Drawing.Size(955, 659);
            this.Controls.Add(this.glassPanelModule);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmAddModule";
            this.Text = "FrmAddModule";
            this.Load += new System.EventHandler(this.FrmAddModule_Load);
            this.glassPanelModule.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvModule)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.GlassPanel glassPanelModule;
        private System.Windows.Forms.DataGridView dgvModule;
    }
}