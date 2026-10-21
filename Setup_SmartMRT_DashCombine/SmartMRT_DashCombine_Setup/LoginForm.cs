//using System;
//using System.Windows.Forms;

//namespace SmartMRT_DashCombine_Setup
//{
//    public partial class LoginForm : Form
//    {
//        public LoginForm()
//        {
//            InitializeComponent();
//            // populate roles
//            cboRole.Items.Add("Guest");
//            cboRole.Items.Add("Admin1");
//            cboRole.Items.Add("Admin");
//            cboRole.SelectedIndex = 0;
//        }

//        private void btnLogin_Click(object sender, EventArgs e)
//        {
//            var user = txtUser.Text?.Trim() ?? string.Empty;
//            var roleText = cboRole.SelectedItem as string ?? "Guest";

//            UserRole role = UserRole.Guest;
//            if (string.Equals(roleText, "Admin", StringComparison.OrdinalIgnoreCase)) role = UserRole.Admin;
//            else if (string.Equals(roleText, "Admin1", StringComparison.OrdinalIgnoreCase)) role = UserRole.Admin1;

//            // set session
//            AppSession.SetSession(user, role);

//            this.DialogResult = DialogResult.OK;
//            this.Close();
//        }

//        private void btnCancel_Click(object sender, EventArgs e)
//        {
//            this.DialogResult = DialogResult.Cancel;
//            this.Close();
//        }
//    }
//}
