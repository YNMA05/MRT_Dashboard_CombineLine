using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartMRT_DashCombine_Setup.Controls
{
    public partial class GlassDatePicker : UserControl
    {
       // private DateTimePicker glassDate;
       // private bool calendarVisible = false;
        private Form calendarPopup;
        private MonthCalendar monthCalendar;
        public GlassDatePicker()
        {

            SetStyle(ControlStyles.UserPaint |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw,
                     true);
            DoubleBuffered = true;
            InitializeComponent();
            //glassDate.Visible = false;
            //glassButton1.Click += (s, e) =>
            //{
            //    calendarVisible = !calendarVisible;
            //    glassDate.Visible = calendarVisible;
            //};

            //glassButton1.Click += glassButton1_Click;
        }

        public string DateText
        {
            get { return modernTextBox1.Text; }
            set { modernTextBox1.Text = value; }
        }

        public DateTime DateValue
        {
            get
            {
                DateTime value;

                if (DateTime.TryParseExact(
                    modernTextBox1.Text,
                    "dd MM yyyy",
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None,
                    out value))
                {
                    return value;
                }

                return DateTime.Today;
            }
            set
            {
                modernTextBox1.Text =
                    value.ToString("dd MM yyyy");
            }
        }
        public GlassDatePicker(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
           // glassButton1.Click += glassButton1_Click;
        }

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
                       new SolidBrush(
                           Color.FromArgb(32, 43, 54)))
                {
                    g.FillPath(brush, path);
                }

                // Border
                Color borderColor;

                if (calendarPopup != null &&
                    calendarPopup.Focused)
                {
                    borderColor =
                        Color.FromArgb(130, 90, 210, 240);
                }
                else
                {
                    borderColor =
                        Color.FromArgb(60, 100, 125);
                }

                using (Pen pen =
                       new Pen(borderColor, 1.2F))
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

        private void glassButton1_Click(object sender, EventArgs e)
        {
            if (calendarPopup != null && !calendarPopup.IsDisposed)
            {
                CloseCalendarPopup();
                return;
            }
            ShowCalendarPopup();
        }

        private void ShowCalendarPopup()
        {
            if (calendarPopup != null && !calendarPopup.IsDisposed)
                return;

            // -----------------------------
            // Calendar
            // -----------------------------

            MonthCalendar calendar = new MonthCalendar
            {
                MaxSelectionCount = 1,
                ShowToday = true,
                ShowTodayCircle = true,
                Location = new Point(5, 5)
            };

            calendar.Size = calendar.GetPreferredSize(
                new Size(0, 0));

            // -----------------------------
            // Popup
            // -----------------------------

            Form popup = new Form
            {
                FormBorderStyle = FormBorderStyle.None,
                StartPosition = FormStartPosition.Manual,
                ShowInTaskbar = false,
                ShowIcon = false,
                BackColor = Color.FromArgb(32, 43, 54),
                Padding = new Padding(5),
                TopMost = true
            };

            popup.Controls.Add(calendar);

            popup.ClientSize = new Size(
                calendar.Width + 10,
                calendar.Height + 10);

            // -----------------------------
            // Events
            // -----------------------------

            calendar.DateSelected += MonthCalendar_DateSelected;

            // -----------------------------
            // Store references
            // -----------------------------

            calendarPopup = popup;
            monthCalendar = calendar;

            // -----------------------------
            // Position ABOVE the control
            // -----------------------------

            Point screenPoint = PointToScreen(
                new Point(
                    0,
                    -popup.Height - 5));

            popup.Location = screenPoint;

            // -----------------------------
            // Button state
            // -----------------------------

            glassButton1.Selected = true;

            // -----------------------------
            // Show popup
            // -----------------------------

            popup.Show();

            popup.BringToFront();

            Invalidate();
        }

        private void MonthCalendar_DateSelected(object sender, DateRangeEventArgs e)
        {
            modernTextBox1.Text = e.Start.ToString("dd MM yyyy");
            CloseCalendarPopup();
        }


        private void CloseCalendarPopup()
        {
            Form popup = calendarPopup;
            MonthCalendar calendar = monthCalendar;

            // Clear references first
            calendarPopup = null;
            monthCalendar = null;

            glassButton1.Selected = false;

            if (calendar != null)
            {
                calendar.DateSelected -= MonthCalendar_DateSelected;
            }

            if (popup != null && !popup.IsDisposed)
            {
               // popup.Deactivate -= CalendarPopup_Deactivate;
                popup.Close();
                popup.Dispose();
            }

            Invalidate();
        }

        private void CalendarPopup_Deactivate(object sender, EventArgs e)
        {
            CloseCalendarPopup();
        }

        private void modernTextBox1_Load(object sender, EventArgs e)
        {

        }
    }
}
