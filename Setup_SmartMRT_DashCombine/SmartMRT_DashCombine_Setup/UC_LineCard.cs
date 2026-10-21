using SmartMRT_DashCombine_Setup.Controls;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartMRT_DashCombine_Setup
{
    public partial class UC_LineCard : UserControl
    {
        public event EventHandler EditClicked;
        public event EventHandler DeleteClicked;
        public event EventHandler SPClicked;
        private int _lineNumber;
        private string _ipAddress;
        private GlassButton glassEdit;
        private GlassButton glassDelete;
        private GlassPanel pnlCard;
        private string _storedprocedure;
        private GlassButton glassSP;
        public bool IsEditMode { get; private set; }
        public int LineID { get; set; }

        public int LineNumber
        {
            get => _lineNumber;
            set
            {
                _lineNumber = value;
                lblLine.Text = $"Line {value}";
            }
        }

        public string IPAddress
        {
            get => _ipAddress;
            set
            {
                _ipAddress = value;
                lblIpAddress.Text = value;
            }
        }

        public int MasterLineID { get; set; }

        public string StoredProcedure
        {
            get => _storedprocedure;
            set => _storedprocedure = value;
        }

        public bool IsActive
        {
            get => chkActive.Checked;
            set => chkActive.Checked = value;
        }

        private bool _isOnline;

        public bool IsOnline
        {
            get => _isOnline;
            private set
            {
                _isOnline = value;
            }
        }

        public enum MotorStatus
        {
            NotActive,
            Running,
            Stopped,
            Unknown
        }
        private bool isInitialized = false;
        public UC_LineCard()
        {
            InitializeComponent();
            ApplyModernStyle();
            Width = 380;
            Height = 180;

            Margin = new Padding(10);

            statusTimer = new System.Windows.Forms.Timer();
            statusTimer.Interval = 5000; // 5 seconds

            statusTimer.Tick += async (s, e) =>
            {
                await CheckLineStatusAsync();
            };

            chkActive.CheckedChanged +=
                chkActive_CheckedChanged;

            // Apply permissions based on current session
            ApplyPermissions();
            AppSession.SessionChanged += (s, e) => ApplyPermissions();
        }

        private void ApplyModernStyle()
        {
            pnlCard = new GlassPanel
            {
                Name = "pnlCard",
                Location = new Point(0, 0),
                Size = new Size(
            this.Width - 1,
            this.Height - 1),

                GlassOpacity = 100,

                GlassColor =
            Color.FromArgb(10, 25, 45),

                BorderColor =
            Color.FromArgb(50, 100, 140),

                CornerRadius = 18
            };

            glassDelete = new GlassButton
            {
                Name = "glassDelete",
                Text = "🗑  Delete",
                Size = new Size(105, 32),
                Location = btnDelete.Location,

                BorderColor =
        Color.FromArgb(255, 92, 92),

                GlassColor =
        Color.FromArgb(45, 30, 35),

                TextColor = Color.White,

                Font =
        new Font("Segoe UI", 9F, FontStyle.Bold)
            };

            glassEdit = new GlassButton
            {
                Name = "glassEdit",
                Text = "✎  Edit",
                Size = new Size(105, 32),
                Location = btnEdit.Location,

                BorderColor =
        Color.FromArgb(0, 212, 255),

                GlassColor =
        Color.FromArgb(20, 45, 65),

                TextColor = Color.White,

                Font =
        new Font("Segoe UI", 9F, FontStyle.Bold)
            };

            glassSP = new GlassButton
            {
                Name = "glassSP",
                Text = "⚙  SP",
                Size = new Size(105, 32),
                Location = btnSp.Location,
                BorderColor =
        Color.FromArgb(255, 92, 92),

                GlassColor =
        Color.FromArgb(45, 10, 25),

                TextColor = Color.White,

                Font =
        new Font("Segoe UI", 9F, FontStyle.Bold)
            };
            glassSP.Click += btnSp_Click;
            glassDelete.Click += btnDelete_Click;
            glassEdit.Click += btnEdit_Click;

            btnEdit.Parent.Controls.Add(glassEdit);
            btnDelete.Parent.Controls.Add(glassDelete);
            btnSp.Parent.Controls.Add(glassSP);

            btnDelete.Visible = false;
            btnEdit.Visible = false;
            btnSp.Visible = false;

            Controls.Add(pnlCard);

            pnlCard.SendToBack();
        }

        private void ApplyPermissions()
        {
            try
            {
                bool canEdit = PermissionManager.HasPermission("UC_LineCard.Edit");
                // set UI editability
                SetEditMode(canEdit);
            }
            catch
            {
                // on error, default to read-only
                SetEditMode(false);
            }
        }
        public void SetEditMode(bool editMode)
        {
            IsEditMode = editMode;
            chkActive.Enabled = editMode;
            btnEdit.Enabled = editMode;
            btnDelete.Enabled = editMode;
            btnSp.Enabled = editMode;
        }
        private async void chkActive_CheckedChanged(
            object sender,
            EventArgs e)
        {
            if (!isInitialized)
                return;

            if (chkActive.Checked)
            {
                await CheckLineStatusAsync();
                statusTimer.Start();
            }
            else
            {
                statusTimer.Stop();

                lblDatabaseStatus.Text =
                    "⚪ Not Checked";

                lblMotorStatus.Text =
                    "⚪ Not Checked";
                lblSpStatus.Text =
                     "⚪ Not Checked";
            }
        }

        //public async Task CheckLineStatusAsync()
        //{
        //    // Dashboard setup says this line is not enabled
        //    if (!IsActive)
        //    {
        //        lblDatabaseStatus.Text =
        //            "⚪ Not Checked";

        //        lblMotorStatus.Text =
        //            "⚪ Not Checked";

        //        return;
        //    }

        //    // Check database
        //    lblDatabaseStatus.Text =
        //        "🟡 Connecting...";

        //    // Disable interactive controls while testing connection so user sees 'Connecting...' state
        //    var prevCursor = Cursor;
        //    Cursor = Cursors.WaitCursor;

        //    bool prevEditEnabled = btnEdit.Enabled;
        //    bool prevDeleteEnabled = btnDelete.Enabled;
        //    try
        //    {
        //        btnEdit.Enabled = false;
        //        btnDelete.Enabled = false;

        //        bool connected =
        //            await TestConnectionAsync();

        //        if (!connected)
        //        {
        //            lblDatabaseStatus.Text =
        //                "🔴 Offline";

        //            lblMotorStatus.Text =
        //                "⚪ Unknown";
        //            //btnEdit.Enabled = true;
        //            //btnDelete.Enabled = true;
        //            return;
        //        }
        //    }
        //    finally
        //    {
        //        // Restore UI state
        //        btnEdit.Enabled = prevEditEnabled;
        //        btnDelete.Enabled = prevDeleteEnabled;
        //        Cursor = prevCursor;
        //    }

        //    // Database connected
        //    lblDatabaseStatus.Text =
        //        "🟢 Connected";

        //    // Check motor
        //    MotorStatus motorStatus =
        //        await CheckMotorStatusAsync();

        //    switch (motorStatus)
        //    {
        //        case MotorStatus.Running:
        //            lblMotorStatus.Text =
        //                "🟢 Running";
        //            break;

        //        case MotorStatus.Stopped:
        //            lblMotorStatus.Text =
        //                "🔴 Stopped";
        //            break;

        //        case MotorStatus.NotActive:
        //            lblMotorStatus.Text =
        //                "⚪ Not Active";
        //            break;

        //        case MotorStatus.Unknown:
        //            lblMotorStatus.Text =
        //                "⚪ Unknown";
        //            break;
        //    }
        //}
        public async Task CheckLineStatusAsync()
        {
            // Dashboard setup says this line is not enabled
            if (!IsActive)
            {
                lblDatabaseStatus.Text = "⚪ Not Checked";
                lblSpStatus.Text = "⚪ Not Checked";
                lblMotorStatus.Text = "⚪ Not Checked";
                return;
            }

            // Show checking status
            lblDatabaseStatus.Text = "🟡 Connecting...";
            lblSpStatus.Text = "🟡 Checking...";
            lblMotorStatus.Text = "🟡 Checking...";

            try
            {
                // =====================================================
                // 1. Check Database Connection
                // =====================================================
                bool connected =
                    await TestConnectionAsync();

                IsOnline = connected;

                if (!connected)
                {
                    lblDatabaseStatus.Text = "🔴 Offline";
                    lblSpStatus.Text = "⚪ Unknown";
                    lblMotorStatus.Text = "⚪ Unknown";

                    return;
                }

                lblDatabaseStatus.Text =
                    "🟢 Connected";


                // =====================================================
                // 2. Check Motor
                // =====================================================
                MotorStatus motorStatus =
                    await CheckMotorStatusAsync();

                switch (motorStatus)
                {
                    case MotorStatus.Running:
                        lblMotorStatus.Text =
                            "🟢 Running";
                        break;

                    case MotorStatus.Stopped:
                        lblMotorStatus.Text =
                            "🔴 Stopped";
                        break;

                    case MotorStatus.NotActive:
                        lblMotorStatus.Text =
                            "⚪ Not Active";
                        break;

                    case MotorStatus.Unknown:
                        lblMotorStatus.Text =
                            "⚪ Unknown";
                        break;
                }


                // =====================================================
                // 3. Check ALL Required Stored Procedures
                // =====================================================
                // =====================================================
                // Check Table Types + Stored Procedures
                // Display combined result in lblSpStatus
                // =====================================================

                TableTypeCheckResult typeResult =
                    await CheckAllTableTypesAsync();

                if (typeResult.Missing.Count > 0)
                {
                    lblSpStatus.Text =
                        $"🔴 Missing Types ({typeResult.Missing.Count})";

                    foreach (string missingType in typeResult.Missing)
                    {
                        Logging.FolderLog.Error(
                            $"Missing table type: {missingType}");
                    }

                    return;
                }

                StoredProcedureCheckResult spResult =
                    await CheckAllStoredProceduresAsync();

                if (spResult.Required.Count == 0)
                {
                    lblSpStatus.Text = "⚪ No SP Required";
                }
                else if (spResult.AllExist)
                {
                    lblSpStatus.Text =
                        $"🟢 Ready ({spResult.Required.Count} SP)";
                }
                else
                {
                    lblSpStatus.Text =
                        $"🔴 Missing SP ({spResult.Missing.Count})";

                    foreach (string missingSP in spResult.Missing)
                    {
                        Logging.FolderLog.Error(
                            $"Missing stored procedure: {missingSP}");
                    }
                }
            }
            catch (Exception ex)
            {
                lblDatabaseStatus.Text =
                    "🔴 Offline";

                lblSpStatus.Text =
                    "⚪ Unknown";

                lblMotorStatus.Text =
                    "⚪ Unknown";

                Logging.FolderLog.Error(
                    $"CheckLineStatusAsync error: {ex}");
            }
        }

        private async Task<bool> TestConnectionAsync()
        {
            // Build connection string with a short connect timeout so UI feedback is fast when unreachable.
            string connectionString =
                ConnectionMain.GetLineLocalDBConnection(
                    IPAddress);

            var builder = new SqlConnectionStringBuilder(connectionString)
            {
                // short timeout in seconds; adjust as needed (e.g., 3 seconds)
                ConnectTimeout = 3
            };
            connectionString = builder.ConnectionString;

            const int maxRetries = 2; // total attempts = maxRetries + 1
            int delayMs = 1000;

            for (int attempt = 0; attempt <= maxRetries; attempt++)
            {
                try
                {
                    // Attempt an async open; ConnectTimeout controls how long the driver waits.
                    using (var conn = new SqlConnection(connectionString))
                    {
                        await conn.OpenAsync();
                        return true;
                    }
                }
                catch
                {
                    if (attempt == maxRetries)
                    {
                        return false;
                    }

                    // Wait then retry (exponential backoff)
                    await Task.Delay(delayMs);
                    delayMs *= 2;
                }
            }

            return false;
        }

        private async Task<MotorStatus> CheckMotorStatusAsync()
        {
            try
            {
                string connectionString =
                    ConnectionMain.GetLineLocalDBConnection(
                        IPAddress);

                using (SqlConnection conn =
                    new SqlConnection(connectionString))
                {
                    await conn.OpenAsync();

                    string query = @"SELECT MotorStatus FROM PUSHERCOUNTER WHERE Prodline = @LineID";

                    using (SqlCommand cmd =
                        new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@LineID",LineNumber);

                        object result =
                            await cmd.ExecuteScalarAsync();

                        // No record found
                        if (result == null ||
                            result == DBNull.Value)
                        {
                            return MotorStatus.NotActive;
                        }

                        string status =
                            result.ToString().Trim();

                        if (status.Equals("Y" ,StringComparison.OrdinalIgnoreCase) || status.Equals("y", StringComparison.OrdinalIgnoreCase))
                        {
                            return MotorStatus.Running;
                        }

                        if (status.Equals("n",StringComparison.OrdinalIgnoreCase) || status.Equals("N", StringComparison.OrdinalIgnoreCase))
                        {
                            return MotorStatus.Stopped;
                        }

                        // Any other value
                        return MotorStatus.NotActive;
                    }
                }
            }
            catch
            {
                return MotorStatus.Unknown;
            }
        }

        private async Task<List<string>> GetRequiredStoredProceduresAsync()
        {
            List<string> procedures = new List<string>();

            try
            {
                string connectionString =
                    ConnectionMain.GetDashboardConnection();

                using (SqlConnection conn =
                    new SqlConnection(connectionString))
                {
                    await conn.OpenAsync();

                    string query = @"
                SELECT PROCEDURE_NAME
                FROM DC_SP_RECORD
                WHERE STATUS_ACTIVE = 1
                ORDER BY ID;";

                    using (SqlCommand cmd =
                        new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader =
                            await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                procedures.Add(
                                    reader["PROCEDURE_NAME"].ToString());
                            }
                        }
                    }
                }
            }
            catch
            {
                Logging.FolderLog.Error("Failed to retrieve required stored procedures from the dashboard database.");
            }

            return procedures;
        }

        private class StoredProcedureCheckResult
        {
            public List<string> Required { get; set; } = new List<string>();

            public List<string> Existing { get; set; } = new List<string>();

            public List<string> Missing { get; set; } = new List<string>();

            public bool AllExist =>
                Missing.Count == 0;
        }

        private class TableTypeCheckResult
        {
            public List<string> Required { get; set; }
                = new List<string>();

            public List<string> Existing { get; set; }
                = new List<string>();

            public List<string> Missing { get; set; }
                = new List<string>();

            public bool AllExist => Missing.Count == 0;
        }

        private async Task<bool> CheckTableTypeAsync(
    string typeName)
        {
            try
            {
                string connectionString =
                    ConnectionMain.GetLineGlobalDBConnection(IPAddress);

                using (SqlConnection conn =
                    new SqlConnection(connectionString))
                {
                    await conn.OpenAsync();

                    string query = @"
                SELECT COUNT(*)
                FROM sys.table_types tt
                INNER JOIN sys.schemas s
                    ON tt.schema_id = s.schema_id
                WHERE tt.name = @TypeName
                  AND s.name = 'dbo';";

                    using (SqlCommand cmd =
                        new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue(
                            "@TypeName",
                            typeName);

                        int count = Convert.ToInt32(
                            await cmd.ExecuteScalarAsync());

                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.FolderLog.Error(
                    "Failed to check table type " +
                    typeName + ": " + ex.Message);

                return false;
            }
        }

        private async Task<TableTypeCheckResult>
    CheckAllTableTypesAsync()
        {
            TableTypeCheckResult result =
                new TableTypeCheckResult();

            result.Required.AddRange(new[]
            {
        "ProductInfoType",
        "WorkerInfoType",
        "StationAssignInfoType"
    });

            foreach (string typeName in result.Required)
            {
                bool exists =
                    await CheckTableTypeAsync(typeName);

                if (exists)
                {
                    result.Existing.Add(typeName);
                }
                else
                {
                    result.Missing.Add(typeName);
                }
            }

            return result;
        }

        private async Task<bool> CheckStoredProcedureAsync(
      string procedureName)
        {
            try
            {
                string connectionString =
                    ConnectionMain.GetLineGlobalDBConnection(IPAddress);

                using (SqlConnection conn =
                    new SqlConnection(connectionString))
                {
                    await conn.OpenAsync();

                    string query = @"
                SELECT COUNT(*)
                FROM sys.procedures
                WHERE name = @ProcedureName
                  AND schema_id = SCHEMA_ID('dbo');";

                    using (SqlCommand cmd =
                        new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue(
                            "@ProcedureName",
                            procedureName);

                        int count =
                            Convert.ToInt32(
                                await cmd.ExecuteScalarAsync());

                        return count > 0;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        private async Task<StoredProcedureCheckResult>
    CheckAllStoredProceduresAsync()
        {
            StoredProcedureCheckResult result =
                new StoredProcedureCheckResult();

            // =========================================================
            // Get required SPs from Dashboard Global DB
            // =========================================================
            result.Required =
                await GetRequiredStoredProceduresAsync();

            if (result.Required.Count == 0)
            {
                Logging.FolderLog.Error(
                    "No required stored procedures found in DC_SP_RECORD.");

                return result;
            }


            // =========================================================
            // Check each SP in the Line Global DB
            // =========================================================
            foreach (string procedureName in result.Required)
            {
                bool exists =
                    await CheckStoredProcedureAsync(
                        procedureName);

                if (exists)
                {
                    result.Existing.Add(
                        procedureName);
                }
                else
                {
                    result.Missing.Add(
                        procedureName);
                }
            }

            return result;
        }

        private async Task CheckDatabaseObjectsAsync()
        {
            StoredProcedureCheckResult spResult =
                await CheckAllStoredProceduresAsync();

            TableTypeCheckResult typeResult =
                await CheckAllTableTypesAsync();

            List<string> missingObjects = new List<string>();

            foreach (string sp in spResult.Missing)
            {
                missingObjects.Add("Stored procedure: " + sp);
            }

            foreach (string type in typeResult.Missing)
            {
                missingObjects.Add("Table type: " + type);
            }

            if (missingObjects.Count > 0)
            {
                MessageBox.Show(
                    "The following database objects are missing:\r\n\r\n" +
                    string.Join("\r\n", missingObjects),
                    "Database Setup Required",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            MessageBox.Show(
                "All required stored procedures and table types exist.",
                "Database Check",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
        private void lblMotorStatus_Click(object sender, EventArgs e)
        {

        }

        private void lblLine_Click(object sender, EventArgs e)
        {

        }

        private async void UC_LineCard_Load(
      object sender,
      EventArgs e)
        {
            // refresh permissions when control loads
            ApplyPermissions();

            lblLine.Text =
                $"Line {LineNumber}";

            lblIpAddress.Text =
                IPAddress;

            if (!IsActive)
            {
                lblDatabaseStatus.Text =
                    "⚪ Not Checked";

                lblMotorStatus.Text =
                    "⚪ Not Checked";

                lblSpStatus.Text = "⚪ Not Checked";
                return;
            }

            await CheckLineStatusAsync();

            statusTimer.Start();
        }

        private void chkActive_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            EditClicked?.Invoke(
                this,
                EventArgs.Empty);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteClicked?.Invoke(
                this,
                EventArgs.Empty);
        }

        private void btnSp_Click(object sender, EventArgs e)
        {
            SPClicked?.Invoke(
                this,
                EventArgs.Empty);
        }

        private void lblDatabaseStatus_Click(object sender, EventArgs e)
        {

        }
    }
}