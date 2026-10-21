using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace SmartMRT_DashCombine_Setup
{
    public partial class UC_Line_ViewCard : UserControl
    {
        public int LineID { get; set; }

        private int _lineNumber;

        public int LineNumber
        {
            get => _lineNumber;
            set
            {
                _lineNumber = value;

                if (lblLine != null)
                    lblLine.Text = $"Line {value}";
            }
        }

        private string _ipAddress;

        public string IPAddress
        {
            get => _ipAddress;
            set
            {
                _ipAddress = value;

                if (lblIPAddress != null)
                    lblIPAddress.Text = value;
            }
        }

        private bool _isOnline;

        public bool IsOnline
        {
            get => _isOnline;
            set
            {
                _isOnline = value;
                UpdateStatusAppearance();
            }
        }

        public bool IsSelected
        {
            get => chkSelected.Checked;
            set
            {
                chkSelected.Checked = value;
                UpdateCardAppearance();
            }
        }

        public UC_Line_ViewCard()
        {
            InitializeComponent();

            DoubleBuffered = true;

            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw,
                true);

            this.Margin = new Padding(8);

            this.Click += Card_Click;
            lblLine.Click += Card_Click;
            lblIPAddress.Click += Card_Click;
            chkSelected.Click += CheckBox_Click;

            UpdateCardAppearance();
        }

        private void Card_Click(object sender, EventArgs e)
        {
            IsSelected = !IsSelected;
        }

        private void CheckBox_Click(object sender, EventArgs e)
        {
            UpdateCardAppearance();
        }

        private void UpdateCardAppearance()
        {
            Invalidate();
        }

        private void UpdateStatusAppearance()
        {
            if (lblStatus != null)
            {
                lblStatus.Text =
                    IsOnline ? "ONLINE" : "OFFLINE";

                lblStatus.ForeColor =
                    IsOnline
                        ? Color.FromArgb(56, 211, 159)
                        : Color.FromArgb(255, 92, 92);
            }

            Invalidate();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            LayoutCard();
        }

        private void LayoutCard()
        {
            if (lblLine == null ||
                lblIPAddress == null ||
                chkSelected == null)
                return;

            int padding = 14;

            chkSelected.Location =
                new Point(
                    ClientSize.Width -
                    chkSelected.Width -
                    padding,
                    (ClientSize.Height -
                     chkSelected.Height) / 2);

            lblLine.Location =
                new Point(
                    padding,
                    10);

            lblLine.Size =
                new Size(
                    Math.Max(
                        1,
                        chkSelected.Left -
                        padding -
                        10),
                    26);

            lblIPAddress.Location =
                new Point(
                    padding,
                    lblLine.Bottom + 2);

            lblIPAddress.Size =
                new Size(
                    Math.Max(
                        1,
                        chkSelected.Left -
                        padding -
                        10),
                    22);

            if (lblStatus != null)
            {
                lblStatus.Location =
                    new Point(
                        padding,
                        lblIPAddress.Bottom + 2);

                lblStatus.Size =
                    new Size(
                        100,
                        20);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.SmoothingMode =
                SmoothingMode.AntiAlias;

            Rectangle rect =
                new Rectangle(
                    1,
                    1,
                    Width - 2,
                    Height - 2);

            int radius = 14;

            using (GraphicsPath path =
                CreateRoundedRectangle(
                    rect,
                    radius))
            {
                Color background =
                    IsSelected
                        ? Color.FromArgb(20, 52, 82)
                        : Color.FromArgb(16, 36, 71);

                using (SolidBrush brush =
                    new SolidBrush(background))
                {
                    e.Graphics.FillPath(
                        brush,
                        path);
                }

                Color border =
                    IsSelected
                        ? Color.FromArgb(0, 212, 255)
                        : Color.FromArgb(40, 75, 122);

                using (Pen pen =
                    new Pen(border,
                        IsSelected ? 1.8f : 1.0f))
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

        private void UC_Line_ViewCard_Load(
            object sender,
            EventArgs e)
        {
            lblLine.Text =
                $"Line {LineNumber}";

            lblIPAddress.Text =
                IPAddress;

            UpdateStatusAppearance();
            LayoutCard();
        }
    }
}