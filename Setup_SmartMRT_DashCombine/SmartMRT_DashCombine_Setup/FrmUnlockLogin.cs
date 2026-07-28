using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace SmartMRT_DashCombine_Setup
{
    public partial class FrmUnlockLogin : Form
    {
        public string Username { get; set; }

        public FrmUnlockLogin()
        {
            InitializeComponent();
        }

        private void FrmUnlockLogin_Load(object sender, EventArgs e)
        {
        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            Username = txtUsername.Text;

            this.DialogResult = DialogResult.OK;
        }
    }
}