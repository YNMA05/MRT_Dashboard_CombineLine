using Newtonsoft.Json;
using SmartMRT_DashCombine_Setup.Controls;
using SmartMRT_DashCombine_Setup.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartMRT_DashCombine_Setup
{
    public partial class FrmAddUser : Form
    {
        //private DashboardConfig config;
        //private bool isEditMode = false;
        private bool showPassword = false;
        public class RoleItem
        {
            public int RoleID { get; set; }

            public string RoleName { get; set; }

            public override string ToString()
            {
                return RoleName;
            }
        }
        public FrmAddUser()
        {
            InitializeComponent();
            ApplyModernStyle();
            //config = DashboardConfigManager.Load();
            this.Load += FrmAddUser_Load;
        }

        private void ApplyModernStyle()
        {
            txtPwd.UseSystemPasswordChar = true;

            //btnSave.BackColor = Color.FromArgb(0, 192, 192);
            //btnSave.ForeColor = Color.White;
            //btnSave.BackColor = Color.Transparent;
            //btnSave.ForeColor = Color.FromArgb(120, 220, 255);
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }


        private void FrmAddUser_Load(object sender, EventArgs e)
        {
            try
            {
                BuildUI();
                //string password =
                //    PasswordEncryption.Decrypt(
                //        config.DashboardDatabase.Password);

                //string connectionString =
                //    ConnectionMain.GetSqlConnectionString(
                //        config.DashboardDatabase.Server,
                //        config.DashboardDatabase.Instance,
                //        config.DashboardDatabase.Username,
                //        password,
                //        config.DashboardDatabase.Database);
                string connectionString = ConnectionMain.GetDashboardConnection();

                string sql = @"
            SELECT
                ROLEID,
                ROLENAME
            FROM DC_ROLE
            WHERE STATUS_ACTIVE = 1 AND ROLEID != 1
            ORDER BY ROLENAME";

                using (SqlConnection conn =
                    new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd =
                        new SqlCommand(sql, conn))
                    {
                        using (SqlDataReader reader =
                            cmd.ExecuteReader())
                        {
                            ddlRole.Items.Clear();

                            while (reader.Read())
                            {
                                ddlRole.Items.Add(
                                    new RoleItem
                                    {
                                        RoleID =
                                            Convert.ToInt32(
                                                reader["ROLEID"]),

                                        RoleName =
                                            reader["ROLENAME"]
                                                .ToString()
                                    });
                            }
                        }
                    }
                }

                if (ddlRole.Items.Count > 0)
                    ddlRole.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Failed to load roles:\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void BuildUI()
        {
           // Controls.Clear();
            Label lblTitle = new Label
            {
                Text = "Create User",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(30, 25)
            };

            Label lblSubtitle = new Label
            {
                Text = "Add a new SmartMRT system user",
                Font = new Font("Segoe UI", 9.5f),
                ForeColor = Color.FromArgb(150, 180, 210),
                AutoSize = true,
                Location = new Point(32, 60)
            };

            GlassButton btnSaveGlass = new GlassButton
            {
                Name = "btnSave",
                Text = "✓  Create User",
                Size = new Size(150, 42),
                Location = new Point(220, 350),

                BorderColor =
        Color.FromArgb(120, 220, 255),

                GlassColor =
        Color.FromArgb(25, 55, 65),

                TextColor = Color.White
            };

            GlassButton btnCancelGlass = new GlassButton
            {
                Name = "btnCancel",
                Text = " Cancel",
                Size = new Size(150, 42),
                Location = new Point(60,350),

                BorderColor =
        Color.FromArgb(120, 220, 255),

                GlassColor =
        Color.FromArgb(25, 55, 65),

                TextColor = Color.White
            };

            btnSaveGlass.Click += btnSave_Click;
            btnCancelGlass.Click += btnCancel_Click;
            Controls.Add(btnCancelGlass);
            Controls.Add(btnSaveGlass);

            Controls.Add(lblTitle);
            Controls.Add(lblSubtitle);

            lblTitle.BringToFront();
            lblSubtitle.BringToFront();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate username
                if (string.IsNullOrWhiteSpace(txtUsername.Text))
                {
                    MessageBox.Show(
                        "Please insert username!",
                        "Validation",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    txtUsername.Focus();
                    return;
                }

                // Validate password
                if (string.IsNullOrWhiteSpace(txtPwd.Text))
                {
                    MessageBox.Show(
                        "Please insert password!",
                        "Validation",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    txtPwd.Focus();
                    return;
                }

                // Validate role
                RoleItem selectedRole =
                    ddlRole.SelectedItem as RoleItem;

                if (selectedRole == null)
                {
                    MessageBox.Show(
                        "Please select a role.",
                        "Validation",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    ddlRole.Focus();
                    return;
                }

                // Get database connection
                //string dbPassword =
                //    PasswordEncryption.Decrypt(
                //        config.DashboardDatabase.Password);

                //string connectionString =
                //    ConnectionMain.GetSqlConnectionString(
                //        config.DashboardDatabase.Server,
                //        config.DashboardDatabase.Instance,
                //        config.DashboardDatabase.Username,
                //        dbPassword,
                //        config.DashboardDatabase.Database);
                string connectionString = ConnectionMain.GetDashboardConnection();

                // Hash user password
                string passwordHash =
                    BCrypt.Net.BCrypt.HashPassword(
                        txtPwd.Text);

                string insertSql = @"
            INSERT INTO DC_USER
            (
                USERNAME,
                PASSWORD_HASH,
                ROLEID,
                STATUS_ACTIVE
            )
            VALUES
            (
                @Username,
                @PasswordHash,
                @RoleID,
                @Status
            )";

                using (SqlConnection conn =
                    new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd =
                        new SqlCommand(insertSql, conn))
                    {
                        cmd.Parameters.Add(
                            "@Username",
                            SqlDbType.NVarChar, 100)
                            .Value = txtUsername.Text.Trim();

                        cmd.Parameters.Add(
                            "@PasswordHash",
                            SqlDbType.NVarChar, 255)
                            .Value = passwordHash;

                        cmd.Parameters.Add(
                            "@RoleID",
                            SqlDbType.Int)
                            .Value = selectedRole.RoleID;

                        cmd.Parameters.Add(
                            "@Status",
                            SqlDbType.Bit)
                            .Value = true;

                        cmd.ExecuteNonQuery();
                    }
                }


                ActivityLogger.Log(
                    connectionString,
                    "CREATE_USER",
                    txtUsername.Text.Trim(),
                    $"Created user: {txtUsername.Text.Trim()}");

                MessageBox.Show(
                    "User created successfully!",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                txtUsername.Clear();
                txtPwd.Clear();
                ddlRole.SelectedIndex = -1;
                this.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Failed to create user:\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void FrmAddUser_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void btnShowPwd_Click(object sender, EventArgs e)
        {
            showPassword =! showPassword;
            txtPwd.UseSystemPasswordChar = !showPassword;
            //btnShowPwd.Text = showPassword ? "Hide" : "Show";
        }

        private string GetPasswordStrength(string password)
        {
            if (password.Length < 6)
                return "Weak";

            bool hasUpper =
                password.Any(char.IsUpper);

            bool hasLower =
                password.Any(char.IsLower);

            bool hasNumber =
                password.Any(char.IsDigit);

            if (password.Length >= 10 &&
                hasUpper &&
                hasLower &&
                hasNumber)
            {
                return "Strong";
            }

            return "Medium";
        }

        private void txtPwd_TextChanged(object sender, EventArgs e)
        {
            string password = txtPwd.Text;
            lblPasswordStrength.Visible = true;
            progressBarPwd.Visible = true;

            int progress = GetPasswordStrengthValue(password);

            progressBarPwd.Value = progress;

            string strength = GetPasswordStrength(password);

            lblPasswordStrength.Text =
                "Password Strength : " + strength;

            if (strength == "Weak")
            {
                progressBarPwd.ForeColor =
                    Color.FromArgb(255, 92, 92);

                lblPasswordStrength.ForeColor =
                    Color.FromArgb(255, 92, 92);
            }
            else if (strength == "Medium")
            {
                progressBarPwd.ForeColor =
                    Color.FromArgb(255, 181, 71);

                lblPasswordStrength.ForeColor =
                    Color.FromArgb(255, 181, 71);
            }
            else 
            {
                progressBarPwd.ForeColor =
                    Color.FromArgb(56, 211, 159);

                lblPasswordStrength.ForeColor =
                    Color.FromArgb(56, 211, 159);
            }
        }

        private int GetPasswordStrengthValue(string password)
        {
            if (string.IsNullOrEmpty(password))
                return 0;

            int score = 0;

            if (password.Length >= 6)
                score += 25;

            if (password.Length >= 10)
                score += 25;

            if (password.Any(char.IsUpper))
                score += 15;

            if (password.Any(char.IsLower))
                score += 15;

            if (password.Any(char.IsDigit))
                score += 10;

            if (password.Any(ch => !char.IsLetterOrDigit(ch)))
                score += 10;

            return Math.Min(score, 100);
        }

        private void ddlRole_Load(object sender, EventArgs e)
        {

        }
    }
}
