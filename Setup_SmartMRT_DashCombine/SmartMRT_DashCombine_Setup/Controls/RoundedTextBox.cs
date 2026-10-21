using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SmartMRT_DashCombine_Setup.Controls
{
    // Simple rounded textbox with placeholder and floating-label behavior.
    public class RoundedTextBox : UserControl
    {
        private TextBox innerTextBox;
        private Label floatingLabel;
        private string placeholder = "";
        private bool isFloating = false;

        public RoundedTextBox()
        {
            // enable transparent background support and optimized double buffering
            // Add UserPaint, DoubleBuffer and ResizeRedraw to reduce flicker
            this.SetStyle(
                ControlStyles.SupportsTransparentBackColor |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.ResizeRedraw |
                ControlStyles.DoubleBuffer,
                true);
            this.UpdateStyles();
            this.BackColor = Color.Transparent;
            this.Size = new Size(400, 40);

            innerTextBox = new TextBox
            {
                BorderStyle = BorderStyle.None,
                Location = new Point(12, 14),
                Width = this.Width - 24,
                // Use an opaque backcolor: semi-transparent colors are not supported by standard WinForms TextBox
                BackColor = Color.White,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F)
            };
            innerTextBox.TextChanged += (s, e) => UpdateFloatingState();

            innerTextBox.GotFocus += (s, e) => UpdateFloatingState(true);
            innerTextBox.LostFocus += (s, e) => UpdateFloatingState(false);

            floatingLabel = new Label
            {
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleLeft,
                Location = new Point(12, 6),
                Size = new Size(this.Width - 24, 18),
                ForeColor = Color.FromArgb(180, Color.White),
                Font = new Font("Segoe UI", 9F),
                Text = placeholder
            };

            this.Controls.Add(floatingLabel);
            this.Controls.Add(innerTextBox);

            this.Padding = new Padding(6);
            this.Resize += (s, e) =>
            {
                innerTextBox.Width = this.Width - 24;
                floatingLabel.Size = new Size(this.Width - 24, 18);
            };
        }

        // Paint the parent's background into this control so transparency looks correct
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (this.Parent != null && this.BackColor == Color.Transparent)
            {
                var pe = e.Graphics;
                var parent = this.Parent;
                var translate = this.Location;

                // ask the parent to draw itself onto our graphics at the correct offset
                var state = pe.Save();
                pe.TranslateTransform(-translate.X, -translate.Y);
                var msg = new PaintEventArgs(pe, new Rectangle(0, 0, parent.Width, parent.Height));
                // Let the parent paint itself into our graphics
                var paintMethod = typeof(Control).GetMethod("InvokePaintBackground", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                var paintMethod2 = typeof(Control).GetMethod("InvokePaint", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                if (paintMethod != null)
                    paintMethod.Invoke(parent, new object[] { parent, msg });
                if (paintMethod2 != null)
                    paintMethod2.Invoke(parent, new object[] { parent, msg });
                pe.Restore(state);
            }
            else
            {
                base.OnPaintBackground(e);
            }
        }

        [Category("Appearance")]
        public string Placeholder
        {
            get => placeholder;
            set
            {
                placeholder = value;
                floatingLabel.Text = value;
                Invalidate();
            }
        }

        [Browsable(false)]
        public override string Text
        {
            get => innerTextBox.Text;
            set => innerTextBox.Text = value;
        }

        [Category("Behavior")]
        public bool UseFloatingLabel { get; set; } = true;

        private void UpdateFloatingState(bool focused = false)
        {
            bool hasText = !string.IsNullOrEmpty(innerTextBox.Text);
            bool shouldFloat = UseFloatingLabel && (focused || hasText);

            if (shouldFloat == isFloating) return;

            isFloating = shouldFloat;

            if (isFloating)
            {
                // move label up and shrink
                floatingLabel.Font = new Font("Segoe UI", 8F);
                floatingLabel.Location = new Point(12, -2);
                floatingLabel.ForeColor = Color.FromArgb(220, 180, 220, 255); // lighter neon tint
            }
            else
            {
                floatingLabel.Font = new Font("Segoe UI", 9F);
                floatingLabel.Location = new Point(12, 6);
                floatingLabel.ForeColor = Color.FromArgb(180, Color.White);
            }

            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            var rect = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            int radius = 10;

            using (var path = RoundedRect(rect, radius))
            {
                // background
                using (var bg = new SolidBrush(Color.FromArgb(38, 20, 20, 30))) // semi-transparent dark
                {
                    g.FillPath(bg, path);
                }

                // inner subtle highlight
                using (var pen = new Pen(Color.FromArgb(40, Color.White), 1))
                {
                    g.DrawPath(pen, path);
                }
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

        // expose inner textbox events and properties as needed
        public new event EventHandler TextChanged
        {
            add => innerTextBox.TextChanged += value;
            remove => innerTextBox.TextChanged -= value;
        }

        public void SetReadOnly(bool readOnly)
        {
            innerTextBox.ReadOnly = readOnly;
            // Use opaque colors for the inner textbox background to avoid ArgumentException
            innerTextBox.BackColor = readOnly ? Color.LightGray : Color.White;
        }
    }
}
