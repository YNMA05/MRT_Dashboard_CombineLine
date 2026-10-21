using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;

namespace SmartMRT_DashCombine_Setup.Controls
{
    public class GlassButton : Button
    {
        private bool isHovered = false;

        public bool Selected { get; set; } = false;

        public Color BorderColor { get; set; } =
            Color.FromArgb(120, 220, 255);

        public Color TextColor { get; set; } =
            Color.White;

        public Color GlassColor { get; set; } =
            Color.FromArgb(25, 55, 65);

        public Color SelectedColor { get; set; } =
            Color.FromArgb(0, 140, 190);
        public Color HoverColor { get; set; } =
        Color.FromArgb(45, 85, 95);

        public GlassButton()
        {
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;

            BackColor = Color.Transparent;
            ForeColor = TextColor;

            Font = new Font(
                "Segoe UI",
                10F,
                FontStyle.Bold);

            Cursor = Cursors.Hand;

            MouseEnter += GlassButton_MouseEnter;
            MouseLeave += GlassButton_MouseLeave;
        }

        private void GlassButton_MouseEnter(
            object sender,
            EventArgs e)
        {
            isHovered = true;
            Invalidate();
        }

        private void GlassButton_MouseLeave(
            object sender,
            EventArgs e)
        {
            isHovered = false;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode =
                SmoothingMode.AntiAlias;

            Rectangle rect = ClientRectangle;

            rect.Width -= 1;
            rect.Height -= 1;

            using (GraphicsPath path =
                CreateRoundedRectangle(rect, 10))
            {
                Color background;
                if (Selected)
                {
                    background = SelectedColor;
                }
                else if (isHovered)
                {
                    background = HoverColor;
                }
                else
                {
                    background = GlassColor;
                }

                using (SolidBrush brush =
                    new SolidBrush(background))
                {
                    e.Graphics.FillPath(
                        brush,
                        path);
                }

                Color border =
                    Selected
                        ? Color.FromArgb(0, 212, 255)
                        : BorderColor;

                using (Pen pen =
                    new Pen(border, 1.5f))
                {
                    e.Graphics.DrawPath(
                        pen,
                        path);
                }

                Color textColor =
                    Selected
                        ? Color.White
                        : TextColor;

                TextRenderer.DrawText(
                    e.Graphics,
                    Text,
                    Font,
                    rect,
                    textColor,
                    TextFormatFlags.HorizontalCenter |
                    TextFormatFlags.VerticalCenter);
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