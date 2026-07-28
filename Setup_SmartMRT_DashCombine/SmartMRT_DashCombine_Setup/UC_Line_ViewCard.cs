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

        public bool IsSelected
        {
            get => chkSelected.Checked;
            set => chkSelected.Checked = value;
        }

        public UC_Line_ViewCard()
        {
            InitializeComponent();
        }


        private void UC_Line_ViewCard_Load(
            object sender,
            EventArgs e)
        {
            lblLine.Text =
                $"Line {LineNumber}";

            lblIPAddress.Text =
                IPAddress;
        }      
    }
}

