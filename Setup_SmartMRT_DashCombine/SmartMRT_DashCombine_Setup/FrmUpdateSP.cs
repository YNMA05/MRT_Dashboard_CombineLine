using SmartMRT_DashCombine_Setup.Controls;
using SmartMRT_DashCombine_Setup.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartMRT_DashCombine_Setup
{
    public partial class FrmUpdateSP : Form
    {
        public string NameProcedure {  get; set; }
        public string ScriptFile { get; set; }
        public int VersionProcedure {  get; set; }
        public bool StatusProcedure { get; set; }
        public string DateModified {  get; set; }
        private class  LineInfo
        {
            public string LineNum { get; set; } = string.Empty;
            public string IPAddress { get; set; } = string.Empty;
        }
        private class DeploymentProgress
        {
            public int Completed {  get; set; }
            public int Total { get; set; }
            public string Message { get; set; }
        }
        public FrmUpdateSP()
        {
            InitializeComponent();
        }

        private void FrmUpdateSP_Load(object sender, EventArgs e)
        {
            getSpData();
        }
        private class SPInfo
        {
            public string Name { get; set; }
            //public string ScriptFile { get; set; }
            public int Version { get; set; }
            public bool Active { get; set; }
            public DateTime DateModified { get; set; }

            public override string ToString()
            {
                return Name;
            }
        }



        private void getSpData()
        {
            try
            {
                string connectionString =
                    ConnectionMain.GetDashboardConnection();

                if (string.IsNullOrEmpty(connectionString))
                {
                    MessageBox.Show(
                        "Database connection is not available.",
                        "Database",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                string getSPList = @"
            SELECT
                PROCEDURE_NAME,
                SCRIPT_VERSION,
                STATUS_ACTIVE,
                DATEMODIFIED
            FROM dbo.DC_SP_RECORD
            ORDER BY PROCEDURE_NAME;";

                using (SqlConnection conn =
                    new SqlConnection(connectionString))
                using (SqlCommand cmd =
                    new SqlCommand(getSPList, conn))
                {
                    conn.Open();

                    ddlListSP.Items.Clear();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            SPInfo sp = new SPInfo
                            {
                                Name = reader["PROCEDURE_NAME"].ToString(),

                                Version = Convert.ToInt32(
                                    reader["SCRIPT_VERSION"]),

                                Active = Convert.ToBoolean(
                                    reader["STATUS_ACTIVE"]),

                                DateModified = Convert.ToDateTime(
                                    reader["DATEMODIFIED"])
                            };

                            ddlListSP.Items.Add(sp);
                        }
                    }
                }

                if (ddlListSP.Items.Count > 0)
                {
                    ddlListSP.SelectedIndex = 0;
                }
                else
                {
                    MessageBox.Show(
                        "No stored procedures found.",
                        "Stored Procedures",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Failed to load stored procedures.\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        private string previousVersion = "";
        private string previousDateModified = "";
        private string previousRemark = "";
        private void chkNewSP_CheckedChanged(object sender, EventArgs e)
        {
            lblDeployProgress.Visible = false;
            progressBarDeploy.Visible = false;
            if (chkNewSP.Checked)
            {
                // Save the currently displayed values
                previousVersion = txtVersion.Text;
                previousDateModified = dateModified.DateText;
                previousRemark = txtVersion.Text;

                // New procedure mode
                btnNew.Enabled = true;
                btnUpdateScript.Enabled = false;
                btnUpdateAll.Enabled = false;
                ddlListSP.Enabled = false;

                // Set default values for a new procedure
                dateModified.DateText =
                    DateTime.Now.ToString("yyyy-MM-dd");
                txtName.ReadOnly = false;
                txtVersion.ReadOnly = true;
                txtVersion.Text = "1";
            }
            else
            {
                // Exit new procedure mode
                btnNew.Enabled = false;
                btnUpdateScript.Enabled = true;
                btnUpdateAll.Enabled = true;
                ddlListSP.Enabled = true;

                // Restore previous values
                txtVersion.Text = previousVersion;
                dateModified.DateText = previousDateModified;
                txtRemark.Text = previousRemark;
                txtName.ReadOnly = true;
                // Keep version read-only if users should not edit it manually
                txtVersion.ReadOnly = false;

                // Reload the selected existing procedure
                if (ddlListSP.SelectedItem != null)
                {
                    ddlListSP_SelectedIndexChanged(
                        ddlListSP,
                        EventArgs.Empty);
                }
            }
        }

        private void ddlListSP_Load(object sender, EventArgs e)
        {
            txtName.ReadOnly = true;
            txtVersion.ReadOnly = true;
        }



        private void LoadProcedureScript(string procedureName)
        {
            try
            {
                string connectionString =
                    ConnectionMain.GetDashboardConnection();

                string query = @"
            SELECT TOP 1
                PROCEDURE_SCRIPT,
                SCRIPT_VERSION,REMARK,DATEMODIFIED
            FROM dbo.DC_SP_UPDATEVERSION
            WHERE PROCEDURE_NAME = @ProcedureName
            ORDER BY SCRIPT_VERSION DESC;";

                string scriptFile = null;

                using (SqlConnection conn =
                    new SqlConnection(connectionString))
                using (SqlCommand cmd =
                    new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue(
                        "@ProcedureName",
                        procedureName);

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            scriptFile =
                                reader["PROCEDURE_SCRIPT"].ToString();

                            txtVersion.Text =
                                reader["SCRIPT_VERSION"].ToString();
                            txtRemark.Text = reader["REMARK"].ToString();
                            dateModified.DateText = reader["DATEMODIFIED"].ToString();
                        }
                    }
                }

                if (string.IsNullOrWhiteSpace(scriptFile))
                {
                    rtbScript.Clear();

                    MessageBox.Show(
                        "No script filename was found.",
                        "Stored Procedure",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                string folderPath = Path.Combine(
                    Environment.GetFolderPath(
                        Environment.SpecialFolder.CommonApplicationData),
                    "SmartMRT_Dashboard",
                    "StoredProcedure");

                string filePath = Path.Combine(
                    folderPath,
                    Path.GetFileName(scriptFile));

                if (!File.Exists(filePath))
                {
                    rtbScript.Clear();

                    MessageBox.Show(
                        "SQL script file not found:\n\n" + filePath,
                        "File Not Found",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                // Read the actual SQL code from the file.
                rtbScript.Text = File.ReadAllText(filePath);
                rtbScript.SelectionStart = 0;
                rtbScript.SelectionLength = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Failed to load SQL script.\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }


        private void ddlListSP_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            lblDeployProgress.Visible = false;
            progressBarDeploy.Visible = false;
            SPInfo sp = ddlListSP.SelectedItem as SPInfo;

            if (sp == null)
                return;

            NameProcedure = sp.Name;
            VersionProcedure = sp.Version;
            StatusProcedure = sp.Active;

            DateModified = sp.DateModified.ToString(
                "yyyy-MM-dd");

            txtName.Text = sp.Name;
            txtVersion.Text = sp.Version.ToString();
            chkActive.Checked = sp.Active;
            dateModified.Text = DateModified;

            // Load the latest SQL script into the RichTextBox.
            LoadProcedureScript(sp.Name);
        }
        private bool isEditingScript = false;
        private string originalScript = string.Empty;
        private void btnEdit_Click(object sender, EventArgs e)
        {
            lblDeployProgress.Visible = false;
            progressBarDeploy.Visible = false;
            if (!isEditingScript)
            {
                // Enter edit mode
                originalScript = rtbScript.Text;

                isEditingScript = true;
                rtbScript.ReadOnly = false;
                btnEdit.Text = "Cancel Edit";
            }
            else
            {
                // Cancel editing and restore original script
                rtbScript.Text = originalScript;

                isEditingScript = false;
                rtbScript.ReadOnly = true;
                btnEdit.Text = "Edit Script";
            }
        }


        private void btnNew_Click(object sender, EventArgs e)
        {
            string procedureName = txtName.Text.Trim();
            string script = rtbScript.Text.Trim();

            // 1. Validate the procedure name
            if (string.IsNullOrWhiteSpace(procedureName))
            {
                MessageBox.Show(
                    "Please enter a stored procedure name.",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtName.Focus();
                return;
            }

            // Allow simple SQL procedure names only
            if (!Regex.IsMatch(
                procedureName,
                @"^[A-Za-z_][A-Za-z0-9_]*$"))
            {
                MessageBox.Show(
                    "Invalid procedure name.\n\n" +
                    "Use letters, numbers and underscores. " +
                    "The name must not start with a number.",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            // 2. Validate the SQL script
            if (string.IsNullOrWhiteSpace(script))
            {
                MessageBox.Show(
                    "Please enter the SQL script.",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                rtbScript.Focus();
                return;
            }

            // Ensure the script refers to the entered procedure.
            if (!Regex.IsMatch(
                script,
                @"\b(?:CREATE\s+OR\s+ALTER|CREATE|ALTER)\s+" +
                @"PROC(?:EDURE)?\s+" +
                @"(?:\[dbo\]\s*\.\s*)?\[?" +
                Regex.Escape(procedureName) +
                @"\]?\b",
                RegexOptions.IgnoreCase))
            {
                MessageBox.Show(
                    "The SQL script does not appear to create or alter " +
                    "the procedure named '" + procedureName + "'.",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            string connectionString =
                ConnectionMain.GetDashboardConnection();

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                MessageBox.Show(
                    "Database connection is not available.",
                    "Database",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            string folderPath = Path.Combine(
                Environment.GetFolderPath(
                    Environment.SpecialFolder.CommonApplicationData),
                "SmartMRT_Dashboard",
                "StoredProcedure");

            string scriptFile = procedureName + ".sql";

            string filePath = Path.Combine(
                folderPath,
                scriptFile);

            bool fileCreated = false;

            try
            {
                // 3. Check whether this procedure already exists
                using (SqlConnection conn =
                    new SqlConnection(connectionString))
                {
                    conn.Open();

                    string checkQuery = @"
                SELECT COUNT(*)
                FROM dbo.DC_SP_RECORD
                WHERE PROCEDURE_NAME = @Name;";

                    using (SqlCommand checkCmd =
                        new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue(
                            "@Name", procedureName);

                        int count = Convert.ToInt32(
                            checkCmd.ExecuteScalar());

                        if (count > 0)
                        {
                            MessageBox.Show(
                                "This procedure already exists.",
                                "Duplicate Procedure",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    // Do not overwrite an existing script file.
                    if (File.Exists(filePath))
                    {
                        MessageBox.Show(
                            "A script file with this name already exists:\n\n" +
                            filePath,
                            "Duplicate File",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        return;
                    }

                    // 4. Save the SQL file
                    Directory.CreateDirectory(folderPath);

                    File.WriteAllText(filePath, script);
                    fileCreated = true;

                    // 5. Save the master record and history together
                    using (SqlTransaction transaction =
                        conn.BeginTransaction())
                    {
                        try
                        {
                            string insertRecord = @"
                        INSERT INTO dbo.DC_SP_RECORD
                        (
                            PROCEDURE_NAME,
                            SCRIPT_VERSION,
                            STATUS_ACTIVE,
                            DATEMODIFIED
                        )
                        VALUES
                        (
                            @Name,
                            1,
                            1,
                            SYSDATETIME()
                        );";

                            using (SqlCommand cmd =
                                new SqlCommand(
                                    insertRecord, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue(
                                    "@Name", procedureName);

                                cmd.ExecuteNonQuery();
                            }

                            string insertHistory = @"
                        INSERT INTO dbo.DC_SP_UPDATEVERSION
                        (
                            PROCEDURE_NAME,
                            PROCEDURE_SCRIPT,
                            SCRIPT_VERSION,
                            REMARK,
                            DATEMODIFIED
                        )
                        VALUES
                        (
                            @Name,
                            @ScriptFile,
                            1,
                            @Remark,
                            SYSDATETIME()
                        );";

                            using (SqlCommand cmd =
                                new SqlCommand(
                                    insertHistory, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue(
                                    "@Name", procedureName);

                                // Store the filename in your existing column.
                                cmd.Parameters.AddWithValue(
                                    "@ScriptFile", scriptFile);

                                cmd.Parameters.AddWithValue(
                                    "@Remark", "Initial release");

                                cmd.ExecuteNonQuery();
                            }

                            transaction.Commit();
                        }
                        catch
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }

                MessageBox.Show(
                    "Stored procedure created successfully.\n\n" +
                    "Name: " + procedureName + "\n" +
                    "Version: 1\n" +
                    "File: " + filePath,
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                // 6. Refresh the ComboBox
                getSpData();

                chkNewSP.Checked = false;
            }
            catch (Exception ex)
            {
                // Remove the new file if the database operation failed.
                if (fileCreated)
                {
                    try
                    {
                        if (File.Exists(filePath))
                            File.Delete(filePath);
                    }
                    catch
                    {
                        // Keep the original exception for reporting.
                    }
                }

                MessageBox.Show(
                    "Failed to create stored procedure record.\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }


        private void btnUpdateScript_Click(object sender, EventArgs e)
        {
            SPInfo sp = ddlListSP.SelectedItem as SPInfo;
            lblDeployProgress.Visible = false;
            progressBarDeploy.Visible = false;

            if (sp == null)
            {
                MessageBox.Show("Please select a stored procedure.");
                return;
            }

            if (rtbScript.ReadOnly)
            {
                MessageBox.Show(
                    "Please click Edit before updating the script.",
                    "Update Script",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            string procedureName = sp.Name;
            string newScript = rtbScript.Text.Trim();

            if (string.IsNullOrWhiteSpace(newScript))
            {
                MessageBox.Show("The SQL script cannot be empty.");
                return;
            }

            if (string.Equals(
                originalScript.Trim(),
                newScript,
                StringComparison.Ordinal))
            {
                MessageBox.Show(
                    "No changes were made to the script.",
                    "Update Script",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            string connectionString =
                ConnectionMain.GetDashboardConnection();

            string folderPath = Path.Combine(
                Environment.GetFolderPath(
                    Environment.SpecialFolder.CommonApplicationData),
                "SmartMRT_Dashboard",
                "StoredProcedure");

            string currentFile = Path.Combine(
                folderPath,
                procedureName + ".sql");

            int currentVersion = 0;
            bool active = true;

            string oldHistoryFile = null;
            string oldHistoryVersionFile = null;
            string newHistoryFile = null;

            string oldCurrentContents = null;
            bool currentFileExisted = false;

            try
            {
                Directory.CreateDirectory(folderPath);

                if (File.Exists(currentFile))
                {
                    oldCurrentContents = File.ReadAllText(currentFile);
                    currentFileExisted = true;
                }

                using (SqlConnection conn =
                    new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Read the current approved version.
                    string getVersion = @"
                SELECT SCRIPT_VERSION, STATUS_ACTIVE
                FROM dbo.DC_SP_RECORD
                WHERE PROCEDURE_NAME = @Name;";

                    using (SqlCommand cmd =
                        new SqlCommand(getVersion, conn))
                    {
                        cmd.Parameters.AddWithValue(
                            "@Name", procedureName);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (!reader.Read())
                            {
                                MessageBox.Show(
                                    "The procedure was not found in DC_SP_RECORD.");
                                return;
                            }

                            currentVersion = Convert.ToInt32(
                                reader["SCRIPT_VERSION"]);

                            active = Convert.ToBoolean(
                                reader["STATUS_ACTIVE"]);
                        }
                    }

                    int newVersion = currentVersion + 1;

                    // Preserve the previous version using the original
                    // contents captured when Edit mode was entered.
                    //  string previousVersionFile =
                    //    procedureName + "_v" +
                    //  currentVersion + ".sql";

                    string backupFileName = procedureName + ".bak.sql";
                    string backupFilePath = Path.Combine(folderPath, backupFileName);

                    string previousVersionPath = Path.Combine(
                        folderPath, backupFileName);

                    // If this version was already archived, don't overwrite it.
                    if (!File.Exists(previousVersionPath))
                    {
                        string previousScript = originalScript;

                        if (string.IsNullOrWhiteSpace(previousScript))
                            previousScript = oldCurrentContents;

                        if (string.IsNullOrWhiteSpace(previousScript))
                        {
                            throw new InvalidOperationException(
                                "The previous SQL script is unavailable.");
                        }

                        File.WriteAllText(backupFilePath, previousScript);

                        oldHistoryVersionFile = previousVersionPath;
                    }

                    // Keep the current deployment filename unchanged.
                    File.WriteAllText(currentFile, newScript);

                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            // Point the previous history record to its snapshot.
                            string updateOldHistory = @"
                        UPDATE dbo.DC_SP_UPDATEVERSION
                        SET PROCEDURE_SCRIPT = @OldFile
                        WHERE PROCEDURE_NAME = @Name
                          AND SCRIPT_VERSION = @OldVersion
                          AND PROCEDURE_SCRIPT = @CurrentFileName;";

                            using (SqlCommand cmd =
                                new SqlCommand(updateOldHistory, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue(
                                    "@OldFile", backupFileName);

                                cmd.Parameters.AddWithValue(
                                    "@Name", procedureName);

                                cmd.Parameters.AddWithValue(
                                    "@OldVersion", currentVersion);

                                cmd.Parameters.AddWithValue(
                                    "@CurrentFileName",
                                    procedureName + ".sql");

                                cmd.ExecuteNonQuery();
                            }

                            // Update the current approved version.
                            string updateRecord = @"
                        UPDATE dbo.DC_SP_RECORD
                        SET SCRIPT_VERSION = @NewVersion,
                            DATEMODIFIED = SYSDATETIME()
                        WHERE PROCEDURE_NAME = @Name;";

                            using (SqlCommand cmd =
                                new SqlCommand(updateRecord, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue(
                                    "@NewVersion", newVersion);

                                cmd.Parameters.AddWithValue(
                                    "@Name", procedureName);

                                cmd.ExecuteNonQuery();
                            }

                            // Insert the new version into history.
                            string insertHistory = @"
                        INSERT INTO dbo.DC_SP_UPDATEVERSION
                        (
                            PROCEDURE_NAME,
                            PROCEDURE_SCRIPT,
                            SCRIPT_VERSION,
                            REMARK,
                            DATEMODIFIED
                        )
                        VALUES
                        (
                            @Name,
                            @ScriptFile,
                            @Version,
                            @Remark,
                            SYSDATETIME()
                        );";

                            using (SqlCommand cmd =
                                new SqlCommand(insertHistory, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue(
                                    "@Name", procedureName);

                                cmd.Parameters.AddWithValue(
                                    "@ScriptFile", procedureName + ".sql");

                                cmd.Parameters.AddWithValue(
                                    "@Version", newVersion);

                                cmd.Parameters.AddWithValue(
                                    "@Remark", "Updated script");

                                cmd.ExecuteNonQuery();
                            }

                            transaction.Commit();
                        }
                        catch
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }

                    // The database now records the new version.
                    txtVersion.Text = newVersion.ToString();
                    dateModified.DateText =
                        DateTime.Now.ToString("yyyy-MM-dd");

                    rtbScript.ReadOnly = true;
                    btnEdit.Text = "Edit";
                    isEditingScript = false;
                    originalScript = newScript;

                    MessageBox.Show(
                        procedureName + " updated successfully.\n\n" +
                        "Previous version: " + currentVersion + "\n" +
                        "New version: " + newVersion + "\n\n" +
                        "The script has not yet been deployed to the lines.",
                        "Update Script",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    getSpData();
                }
            }
            catch (Exception ex)
            {
                // Restore the current deployment file if an operation failed.
                try
                {
                    if (currentFileExisted)
                    {
                        File.WriteAllText(
                            currentFile, oldCurrentContents);
                    }
                    else if (File.Exists(currentFile))
                    {
                        File.Delete(currentFile);
                    }

                    if (oldHistoryVersionFile != null &&
                        File.Exists(oldHistoryVersionFile))
                    {
                        File.Delete(oldHistoryVersionFile);
                    }

                    if (newHistoryFile != null &&
                        File.Exists(newHistoryFile))
                    {
                        File.Delete(newHistoryFile);
                    }
                }
                catch
                {
                    // If restoration fails, retain the files for recovery.
                }

                MessageBox.Show(
                    "Failed to update the stored procedure.\n\n" +
                    ex.Message,
                    "Update Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private List<LineInfo> GetActiveLines()
        {
            List<LineInfo> lines = new List<LineInfo>();

            string connectionString =
                ConnectionMain.GetDashboardConnection();

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException(
                    "Database connection is not available.");
            }

            string sql = @"
        SELECT LINE_NUM, IP_ADDRESS
        FROM dbo.DC_LINEDETAIL
        WHERE STATUS_ACTIVE = 1
          AND IP_ADDRESS IS NOT NULL
          AND LTRIM(RTRIM(IP_ADDRESS)) <> ''
        ORDER BY LINE_NUM;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lines.Add(new LineInfo
                        {
                            LineNum = reader["LINE_NUM"].ToString(),
                            IPAddress = reader["IP_ADDRESS"].ToString()
                        });
                    }
                }
            }

            return lines;
        }
        private void ExecuteSqlScript(
    SqlConnection conn,
    string script)
        {
            string[] batches = Regex.Split(
                script,
                @"^\s*GO(?:\s+\d+)?\s*(?:--.*)?$",
                RegexOptions.Multiline |
                RegexOptions.IgnoreCase);

            foreach (string batch in batches)
            {
                string sqlBatch = batch.Trim();

                if (string.IsNullOrWhiteSpace(sqlBatch))
                    continue;

                using (SqlCommand cmd =
                    new SqlCommand(sqlBatch, conn))
                {
                    cmd.CommandTimeout = 120;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void EnsureTableTypes(SqlConnection conn, string folderPath)
        {
            string[] typeFiles =
            {
        "ProductInfoType.sql",
        "WorkerInfoType.sql",
        "StationAssignInfoType.sql"
    };

            foreach (string fileName in typeFiles)
            {
                string filePath = Path.Combine(folderPath, fileName);

                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException(
                        "Required table type script not found:\n" + filePath);
                }

                string typeScript = File.ReadAllText(filePath);

                if (string.IsNullOrWhiteSpace(typeScript))
                {
                    throw new InvalidOperationException(
                        "Table type script is empty: " + fileName);
                }

                ExecuteSqlScript(conn, typeScript);
            }
        }

        private bool TestLineConnection(
    string ipAddress,
    out string errorMessage)
        {
            errorMessage = null;

            try
            {
                string connectionString =
                    ConnectionMain.GetLineGlobalDBConnection(ipAddress);

                using (SqlConnection conn =
                    new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd =
                        new SqlCommand("SELECT 1", conn))
                    {
                        cmd.CommandTimeout = 5;
                        cmd.ExecuteScalar();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        private async void btnUpdateAll_Click(object sender, EventArgs e)
        {
            SPInfo sp = ddlListSP.SelectedItem as SPInfo;

            if (sp == null)
            {
                MessageBox.Show(
                    "Please select a stored procedure.",
                    "Update All Lines",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            // Require the script to be saved before deployment.
            if (!rtbScript.ReadOnly)
            {
                MessageBox.Show(
                    "Please save the script using Update Script before deploying.",
                    "Update All Lines",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            string procedureName = sp.Name;

            // Prevent an invalid procedure name from being used as a filename.
            if (!Regex.IsMatch(
                procedureName,
                @"^[A-Za-z_][A-Za-z0-9_]*$"))
            {
                MessageBox.Show(
                    "The selected procedure name is invalid.",
                    "Update All Lines",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            string folderPath = Path.Combine(
                Environment.GetFolderPath(
                    Environment.SpecialFolder.CommonApplicationData),
                "SmartMRT_Dashboard",
                "StoredProcedure");

            // Deploy only the current script, never the .bak.sql file.
            string currentFile = Path.Combine(
                folderPath,
                procedureName + ".sql");

            if (!File.Exists(currentFile))
            {
                MessageBox.Show(
                    "The current SQL script was not found:\n\n" +
                    currentFile,
                    "Update All Lines",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            string script;

            try
            {
                script = File.ReadAllText(currentFile);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Failed to read the SQL script.\n\n" + ex.Message,
                    "Update All Lines",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(script))
            {
                MessageBox.Show(
                    "The SQL script is empty.",
                    "Update All Lines",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            List<LineInfo> lines;

            try
            {
                lines = GetActiveLines();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Failed to retrieve active lines.\n\n" + ex.Message,
                    "Update All Lines",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (lines.Count == 0)
            {
                MessageBox.Show(
                    "No active lines were found.",
                    "Update All Lines",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show(
                "Deploy stored procedure:\n" + procedureName +
                "\n\nCurrent version: " + sp.Version +
                "\nActive lines: " + lines.Count +
                "\n\nContinue with deployment?",
                "Confirm Deployment",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
                return;

            int successCount = 0;
            int failedCount = 0;
            progressBarDeploy.Minimum = 0;
            progressBarDeploy.Maximum = lines.Count;
            progressBarDeploy.Value = 0;
            progressBarDeploy.Visible = true;
            lblDeployProgress.Text = "Starting deployment...";

            btnUpdateAll.Enabled = false;
            StringBuilder results = new StringBuilder();

            try
            {
                // Create Progress on the UI thread.
                IProgress<Tuple<int, string>> progress =
                    new Progress<Tuple<int, string>>(p =>
                    {
                        progressBarDeploy.Value = p.Item1;
                        lblDeployProgress.Text = p.Item2;
                    });

                await Task.Run(() =>
                {
                    for (int i = 0; i < lines.Count; i++)
                    {
                        LineInfo line = lines[i];

                        try
                        {
                            string connectionError;

                            if (!TestLineConnection(
                                line.IPAddress, out connectionError))
                            {
                                Interlocked.Increment(ref failedCount);

                                lock (results)
                                {
                                    results.AppendLine(
                                        "Line " + line.LineNum +
                                        " (" + line.IPAddress +
                                        "): CONNECTION FAILED");

                                    results.AppendLine(connectionError);
                                    results.AppendLine();
                                }
                            }
                            else
                            {
                                string connectionString =
                                    ConnectionMain.GetLineGlobalDBConnection(
                                        line.IPAddress);

                                using (SqlConnection conn =
                                    new SqlConnection(connectionString))
                                {
                                    conn.Open();
                                    EnsureTableTypes(conn, folderPath);
                                    ExecuteSqlScript(conn, script);
                                }

                                Interlocked.Increment(ref successCount);

                                lock (results)
                                {
                                    results.AppendLine(
                                        "Line " + line.LineNum +
                                        " (" + line.IPAddress +
                                        "): SUCCESS");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Interlocked.Increment(ref failedCount);

                            lock (results)
                            {
                                results.AppendLine(
                                    "Line " + line.LineNum +
                                    " (" + line.IPAddress +
                                    "): FAILED - " + ex.Message);
                            }
                        }

                        // Advance after each line, including failures.
                        progress.Report(Tuple.Create(
                            i + 1,
                            "Processed " + (i + 1) +
                            " of " + lines.Count +
                            " lines — Line " + line.LineNum));
                    }
                });

                lblDeployProgress.Text =
                    "Completed: " + successCount +
                    " successful, " + failedCount + " failed.";

                MessageBox.Show(
                    results.ToString(),
                    "Deployment Results",
                    MessageBoxButtons.OK,
                    failedCount == 0
                        ? MessageBoxIcon.Information
                        : MessageBoxIcon.Warning);
            }
            finally
            {
                btnUpdateAll.Enabled = true;
            }

            //string summary =
            //    "Stored procedure: " + procedureName +
            //    "\nVersion: " + sp.Version +
            //    "\n\nSuccessful: " + successCount +
            //    "\nFailed: " + failedCount +
            //    "\n\nDeployment results:\n\n" +
            //    results.ToString();

            //MessageBox.Show(
            //    summary,
            //    failedCount == 0
            //        ? "Deployment Completed"
            //        : "Deployment Completed with Errors",
            //    MessageBoxButtons.OK,
            //    failedCount == 0
            //        ? MessageBoxIcon.Information
            //        : MessageBoxIcon.Warning);
        }
    
           
    }
}
