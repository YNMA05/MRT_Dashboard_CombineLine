namespace SmartMRT_DashCombine_Setup
{
    partial class UC_Line_ModuleCard
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
            this.lblModuleName = new System.Windows.Forms.Label();
            this.pictureModule = new System.Windows.Forms.PictureBox();
            this.btnChangeImage = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureModule)).BeginInit();
            this.SuspendLayout();
            // 
            // lblModuleName
            // 
            this.lblModuleName.AutoSize = true;
            this.lblModuleName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblModuleName.ForeColor = System.Drawing.Color.White;
            this.lblModuleName.Location = new System.Drawing.Point(19, 8);
            this.lblModuleName.Name = "lblModuleName";
            this.lblModuleName.Size = new System.Drawing.Size(80, 13);
            this.lblModuleName.TabIndex = 0;
            this.lblModuleName.Text = "lblModuleName";
            // 
            // pictureModule
            // 
            this.pictureModule.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.pictureModule.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureModule.Location = new System.Drawing.Point(22, 34);
            this.pictureModule.Name = "pictureModule";
            this.pictureModule.Size = new System.Drawing.Size(445, 270);
            this.pictureModule.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureModule.TabIndex = 1;
            this.pictureModule.TabStop = false;
            this.pictureModule.Click += new System.EventHandler(this.pictureModule_Click);
            // 
            // btnChangeImage
            // 
            this.btnChangeImage.BackColor = System.Drawing.Color.White;
            this.btnChangeImage.Location = new System.Drawing.Point(315, 5);
            this.btnChangeImage.Name = "btnChangeImage";
            this.btnChangeImage.Size = new System.Drawing.Size(152, 23);
            this.btnChangeImage.TabIndex = 2;
            this.btnChangeImage.Text = "btnChangeImage";
            this.btnChangeImage.UseVisualStyleBackColor = false;
            this.btnChangeImage.Click += new System.EventHandler(this.btnChangeImage_Click);
            // 
            // UC_Line_ModuleCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.Controls.Add(this.btnChangeImage);
            this.Controls.Add(this.pictureModule);
            this.Controls.Add(this.lblModuleName);
            this.Name = "UC_Line_ModuleCard";
            this.Size = new System.Drawing.Size(488, 318);
            this.Load += new System.EventHandler(this.UC_Line_ModuleCard_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureModule)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblModuleName;
        private System.Windows.Forms.PictureBox pictureModule;
        private System.Windows.Forms.Button btnChangeImage;
    }
}
