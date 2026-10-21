using Newtonsoft.Json;
using System;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using BCrypt.Net;
using SmartMRT_DashCombine_Setup.Controls;
using SmartMRT_DashCombine_Setup.Logging;
using System.Runtime.CompilerServices;

namespace SmartMRT_DashCombine_Setup
{
    public partial class UC_MRT_DashCombine_DB : UserControl
    {
       // private string configFile = "DashboardConfig.json";
        private bool isEditMode = false;
        private GlassButton glassSaveButton;
        private GlassButton glassTestButton;
        private GlassButton glassDBButton;
        private GlassButton glassTableButton;
        private GlassButton glassSaveIP;
        private bool isIPChanged = false;
        private bool isLocalDatabase = false; // Flag to indicate if the database is local
        public event EventHandler ConfigurationSaved;

        public UC_MRT_DashCombine_DB()
        {
            InitializeComponent();

            //UC_MRT_DashCombine_DB_Load(this, EventArgs.Empty);
            //this.Load +=UC_MRT_DashCombine_DB_Load;
            // Configure glass panel
            ApplyModernStyle();
            this.Load += UC_MRT_DashCombine_DB_Load;
            //SetEditMode(false);
        }
        private void ApplyModernStyle()
        {
            pnlGlass.GlassOpacity = 180;
            pnlGlass.GlassColor =
                 Color.FromArgb(16, 36, 71);

            pnlGlass.BorderColor =
                 Color.FromArgb(40, 75, 122);

            pnlGlass.CornerRadius = 20;

            txtPwd.UseSystemPasswordChar = true;


            // -------------------------
            // CREATE GLASS SAVE BUTTON
            // -------------------------

            glassSaveButton = new GlassButton
            {
                Name = "glassSaveButton",
                Text = "✓  Save Configuration",
                Size = btnSave.Size,
                Location = btnSave.Location,
                BorderColor = Color.FromArgb(56, 211, 159),
                GlassColor = Color.FromArgb(25, 55, 65),
                TextColor = Color.White,
                Font = new Font( "Segoe UI",10F, FontStyle.Bold)
            };
            glassTestButton = new GlassButton
            {
                Name = "glassTestButton",
                Text = "Test Connection",
                Size = btnTestConnection.Size,
                Location = btnTestConnection.Location,
                BorderColor = Color.FromArgb(120, 220, 255),
                GlassColor =  Color.FromArgb(20, 45, 65),
                TextColor = Color.White,
                Font = new Font("Segoe UI",10F, FontStyle.Bold)
            };
            glassDBButton = new GlassButton
            {
                Name = "glassDBButton",
                Text = "Create Dashboard Database",
                Size = btnCreateDb.Size,
                Location = btnCreateDb.Location,
                BorderColor = Color.FromArgb(70, 130, 180),
                GlassColor = Color.FromArgb(20, 35, 55),
                TextColor =  Color.White,
                Font = new Font("Segoe UI",10F,FontStyle.Bold)
            };
            glassTableButton = new GlassButton
            {
                Name = "glassTableButton",
                Text = "Create Dashboard Table",
                Size = btnCreateTable.Size,
                Location = btnCreateTable.Location,
                BorderColor = Color.FromArgb(70, 130, 180),
                GlassColor = Color.FromArgb(20, 35, 55),
                TextColor = Color.White,
                Font = new Font("Segoe UI",10F, FontStyle.Bold)
            };
            glassSaveIP = new GlassButton
            {
                Name = "glassSaveIP",
                Text = "Save IP Address",
                Size = btnUpdateIP.Size,
                Location = btnUpdateIP.Location,
                BorderColor = Color.FromArgb(56, 211, 159),
                GlassColor = Color.FromArgb(25, 55, 65),
                TextColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };

            glassSaveButton.Click += btnSave_Click;
            //glassDBButton.Click += btnCreateDb_Click;
            glassTableButton.Click += btnCreateTable_Click;
            glassTestButton.Click += btnTestConnection_Click;
            glassSaveIP.Click += btnUpdateIP_Click;
            glassDBButton.Click += btnCreateDb_Click;


            // Add the glass button to the same parent
            btnSave.Parent.Controls.Add(glassSaveButton);
            btnCreateDb.Parent.Controls.Add(glassDBButton);
            btnCreateTable.Parent.Controls.Add(glassTableButton);
            btnTestConnection.Parent.Controls.Add(glassTestButton);
            btnUpdateIP.Parent.Controls.Add(glassSaveIP);

            // Hide the old Designer button
            btnSave.Visible = false;
            btnTestConnection.Visible = false;
            btnCreateTable.Visible = false;
            btnCreateDb.Visible = false;
            btnUpdateIP.Visible = false;
            btnUpdateIP.Visible = false;
        }

        public void SetEditMode(bool editMode)
        {
            isEditMode = editMode;

            // Normal database configuration fields
            txtInstance.Enabled = editMode;
            txtInstance.ReadOnly = !editMode;

            txtDb.Enabled = editMode;
            txtDb.ReadOnly = !editMode;

            txtUsername.Enabled = editMode;
            txtUsername.ReadOnly = !editMode;

            txtPwd.Enabled = editMode;
            txtPwd.ReadOnly = !editMode;

            // IP field
            // IP can ALWAYS be edited when IP recovery is required.
            txtIP.Enabled = editMode || isIPChanged;
            txtIP.ReadOnly = !(editMode || isIPChanged);

            // Normal Save Configuration
            if (glassSaveButton != null)
            {
                glassSaveButton.Enabled =
                    editMode && !isIPChanged;
            }

            if (glassDBButton != null)
                glassDBButton.Enabled = editMode;
            if (glassTableButton != null)
                glassTableButton.Enabled = editMode;

            // IP recovery button
            if (glassSaveIP != null)
            {
                glassSaveIP.Visible = isIPChanged;
                glassSaveIP.Enabled = isIPChanged;
            }

            // Test connection can always be used
            if (glassTestButton != null)
                glassTestButton.Enabled = true;
        }

        private void UC_MRT_DashCombine_DB_Load(object sender, EventArgs e)
        {
            // if (File.Exists(configFile))
            // {
            try
            {
                // string json = File.ReadAllText(configFile);
                DashboardConfig config =
                    DashboardConfigManager.Load();

                if (config != null &&
                    config.DashboardDatabase != null)
                {
                    txtIP.Text =
                        config.DashboardDatabase.Server;

                    txtInstance.Text =
                        config.DashboardDatabase.Instance;

                    txtDb.Text =
                        config.DashboardDatabase.Database;

                    txtUsername.Text =
                        config.DashboardDatabase.Username;

                    txtPwd.Text =
                        PasswordEncryption.Decrypt(
                            config.DashboardDatabase.Password);

                    isLocalDatabase =
                        config.DashboardDatabase.IsLocalDatabase;

                    rButton1.Checked = isLocalDatabase;
                    rbutton2.Checked = !isLocalDatabase;

                    if (isLocalDatabase)
                    {
                        CheckLocalIPChange();
                    }
                }
                else
                {
                    txtIP.Text = GetLocalIPAddress();
                    txtDb.Text = "Dashboard_GlobalDB";
                    txtInstance.Text = "SQLEXPRESS";

                    isLocalDatabase = true;

                    rButton1.Checked = true;
                    rbutton2.Checked = false;
                }
                //     }

            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Failed to load configuration.\n\n" + ex.Message,
                    "Load Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void CheckLocalIPChange()
        {
            DashboardConfig config =
                DashboardConfigManager.Load();

            if (config == null ||
                config.DashboardDatabase == null)
                return;

            // Only check local IP when SQL Server
            // is installed on this PC.
            if (!config.DashboardDatabase.IsLocalDatabase)
                return;

            string configuredIP =
                config.DashboardDatabase.Server;

            string currentIP =
                GetLocalIPAddress();

            if (string.IsNullOrWhiteSpace(configuredIP) ||
                string.IsNullOrWhiteSpace(currentIP))
                return;

            if (!string.Equals(
                configuredIP,
                currentIP,
                StringComparison.OrdinalIgnoreCase))
            {
                isIPChanged = true;

                txtIP.Enabled = true;
                txtIP.ReadOnly = false;

                if (glassSaveIP != null)
                {
                    glassSaveIP.Visible = true;
                    glassSaveIP.Enabled = true;
                }

                if (glassSaveButton != null)
                    glassSaveButton.Enabled = false;

                txtIP.Focus();
                txtIP.SelectAll();
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
            // Prefer the rounded textbox IP if present (user-editable overlay), otherwise use the plain textbox.
            string effectiveIP = txtIP.Text;


            // Keep txtIP in sync so other existing code that reads txtIP.Text works unchanged
            txtIP.Text = effectiveIP;

            if (string.IsNullOrWhiteSpace(effectiveIP))
            {
                MessageBox.Show("Server IP is required.");
                // Focus the rounded textbox if present, otherwise the original textbox
                if (this.Controls.ContainsKey("rtbIP"))
                {
                    var rtbControl = this.Controls["rtbIP"];
                    var focusMethod = rtbControl.GetType().GetMethod("Focus", Type.EmptyTypes);
                    if (focusMethod != null) focusMethod.Invoke(rtbControl, null);
                    else txtIP.Focus();
                }
                else
                {
                    txtIP.Focus();
                }
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
                // Update a status label if present for better UX
                Label statusLabel = null;
                if (this.Controls.ContainsKey("lblConnectionStatus"))
                {
                    statusLabel = this.Controls["lblConnectionStatus"] as Label;
                    if (statusLabel != null)
                    {
                        statusLabel.Text = "Connecting...";
                        statusLabel.ForeColor = System.Drawing.Color.Orange;
                    }
                }

                // Disable buttons while testing
                btnTestConnection.Enabled = false;
                btnCreateDb.Enabled = false;
                btnCreateTable.Enabled = false;

                bool connected = false;

                try
                {
                    // Try opening a SQL connection to the server (use master to validate server + auth)
                    string connStr = BuildConnectionString(useMaster: true);
                    using (var conn = new SqlConnection(connStr))
                    {
                        // OpenAsync will respect ConnectTimeout in the connection string
                        await conn.OpenAsync();
                        connected = (conn.State == System.Data.ConnectionState.Open);
                    }
                }
                catch (Exception ex)
                {
                    // detailed error in status tooltip/message
                    if (this.Controls.ContainsKey("lblConnectionStatus"))
                    {
                        var lbl = this.Controls["lblConnectionStatus"] as Label;
                        if (lbl != null)
                        {
                            lbl.Text = "Connection Failed";
                            lbl.ForeColor = System.Drawing.Color.Red;
                            lbl.Tag = ex.Message;
                        }
                    }

                    MessageBox.Show(
                        "Connection failed:\n" + ex.Message,
                        "Connection Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }

                if (connected)
                {
                    if (statusLabel != null)
                    {
                        statusLabel.Text = "Connected";
                        statusLabel.ForeColor = System.Drawing.Color.Lime;
                    }

                    MessageBox.Show(
                        "Connection Successful!",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    // allow DB/table operations
                    btnCreateDb.Enabled = true;
                    btnCreateTable.Enabled = true;
                }
                else
                {
                    if (statusLabel != null)
                    {
                        statusLabel.Text = "Connection Failed";
                        statusLabel.ForeColor = System.Drawing.Color.Red;
                    }
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
            finally
            {
                btnTestConnection.Enabled = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DashboardConfig config =
                    new DashboardConfig();

                config.DashboardDatabase =
                    new DashboardDatabase
                    {
                        Server = txtIP.Text.Trim(),
                        Instance = txtInstance.Text.Trim(),
                        Database = txtDb.Text.Trim(),
                        Username = txtUsername.Text.Trim(),

                        Password =
                            PasswordEncryption.Encrypt(
                                txtPwd.Text),

                        IsLocalDatabase =
                            isLocalDatabase
                    };

                config.Dashboard =
                    new DashboardSetting
                    {
                        RefreshInterval = 10,
                        SignalRPort = 5000
                    };

                if (!DashboardConfigManager.Save(config))
                    return;

                MessageBox.Show(
                    "Configuration saved successfully! Make sure to create database and table before leaving this page!",
                    "Saved",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);                
            }  
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Failed to save configuration.\n\n" +
                    ex.Message,
                    "Save Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        private void SaveIPAddress()
        {
            try
            {
                string currentIP = GetLocalIPAddress();

                if (string.IsNullOrWhiteSpace(currentIP))
                {
                    MessageBox.Show(
                        "Unable to detect the local IP address.",
                        "IP Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                DashboardConfig config =
                    DashboardConfigManager.Load();

                if (config == null)
                {
                    MessageBox.Show(
                        "Dashboard configuration could not be loaded.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    return;
                }

                if (config.DashboardDatabase == null)
                {
                    MessageBox.Show(
                        "Dashboard database configuration is missing.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    return;
                }

                // ONLY update the Server IP
                config.DashboardDatabase.Server = currentIP;

                if (!DashboardConfigManager.Save(config))
                    return;

                // Update UI
                txtIP.Text = currentIP;

                isIPChanged = false;

                txtIP.ReadOnly = !isEditMode;
                txtIP.Enabled = isEditMode;

                // Hide Update IP button
                if (glassSaveIP != null)
                {
                    glassSaveIP.Visible = false;
                    glassSaveIP.Enabled = false;
                }

                // Re-enable normal Save Configuration
                if (glassSaveButton != null)
                {
                    glassSaveButton.Enabled = isEditMode;
                }

                MessageBox.Show(
                    "Database IP address updated successfully.\n\n" +
                    "New IP: " + currentIP,
                    "IP Updated",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Failed to update IP address.\n\n" +
                    ex.Message,
                    "Update IP Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void SetIPChangeState(bool ipChanged)
        {
            if (glassSaveIP != null)
                glassSaveIP.Visible = ipChanged;

            if (glassSaveButton != null)
                glassSaveButton.Enabled =
                    isEditMode && !ipChanged;
        }
        //private void SaveIPAddress()
        //{
        //    try
        //    {
        //        string currentIP = GetLocalIPAddress();

        //        if (string.IsNullOrWhiteSpace(currentIP))
        //        {
        //            MessageBox.Show(
        //                "Unable to detect the local IP address.",
        //                "IP Error",
        //                MessageBoxButtons.OK,
        //                MessageBoxIcon.Warning);

        //            return;
        //        }

        //        DashboardConfig config =
        //            DashboardConfigManager.Load();

        //        if (config == null)
        //        {
        //            MessageBox.Show(
        //                "Dashboard configuration could not be loaded.",
        //                "Error",
        //                MessageBoxButtons.OK,
        //                MessageBoxIcon.Error);

        //            return;
        //        }

        //        // ONLY update Server IP
        //        config.DashboardDatabase.Server = currentIP;

        //        string json =
        //            JsonConvert.SerializeObject(
        //                config,
        //                Formatting.Indented);

        //        File.WriteAllText(configFile, json);

        //        txtIP.Text = currentIP;
        //        txtIP.ReadOnly = true;

        //        if (glassSaveIP != null)
        //            glassSaveIP.Visible = false;

        //        MessageBox.Show(
        //            "Database IP has been updated to:\n\n" +
        //            currentIP,
        //            "IP Updated",
        //            MessageBoxButtons.OK,
        //            MessageBoxIcon.Information);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(
        //            "Failed to update IP address.\n\n" +
        //            ex.Message,
        //            "Update IP Error",
        //            MessageBoxButtons.OK,
        //            MessageBoxIcon.Error);
        //    }
        //}

        private string BuildConnectionString(bool useMaster = false)
        {
            // Build data source (server\instance if instance provided)
            string server = txtIP.Text?.Trim() ?? "";
            string instance = txtInstance.Text?.Trim();
            string dataSource = string.IsNullOrEmpty(instance) ? server : $"{server}\\{instance}";

            // Choose database: master when validating server/auth, otherwise the configured DB
            string initialCatalog = useMaster ? "master" : (txtDb.Text?.Trim() ?? "");

            // If username/password provided use SQL auth, otherwise use integrated security
            string username = txtUsername.Text?.Trim();
            string password = txtPwd.Text ?? "";

            var builder = new SqlConnectionStringBuilder
            {
                DataSource = dataSource,
                InitialCatalog = initialCatalog,
                ConnectTimeout = 15,
                IntegratedSecurity = string.IsNullOrEmpty(username),
                MultipleActiveResultSets = false,
                // optional: you can remove the next two lines if you prefer default encryption settings
                Encrypt = false,
                TrustServerCertificate = true
            };

            if (!builder.IntegratedSecurity)
            {
                builder.UserID = username;
                builder.Password = password;
            }

            return builder.ConnectionString;
        }

        private void btnCreateTable_Click(object sender, EventArgs e)
        {
            try
            {
                using (var conn =
                    new SqlConnection(BuildConnectionString()))
                {
                    conn.Open();
                    CreateDCUser(conn); 
                    CreateDCRole(conn); 
                    CreateDCLicenseDetail(conn);
                    CreateDCSetup(conn);
                    CreateDCSPRecord(conn);
                    CreateDCSPUpdate(conn);
                    CreateDCDashModule(conn);
                    CreateDCLineDetail(conn);
                    CreateDCLog(conn);
                    CreateDCPermission(conn);
                    CreateDCProdDailyRec(conn);
                    CreateDCProdoutrec(conn);
                    CreateDCRolePermission(conn);
                    CreateDCViewline(conn);
                    CreateDefaultData(conn);                    
                }

                MessageBox.Show(
                    "Database Table initialized successfully.",
                    "Table Setup",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                Logging.FolderLog.Info("Table initialized successfully.");
                ConfigurationSaved?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Failed to initialize database table.\n\n" +
                    ex.Message,
                    "Database Table Setup Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                Logging.FolderLog.Error("Failed to initialize database table.", ex);
            }
        }



        private void CreateDefaultData(SqlConnection connection)
        {
            InsertDefaultRoles(connection);
            CreateDefaultAdmin(connection);
            InsertDefaultDashModules(connection);
            InsertDefaultPermissions(connection);
            InsertDefaultRolePermission(connection);
            InsertDefaultLicenseDetail(connection);
            InsertDefaultSPRecord(connection);
            InsertDefaultSPUpdate(connection);
           // Logging.FolderLog.Info("Default data inserted successfully.");
        }

        private void InsertDefaultRoles(SqlConnection connection)
        {
            string sql = @"
        INSERT INTO dbo.DC_ROLE
        (
            ROLENAME,
            STATUS_ACTIVE
        )
        SELECT
            V.ROLENAME,
            V.STATUS_ACTIVE
        FROM
        (
            SELECT
                'ADMIN' AS ROLENAME,
                CAST(1 AS BIT) AS STATUS_ACTIVE
            UNION ALL
            SELECT
                'SUPERUSER',
                CAST(1 AS BIT)
            UNION ALL
            SELECT
                'USER',
                CAST(1 AS BIT)
        ) V
        WHERE NOT EXISTS
        (
            SELECT 1
            FROM dbo.DC_ROLE D
            WHERE D.ROLENAME = V.ROLENAME
        );";

            using (SqlCommand cmd =
                new SqlCommand(sql, connection))
            {
                cmd.ExecuteNonQuery();
            }
        }

        private void InsertDefaultDashModules(SqlConnection connection)
        {
            string sql = @"
        INSERT INTO dbo.DC_DASHMODULE
        (
            MODULENAME,
            MODULECODE,
            STATUS_ACTIVE,
            IMAGEPATH,
            DATEMODIFIED
        )
        SELECT
            V.MODULENAME,
            V.MODULECODE,
            V.STATUS_ACTIVE,
            V.IMAGEPATH,
            SYSDATETIME()
        FROM
        (
            SELECT
                'Production Bar Chart' AS MODULENAME,
                'V_PRODCHART' AS MODULECODE,
                CAST(1 AS BIT) AS STATUS_ACTIVE,
                NULL AS IMAGEPATH

            UNION ALL

            SELECT
                'Top 3 Line',
                'V_TOP3',
                CAST(1 AS BIT),
                NULL

            UNION ALL

            SELECT
                'Production Output',
                'V_PRODOUTPUT',
                CAST(1 AS BIT),
                NULL

            UNION ALL

            SELECT
                'Production Table',
                'V_PRODTABLE',
                CAST(1 AS BIT),
                NULL

        ) V
        WHERE NOT EXISTS
        (
            SELECT 1
            FROM dbo.DC_DASHMODULE D
            WHERE D.MODULECODE = V.MODULECODE
        );";

            using (SqlCommand cmd =
                new SqlCommand(sql, connection))
            {
                cmd.ExecuteNonQuery();
            }
        }

        private void InsertDefaultLicenseDetail(SqlConnection connection)
        {
            string sql = @"
            INSERT INTO dbo.DC_LICENSEDETAIL
            (
                LICENSE_GROUP,
                LICENSE_TYPE,
                DASHBOARD,
                MAX_LINES,
                MAX_PAGES,
                LICENSE_DESC,
                DATEMODIFIED
            )
            SELECT
                V.LICENSE_GROUP,
                V.LICENSE_TYPE,
                V.DASHBOARD,
                V.MAX_LINES,
                V.MAX_PAGES,
                V.LICENSE_DESC,
                SYSDATETIME()
            FROM
            (
                SELECT
                    'FULL' AS LICENSE_GROUP,
                    'FBASIC_DC' AS LICENSE_TYPE,
                    'COMBINED' AS DASHBOARD,
                    '2' AS MAX_LINES,
                    'ALL' AS MAX_PAGES,
                    'Connect 2 Lines' AS LICENSE_DESC
                UNION ALL
                SELECT
                    'FULL',
                    'FBASIC_DS',
                    'STANDARD',
                    '1',
                    '2',
                    'Package for 2 Pages'
                UNION ALL
                SELECT
                    'FULL',
                    'FFULL_DC',
                    'COMBINED',
                    '10',
                    'ALL',
                    'Connect 10 Lines'
                UNION ALL
                SELECT
                    'FULL',
                    'FFULL_DS',
                    'STANDARD',
                    '1',
                    'ALL',
                    'All Pages (include defcet)'
                UNION ALL
                SELECT
                    'FULL',
                    'FSTANDARD_DC',
                    'COMBINED',
                    '5',
                    'ALL',
                    'Connect 5 Lines'
                UNION ALL
                SELECT
                    'FULL',
                    'FSTANDARD_DS',
                    'STANDARD',
                    '1',
                    '5',
                    'Package for 5 Pages'
                UNION ALL
                SELECT
                    'TRIAL',
                    'TBASIC_DC',
                    'COMBINED',
                    '2',
                    'ALL',
                    'Trial Connect 2 Lines'
                UNION ALL
                SELECT
                    'TRIAL',
                    'TBASIC_DS',
                    'STANDARD',
                    '1',
                    '2',
                    'Trial Package for 2 Pages'
                UNION ALL
                SELECT
                    'TRIAL',
                    'TFULL_DC',
                    'COMBINED',
                    '10',
                    'ALL',
                    'Trial Connect 10 Lines'
                UNION ALL
                SELECT
                    'TRIAL',
                    'TFULL_DS',
                    'STANDARD',
                    '1',
                    'ALL',
                    'Trial All Pages'
                UNION ALL
                SELECT
                    'TRIAL',
                    'TSTANDARD_DC',
                    'COMBINED',
                    '5',
                    'ALL',
                    'Trial Connect 5 Lines'
                UNION ALL
                SELECT
                    'TRIAL',
                    'TSTANDARD_DS',
                    'STANDARD',
                    '1',
                    '5',
                    'Trial Package for 5 Pages'
            ) V
            WHERE NOT EXISTS
            (
                SELECT 1
                FROM dbo.DC_LICENSEDETAIL D
                WHERE D.LICENSE_TYPE = V.LICENSE_TYPE
            );";
            using (SqlCommand cmd =
                new SqlCommand(sql, connection))
            {
                cmd.ExecuteNonQuery();
            }
        }

        private void InsertDefaultPermissions(SqlConnection connection)
        {
            string sql = @"
            INSERT INTO dbo.DC_PERMISSION
            (
                PERMISSIONCODE,
                PERMISSIONNAME
            )
            SELECT
                V.PERMISSIONCODE,
                V.PERMISSIONNAME
            FROM
            (
                SELECT
                    'EDIT_DATABASE' AS PERMISSIONCODE,
                    'View Database' AS PERMISSIONNAME
                UNION ALL
                SELECT
                    'CREATE_USER',
                    'Create User'
                UNION ALL
                SELECT
                    'EDIT_LINE_SETUP',
                    'Edit Line Setup'
                UNION ALL
                SELECT
                    'UPLOAD_MODULE_IMAGE',
                    'Upload Module Image'
                UNION ALL
                SELECT
                    'CREATE_MODULE',
                    'Create Dashboard Module'
                UNION ALL
                SELECT
                    'ASSIGN_MODULE_LINE',
                    'Assign Lines to Module'
                UNION ALL
                SELECT
                    'VIEW_LICENSE_KEY',
                    'View License Key'
                UNION ALL
                SELECT
                    'ADD_LICENSE_KEY',
                    'Add License Key'
                UNION ALL
                SELECT 
                    'UPDATE_SP',
                    'Update Stored Procedure'
                UNION ALL
                SELECT
                    'INSTALL_SERVICE',
                    'Service Installation'
                UNION ALL
                SELECT
                    'START_SERVICE',
                    'Service Start And Restart'
            ) V
            WHERE NOT EXISTS
            (
                SELECT 1
                FROM dbo.DC_PERMISSION D
                WHERE D.PERMISSIONCODE = V.PERMISSIONCODE
            );";
            using (SqlCommand cmd =
                new SqlCommand(sql, connection))
            {
                cmd.ExecuteNonQuery();
            }
        }

        private void InsertDefaultRolePermission(SqlConnection connection)
        {
            string sql = @"
            INSERT INTO dbo.DC_ROLE_PERMISSION
            (ROLEID,PERMISSIONID) SELECT V.ROLEID, V.PERMISSIONID
            FROM(
                SELECT 1 AS ROLEID, 1 AS PERMISSIONID
                UNION ALL
                SELECT 1,2
                UNION ALL
                SELECT 1,3
                UNION ALL 
                SELECT 1,4
                UNION ALL
                SELECT 1,5
                UNION ALL
                SELECT 1,6
                UNION ALL
                SELECT 1,7
                UNION ALL
                SELECT 1,8
                UNION ALL
                SELECT 2,2
                UNION ALL
                SELECT 2,3
               UNION ALL
                SELECT 2,6
               UNION ALL
                SELECT 2,7
               UNION ALL
                SELECT 3,2
               UNION ALL
                SELECT 3,6
              UNION ALL
                SELECT 1,9
              UNION ALL
                SELECT 1,10
              UNION ALL
                SELECT 1,11
              UNION ALL
                SELECT 2,11
              UNION ALL
                SELECT 2,1
              UNION ALL
                SELECT 3,7
            ) V
            WHERE NOT EXISTS
            (SELECT 1 FROM dbo.DC_ROLE_PERMISSION D 
            WHERE D.ROLEID = V.ROLEID AND D.PERMISSIONID = V.PERMISSIONID);";
            using (SqlCommand cmd =
                new SqlCommand(sql, connection))
            {
                cmd.ExecuteNonQuery();
            }
        }

        private void InsertDefaultSPRecord(SqlConnection connection)
        {
            string sql = @"
        INSERT INTO dbo.DC_SP_RECORD
        (
            PROCEDURE_NAME,
            SCRIPT_VERSION,
            STATUS_ACTIVE,
            DATEMODIFIED
        )
        SELECT
            V.PROCEDURE_NAME,
            V.SCRIPT_VERSION,
            V.STATUS_ACTIVE,
            SYSDATETIME()
            
        FROM
        (
            SELECT
                'DC_GetDataGlobal' AS PROCEDURE_NAME,
                1 AS SCRIPT_VERSION,
                CAST(1 AS BIT) AS STATUS_ACTIVE
            UNION ALL
            SELECT
                'DC_GetDataLocal',1,
                CAST(1 AS BIT)
        ) V
        WHERE NOT EXISTS
        (
            SELECT 1
            FROM dbo.DC_SP_RECORD D
            WHERE D.PROCEDURE_NAME = V.PROCEDURE_NAME
        );";

            using (SqlCommand cmd =
                new SqlCommand(sql, connection))
            {
                cmd.ExecuteNonQuery();
            }
        }
        private void InsertDefaultSPUpdate(SqlConnection connection)
        {
            string sql = @"
        INSERT INTO dbo.DC_SP_UPDATEVERSION
        (
            PROCEDURE_NAME,
            PROCEDURE_SCRIPT,SCRIPT_VERSION,
            REMARK,DATEMODIFIED
        )
        SELECT
            V.PROCEDURE_NAME,
            V.PROCEDURE_SCRIPT,
            V.SCRIPT_VERSION,
            V.REMARK,
            SYSDATETIME()
        FROM
        (
            SELECT
                'DC_GetDataGlobal' AS PROCEDURE_NAME,
                'DC_GetDataGlobal.sql' AS PROCEDURE_SCRIPT,
                1 AS SCRIPT_VERSION,
                'Initial Release' AS REMARK
            UNION ALL
            SELECT
                'DC_GetDataLocal','DC_GetDataLocal.sql',
                1,'Initial Release'
        ) V
        WHERE NOT EXISTS
        (
            SELECT 1
            FROM dbo.DC_SP_UPDATEVERSION D
            WHERE D.PROCEDURE_NAME = V.PROCEDURE_NAME 
            AND D.PROCEDURE_SCRIPT = V.PROCEDURE_SCRIPT
        );";

            using (SqlCommand cmd =
                new SqlCommand(sql, connection))
            {
                cmd.ExecuteNonQuery();
            }
        }

        private void CreateDefaultAdmin(SqlConnection connection)
        {
            try
            {

                string username = "admin";
                string password = "smcSECRET@123";

                // Check whether ADMIN account already exists
                string checkSql = @"
            SELECT COUNT(*)
            FROM DC_USER
            WHERE USERNAME = @Username";

                using (SqlCommand checkCmd =
                    new SqlCommand(checkSql, connection))
                {
                    checkCmd.Parameters.AddWithValue(
                        "@Username",
                        username);

                    int count =
                        Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (count > 0)
                    {
                        return;
                    }
                }

                // Hash the password
                string passwordHash =
                    BCrypt.Net.BCrypt.HashPassword(password);

                // Insert ADMIN
                string insertSql = @"
        INSERT INTO DC_USER
        (
            USERNAME,
            PASSWORD_HASH,
            ROLEID,
            STATUS_ACTIVE
        )
        SELECT
            @Username,
            @PasswordHash,
            ROLEID,
            1
        FROM DC_ROLE
        WHERE ROLENAME = 'ADMIN';";

                using (SqlCommand insertCmd =
                    new SqlCommand(insertSql, connection))
                {
                    insertCmd.Parameters.AddWithValue(
                        "@Username",
                        username);

                    insertCmd.Parameters.AddWithValue(
                        "@PasswordHash",

                        passwordHash);

                    insertCmd.ExecuteNonQuery();
                }

            }

            catch
            {

            }
        }

        //private void CreateDCDashModule(SqlConnection connection)
        //{
        //    string sql = @"
        //    IF OBJECT_ID('dbo.DC_DASHMODULE', 'U') IS NULL
        //    BEGIN

        //        CREATE TABLE dbo.DC_DASHMODULE
        //        (
        //            MODULEID INT IDENTITY(1,1) NOT NULL,
        //            MODULENAME NVARCHAR(50) NULL,
        //            MODULECODE NVARCHAR(50) NOT NULL,
        //            STATUS_ACTIVE BIT NULL,
        //            IMAGEPATH NVARCHAR(500) NULL,
        //            DATEMODIFIED DATETIME2(7) NOT NULL,

        //            CONSTRAINT PK_DC_DASHMODULE
        //                PRIMARY KEY CLUSTERED
        //                (
        //                    MODULECODE ASC
        //                )
        //        );

        //        ALTER TABLE dbo.DC_DASHMODULE
        //        ADD CONSTRAINT DF_DC_DASHCOMBINE_DATEMODIFIED
        //            DEFAULT (SYSDATETIME())
        //            FOR DATEMODIFIED;

        //    END";

        //    using (SqlCommand cmd =
        //        new SqlCommand(sql, connection))
        //    {
        //        cmd.ExecuteNonQuery();
        //    }
        //}

        private void CreateDCDashModule(SqlConnection connection)
        {
            string sql = @"
            IF OBJECT_ID('dbo.DC_DASHMODULE', 'U') IS NULL
            BEGIN
                CREATE TABLE dbo.DC_DASHMODULE
                (
                    MODULEID INT IDENTITY(1,1) NOT NULL,
                    MODULENAME NVARCHAR(50) NULL,
                    MODULECODE NVARCHAR(50) NOT NULL,
                    STATUS_ACTIVE BIT NULL,
                    IMAGEPATH NVARCHAR(500) NULL,

                    DATEMODIFIED DATETIME2(7) NOT NULL
                        CONSTRAINT DF_DC_DASHMODULE_DATEMODIFIED
                        DEFAULT (SYSDATETIME()),

                    CONSTRAINT PK_DC_DASHMODULE
                        PRIMARY KEY CLUSTERED (MODULECODE ASC)
                );
            END;";

            using (SqlCommand cmd = new SqlCommand(sql, connection))
            {
                cmd.ExecuteNonQuery();
            }
        }

        private void CreateDCLicenseDetail(SqlConnection connection)
        {
            string sql = @"
            IF OBJECT_ID('dbo.DC_LICENSEDETAIL', 'U') IS NULL
            BEGIN
                CREATE TABLE dbo.DC_LICENSEDETAIL
                (
                    LICENSEID INT IDENTITY(1,1) NOT NULL,
                    LICENSE_GROUP NVARCHAR(10) NULL,
                    LICENSE_TYPE NVARCHAR(50) NOT NULL,
                    DASHBOARD NVARCHAR(20) NULL,
                    MAX_LINES NVARCHAR(10) NULL,
                    MAX_PAGES NVARCHAR(10) NULL,
                    LICENSE_DESC NVARCHAR(50) NULL,
                    DATEMODIFIED DATETIME2(7) NOT NULL
                        CONSTRAINT DF_DC_LICENSEDETAIL_DATEMODIFIED
                        DEFAULT (SYSDATETIME()),

                    CONSTRAINT PK_DC_LICENSEDETAIL
                        PRIMARY KEY CLUSTERED (LICENSEID),

                    CONSTRAINT UQ_DC_LICENSEDETAIL_LICENSE_TYPE
                        UNIQUE (LICENSE_TYPE)
                );
            END;";

            using (SqlCommand cmd = new SqlCommand(sql, connection))
            {
                cmd.ExecuteNonQuery();
            }
        }

        //private void CreateDCLineDetail(SqlConnection connection)
        //{
        //    string sql = @"
        //    IF OBJECT_ID('dbo.DC_LINEDETAIL', 'U') IS NULL
        //    BEGIN
        //      CREATE TABLE [dbo].[DC_LINEDETAIL](
	       //     [LINEID] [int] IDENTITY(1,1) NOT NULL,
	       //     [LINE_NUM] [int] NULL,
	       //     [IP_ADDRESS] [nvarchar](20) NULL,
	       //     [MASTER_LINE_ID] [int] NULL,
	       //     [STATUS_ACTIVE] [bit] NULL,
	       //     [DATEMODIFIED] [datetime2](7) NOT NULL,
        //     CONSTRAINT [PK_DC_LINEDETAIL] PRIMARY KEY CLUSTERED 
        //    (
	       //     [LINEID] ASC
        //    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
        //    ) ON [PRIMARY]
            

        //    ALTER TABLE [dbo].[DC_LINEDETAIL] ADD  CONSTRAINT [DF_DC_LINEDETAIL_DATEMODIFIED]  DEFAULT (sysdatetime()) FOR [DATEMODIFIED]
            

        //    ALTER TABLE [dbo].[DC_LINEDETAIL]  WITH CHECK ADD  CONSTRAINT [FK_DC_LINEDETAIL_MASTER] FOREIGN KEY([MASTER_LINE_ID])
        //    REFERENCES [dbo].[DC_LINEDETAIL] ([LINEID])
            

        //    ALTER TABLE [dbo].[DC_LINEDETAIL] CHECK CONSTRAINT [FK_DC_LINEDETAIL_MASTER]

        //    END";
        //    using (SqlCommand cmd =
        //        new SqlCommand(sql, connection))
        //    {
        //        cmd.ExecuteNonQuery();
        //    }
        //}
        private void CreateDCLineDetail(SqlConnection connection)
        {
            string sql = @"
            IF OBJECT_ID('dbo.DC_LINEDETAIL', 'U') IS NULL
            BEGIN
                CREATE TABLE dbo.DC_LINEDETAIL
                (
                    LINEID INT IDENTITY(1,1) NOT NULL,
                    LINE_NUM INT NULL,
                    IP_ADDRESS NVARCHAR(20) NULL,
                    MASTER_LINE_ID INT NULL,
                    STATUS_ACTIVE BIT NULL,

                    DATEMODIFIED DATETIME2(7) NOT NULL
                        CONSTRAINT DF_DC_LINEDETAIL_DATEMODIFIED
                        DEFAULT (SYSDATETIME()),

                    CONSTRAINT PK_DC_LINEDETAIL
                        PRIMARY KEY CLUSTERED (LINEID),

                    CONSTRAINT FK_DC_LINEDETAIL_MASTER
                        FOREIGN KEY (MASTER_LINE_ID)
                        REFERENCES dbo.DC_LINEDETAIL (LINEID)
                );
            END;";

            using (SqlCommand cmd = new SqlCommand(sql, connection))
            {
                cmd.ExecuteNonQuery();
            }
        }

        private void CreateDCLog(SqlConnection connection)
        {
            string sql = @"
            IF OBJECT_ID('dbo.DC_LOG', 'U') IS NULL
            BEGIN
                CREATE TABLE dbo.DC_LOG
                (
                    ID INT IDENTITY(1,1) NOT NULL,
                    USERID INT NULL,
                    [ACTION] NVARCHAR(MAX) NULL,
                    [TARGET] NVARCHAR(100) NULL,
                    REMARK NVARCHAR(MAX) NULL,

                    DATEMODIFIED DATETIME2(7) NOT NULL
                        CONSTRAINT DF_DC_LOG_DATEMODIFIED
                        DEFAULT (SYSDATETIME()),

                    CONSTRAINT PK_DC_LOG
                        PRIMARY KEY CLUSTERED (ID),

                    CONSTRAINT FK_DC_LOG_DC_USER
                        FOREIGN KEY (USERID)
                        REFERENCES dbo.DC_USER (USERID)
                );
            END;";

            using (SqlCommand cmd = new SqlCommand(sql, connection))
            {
                cmd.ExecuteNonQuery();
            }
        }
        private void CreateDCPermission(SqlConnection connection)
        {
            string sql = @"
            IF OBJECT_ID('dbo.DC_PERMISSION', 'U') IS NULL
            BEGIN
                CREATE TABLE dbo.DC_PERMISSION
                (
                    PERMISSIONID INT IDENTITY(1,1) NOT NULL,
                    PERMISSIONCODE NVARCHAR(100) NOT NULL,
                    PERMISSIONNAME NVARCHAR(200) NOT NULL,

                    CONSTRAINT PK_DC_PERMISSION
                        PRIMARY KEY CLUSTERED (PERMISSIONID),

                    CONSTRAINT UQ_DC_PERMISSION_PERMISSIONCODE
                        UNIQUE (PERMISSIONCODE)
                );
            END;";

            using (SqlCommand cmd = new SqlCommand(sql, connection))
            {
                cmd.ExecuteNonQuery();
            }
        }

        //private void CreateDCPermission(SqlConnection connection)
        //{
        //    string sql = @"
        //    IF OBJECT_ID('dbo.DC_PERMISSION', 'U') IS NULL
        //    BEGIN             
        //        CREATE TABLE [dbo].[DC_PERMISSION](
	       //         [PERMISSIONID] [int] IDENTITY(1,1) NOT NULL,
	       //         [PERMISSIONCODE] [nvarchar](100) NOT NULL,
	       //         [PERMISSIONNAME] [nvarchar](200) NOT NULL,
        //        PRIMARY KEY CLUSTERED 
        //        (
	       //         [PERMISSIONID] ASC
        //        )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
        //        UNIQUE NONCLUSTERED 
        //        (
	       //         [PERMISSIONCODE] ASC
        //        )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
        //        ) ON [PRIMARY]
        //    END";
        //    using (SqlCommand cmd =
        //        new SqlCommand(sql, connection))
        //    {
        //        cmd.ExecuteNonQuery();
        //    }
        //}

        private void CreateDCProdDailyRec(SqlConnection connection)
                {
                    string sql = @"
            IF OBJECT_ID('dbo.DC_PRODDAILY_REC', 'U') IS NULL
            BEGIN
                CREATE TABLE dbo.DC_PRODDAILY_REC
                (
                    PRODUCTION_DATE DATE NOT NULL,
                    LINE_ID INT NOT NULL,
                    SHIFT VARCHAR(20) NOT NULL,

                    PRODUCTION_COUNT INT NOT NULL
                        CONSTRAINT DF_DC_PRODDAILY_REC_PRODUCTION_COUNT
                        DEFAULT (0),

                    LAST_SEEN DATETIME2(7) NULL,

                    CONSTRAINT PK_DC_PRODDAILY_REC
                        PRIMARY KEY CLUSTERED
                        (PRODUCTION_DATE, LINE_ID, SHIFT)
                );
            END;";

            using (SqlCommand cmd = new SqlCommand(sql, connection))
            {
                cmd.ExecuteNonQuery();
            }
        }


        private void CreateDCProdoutrec(SqlConnection connection)
        {
            string sql = @"
            IF OBJECT_ID('dbo.DC_PRODOUTREC', 'U') IS NULL
            BEGIN
                CREATE TABLE dbo.DC_PRODOUTREC
                (
                    PRODUCTION_DATE DATETIME2(7) NULL,
                    LINE_ID INT NULL,
                    SHIFT INT NULL,

                    PRODUCTION_COUNT INT NULL
                        CONSTRAINT DF_DC_PRODOUTREC_PRODUCTION_COUNT
                        DEFAULT (0),

                    LAST_UPDATE DATETIME2(7) NULL
                );
            END;";

            using (SqlCommand cmd = new SqlCommand(sql, connection))
            {
                cmd.ExecuteNonQuery();
            }
        }


        private void CreateDCRole(SqlConnection connection)
        {
            string sql = @"
            IF OBJECT_ID('dbo.DC_ROLE', 'U') IS NULL
            BEGIN
                CREATE TABLE dbo.DC_ROLE
                (
                    ROLEID INT IDENTITY(1,1) NOT NULL,
                    ROLENAME NVARCHAR(50) NOT NULL,

                    STATUS_ACTIVE BIT NOT NULL
                        CONSTRAINT DF_DC_ROLE_STATUS_ACTIVE
                        DEFAULT (1),

                    CONSTRAINT PK_DC_ROLE
                        PRIMARY KEY CLUSTERED (ROLEID),

                    CONSTRAINT UQ_DC_ROLE_ROLENAME
                        UNIQUE (ROLENAME)
                );
            END;";

            using (SqlCommand cmd = new SqlCommand(sql, connection))
            {
                cmd.ExecuteNonQuery();
            }
        }

        private void CreateDCRolePermission(SqlConnection connection)
        {
            string sql = @"
            IF OBJECT_ID('dbo.DC_ROLE_PERMISSION', 'U') IS NULL
            BEGIN
                CREATE TABLE dbo.DC_ROLE_PERMISSION
                (
                    ROLEID INT NOT NULL,
                    PERMISSIONID INT NOT NULL,

                    CONSTRAINT PK_DC_ROLE_PERMISSION
                        PRIMARY KEY CLUSTERED (ROLEID, PERMISSIONID),

                    CONSTRAINT FK_DC_ROLE_PERMISSION_DC_ROLE
                        FOREIGN KEY (ROLEID)
                        REFERENCES dbo.DC_ROLE (ROLEID),

                    CONSTRAINT FK_DC_ROLE_PERMISSION_DC_PERMISSION
                        FOREIGN KEY (PERMISSIONID)
                        REFERENCES dbo.DC_PERMISSION (PERMISSIONID)
                );
            END;";

            using (SqlCommand cmd = new SqlCommand(sql, connection))
            {
                cmd.ExecuteNonQuery();
            }
        }

        private void CreateDCSetup(SqlConnection connection)
                {
                    string sql = @"
            IF OBJECT_ID('dbo.DC_SETUP', 'U') IS NULL
            BEGIN
                CREATE TABLE dbo.DC_SETUP
                (
                    ID INT IDENTITY(1,1) NOT NULL,
                    COMPANY_NAME NVARCHAR(50) NULL,
                    SIGNATURE NVARCHAR(MAX) NULL,
                    LICENSE_TYPE NVARCHAR(50) NULL,
                    HW_HASH NVARCHAR(MAX) NULL,
                    LICENSE_PAYLOAD NVARCHAR(MAX) NULL,
                    ISSUED_UTC DATETIME2(7) NULL,
                    EXPIRY_UTC DATETIME2(7) NULL,

                    IS_ACTIVE BIT NOT NULL
                        CONSTRAINT DF_DC_SETUP_IsActive
                        DEFAULT (1),

                    DATEMODIFIED DATETIME2(7) NOT NULL
                        CONSTRAINT DF_DC_SETUP_DATEMODIFIED
                        DEFAULT (SYSDATETIME()),

                    CONSTRAINT PK_DC_SETUP
                        PRIMARY KEY CLUSTERED (ID),

                    CONSTRAINT FK_DC_SETUP_DC_LICENSEDETAIL
                        FOREIGN KEY (LICENSE_TYPE)
                        REFERENCES dbo.DC_LICENSEDETAIL (LICENSE_TYPE)
                );
            END;";

                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }

        private void CreateDCUser(SqlConnection connection)
        {
            string sql = @"
            IF OBJECT_ID('dbo.DC_USER','U')IS NULL
            BEGIN
                CREATE TABLE [dbo].[DC_USER](
	            [USERID] [int] IDENTITY(1,1) NOT NULL,
	            [USERNAME] [nvarchar](50) NOT NULL,
	            [PASSWORD_HASH] [nvarchar](500) NOT NULL,
	            [ROLEID] [int] NULL,
	            [STATUS_ACTIVE] [bit] NULL,
	            [CREATED_DATE] [datetime2](7) NOT NULL,
	            [DATEMODIFIED] [datetime2](7) NOT NULL,
             CONSTRAINT [PK_DC_USER] PRIMARY KEY CLUSTERED 
            (
	            [USERID] ASC
            )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
             CONSTRAINT [UQ_DC_USER_USERNAME] UNIQUE NONCLUSTERED 
            (
	            [USERNAME] ASC
            )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
            ) ON [PRIMARY]
            

            ALTER TABLE [dbo].[DC_USER] ADD  CONSTRAINT [DF_DC_USER_CREATED_DATE]  DEFAULT (sysdatetime()) FOR [CREATED_DATE]
            

            ALTER TABLE [dbo].[DC_USER] ADD  CONSTRAINT [DF_DC_USER_DATEMODIFIED]  DEFAULT (sysdatetime()) FOR [DATEMODIFIED]
            END ";
            using (SqlCommand cmd =
               new SqlCommand(sql, connection))
            {
                cmd.ExecuteNonQuery();
            }
        }


        private void CreateDCViewline(SqlConnection connection)
        {
            string sql = @"
    IF OBJECT_ID('dbo.DC_VIEWLINE', 'U') IS NULL
    BEGIN
        CREATE TABLE dbo.DC_VIEWLINE
        (
            VIEWLINEID INT IDENTITY(1,1) NOT NULL,
            MODULECODE NVARCHAR(50) NOT NULL,
            LINEID INT NOT NULL,
            DATEMODIFIED DATETIME2(7) NOT NULL
                CONSTRAINT DF_DC_VIEWLINE_DATEMODIFIED
                DEFAULT (SYSDATETIME()),

            CONSTRAINT PK_DC_VIEWLINE_1
                PRIMARY KEY CLUSTERED (VIEWLINEID),

            CONSTRAINT FK_DC_VIEWLINE_DASHMODULE
                FOREIGN KEY (MODULECODE)
                REFERENCES dbo.DC_DASHMODULE (MODULECODE),

            CONSTRAINT FK_DC_VIEWLINE_LINE
                FOREIGN KEY (LINEID)
                REFERENCES dbo.DC_LINEDETAIL (LINEID)
                ON DELETE CASCADE
        );
    END;";

            using (SqlCommand cmd = new SqlCommand(sql, connection))
            {
                cmd.ExecuteNonQuery();
            }
        }

        private void CreateDCSPRecord(SqlConnection connection)
        {
            string sql = @"
            IF OBJECT_ID('dbo.DC_SP_RECORD','U')IS NULL
            BEGIN
            CREATE TABLE [dbo].[DC_SP_RECORD](
	            [ID] [int] IDENTITY(1,1) NOT NULL,
	            [PROCEDURE_NAME] [nvarchar](128) NOT NULL,
	            [SCRIPT_VERSION] [int] NULL,
	            [STATUS_ACTIVE] [bit] NOT NULL,
	            [DATEMODIFIED] [datetime2](7) NOT NULL,
             CONSTRAINT [PK__DC_LINEP__3214EC276B1B3F64] PRIMARY KEY CLUSTERED 
            (
	            [PROCEDURE_NAME] ASC
            )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
            ) ON [PRIMARY]
            

            ALTER TABLE [dbo].[DC_SP_RECORD] ADD  CONSTRAINT [DF__DC_LINEPR__STATU__690797E6]  DEFAULT ((1)) FOR [STATUS_ACTIVE]
            

            ALTER TABLE [dbo].[DC_SP_RECORD] ADD  CONSTRAINT [DF__DC_LINEPR__DATEM__6AEFE058]  DEFAULT (sysdatetime()) FOR [DATEMODIFIED]
            
            END";
            using (SqlCommand cmd =
                   new SqlCommand(sql, connection))
            {
                cmd.ExecuteNonQuery();
            }
        }

        private void CreateDCSPUpdate(SqlConnection connection)
        {
            string sql = @"
            IF OBJECT_ID('dbo.DC_SP_UPDATEVERSION','U')IS NULL
            BEGIN
            CREATE TABLE [dbo].[DC_SP_UPDATEVERSION](
	            [ID] [int] IDENTITY(1,1) NOT NULL,
	            [PROCEDURE_NAME] [nvarchar](128) NOT NULL,
	            [PROCEDURE_SCRIPT] [nvarchar](max) NULL,
	            [SCRIPT_VERSION] [int] NULL,
	            [REMARK] [nvarchar](max) NOT NULL,
	            [DATEMODIFIED] [datetime2](7) NOT NULL,
             CONSTRAINT [PK__DC_SERVI__3214EC27A01817F2] PRIMARY KEY CLUSTERED 
            (
	            [ID] ASC
            )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
            ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
            

            ALTER TABLE [dbo].[DC_SP_UPDATEVERSION] ADD  CONSTRAINT [DF__DC_SERVIC__DATEM__662B2B3B]  DEFAULT (sysdatetime()) FOR [DATEMODIFIED]
            

            ALTER TABLE [dbo].[DC_SP_UPDATEVERSION]  WITH NOCHECK ADD  CONSTRAINT [FK_DC_SP_UPDATEVERSION_DC_SP_RECORD] FOREIGN KEY([PROCEDURE_NAME])
            REFERENCES [dbo].[DC_SP_RECORD] ([PROCEDURE_NAME])
            

            ALTER TABLE [dbo].[DC_SP_UPDATEVERSION] CHECK CONSTRAINT [FK_DC_SP_UPDATEVERSION_DC_SP_RECORD]

            END";
            using (SqlCommand cmd =
                   new SqlCommand(sql, connection))
            {
                cmd.ExecuteNonQuery();
            }
        }

        private void btnUpdateIP_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Update IP button clicked!");

            SaveIPAddress();
        }

        private void rButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (!rButton1.Checked)
                return;

            isLocalDatabase = true;

            txtIP.ReadOnly = !isEditMode;

            if (glassSaveIP != null)
                glassSaveIP.Visible = false;
        }

        private void rbutton2_CheckedChanged(
            object sender,
            EventArgs e)
        {
            if (rbutton2.Checked)
            {
                isLocalDatabase = false;

                // Remote database
               // txtIP.ReadOnly = !isEditMode;

                if (glassSaveIP != null)
                {
                    glassSaveIP.Visible = false;
                }
            }
        }

        private void btnCreateDb_Click(object sender, EventArgs e)
        {
            try
            {
               // MessageBox.Show("DB CLICK");
                string databaseName = txtDb.Text.Trim();

                if (string.IsNullOrWhiteSpace(databaseName))
                {
                    MessageBox.Show(
                        "Please enter a database name.",
                        "Create Database",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    txtDb.Focus();
                    return;
                }

                // Basic SQL database-name validation
                if (!IsValidDatabaseName(databaseName))
                {
                    MessageBox.Show(
                        "The database name is invalid.",
                        "Create Database",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    txtDb.Focus();
                    return;
                }

                DialogResult result = MessageBox.Show(
                    "Create database '" + databaseName + "'?\n\n" +
                    "The database will be created on:\n" +
                    txtIP.Text.Trim() +
                    (string.IsNullOrWhiteSpace(txtInstance.Text)
                        ? ""
                        : "\\" + txtInstance.Text.Trim()),
                    "Create Database",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                    return;

                string connectionString =
                    BuildMasterConnectionString();

                using (SqlConnection connection =
                    new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql =
                        "IF DB_ID(@DatabaseName) IS NULL " +
                        "BEGIN " +
                        "    CREATE DATABASE [" + databaseName + "] " +
                        "END";

                    using (SqlCommand command =
                        new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue(
                            "@DatabaseName",
                            databaseName);

                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show(
                    "Database created successfully.\n\n" +
                    "Database: " + databaseName,
                    "Create Database",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                txtDb.Text = databaseName;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(
                    "Unable to create the database.\n\n" +
                    ex.Message,
                    "Database Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                FolderLog.Info(
                    "Create database failed: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unexpected error while creating the database.\n\n" +
                    ex.Message,
                    "Create Database Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                FolderLog.Info(
                    "Create database error: " + ex.Message);
            }
        }

        private string BuildMasterConnectionString()
        {
            string server = txtIP.Text.Trim();
            string instance = txtInstance.Text.Trim();
            string username = txtUsername.Text.Trim();
            string password = txtPwd.Text;

            string dataSource = server;

            if (!string.IsNullOrWhiteSpace(instance))
                dataSource += "\\" + instance;

            SqlConnectionStringBuilder builder =
                new SqlConnectionStringBuilder();

            builder.DataSource = dataSource;
            builder.InitialCatalog = "master";
            builder.UserID = username;
            builder.Password = password;
            builder.IntegratedSecurity = false;
            builder.ConnectTimeout = 10;

            return builder.ConnectionString;
        }

        private bool IsValidDatabaseName(string databaseName)
        {
            if (string.IsNullOrWhiteSpace(databaseName))
                return false;

            if (databaseName.Length > 128)
                return false;

            foreach (char c in databaseName)
            {
                if (!char.IsLetterOrDigit(c) &&
                    c != '_' &&
                    c != '-')
                {
                    return false;
                }
            }

            return true;
        }

    }
}