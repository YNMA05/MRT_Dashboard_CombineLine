using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SmartMRT_DashCombine_Setup.Controls
{
    public class ModernComboBox : UserControl
    {
        private ComboBox innerComboBox;

        public ModernComboBox()
        {
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw,
                true);

            DoubleBuffered = true;

            BackColor = Color.Transparent;

            Size = new Size(350, 38);

            innerComboBox = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                FlatStyle = FlatStyle.Flat,

                Location = new Point(8, 5),
                Size = new Size(Width - 16, Height - 10),

                BackColor = Color.FromArgb(25, 35, 45),
                ForeColor = Color.White,

                Font = new Font("Segoe UI", 10.5F),

                Margin = new Padding(0)
            };

            innerComboBox.GotFocus += (s, e) =>
            {
                Invalidate();
            };

            innerComboBox.LostFocus += (s, e) =>
            {
                Invalidate();
            };

            Controls.Add(innerComboBox);

            Resize += (s, e) =>
            {
                if (innerComboBox != null)
                {
                    innerComboBox.Width = Width - 16;
                    innerComboBox.Height = Height - 10;
                }

                Invalidate();
            };
        }

        // ==========================================
        // COMBOBOX PROPERTIES
        // ==========================================

        public ComboBox.ObjectCollection Items
        {
            get { return innerComboBox.Items; }
        }

        public object SelectedItem
        {
            get { return innerComboBox.SelectedItem; }
            set { innerComboBox.SelectedItem = value; }
        }

        public int SelectedIndex
        {
            get { return innerComboBox.SelectedIndex; }
            set { innerComboBox.SelectedIndex = value; }
        }

        public object SelectedValue
        {
            get { return innerComboBox.SelectedValue; }
            set { innerComboBox.SelectedValue = value; }
        }

        public object DataSource
        {
            get { return innerComboBox.DataSource; }
            set { innerComboBox.DataSource = value; }
        }

        public string DisplayMember
        {
            get { return innerComboBox.DisplayMember; }
            set { innerComboBox.DisplayMember = value; }
        }

        public string ValueMember
        {
            get { return innerComboBox.ValueMember; }
            set { innerComboBox.ValueMember = value; }
        }

        public new string Text
        {
            get { return innerComboBox.Text; }
            set { innerComboBox.Text = value; }
        }

        // ==========================================
        // EVENTS
        // ==========================================

        public event EventHandler SelectedIndexChanged
        {
            add
            {
                innerComboBox.SelectedIndexChanged += value;
            }
            remove
            {
                innerComboBox.SelectedIndexChanged -= value;
            }
        }

        // ==========================================
        // PAINT
        // ==========================================

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.SmoothingMode =
                SmoothingMode.AntiAlias;

            Rectangle rect = ClientRectangle;

            rect.Width -= 1;
            rect.Height -= 1;

            using (GraphicsPath path =
                CreateRoundedRectangle(rect, 8))
            {
                Color borderColor =
                    innerComboBox.Focused
                        ? Color.FromArgb(0, 212, 255)
                        : Color.FromArgb(40, 75, 110);

                using (Pen pen =
                    new Pen(borderColor, 1.5f))
                {
                    e.Graphics.DrawPath(pen, path);
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