using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartMRT_DashCombine_Setup
{
    public partial class UC_Line_ModuleCard : UserControl
    {
      
    public int ModuleID { get; set; }

        public string ModuleName
        {
            get => lblModuleName.Text;
            set => lblModuleName.Text = value;
        }

        public string ModuleCode { get; set; }

        public event EventHandler ModuleClicked;

        public UC_Line_ModuleCard()
        {
            InitializeComponent();

            this.Width = 480;
            this.Height = 320;

            this.Margin = new Padding(5);

            this.Click += ModuleCard_Click;
            lblModuleName.Click += ModuleCard_Click;
            pictureModule.Click += ModuleCard_Click;
        }

        private void ModuleCard_Click(
            object sender,
            EventArgs e)
        {
            ModuleClicked?.Invoke(
                this,
                EventArgs.Empty);
        }

        private void UC_Line_ModuleCard_Load(object sender, EventArgs e)
        {

        }

        private void pictureModule_Click(object sender, EventArgs e)
        {

        }

        private void btnChangeImage_Click(
    object sender,
    EventArgs e)
        {
            using (OpenFileDialog dialog =
                new OpenFileDialog())
            {
                dialog.Filter =
                    "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

                if (dialog.ShowDialog() ==
                    DialogResult.OK)
                {
                    pictureModule.Image =
                        Image.FromFile(
                            dialog.FileName);
                }
            }
        }
    }
}
