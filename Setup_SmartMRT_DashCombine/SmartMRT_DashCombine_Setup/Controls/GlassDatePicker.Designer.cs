namespace SmartMRT_DashCombine_Setup.Controls
{
    partial class GlassDatePicker
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
            this.glassButton1 = new SmartMRT_DashCombine_Setup.Controls.GlassButton();
            this.modernTextBox1 = new SmartMRT_DashCombine_Setup.Controls.ModernTextBox();
            this.SuspendLayout();
            // 
            // glassButton1
            // 
            this.glassButton1.BackColor = System.Drawing.Color.Transparent;
            this.glassButton1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(220)))), ((int)(((byte)(255)))));
            this.glassButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.glassButton1.FlatAppearance.BorderSize = 0;
            this.glassButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.glassButton1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.glassButton1.ForeColor = System.Drawing.Color.White;
            this.glassButton1.GlassColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(55)))), ((int)(((byte)(65)))));
            this.glassButton1.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(85)))), ((int)(((byte)(95)))));
            this.glassButton1.Location = new System.Drawing.Point(5, 7);
            this.glassButton1.Name = "glassButton1";
            this.glassButton1.Selected = false;
            this.glassButton1.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(140)))), ((int)(((byte)(190)))));
            this.glassButton1.Size = new System.Drawing.Size(44, 26);
            this.glassButton1.TabIndex = 1;
            this.glassButton1.Text = "📆";
            this.glassButton1.TextColor = System.Drawing.Color.White;
            this.glassButton1.UseVisualStyleBackColor = false;
            this.glassButton1.Click += new System.EventHandler(this.glassButton1_Click);
            // 
            // modernTextBox1
            // 
            this.modernTextBox1.BackColor = System.Drawing.Color.Transparent;
            this.modernTextBox1.Location = new System.Drawing.Point(55, 3);
            this.modernTextBox1.Name = "modernTextBox1";
            this.modernTextBox1.ReadOnly = false;
            this.modernTextBox1.Size = new System.Drawing.Size(177, 30);
            this.modernTextBox1.TabIndex = 0;
            this.modernTextBox1.UseSystemPasswordChar = false;
            this.modernTextBox1.Load += new System.EventHandler(this.modernTextBox1_Load);
            // 
            // GlassDatePicker
            // 
            this.Controls.Add(this.glassButton1);
            this.Controls.Add(this.modernTextBox1);
            this.Name = "GlassDatePicker";
            this.Size = new System.Drawing.Size(249, 35);
            this.ResumeLayout(false);

        }

        #endregion

        private ModernTextBox modernTextBox1;
        private GlassButton glassButton1;
    }
}
