using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace SmartMRT_DashCombine_Setup
{
    public partial class UC_Line_ModuleCard : UserControl
    {
        // =====================================================
        // EVENTS
        // =====================================================

        public event EventHandler ImageChanged;
        public event EventHandler ModuleClicked;
        public event EventHandler UploadImageRequested;


        // =====================================================
        // PROPERTIES
        // =====================================================

        public int ModuleID { get; set; }

        public string ModuleName
        {
            get => lblModuleName.Text;
            set => lblModuleName.Text = value;
        }

        public string ModuleCode { get; set; }

        private string _imagePath;

        public string ImagePath
        {
            get => _imagePath;

            set
            {
                _imagePath = value;
                LoadModuleImage();
            }
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                Invalidate(); // Redraw the control to reflect selection state
            }
        }

        private bool _canUploadImage;
        public bool CanUploadImage
        {
            get => _canUploadImage;
            set
            {
                _canUploadImage = value;
                if (btnChangeImage != null)
                {
                    btnChangeImage.Visible = value;
                }
            }
        }


        // =====================================================
        // COLORS
        // =====================================================

        private readonly Color BackgroundColor =
            Color.FromArgb(16, 36, 71);

        private readonly Color BorderColor =
            Color.FromArgb(40, 75, 122);

        private readonly Color AccentColor =
            Color.FromArgb(0, 212, 255);

        private readonly Color TextColor =
            Color.White;

        private readonly Color SecondaryTextColor =
            Color.FromArgb(168, 179, 199);


        // =====================================================
        // CONSTRUCTOR
        // =====================================================

        public UC_Line_ModuleCard()
        {
            InitializeComponent();

            CanUploadImage = false;

            InitializeCard();
            InitializeImage();
            InitializeLabels();
            InitializeButton();
            InitializeEvents();
        }


        // =====================================================
        // CARD
        // =====================================================

        private void InitializeCard()
        {
            this.Margin = new Padding(8);
            this.BackColor = BackgroundColor;

            this.DoubleBuffered = true;

            this.SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw,
                true);

            // Example size
            this.Width = 150;
            this.Height = 250;
        }


        // =====================================================
        // IMAGE
        // =====================================================

        private void InitializeImage()
        {
            pictureModule.BorderStyle =
                BorderStyle.None;

            pictureModule.SizeMode =
                PictureBoxSizeMode.Zoom;

            pictureModule.BackColor =
                Color.FromArgb(8, 21, 47);

            pictureModule.Cursor =
                Cursors.Hand;
        }


        // =====================================================
        // LABELS
        // =====================================================

        private void InitializeLabels()
        {
            lblModuleName.ForeColor =
                TextColor;

            lblModuleName.Font =
                new Font(
                    "Segoe UI",
                    10F,
                    FontStyle.Bold);

            lblModuleName.AutoEllipsis = true;

            lblModuleName.Cursor =
                Cursors.Hand;
        }


        // =====================================================
        // BUTTON
        // =====================================================

        private void InitializeButton()
        {
            btnChangeImage.Text = "📷";

            btnChangeImage.FlatStyle = FlatStyle.Flat;
            btnChangeImage.FlatAppearance.BorderSize = 1;
            btnChangeImage.FlatAppearance.BorderColor =
                Color.FromArgb(0, 212, 255);

            btnChangeImage.BackColor =
                Color.FromArgb(25, 55, 75);

            btnChangeImage.ForeColor =
                Color.FromArgb(0, 212, 255);

            btnChangeImage.Font =
                new Font(
                    "Segoe UI Emoji",
                    11F,
                    FontStyle.Regular);

            btnChangeImage.Cursor =
                Cursors.Hand;

            btnChangeImage.Size =
                new Size(38, 38);

            btnChangeImage.Anchor =
                AnchorStyles.Bottom |
                AnchorStyles.Right;

            // Position manually
            btnChangeImage.Location =
                new Point(
                    this.ClientSize.Width -
                    btnChangeImage.Width -
                    12,
                    this.ClientSize.Height -
                    btnChangeImage.Height -
                    12);
        }


        // =====================================================
        // EVENTS
        // =====================================================

        private void InitializeEvents()
        {
            this.Click += ModuleCard_Click;

            lblModuleName.Click += ModuleCard_Click;

            pictureModule.Click += ModuleCard_Click;
            // Camera button
           // btnChangeImage.Click += btnChangeImage_Click;
            btnChangeImage.MouseEnter +=
                BtnChangeImage_MouseEnter;

            btnChangeImage.MouseLeave +=
                BtnChangeImage_MouseLeave;
        }


        // =====================================================
        // MODULE CLICK
        // =====================================================

        private void ModuleCard_Click(
            object sender,
            EventArgs e)
        {
            ModuleClicked?.Invoke(
                this,
                EventArgs.Empty);
        }


        // =====================================================
        // IMAGE LOADING
        // =====================================================

        private void LoadModuleImage()
        {
            // Dispose old image first
            if (pictureModule.Image != null)
            {
                Image oldImage =
                    pictureModule.Image;

                pictureModule.Image = null;

                oldImage.Dispose();
            }

            if (string.IsNullOrWhiteSpace(
                _imagePath))
            {
                ShowImagePlaceholder();
                return;
            }

            string fullPath =
                Path.Combine(
                    Application.StartupPath,
                    _imagePath);

            if (!File.Exists(fullPath))
            {
                ShowImagePlaceholder();
                return;
            }

            try
            {
                using (Image temp =
                    Image.FromFile(fullPath))
                {
                    pictureModule.Image =
                        new Bitmap(temp);
                }
            }
            catch
            {
                ShowImagePlaceholder();
            }
        }


        // =====================================================
        // PLACEHOLDER
        // =====================================================

        private void ShowImagePlaceholder()
        {
            Bitmap bitmap =
                new Bitmap(
                    Math.Max(1, pictureModule.Width),
                    Math.Max(1, pictureModule.Height));

            using (Graphics g =
                Graphics.FromImage(bitmap))
            {
                g.Clear(
                    Color.FromArgb(
                        8,
                        21,
                        47));

                g.SmoothingMode =
                    SmoothingMode.AntiAlias;

                using (Font font =
                    new Font(
                        "Segoe UI",
                        10F,
                        FontStyle.Bold))
                {
                    string text =
                        "NO IMAGE";

                    SizeF size =
                        g.MeasureString(
                            text,
                            font);

                    using (Brush brush =
                        new SolidBrush(
                            Color.FromArgb(
                                100,
                                130,
                                160)))
                    {
                        g.DrawString(
                            text,
                            font,
                            brush,
                            (bitmap.Width - size.Width) / 2,
                            (bitmap.Height - size.Height) / 2);
                    }
                }
            }

            pictureModule.Image = bitmap;
        }


        // =====================================================
        // CHANGE IMAGE
        // =====================================================
        private void btnChangeImage_Click(
    object sender,
    EventArgs e)
        {
            using (OpenFileDialog dialog =
                new OpenFileDialog())
            {
                dialog.Title = "Select Module Image";

                dialog.Filter =
                    "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

                dialog.Multiselect = false;

                if (dialog.ShowDialog() !=
                    DialogResult.OK)
                {
                    return;
                }

                string imageFolder =
                    Path.Combine(
                        Application.StartupPath,
                        "ModuleImages");

                Directory.CreateDirectory(imageFolder);

                string extension =
                    Path.GetExtension(dialog.FileName);

                string fileName =
                    $"Module_{ModuleID}{extension}";

                string destination =
                    Path.Combine(
                        imageFolder,
                        fileName);

                try
                {
                    File.Copy(
                        dialog.FileName,
                        destination,
                        true);

                    ImagePath =
                        Path.Combine(
                            "ModuleImages",
                            fileName);

                    ImageChanged?.Invoke(
                        this,
                        EventArgs.Empty);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "Unable to save the module image.\r\n\r\n" +
                        ex.Message,
                        "Image Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }
        //private void btnChangeImage_Click(
        //    object sender,
        //    EventArgs e)
        //{

        //    UploadImageRequested?.Invoke(
        //        this,
        //        EventArgs.Empty);

        //    if (!CanUploadImage)
        //        return;

        //    using (OpenFileDialog dialog =
        //        new OpenFileDialog())
        //    {
        //        dialog.Title =
        //            "Select Module Image";

        //        dialog.Filter =
        //            "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

        //        dialog.Multiselect = false;

        //        if (dialog.ShowDialog() !=
        //            DialogResult.OK)
        //        {
        //            return;
        //        }

        //        string imageFolder =
        //            Path.Combine(
        //                Application.StartupPath,
        //                "ModuleImages");

        //        Directory.CreateDirectory(
        //            imageFolder);

        //        string extension =
        //            Path.GetExtension(
        //                dialog.FileName);

        //        string fileName =
        //            $"Module_{ModuleID}{extension}";

        //        string destination =
        //            Path.Combine(
        //                imageFolder,
        //                fileName);

        //        try
        //        {
        //            File.Copy(
        //                dialog.FileName,
        //                destination,
        //                true);

        //            ImagePath =
        //                Path.Combine(
        //                    "ModuleImages",
        //                    fileName);

        //            ImageChanged?.Invoke(
        //                this,
        //                EventArgs.Empty);
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(
        //                "Unable to save the module image.\r\n\r\n" +
        //                ex.Message,
        //                "Image Error",
        //                MessageBoxButtons.OK,
        //                MessageBoxIcon.Error);
        //        }
        //    }
        //}


        // =====================================================
        // BUTTON HOVER
        // =====================================================

        private void BtnChangeImage_MouseEnter(
            object sender,
            EventArgs e)
        {
            btnChangeImage.BackColor =
                Color.FromArgb(
                    0,
                    90,
                    120);
        }

        private void BtnChangeImage_MouseLeave(
            object sender,
            EventArgs e)
        {
            btnChangeImage.BackColor =
                Color.FromArgb(
                    30,
                    55,
                    85);
        }


        // =====================================================
        // PAINT CARD
        // =====================================================

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.SmoothingMode =
                System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            Rectangle rect =
                new Rectangle(
                    1,
                    1,
                    Width - 3,
                    Height - 3);

            Color background =
                IsSelected
                    ? Color.FromArgb(20, 52, 82)
                    : BackgroundColor;

            Color border =
                IsSelected
                    ? Color.FromArgb(0, 212, 255)
                    : BorderColor;

            float borderWidth =
                IsSelected ? 2.0f : 1.0f;

            using (GraphicsPath path =
                CreateRoundedRectangle(
                    rect,
                    14))
            {
                using (SolidBrush brush =
                    new SolidBrush(background))
                {
                    e.Graphics.FillPath(
                        brush,
                        path);
                }

                using (Pen pen =
                    new Pen(
                        border,
                        borderWidth))
                {
                    e.Graphics.DrawPath(
                        pen,
                        path);
                }
            }
        }


        // =====================================================
        // ROUNDED RECTANGLE
        // =====================================================

        private GraphicsPath CreateRoundedRectangle(
            Rectangle rect,
            int radius)
        {
            GraphicsPath path =
                new GraphicsPath();

            int diameter =
                radius * 2;

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


        // =====================================================
        // CLEANUP
        // =====================================================

        protected override void Dispose(
            bool disposing)
        {
            if (disposing)
            {
                if (pictureModule != null &&
                    pictureModule.Image != null)
                {
                    pictureModule.Image.Dispose();
                    pictureModule.Image = null;
                }
            }

            base.Dispose(disposing);
        }


        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            LayoutCard();
            LayoutCameraButton();
        }

        private void LayoutCameraButton()
        {
            if (btnChangeImage == null)
                return;

            btnChangeImage.Location =
                new Point(
                    ClientSize.Width -
                    btnChangeImage.Width -
                    12,
                    ClientSize.Height -
                    btnChangeImage.Height -
                    12);
        }

        private void LayoutCard()
        {
            if (pictureModule == null ||
                lblModuleName == null ||
                btnChangeImage == null)
                return;

            int padding = 10;

            int imageHeight =
                (int)(ClientSize.Height * 0.68);

            if (imageHeight < 120)
                imageHeight = 120;

            imageHeight =
                Math.Min(
                    imageHeight,
                    ClientSize.Height - 70);


            // ==========================================
            // IMAGE
            // ==========================================

            pictureModule.Location =
                new Point(
                    padding,
                    padding);

            pictureModule.Size =
                new Size(
                    Math.Max(
                        1,
                        ClientSize.Width -
                        padding * 2),

                    Math.Max(
                        1,
                        imageHeight));


            // ==========================================
            // MODULE NAME
            // ==========================================

            lblModuleName.Location =
                new Point(
                    padding,
                    pictureModule.Bottom + 8);

            lblModuleName.Size =
                new Size(
                    Math.Max(
                        1,
                        ClientSize.Width -
                        padding * 2),

                    25);


            // ==========================================
            // IMAGE BUTTON
            // ==========================================

            btnChangeImage.Location =
                new Point(
                    pictureModule.Right -
                    btnChangeImage.Width -
                    8,

                    pictureModule.Bottom -
                    btnChangeImage.Height -
                    8);
        }

        public void SetUploadImagePermission(bool canUpload)
        {
            btnChangeImage.Visible = canUpload;
        }
    }
}