using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace SmartMRT_DashCombine_Setup
{
    public partial class UC_MRT_DashCombine_DB : UserControl
    {
        private string configFile = "DashboardConfig.json";
        private bool isEditMode = false;

        public UC_MRT_DashCombine_DB()
        {
            InitializeComponent();

            //UC_MRT_DashCombine_DB_Load(this, EventArgs.Empty);
            this.Load +=UC_MRT_DashCombine_DB_Load;
            SetEditMode(false);
        }

        public void SetEditMode(bool editMode)
        {
            isEditMode = editMode;
            txtIP.ReadOnly = !editMode;
            txtInstance.ReadOnly = !editMode;
            txtDb.ReadOnly = !editMode;
            txtUsername.ReadOnly = !editMode;
            txtPwd.ReadOnly = !editMode;
            btnCreateTable.Enabled = editMode;
            btnCreateDb.Enabled = editMode;
            btnTestConnection.Enabled = editMode;
            btnSave.Enabled = editMode;

        }

        private void UC_MRT_DashCombine_DB_Load(object sender, EventArgs e)
        {
            if (File.Exists(configFile))
            {
                try
                {
                    string json = File.ReadAllText(configFile);

                    DashboardConfig config =
                        JsonConvert.DeserializeObject<DashboardConfig>(json);

                    if (config != null && config.DashboardDatabase != null)
                    {
                        txtIP.Text = config.DashboardDatabase.Server;
                        txtInstance.Text = config.DashboardDatabase.Instance;
                        txtDb.Text = config.DashboardDatabase.Database;
                        txtUsername.Text = config.DashboardDatabase.Username;
                        txtPwd.Text =
    PasswordEncryption.Decrypt(
        config.DashboardDatabase.Password);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "Failed to load DashboardConfig.json.\n\n" + ex.Message,
                        "Configuration Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            else
            {
                txtIP.Text = GetLocalIPAddress();
                txtDb.Text = "Dashboard_GlobalDB";
                txtInstance.Text = "SQLEXPRESS";
            }
        }

        private string GetLocalIPAddress()
        {
            try
            {
                IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

                foreach (IPAddress ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        return ip.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unable to get local IP address.\n\n" + ex.Message,
                    "Network Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }

            return "127.0.0.1";
        }

        private async void btnTestConnection_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIP.Text))
            {
                MessageBox.Show("Server IP is required.");
                txtIP.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtDb.Text))
            {
                MessageBox.Show("Database name is required.");
                txtDb.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Username is required.");
                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPwd.Text))
            {
                MessageBox.Show("Password is required.");
                txtPwd.Focus();
                return;
            }

            try
            {
                string connStr =
                    ConnectionMain.GetSqlConnectionString(
                        txtIP.Text,
                        txtInstance.Text,
                        txtUsername.Text,
                        txtPwd.Text,
                        txtDb.Text);

                bool connected =
                    await ConnectionTester.TestAsync(connStr);

                if (connected)
                {
                    MessageBox.Show(
                        "Connection Successful!",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(
                        "Connection Failed.",
                        "Failed",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Connection test failed.\n\n" + ex.Message,
                    "Database Connection Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DashboardConfig config = new DashboardConfig();

                config.DashboardDatabase = new DashboardDatabase
                {
                    Server = txtIP.Text.Trim(),
                    Instance = txtInstance.Text.Trim(),
                    Database = txtDb.Text.Trim(),
                    Username = txtUsername.Text.Trim(),

                    Password = PasswordEncryption.Encrypt(
         txtPwd.Text)
                };

                config.Dashboard = new DashboardSetting
                {
                    RefreshInterval = 10,
                    SignalRPort = 5000
                };

                string json =
                    JsonConvert.SerializeObject(
                        config,
                        Formatting.Indented);

                File.WriteAllText(configFile, json);

                MessageBox.Show(
                   "Configuration saved successfully!\n\n" +
                   "Location:\n" +
                   Path.GetFullPath(configFile),
                   "Saved",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Failed to save configuration.\n\n" + ex.Message,
                    "Save Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}