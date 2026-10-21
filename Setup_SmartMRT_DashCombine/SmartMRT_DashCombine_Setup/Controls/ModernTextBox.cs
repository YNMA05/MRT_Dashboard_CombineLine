using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SmartMRT_DashCombine_Setup.Controls
{
    public class ModernTextBox : UserControl
    {
        private TextBox innerTextBox;

        public ModernTextBox()
        {
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw,
                true);

            DoubleBuffered = true;

            BackColor = Color.Transparent;

            Size = new Size(350, 40);

            innerTextBox = new TextBox
            {
                BorderStyle = BorderStyle.None,
                Location = new Point(12, 7),
                Size = new Size(Width - 24, 24),
                BackColor = Color.FromArgb(32, 43, 54),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11F),
                Margin = new Padding(0)
            };

            innerTextBox.GotFocus += (s, e) => Invalidate();
            innerTextBox.LostFocus += (s, e) => Invalidate();
            innerTextBox.TextChanged += (s, e) => { OnTextChanged(e); };


            Controls.Add(innerTextBox);

            Resize += (s, e) =>
            {
                if (innerTextBox != null)
                {
                    innerTextBox.Width = Width - 24;
                    innerTextBox.Height = Height - 14;
                }

                Invalidate();
            };
        }

        [Browsable(false)]
        public override string Text
        {
            get => innerTextBox.Text;
            set => innerTextBox.Text = value;
        }

        [Category("Behavior")]
        public bool UseSystemPasswordChar
        {
            get => innerTextBox.UseSystemPasswordChar;
            set => innerTextBox.UseSystemPasswordChar = value;
        }

        [Category("Behavior")]
        public new bool ReadOnly
        {
            get => innerTextBox.ReadOnly;
            set
            {
                innerTextBox.ReadOnly = value;

                innerTextBox.BackColor = value
                    ? Color.FromArgb(32, 43, 54)
                    : Color.FromArgb(32, 43, 54);

                Invalidate();
            }
        }

        public void FocusTextBox()
        {
            innerTextBox.Focus();
        }

        public void SelectAll()
        {
            if (innerTextBox != null)
            {
                innerTextBox.SelectAll();
            }
        }

        public void TextChangedEventHandler(EventHandler handler)
        {
            innerTextBox.TextChanged += handler;
        }


        public void Clear()
        {
            innerTextBox.Clear();
        }

        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    base.OnPaint(e);

        //    Graphics g = e.Graphics;

        //    g.SmoothingMode = SmoothingMode.AntiAlias;

        //    Rectangle rect = new Rectangle(
        //        0,
        //        0,
        //        Width - 1,
        //        Height - 1);

        //    int radius = 9;

        //    using (GraphicsPath path =
        //           CreateRoundedRectangle(rect, radius))
        //    {
        //        // Input background
        //        using (SolidBrush brush =
        //               new SolidBrush(
        //                   Color.FromArgb(32, 43, 54)))
        //        {
        //            g.FillPath(brush, path);
        //        }

        //        // Border
        //        Color borderColor;

        //        if (innerTextBox != null &&
        //            innerTextBox.Focused)
        //        {
        //            borderColor =
        //                Color.FromArgb(130, 90, 210, 240);
        //        }
        //        else
        //        {
        //            borderColor =
        //                Color.FromArgb(60, 100, 125);
        //        }

        //        using (Pen pen =
        //               new Pen(borderColor, 1.2F))
        //        {
        //            g.DrawPath(pen, path);
        //        }
        //    }
        //}

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;

            g.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = new Rectangle(
                0,
                0,
                Width - 1,
                Height - 1);

            int radius = 9;

            using (GraphicsPath path =
                   CreateRoundedRectangle(rect, radius))
            {
                // Input background
                using (SolidBrush brush =
                       new SolidBrush(Color.FromArgb(32, 43, 54)))
                {
                    g.FillPath(brush, path);
                }

                // Border
                Color borderColor =
                    innerTextBox != null && innerTextBox.Focused
                        ? Color.FromArgb(0, 190, 235)
                        : Color.FromArgb(60, 100, 125);

                float borderThickness =
                    innerTextBox != null && innerTextBox.Focused
                        ? 1.5f
                        : 1.1f;

                using (Pen pen =
                       new Pen(borderColor, borderThickness))
                {
                    g.DrawPath(pen, path);
                }
            }
        }

        private GraphicsPath CreateRoundedRectangle(
            Rectangle rect,
            int radius)
        {
            GraphicsPath path = new GraphicsPath();

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