// Example: how to host the WPF GlassCard in a WinForms form using ElementHost.
// Requirements:
// - Add references to PresentationCore, PresentationFramework, WindowsBase, System.Xaml and WindowsFormsIntegration.
// - If your project is WinForms (.NET Framework), you can add a WPF UserControl item and these files.
// - Alternatively create a separate WPF UserControl Library project and reference it from the WinForms project.

using System;
using System.Drawing;
using System.Windows.Forms;

namespace SmartMRT_DashCombine_Setup.WpfControls
{
    // Simple pure-WinForms glass-like card replacement so no WPF dependencies are required.
    public class WinFormsGlassCard : UserControl
    {
        private Label lblLine;
        private Label lblIp;
        private Label lblStatus;
        private Button btnEdit;
        private Button btnDelete;
        private Timer pulseTimer;
        private float pulseProgress = 0f;

        public WinFormsGlassCard()
        {
            InitializeComponents();
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
        }

        private void InitializeComponents()
        {
            this.Size = new Size(420, 160);
            this.BackColor = Color.Transparent;

            // We will custom paint the card; add child controls for text/buttons on top
            var panel = new Panel
            {
                BackColor = Color.Transparent,
                Size = this.Size,
                Location = new Point(0, 0)
            };

            lblLine = new Label { Text = "Line 01", Font = new Font("Segoe UI", 12F, FontStyle.Bold), ForeColor = Color.White, Location = new Point(12, 12), AutoSize = true };
            lblIp = new Label { Text = "192.168.0.122", Font = new Font("Segoe UI", 10F), ForeColor = Color.FromArgb(220, Color.White), Location = new Point(12, 40), AutoSize = true };
            lblStatus = new Label { Text = "Connecting...", Font = new Font("Segoe UI", 10F), ForeColor = Color.White, Location = new Point(12, 72), AutoSize = true };

            btnEdit = new Button { Text = "Edit", Size = new Size(100, 30), Location = new Point(300, 40) };
            btnDelete = new Button { Text = "Delete", Size = new Size(100, 30), Location = new Point(300, 80) };

            panel.Controls.Add(lblLine);
            panel.Controls.Add(lblIp);
            panel.Controls.Add(lblStatus);
            panel.Controls.Add(btnEdit);
            panel.Controls.Add(btnDelete);

            this.Controls.Add(panel);

            // pulse animation timer for neon glow
            pulseTimer = new Timer { Interval = 40 };
            pulseTimer.Tick += (s, e) =>
            {
                pulseProgress += 0.06f;
                if (pulseProgress > System.Math.PI * 2) pulseProgress -= (float)(System.Math.PI * 2);
                this.Invalidate();
            };
            pulseTimer.Start();
        }

        // Public API
        public void SetLine(string line) => lblLine.Text = line;
        public void SetIp(string ip) => lblIp.Text = ip;
        public void SetDbStatus(string text, Color? color = null)
        {
            lblStatus.Text = text;
            if (color.HasValue) lblStatus.ForeColor = color.Value;
        }

        public void SetControlsEnabled(bool enabled)
        {
            btnEdit.Enabled = enabled;
            btnDelete.Enabled = enabled;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Draw rounded glass card with gradient
            var rect = new Rectangle(6, 6, this.Width - 12, this.Height - 12);
            using (var path = RoundedRect(rect, 12))
            {
                // soft inner gradient (top transparent to bottom slightly white)
                using (var brush = new System.Drawing.Drawing2D.LinearGradientBrush(rect, Color.FromArgb(60, 255, 255, 255), Color.FromArgb(18, 255, 255, 255), 90f))
                {
                    g.FillPath(brush, path);
                }

                // neon glow: draw multiple translucent outlines with increasing blur-like widths
                float glowStrength = 8f + (float)(Math.Sin(pulseProgress) * 2.5);
                Color glowColor = Color.FromArgb(160, 77, 208, 255); // neon blue-ish
                for (int i = 0; i < 6; i++)
                {
                    int alpha = (int)(glowColor.A / (i + 1) / 1.5);
                    using (var pen = new Pen(Color.FromArgb(Math.Max(alpha, 16), glowColor), 1f + i * (glowStrength / 6f)))
                    {
                        pen.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;
                        g.DrawPath(pen, path);
                    }
                }

                // subtle border
                using (var pen = new Pen(Color.FromArgb(110, Color.White), 1f))
                {
                    g.DrawPath(pen, path);
                }
            }

            // draw status dot with pulsing outer ring
            var dotCenter = new Point(30, 94);
            float pulse = 0.9f + 0.12f * (float)(System.Math.Sin(pulseProgress) + 1);
            int outerRadius = (int)(10 * pulse);
            using (var outer = new SolidBrush(Color.FromArgb(64, 77, 208, 255)))
            {
                g.FillEllipse(outer, dotCenter.X - outerRadius, dotCenter.Y - outerRadius, outerRadius * 2, outerRadius * 2);
            }
            using (var inner = new SolidBrush(Color.FromArgb(255, 77, 208, 255)))
            {
                g.FillEllipse(inner, dotCenter.X - 6, dotCenter.Y - 6, 12, 12);
            }
        }

        private System.Drawing.Drawing2D.GraphicsPath RoundedRect(Rectangle r, int radius)
        {
            var path = new System.Drawing.Drawing2D.GraphicsPath();
            int d = radius * 2;
            path.AddArc(r.X, r.Y, d, d, 180, 90);
            path.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            path.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            path.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }
    }

    // Helper to host the WinForms-only control into a target panel
    public static class WinFormsHostExample
    {
        public static void HostGlassCard(Panel targetPanel, string ip, string line)
        {
            var card = new WinFormsGlassCard();
            card.SetIp(ip);
            card.SetLine(line);
            card.SetDbStatus("Connecting...");

            targetPanel.Controls.Clear();
            targetPanel.Controls.Add(card);
            card.Dock = DockStyle.Fill;
        }
    }
}
