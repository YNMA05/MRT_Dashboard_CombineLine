using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SmartMRT_DashCombine_Setup.Controls
{
    public class GlassPanel : Panel
    {
        [Category("Glass Appearance")]
        public int CornerRadius { get; set; } = 18;

        [Category("Glass Appearance")]
        public int GlassOpacity { get; set; } = 180;

        [Category("Glass Appearance")]
        public Color GlassColor { get; set; } =
            Color.FromArgb(16, 36, 71);

        [Category("Glass Appearance")]
        public Color BorderColor { get; set; } =
            Color.FromArgb(40, 75, 122);

        [Category("Glass Appearance")]
        public float BorderThickness { get; set; } = 1.2f;

        public GlassPanel()
        {
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw,
                true);

            DoubleBuffered = true;

            BackColor = Color.FromArgb(16, 36, 71);

            Padding = new Padding(20);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // Paint the parent background first.
            if (Parent != null)
            {
                using (SolidBrush brush =
                    new SolidBrush(Parent.BackColor))
                {
                    e.Graphics.FillRectangle(
                        brush,
                        ClientRectangle);
                }
            }
            else
            {
                e.Graphics.Clear(
                    Color.FromArgb(8, 21, 47));
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.SmoothingMode =
                SmoothingMode.AntiAlias;

            Rectangle rect = ClientRectangle;

            rect.X += 1;
            rect.Y += 1;
            rect.Width -= 2;
            rect.Height -= 2;

            using (GraphicsPath path =
                CreateRoundedRectangle(
                    rect,
                    CornerRadius))
            {
                // Glass surface
                using (SolidBrush brush =
                    new SolidBrush(GlassColor))
                {
                    e.Graphics.FillPath(
                        brush,
                        path);
                }

                // Border
                using (Pen pen =
                    new Pen(
                        BorderColor,
                        BorderThickness))
                {
                    e.Graphics.DrawPath(
                        pen,
                        path);
                }
            }
        }

        private GraphicsPath CreateRoundedRectangle(
            Rectangle rect,
            int radius)
        {
            GraphicsPath path =
                new GraphicsPath();

            int diameter = radius * 2;

            path.AddArc(
                rect.X,
                rect.Y,
                diameter,
                diameter,
                180,
                90);

            path.AddArc(
                rect.Right - diameter,
                rect.Y,
                diameter,
                diameter,
                270,
                90);

            path.AddArc(
                rect.Right - diameter,
                rect.Bottom - diameter,
                diameter,
                diameter,
                0,
                90);

            path.AddArc(
                rect.X,
                rect.Bottom - diameter,
                diameter,
                diameter,
                90,
                90);

            path.CloseFigure();

            return path;
        }
    }
}