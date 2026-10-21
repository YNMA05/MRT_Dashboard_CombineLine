using SmartMRT_DashCombine_Setup.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using SmartMRT_DashCombine_Setup.Controls;

namespace SmartMRT_DashCombine_Setup
{
    public partial class FrmUnlockLogin : Form
    {
        private string connectionString = string.Empty;
        public int RoleID { get; private set; }
        public string Username { get; private set; }
        public string Role { get; private set; }
        public int UserID { get; private set; } = 0;

        private GlassButton glassLogin;
        private GlassButton glassCancel;

       // private string connectionString;

        public FrmUnlockLogin()
        {
            InitializeComponent();
            ApplyModernStyle();
        }

        private void ApplyModernStyle()
        {
            txtPassword.UseSystemPasswordChar = true;

            glassLogin = new GlassButton
            {
                Name = "glassLogin",
                Text = "Log In",
                Size = btnLogIn.Size,
                Location = btnLogIn.Location,
                BorderColor = Color.FromArgb(56, 211, 159),
                GlassColor = Color.FromArgb(25, 55, 65),
                TextColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };

            glassCancel = new GlassButton
            {
                Name = "glassCancel",
                Text = "Cancel",
                Size = btnCancel.Size,
                Location = btnCancel.Location,
                BorderColor = Color.FromArgb(56, 211, 159),
                GlassColor = Color.FromArgb(25, 55, 65),
                TextColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)

            };

            glassCancel.Click += btnCancel_Click;
            glassLogin.Click += btnLogIn_Click;

            btnCancel.Parent.Controls.Add(glassCancel);
            btnLogIn.Parent.Controls.Add(glassLogin);

            btnLogIn.Visible = false;
            btnCancel.Visible = false;

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            this.Close();
        }

        private void FrmUnlockLogin_Load(object sender, EventArgs e)
        {
        }

        //private void btnLogIn_Click(object sender, EventArgs e)
        //{
        //    Username = txtUsername.Text;

        //    this.DialogResult = DialogResult.OK;
        //}
        private void btnLogIn_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show(
                    "Please enter username and password.",
                    "Login",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (ValidateLogin(username, password))
            {
                AppSession.SetSession(UserID, Username, RoleID, Role);
                PermissionManager.LoadPermissions(connectionString, RoleID);
                ActivityLogger.Log(
                    connectionString,
                    "LOGIN",
                    Username,
                    "User logged in successfully");

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                ActivityLogger.Log(connectionString,"LOGIN","Error","Invalid username or password");
                MessageBox.Show(
                    "Invalid username or password.",
                    "Login Failed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                txtPassword.Clear();
                txtPassword.Focus();
            }
        }

        private bool ValidateLogin(string username,string password)
        {
            try
            {
                // Load your DashboardConfig
                //var config = DashboardConfigManager.Load();

                //if (config == null)
                //{
                //    MessageBox.Show(
                //        "DashboardConfig.json was not found.",
                //        "Login",
                //        MessageBoxButtons.OK,
                //        MessageBoxIcon.Error);

                //    return false;
                //}

                //string decryptedPassword =
                //    PasswordEncryption.Decrypt(
                //        config.DashboardDatabase.Password);

                //connectionString =
                // ConnectionMain.GetSqlConnectionString(
                //     config.DashboardDatabase.Server,
                //     config.DashboardDatabase.Instance,
                //     config.DashboardDatabase.Username,
                //     decryptedPassword,
                //     config.DashboardDatabase.Database);

                connectionString = ConnectionMain.GetDashboardConnection();

                using (SqlConnection connection =
                    new SqlConnection(connectionString))
                {
                    connection.Open();


                    string sql = @"
                SELECT
                    U.USERID,
                    U.USERNAME,
                    U.PASSWORD_HASH,
                    U.ROLEID,
                    R.ROLENAME
                FROM DC_USER U
                INNER JOIN DC_ROLE R
                    ON U.ROLEID = R.ROLEID
                WHERE U.USERNAME = @Username
                  AND U.STATUS_ACTIVE = 1
                  AND R.STATUS_ACTIVE = 1;
            ";


                    using (SqlCommand command =
                        new SqlCommand(sql, connection))
                    {
                        command.Parameters.Add(
                            "@Username",SqlDbType.NVarChar,50).Value =
                            username;

                        using (SqlDataReader reader =
                            command.ExecuteReader())
                        {

                            if (!reader.Read())
                                return false;

                            string storedHash =
                                reader["PASSWORD_HASH"].ToString();

                            bool passwordValid =
                                BCrypt.Net.BCrypt.Verify(
                                    password,
                                    storedHash);

                            if (!passwordValid)
                                return false;
                            RoleID =
                                Convert.ToInt32(
                                    reader["ROLEID"]);
                            UserID =
                                Convert.ToInt32(
                                    reader["USERID"]);

                            Username =
                                reader["USERNAME"].ToString();

                            Role =
                                reader["ROLENAME"].ToString();

                            PermissionManager.LoadPermissions(connectionString, RoleID);


                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //ActivityLogger.Log(connectionString, "LOGIN", "Error", "Login Failed");
                MessageBox.Show(
                    $"Login failed:\n{ex.Message}",
                    "Login Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);



                return false;
            }
        }
    }
}