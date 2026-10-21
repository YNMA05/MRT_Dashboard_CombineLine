using Newtonsoft.Json;
using Org.BouncyCastle.Utilities.Net;
using SmartMRT_DashCombine_Setup.Controls;
using SmartMRT_DashCombine_Setup.Security;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartMRT_DashCombine_Setup
{
    public partial class UC_MRT_DashCombine_Setup : UserControl
    {
        private string connectionString = string.Empty;
        private int editingLineID = 0;
        private bool isEditMode = false;
        private GlassButton glassSave;
        private GlassButton glassClear;
        private Panel pnlBody;
        private Panel pnlSummary;
        private Panel cardTotalLine;
        private Panel cardOnlineLine;
        private Panel cardActiveLine;
        private Label lblTotalLine;
        private Label lblOnlineLine;
        private Label lblActiveLine;
        private Panel pnlProcedureManagement;
        private GlassButton glassProcedure;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SendMessage(IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

        private const int EM_SETCUEBANNER = 0x1501;

        //public UC_MRT_DashCombine_Setup()
        //{
        //    InitializeComponent();
        //    ApplyModernStyle();
        //    flowLineList.FlowDirection = FlowDirection.LeftToRight;

        //    flowLineList.WrapContents = true;

        //    flowLineList.AutoScroll = true;

        //    this.Resize += UC_MRT_DashCombine_Setup_Resize;
        //    SetEditMode(false);
        //}
        public UC_MRT_DashCombine_Setup()
        {
            InitializeComponent();

            ApplyModernStyle();

            SetEditMode(false);

            this.Resize += UC_MRT_DashCombine_Setup_Resize;

            UpdateSetupLayout();
        }
        //private void ApplyModernStyle()
        //{
        //    pnlBody = new Panel
        //    {
        //        Dock = DockStyle.Fill,
        //        BackColor = Color.Transparent
        //    };
        //    pnlSummary = new Panel
        //    {
        //        Dock = DockStyle.Top,
        //        Height = 90,
        //        Padding = new Padding(25, 5, 25, 10),
        //        BackColor = Color.Transparent
        //    };
        //    cardOnlineLine = CreateSummaryCard("ONLINE", "0", out lblOnlineLine);
        //    cardActiveLine = CreateSummaryCard("ACTIVE", "0", out lblActiveLine);
        //    cardTotalLine = CreateSummaryCard("TOTAL", "0", out lblTotalLine);

        //    pnlSummary.Controls.Add(cardActiveLine);
        //    pnlSummary.Controls.Add(cardOnlineLine);
        //    pnlSummary.Controls.Add(cardTotalLine);


        //    this.Controls.Add(pnlSummary);

        //    pnlHeader.Dock = DockStyle.Top;
        //    pnlSummary.Dock = DockStyle.Top;
        //    flowLineList.Dock = DockStyle.Fill;

        //    // Explicit WinForms docking order
        //    this.Controls.SetChildIndex(pnlHeader, 0);
        //    this.Controls.SetChildIndex(pnlSummary, 1);
        //    this.Controls.SetChildIndex(flowLineList, 2);

        //    LayoutSummaryCards();
        //}

        private void ApplyModernStyle()
        {
            glassSave = new GlassButton
            {
                Name = "glassSave",
                Text = "Save",
                Size = btnSave.Size,
                Location = btnSave.Location,
                BorderColor = Color.FromArgb(56, 211, 159),
                GlassColor = Color.FromArgb(25, 55, 65),
                TextColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            glassClear = new GlassButton
            {
                Name = "glassClear",
                Text = "Clear Text",
                Size = btnClear.Size,
                Location = btnClear.Location,
                BorderColor = Color.FromArgb(56, 211, 159),
                GlassColor = Color.FromArgb(25, 55, 65),
                TextColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            glassProcedure = new GlassButton
            {
                Name = "glassProcedure",
                Text = "Check Procedures",
                Size = new Size(170, 42),
                BorderColor = Color.FromArgb(56, 211, 159),
                GlassColor = Color.FromArgb(25, 55, 65),
                TextColor = Color.White,
                Font = new Font(
                "Segoe UI",
                9F,
                FontStyle.Bold)
            };
            glassSave.Click += btnSave_Click;
            glassClear.Click += btnClear_Click;
            btnSave.Parent.Controls.Add(glassSave);
            btnClear.Parent.Controls.Add(glassClear);
            btnSave.Visible = false;
            btnClear.Visible = false;
            // =====================================================
            // REMOVE FLOW FROM DESIGNER
            // =====================================================
            if (flowLineList.Parent != null)
                flowLineList.Parent.Controls.Remove(flowLineList);
            // =====================================================
            // SUMMARY
            // =====================================================
            pnlSummary = new Panel
            {
                Name = "pnlSummary",
                BackColor = Color.Transparent,//Color.FromArgb(30, 80, 120), // DEBUG BLUE
                Height = 90
            };
            // =====================================================
            // SUMMARY CARDS
            // =====================================================

            cardOnlineLine = CreateSummaryCard(
                "ONLINE",
                "0",
                out lblOnlineLine);

            cardActiveLine = CreateSummaryCard(
                "ACTIVE",
                "0",
                out lblActiveLine);

            cardTotalLine = CreateSummaryCard(
                "TOTAL",
                "0",
                out lblTotalLine);


            pnlSummary.Controls.Add(cardOnlineLine);
            pnlSummary.Controls.Add(cardActiveLine);
            pnlSummary.Controls.Add(cardTotalLine);


            // =====================================================
            // FLOW LIST
            // =====================================================

            flowLineList.Dock = DockStyle.None;

            flowLineList.FlowDirection =
                FlowDirection.LeftToRight;

            flowLineList.WrapContents = true;
            flowLineList.AutoScroll = true;

            flowLineList.BackColor =
                Color.FromArgb(8, 21, 47);

            // =====================================================
            // ADD DIRECTLY TO USERCONTROL
            // =====================================================

            this.Controls.Add(flowLineList);
            this.Controls.Add(pnlSummary);
            this.Controls.Add(pnlHeader);
            this.Controls.Add(pnlProcedureManagement);
            pnlHeader.Controls.Add(glassProcedure);
            glassProcedure.Location = new Point(
                pnlHeader.ClientSize.Width - glassProcedure.Width - 20,
                20);
            glassProcedure.Click += glassProcedure_Click;

            // =====================================================
            // Z-ORDER
            // =====================================================

            flowLineList.SendToBack();

            pnlSummary.BringToFront();

            pnlHeader.BringToFront();


            // =====================================================
            // LAYOUT
            // =====================================================

            UpdateSetupLayout();
        }

        private void UpdateBodyLayout()
        {
            if (pnlBody == null)
                return;

            if (pnlSummary == null)
                return;

            if (flowLineList == null)
                return;


            // =====================================================
            // SUMMARY
            // =====================================================

            int summaryHeight = 90;

            pnlSummary.SetBounds(
                0,
                0,
                pnlBody.ClientSize.Width,
                summaryHeight);


            // =====================================================
            // FLOW LIST
            // =====================================================

            flowLineList.SetBounds(
                0,
                summaryHeight,
                pnlBody.ClientSize.Width,
                Math.Max(
                    0,
                    pnlBody.ClientSize.Height - summaryHeight));


            // =====================================================
            // SUMMARY CARDS
            // =====================================================

            LayoutSummaryCards();


            pnlSummary.BringToFront();
        }

        private void UpdateSetupLayout()
        {
            if (pnlHeader == null ||
                pnlSummary == null ||
                flowLineList == null)
                return;


            int headerHeight = 201;
            int summaryHeight = 90;


            // =====================================================
            // HEADER
            // =====================================================

            pnlHeader.SetBounds(
                0,
                0,
                this.ClientSize.Width,
                headerHeight);


            // =====================================================
            // SUMMARY
            // =====================================================

            pnlSummary.SetBounds(
                0,
                headerHeight,
                this.ClientSize.Width,
                summaryHeight);


            // =====================================================
            // FLOW LIST
            // =====================================================

            flowLineList.SetBounds(
                0,
                headerHeight + summaryHeight,
                this.ClientSize.Width,
                Math.Max(
                    0,
                    this.ClientSize.Height
                    - headerHeight
                    - summaryHeight));


            // =====================================================
            // SUMMARY CARDS
            // =====================================================

            LayoutSummaryCards();


            // =====================================================
            // FORCE Z-ORDER
            // =====================================================

            flowLineList.SendToBack();

            pnlSummary.BringToFront();

            pnlHeader.BringToFront();
        }

        private void glassProcedure_Click(object sender, EventArgs e)
        {
            using (FrmUpdateSP form = new FrmUpdateSP())
            {
                form.ShowDialog(this);
            }

            // Always refresh after FrmAddModule closes
            LoadLineCards();

            //await CheckAllLineStatusAsync();
        }

        private Panel CreateSummaryCard(
    string title,
    string value,
    out Label valueLabel)
        {
            Panel card = new Panel
            {
                Height = 80,
                BackColor = Color.FromArgb(16, 36, 71),
                Margin = new Padding(0, 0, 12, 0)
            };

            Label lbl = new Label
            {
                Text = title,
                Font = new Font(
                    "Segoe UI",
                    8.5f,
                    FontStyle.Bold),
                ForeColor = Color.FromArgb(130, 165, 195),
                AutoSize = true,
                Location = new Point(15, 12)
            };

            valueLabel = new Label
            {
                Text = value,
                Font = new Font(
                    "Segoe UI",
                    20,
                    FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 212, 255),
                AutoSize = true,
                Location = new Point(15, 32)
            };

            card.Controls.Add(lbl);
            card.Controls.Add(valueLabel);

            return card;
        }

        private void LayoutSummaryCards()
        {
            if (pnlSummary == null)
                return;

            if (pnlSummary.ClientSize.Width <= 0)
                return;

            int spacing = 12;
            int leftRightPadding = 25;

            int availableWidth =
                pnlSummary.ClientSize.Width
                - (leftRightPadding * 2);

            if (availableWidth <= 0)
                return;

            int cardWidth =
                (availableWidth - (spacing * 2)) / 3;

            if (cardWidth < 100)
                cardWidth = 100;

            Panel[] cards =
            {
        cardOnlineLine,
        cardActiveLine,
        cardTotalLine
    };

            int x = leftRightPadding;

            foreach (Panel card in cards)
            {
                if (card == null)
                    continue;

                card.SetBounds(
                    x,
                    5,
                    cardWidth,
                    80);

                x += cardWidth + spacing;
            }
        }

        private void UpdateSummary()
        {
            int total = 0;
            int active = 0;
            int online = 0;

            foreach (Control control in flowLineList.Controls)
            {
                if (control is UC_LineCard card)
                {
                    total++;

                    if (card.IsActive)
                        active++;

                    if (card.IsOnline)
                        online++;
                }
            }

            lblTotalLine.Text = total.ToString();
            lblActiveLine.Text = active.ToString();
            lblOnlineLine.Text = online.ToString();
        }

        public void SetEditMode(bool editMode)
        {
            isEditMode = editMode;

            // Parent controls
            txtLine.Enabled = editMode;
            txtIpAddress.Enabled = editMode;
            chkStatus.Enabled = editMode;
            txtMaster.Enabled = editMode;
            chkMaster.Enabled = editMode;
            //btnSave.Enabled = editMode;
            //btnClear.Enabled = editMode;
            if (glassClear != null)
                glassClear.Enabled = editMode;
            if (glassSave != null)
                glassSave.Enabled = editMode;
            if (flowLineList != null)
                flowLineList.Enabled = editMode;


            // Line cards
            foreach (Control control in flowLineList.Controls)
            {
                if (control is UC_LineCard lineCard)
                {
                    lineCard.SetEditMode(editMode);
                }
            }
        }

        public void SetUpdateSP(bool canUpdateSP)
        {
            if (glassProcedure == null)
                return;
            glassProcedure.Visible = canUpdateSP;
        }

        private async void UC_MRT_DashCombine_Setup_Load(
            object sender,
            EventArgs e)
        {
            //config = DashboardConfigManager.Load();

            //if (config == null)
            //{
            //    MessageBox.Show(
            //        "DashboardConfig.json was not found.");

            //    return;
            //}

            LoadLineCards();

            await CheckAllLineStatusAsync();
        }

        //private void getCountLine()
        //{
        //    var count = 0;
        //    string connectionString = ConnectionMain.GetDashboardConnection();

        //    string SQL = @"SELECT LICENSE_GROUP, DASHBOARD,MAX_LINES,MAX_PAGES FROM DC_LICENSEDETAIL WHERE LICENSE_TYPE = @LicenseType";

        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {
        //        conn.Open();
        //        using (SqlCommand cmd = new SqlCommand(SQL, conn))
        //        {
        //            cmd.Parameters.AddWithValue("@LicenseType", AppSession.LicenseType);
        //            using (SqlDataReader reader = cmd.ExecuteReader())
        //            {
        //                if (reader.Read())
        //                {
        //                    count = Convert.ToInt32(reader["MAX_LINES"]);
        //                }
        //            }
        //        }
        //    }

        //}
        private async Task CheckAllLineStatusAsync()
        {
            foreach (Control control in flowLineList.Controls)
            {
                if (control is UC_LineCard card)
                {
                    await card.CheckLineStatusAsync();
                }
            }
            UpdateSummary();
        }

        private void LoadLineCards()
        {
            flowLineList.Controls.Clear();

            connectionString =
                ConnectionMain.GetDashboardConnection();

            string sql = @"
        SELECT
            LINEID,
            LINE_NUM,
            IP_ADDRESS,
            MASTER_LINE_ID,
            STATUS_ACTIVE
        FROM DC_LINEDETAIL
        ORDER BY LINE_NUM";

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
                        while (reader.Read())
                        {
                            UC_LineCard card =
                                new UC_LineCard();

                            // Database ID
                            card.LineID =
                                Convert.ToInt32(
                                    reader["LINEID"]);

                            // Line number
                            card.LineNumber =
                                Convert.ToInt32(
                                    reader["LINE_NUM"]);

                            // IP Address
                            card.IPAddress =
                                reader["IP_ADDRESS"]
                                .ToString();

                            // Master LINEID
                            card.MasterLineID =
                                Convert.ToInt32(
                                    reader["MASTER_LINE_ID"]);

                            // Active / inactive
                            card.IsActive =
                                Convert.ToBoolean(
                                    reader["STATUS_ACTIVE"]);

                            // Connect Edit button
                            card.EditClicked +=
                                LineCard_EditClicked;

                            card.DeleteClicked +=
                                LineCard_DeleteClicked;
                            card.SPClicked += LineCard_SPClicked;

                            flowLineList.Controls.Add(card);
                        }
                    }
                }
            }
        }

        private async void LineCard_SPClicked(object sender, EventArgs e)
        {
            UC_LineCard card = sender as UC_LineCard;

            if (card == null)
                return;

            try
            {
                Cursor = Cursors.WaitCursor;

                bool success = await CreateStoredProceduresAsync(card.IPAddress);

                if (success)
                {
                    MessageBox.Show(
                    $"Stored procedures created successfully for Line {card.LineNumber}.",
                        "Stored Procedure",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    ActivityLogger.Log(connectionString, "CREATE SP", $"LINE NUMBER:{txtLine.Text}", "NEW LINE");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Failed to create stored procedures for Line {card.LineNumber}.\n\n{ex.Message}",
                    "Stored Procedure Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                Logging.FolderLog.Error(
                    "Stored Procedure Error : " + ex);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }


        private async Task<bool> CreateStoredProceduresAsync(
            string ipAddress)
        {
            string dashboardConnectionString =
                ConnectionMain.GetDashboardConnection();

            string lineConnectionString =
                ConnectionMain.GetLineGlobalDBConnection(ipAddress);

            if (string.IsNullOrWhiteSpace(dashboardConnectionString))
                throw new Exception(
                    "Dashboard database connection is unavailable.");

            if (string.IsNullOrWhiteSpace(lineConnectionString))
                throw new Exception(
                    "Line database connection is unavailable for IP " +
                    ipAddress + ".");

            string spFolder = Path.Combine(
                Environment.GetFolderPath(
                    Environment.SpecialFolder.CommonApplicationData),
                "SmartMRT_Dashboard",
                "StoredProcedure");

            if (!Directory.Exists(spFolder))
                throw new DirectoryNotFoundException(
                    "Stored Procedure folder not found:\n" + spFolder);

            string sql = @"
        SELECT
            R.PROCEDURE_NAME,
            U.PROCEDURE_SCRIPT,
            R.SCRIPT_VERSION
        FROM dbo.DC_SP_RECORD AS R
        INNER JOIN dbo.DC_SP_UPDATEVERSION AS U
            ON U.PROCEDURE_NAME = R.PROCEDURE_NAME
            AND U.SCRIPT_VERSION = R.SCRIPT_VERSION
        WHERE R.STATUS_ACTIVE = 1
        ORDER BY R.PROCEDURE_NAME;";

            List<(string Name, string File, int Version)> procedures =
                new List<(string Name, string File, int Version)>();

            // Connection 1: Read the procedure registry from DASHBOARD_GLOBALDB.
            using (SqlConnection dashboardConn =
                new SqlConnection(dashboardConnectionString))
            {
                await dashboardConn.OpenAsync();

                using (SqlCommand cmd =
                    new SqlCommand(sql, dashboardConn))
                {
                    using (SqlDataReader reader =
                        await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            procedures.Add((
                                reader["PROCEDURE_NAME"].ToString(),
                                reader["PROCEDURE_SCRIPT"].ToString(),
                                Convert.ToInt32(reader["SCRIPT_VERSION"])
                            ));
                        }
                    }
                }
            }

            if (procedures.Count == 0)
                throw new Exception(
                    "No active stored procedures were found in " +
                    "DASHBOARD_GLOBALDB.dbo.DC_SP_RECORD.");

            // Connection 2: Deploy to the selected line's ONLINE_GLOBALDB.
            using (SqlConnection lineConn =
                new SqlConnection(lineConnectionString))
            {
                await lineConn.OpenAsync();

                await CreateTableTypesAsync(lineConn, spFolder);

                foreach (var procedure in procedures)
                {
                    string scriptPath = Path.Combine(
                        spFolder,
                        procedure.File);

                    if (!File.Exists(scriptPath))
                        throw new FileNotFoundException(
                            "Stored procedure script not found:\n" +
                            scriptPath);

                    string script = File.ReadAllText(scriptPath);

                    if (string.IsNullOrWhiteSpace(script))
                        throw new Exception(
                            "Stored procedure script is empty:\n" +
                            scriptPath);

                    await ExecuteSqlScriptAsync(lineConn, script);
                }
            }

            return true;
        }
        private async Task ExecuteSqlScriptAsync(
       SqlConnection connection,
       string script)
        {
            string[] batches = Regex.Split(
                script,
                @"^\s*GO\s*(?:--.*)?$",
                RegexOptions.Multiline |
                RegexOptions.IgnoreCase);

            foreach (string batch in batches)
            {
                string sql = batch.Trim();

                if (string.IsNullOrWhiteSpace(sql))
                    continue;

                using (SqlCommand cmd =
                       new SqlCommand(sql, connection))
                {
                    cmd.CommandTimeout = 120;

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        //private void LoadLineCards()
        //{
        //    flowLineList.Controls.Clear();

        //    connectionString = ConnectionMain.GetDashboardConnection();

        //    string sql = @"
        //        SELECT
        //            LINEID,
        //            LINE_NUM,
        //            IP_ADDRESS,
        //            MASTER_LINE_ID,
        //            STATUS_ACTIVE
        //        FROM DC_LINEDETAIL
        //        ORDER BY LINE_NUM";

        //    using (SqlConnection conn =
        //        new SqlConnection(connectionString))
        //    {
        //        conn.Open();

        //        using (SqlCommand cmd =
        //            new SqlCommand(sql, conn))
        //        {
        //            using (SqlDataReader reader =
        //                cmd.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    UC_LineCard card =
        //                        new UC_LineCard();

        //                    // Database ID
        //                    card.LineID =
        //                        Convert.ToInt32(
        //                            reader["LINEID"]);

        //                    // Line number
        //                    card.LineNumber =
        //                        Convert.ToInt32(
        //                            reader["LINE_NUM"]);

        //                    // IP Address
        //                    card.IPAddress =
        //                        reader["IP_ADDRESS"]
        //                        .ToString();

        //                    // Active / inactive
        //                    card.IsActive =
        //                        Convert.ToBoolean(
        //                            reader["STATUS_ACTIVE"]);

        //                    // IMPORTANT
        //                    // Connect Edit button
        //                    // to parent
        //                    card.EditClicked +=
        //                        LineCard_EditClicked;
        //                    card.DeleteClicked +=
        //                        LineCard_DeleteClicked;

        //                    flowLineList.Controls.Add(card);
        //                }
        //            }
        //        }
        //    }
        //}

        private void LineCard_DeleteClicked(
    object sender,
    EventArgs e)
        {
            if (sender is UC_LineCard card)
            {
                int lineID =
                    card.LineID;
                int lineNum = card.LineNumber;
                string ipAddress = card.IPAddress;

                DialogResult result =
                    MessageBox.Show(
                        $"Are you sure you want to delete Line {card.LineNumber}?",
                        "Confirm Delete",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    DeleteLine(lineID, lineNum, ipAddress);

                    LoadLineCards();
                }
            }
        }

        private void DeleteLine(
    int lineID, int lineNum, string IpAddress)
        {
            try
            {
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
                connectionString = ConnectionMain.GetDashboardConnection();
                string sql = @"
        DELETE FROM DC_LINEDETAIL
        WHERE LINEID = @LineID";

                using (SqlConnection conn =
                    new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd =
                        new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue(
                            "@LineID",
                            lineID);

                        cmd.ExecuteNonQuery();
                    }
                }
                ActivityLogger.Log(
                    connectionString,
                    "DELETE LINE",
                    $"Line {lineNum}",
                    $"DELETE IP={IpAddress}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                       $"Delete Line failed:\n{ex.Message}",
                       "Setup Line Error",
                       MessageBoxButtons.OK,
                       MessageBoxIcon.Error);
            }
        }
        //    private void LineCard_EditClicked(object sender,
        //EventArgs e)
        //    {
        //        if (sender is UC_LineCard card)
        //        {
        //            // Remember which database row
        //            // we are editing
        //            editingLineID =
        //                card.LineID;

        //            // Send values back to input controls
        //            txtLine.Text =
        //                card.LineNumber.ToString();

        //            txtIpAddress.Text =
        //                card.IPAddress;

        //            chkStatus.Checked =
        //                card.IsActive;

        //            txtMaster.Text = card.Master;
        //            if (txtMaster.)
        //            chkMaster.Checked 
        //        }
        //    }

        private void LineCard_EditClicked(
       object sender,
       EventArgs e)
        {
            UC_LineCard card =
                (UC_LineCard)sender;

            // =========================================
            // Existing database ID
            // =========================================
            editingLineID =
                card.LineID;


            // =========================================
            // Line number
            // =========================================
            txtLine.Text =
                card.LineNumber.ToString();


            // =========================================
            // IP Address
            // =========================================
            txtIpAddress.Text =
                card.IPAddress;


            // =========================================
            // Status
            // =========================================
            chkStatus.Checked =
                card.IsActive;


            // =========================================
            // Check whether this line is its own master
            // =========================================
            if (card.MasterLineID == card.LineID)
            {
                // This line is a MASTER.
                //
                // Example:
                // LINEID = 8
                // MASTER_LINE_ID = 8

                chkMaster.Checked = true;

                txtMaster.Text =
                    card.LineNumber.ToString();

                txtMaster.Enabled = false;
            }
            else
            {
                // This line belongs to another master.
                //
                // Need to convert:
                //
                // MASTER_LINE_ID
                //       ↓
                // LINE_NUM

                chkMaster.Checked = false;

                int masterLineNumber =
                    GetLineNumberByLineID(
                        card.MasterLineID);

                txtMaster.Text =
                    masterLineNumber.ToString();

                txtMaster.Enabled = true;
            }
        }

        private int GetLineNumberByLineID(
    int lineID)
        {
            string sql = @"
        SELECT LINE_NUM
        FROM DC_LINEDETAIL
        WHERE LINEID = @LINEID";

            using (SqlConnection conn =
                new SqlConnection(connectionString))
            using (SqlCommand cmd =
                new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue(
                    "@LINEID",
                    lineID);

                conn.Open();

                object result =
                    cmd.ExecuteScalar();

                if (result == null ||
                    result == DBNull.Value)
                {
                    return 0;
                }

                return Convert.ToInt32(result);
            }
        }
        private int GetCurrentLineCount()
        {
            string connectionString =
                ConnectionMain.GetDashboardConnection();

            string sql = @"
        SELECT COUNT(*)
        FROM DC_LINEDETAIL
        WHERE STATUS_ACTIVE = 1";

            using (SqlConnection conn =
                new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlCommand cmd =
                    new SqlCommand(sql, conn))
                {
                    return Convert.ToInt32(
                        cmd.ExecuteScalar());
                }
            }
        }


        //private async void btnSave_Click(
        //    object sender,
        //    EventArgs e)
        //{
        //    if (!int.TryParse(
        //        txtLine.Text,
        //        out int lineNumber))
        //    {
        //        MessageBox.Show(
        //            "Please enter a valid line number.");

        //        return;
        //    }

        //    string ipAddress =
        //        txtIpAddress.Text.Trim();

        //    if (string.IsNullOrWhiteSpace(
        //        ipAddress))
        //    {
        //        MessageBox.Show(
        //            "Please enter IP address.");

        //        return;
        //    }

        //    if (editingLineID == 0)
        //    {
        //        int currentLineCount = GetCurrentLineCount();
        //        int maxLines = AppSession.MaxLines;

        //        if (currentLineCount >= maxLines)
        //        {
        //            MessageBox.Show(
        //        "You have reached the maximum number of lines " +
        //        "allowed by your license.\n\n" +
        //        "Current lines: " +
        //        currentLineCount +
        //        "\nMaximum allowed: " +
        //        maxLines,
        //        "License Limit",
        //        MessageBoxButtons.OK,
        //        MessageBoxIcon.Warning);

        //            return;
        //        }
        //        // ADD NEW LINE
        //        InsertLine(
        //        lineNumber,
        //        ipAddress,
        //        chkStatus.Checked);
        //    }
        //    else
        //    {
        //        // UPDATE EXISTING LINE
        //        UpdateLine(
        //            editingLineID,
        //            lineNumber,
        //            ipAddress,
        //            chkStatus.Checked);
        //    }

        //    // Reset edit mode
        //    editingLineID = 0;

        //    // Clear input fields
        //    txtLine.Clear();
        //    txtIpAddress.Clear();
        //    chkStatus.Checked = false;

        //    // Reload cards
        //    LoadLineCards();

        //    await CheckAllLineStatusAsync();
        //}

        private async void btnSave_Click(
    object sender,
    EventArgs e)
        {
            // =========================================================
            // 1. Validate Line Number
            // =========================================================
            if (!int.TryParse(
                txtLine.Text.Trim(),
                out int lineNumber))
            {
                MessageBox.Show(
                    "Please enter a valid line number.",
                    "Invalid Line",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }


            // =========================================================
            // 2. Validate IP Address
            // =========================================================
            string ipAddress =
                txtIpAddress.Text.Trim();

            if (string.IsNullOrWhiteSpace(ipAddress))
            {
                MessageBox.Show(
                    "Please enter IP address.",
                    "Invalid IP Address",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }


            // =========================================================
            // 3. Validate Master Line
            // =========================================================

            int? masterLineID = null;

            if (!chkMaster.Checked)
            {
                // -----------------------------------------------------
                // Master checkbox is NOT checked.
                // Therefore txtMaster must contain an existing LINE_NUM.
                // -----------------------------------------------------

                if (!int.TryParse(
                    txtMaster.Text.Trim(),
                    out int masterLineNumber))
                {
                    MessageBox.Show(
                        "Please enter a valid master line number.",
                        "Invalid Master Line",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                // Find the LINEID using LINE_NUM
                masterLineID =
                    GetLineIDByLineNumber(masterLineNumber);

                if (!masterLineID.HasValue)
                {
                    MessageBox.Show(
                        $"Master line number {masterLineNumber} does not exist.",
                        "Master Line Not Found",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }
            }


            // =========================================================
            // 4. ADD NEW LINE
            // =========================================================
            if (editingLineID == 0)
            {
                // -----------------------------------------------------
                // Check license line limit
                // -----------------------------------------------------
                int currentLineCount =
                    GetCurrentLineCount();

                int maxLines =
                    AppSession.MaxLines;

                if (currentLineCount >= maxLines)
                {
                    MessageBox.Show(
                        "You have reached the maximum number of lines " +
                        "allowed by your license.\n\n" +
                        "Current lines: " +
                        currentLineCount +
                        "\nMaximum allowed: " +
                        maxLines,
                        "License Limit",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }


                // -----------------------------------------------------
                // Check duplicate LINE_NUM
                // -----------------------------------------------------
                if (LineNumberExists(lineNumber))
                {
                    MessageBox.Show(
                        $"Line number {lineNumber} already exists.",
                        "Duplicate Line",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }


                // -----------------------------------------------------
                // Insert new line
                //
                // If chkMaster = TRUE:
                //     InsertLine will make MASTER_LINE_ID
                //     equal to the newly generated LINEID.
                //
                // If chkMaster = FALSE:
                //     Use the existing master's LINEID.
                // -----------------------------------------------------
                InsertLine(
                    lineNumber,
                    ipAddress,
                    masterLineID,
                    chkMaster.Checked,
                    chkStatus.Checked);
            }
            else
            {
                // =====================================================
                // UPDATE EXISTING LINE
                // =====================================================

                // -----------------------------------------------------
                // Check duplicate LINE_NUM
                //
                // Exclude the line currently being edited.
                // -----------------------------------------------------
                if (LineNumberExists(
                    lineNumber,
                    editingLineID))
                {
                    MessageBox.Show(
                        $"Line number {lineNumber} already exists.",
                        "Duplicate Line",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }


                // -----------------------------------------------------
                // Update existing line
                // -----------------------------------------------------
                UpdateLine(
                    editingLineID,
                    lineNumber,
                    ipAddress,
                    masterLineID,
                    chkMaster.Checked,
                    chkStatus.Checked);
            }

            // =========================================================
            // 5. Reset edit mode
            // =========================================================
            editingLineID = 0;

            txtLine.Clear();
            txtIpAddress.Clear();
            txtMaster.Clear();

            chkMaster.Checked = false;
            chkStatus.Checked = false;


            // =========================================================
            // 6. Reload cards
            // =========================================================
            LoadLineCards();

            await CheckAllLineStatusAsync();
        }

        private DataTable GetRequiredProcedureScripts()
        {
            string connectionString =
                ConnectionMain.GetDashboardConnection();

            string sql = @"
                  SELECT
            P.PROCEDURE_NAME,
            P.SCRIPT_VERSION,
            S.PROCEDURE_SCRIPT
        FROM dbo.DC_SP_RECORD P
        INNER JOIN dbo.DC_SP_UPDATEVERSION S
            ON S.PROCEDURE_NAME = P.PROCEDURE_NAME
           AND S.SCRIPT_VERSION = P.SCRIPT_VERSION
        WHERE P.STATUS_ACTIVE = 1
        ORDER BY P.ID;";

            using (SqlConnection conn =
                new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();

                conn.Open();
                adapter.Fill(dt);

                return dt;
            }
        }


        private void InstallProceduresOnLine(
    string lineConnectionString,
    string scriptFolder)
        {
            DataTable scripts = GetRequiredProcedureScripts();

            using (SqlConnection lineConn =
                new SqlConnection(lineConnectionString))
            {
                lineConn.Open();

                // Install table types before procedures that depend on them.
                //EnsureTableTypes(lineConn, scriptFolder);

                foreach (DataRow row in scripts.Rows)
                {
                    string procedureName =
                        row["PROCEDURE_NAME"].ToString();

                    string scriptFile =
                        row["PROCEDURE_SCRIPT"].ToString();

                    string scriptPath = Path.Combine(
                        scriptFolder,
                        scriptFile);

                    if (!File.Exists(scriptPath))
                    {
                        throw new FileNotFoundException(
                            "Procedure script not found for " +
                            procedureName + ":\r\n" + scriptPath);
                    }

                    string script = File.ReadAllText(scriptPath);

                    if (string.IsNullOrWhiteSpace(script))
                    {
                        throw new InvalidOperationException(
                            "Procedure script is empty: " + scriptFile);
                    }

                    ExecuteSqlScript(lineConn, script);
                }
            }
        }

       // private void EnsureTableTypes(SqlConnection conn, string folderPath)
       private async Task CreateTableTypesAsync(SqlConnection conn, string folderPath)
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

                await ExecuteSqlScriptAsync(conn, typeScript);
            }
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

        private int? GetLineIDByLineNumber(
    int lineNumber)
        {
            string sql = @"
        SELECT LINEID
        FROM DC_LINEDETAIL
        WHERE LINE_NUM = @LINE_NUM";

            using (SqlConnection conn =
    new SqlConnection(
        ConnectionMain.GetDashboardConnection()))
            using (SqlCommand cmd =
                new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue(
                    "@LINE_NUM",
                    lineNumber);

                conn.Open();

                object result =
                    cmd.ExecuteScalar();

                if (result == null ||
                    result == DBNull.Value)
                {
                    return null;
                }

                return Convert.ToInt32(result);
            }
        }

        private bool LineNumberExists(
    int lineNumber,
    int? excludeLineID = null)
        {
            string sql;

            if (excludeLineID.HasValue)
            {
                sql = @"
            SELECT COUNT(1)
            FROM DC_LINEDETAIL
            WHERE LINE_NUM = @LINE_NUM
              AND LINEID <> @LINEID";
            }
            else
            {
                sql = @"
            SELECT COUNT(1)
            FROM DC_LINEDETAIL
            WHERE LINE_NUM = @LINE_NUM";
            }

            using (SqlConnection conn =
    new SqlConnection(
        ConnectionMain.GetDashboardConnection()))
            using (SqlCommand cmd =
                new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue(
                    "@LINE_NUM",
                    lineNumber);

                if (excludeLineID.HasValue)
                {
                    cmd.Parameters.AddWithValue(
                        "@LINEID",
                        excludeLineID.Value);
                }

                conn.Open();

                int count =
                    Convert.ToInt32(
                        cmd.ExecuteScalar());

                return count > 0;
            }
        }

        private void InsertLine(
          int lineNumber,
          string ipAddress,
          int? masterLineID,
          bool isMaster,
          bool status)
        {
            using (SqlConnection conn =
    new SqlConnection(
        ConnectionMain.GetDashboardConnection()))
            {
                conn.Open();

                using (SqlTransaction transaction =
                    conn.BeginTransaction())
                {
                    try
                    {
                        // =============================================
                        // INSERT LINE
                        // =============================================
                        string insertSql = @"
                    INSERT INTO DC_LINEDETAIL
                    (
                        LINE_NUM,
                        IP_ADDRESS,
                        MASTER_LINE_ID,
                        STATUS_ACTIVE,
                        DATEMODIFIED
                    )
                    OUTPUT INSERTED.LINEID
                    VALUES
                    (
                        @LINE_NUM,
                        @IP_ADDRESS,
                        NULL,
                        @STATUS_ACTIVE,
                        GETDATE()
                    );";

                        int newLineID;

                        using (SqlCommand cmd =
                            new SqlCommand(
                                insertSql,
                                conn,
                                transaction))
                        {
                            cmd.Parameters.AddWithValue(
                                "@LINE_NUM",
                                lineNumber);

                            cmd.Parameters.AddWithValue(
                                "@IP_ADDRESS",
                                ipAddress);

                            cmd.Parameters.AddWithValue(
                                "@STATUS_ACTIVE",
                                status);

                            newLineID =
                                Convert.ToInt32(
                                    cmd.ExecuteScalar());
                        }


                        // =============================================
                        // MASTER LINE
                        // =============================================
                        if (isMaster)
                        {
                            // This line is an independent master.
                            //
                            // Example:
                            // LINEID = 8
                            // LINE_NUM = 4
                            // MASTER_LINE_ID = 8

                            string masterSql = @"
                        UPDATE DC_LINEDETAIL
                        SET
                            MASTER_LINE_ID = @MASTER_LINE_ID,
                            DATEMODIFIED = GETDATE()
                        WHERE LINEID = @LINEID";

                            using (SqlCommand cmd =
                                new SqlCommand(
                                    masterSql,
                                    conn,
                                    transaction))
                            {
                                cmd.Parameters.AddWithValue(
                                    "@MASTER_LINE_ID",
                                    newLineID);

                                cmd.Parameters.AddWithValue(
                                    "@LINEID",
                                    newLineID);

                                cmd.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            // =========================================
                            // CHILD LINE
                            // =========================================

                            if (!masterLineID.HasValue)
                            {
                                throw new Exception(
                                    "Master line ID is required.");
                            }

                            string masterSql = @"
                        UPDATE DC_LINEDETAIL
                        SET
                            MASTER_LINE_ID = @MASTER_LINE_ID,
                            DATEMODIFIED = GETDATE()
                        WHERE LINEID = @LINEID";

                            using (SqlCommand cmd =
                                new SqlCommand(
                                    masterSql,
                                    conn,
                                    transaction))
                            {
                                cmd.Parameters.AddWithValue(
                                    "@MASTER_LINE_ID",
                                    masterLineID.Value);

                                cmd.Parameters.AddWithValue(
                                    "@LINEID",
                                    newLineID);

                                cmd.ExecuteNonQuery();
                            }
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
        }

        private void UpdateLine(
    int lineID,
    int lineNumber,
    string ipAddress,
    int? masterLineID,
    bool isMaster,
    bool status)
        {
            int finalMasterLineID;

            if (isMaster)
            {
                // The existing line becomes its own master.
                //
                // Example:
                // LINEID = 8
                // MASTER_LINE_ID = 8

                finalMasterLineID =
                    lineID;
            }
            else
            {
                if (!masterLineID.HasValue)
                {
                    throw new Exception(
                        "Master line ID is missing.");
                }

                finalMasterLineID =
                    masterLineID.Value;
            }


            string sql = @"
        UPDATE DC_LINEDETAIL
        SET
            LINE_NUM = @LINE_NUM,
            IP_ADDRESS = @IP_ADDRESS,
            MASTER_LINE_ID = @MASTER_LINE_ID,
            STATUS_ACTIVE = @STATUS_ACTIVE,
            DATEMODIFIED = GETDATE()
        WHERE LINEID = @LINEID";
            connectionString = ConnectionMain.GetDashboardConnection();
            using (SqlConnection conn =
    new SqlConnection(
        ConnectionMain.GetDashboardConnection()))
            using (SqlCommand cmd =
                new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue(
                    "@LINEID",
                    lineID);

                cmd.Parameters.AddWithValue(
                    "@LINE_NUM",
                    lineNumber);

                cmd.Parameters.AddWithValue(
                    "@IP_ADDRESS",
                    ipAddress);

                cmd.Parameters.AddWithValue(
                    "@MASTER_LINE_ID",
                    finalMasterLineID);

                cmd.Parameters.AddWithValue(
                    "@STATUS_ACTIVE",
                    status);

                conn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        //  private void InsertLine(
        //int lineNumber,
        //string ipAddress,
        //bool isActive)
        //  {
        //      try
        //      {
        //          //string connectionString =
        //          //    ConnectionMain.GetSqlConnectionString(
        //          //        config.DashboardDatabase.Server,
        //          //        config.DashboardDatabase.Instance,
        //          //        config.DashboardDatabase.Username,
        //          //        PasswordEncryption.Decrypt(config.DashboardDatabase.Password),
        //          //        config.DashboardDatabase.Database);
        //          connectionString = ConnectionMain.GetDashboardConnection();

        //          string sql = @"
        //          INSERT INTO DC_LINEDETAIL
        //          (
        //              LINE_NUM,
        //              IP_ADDRESS,
        //              STATUS_ACTIVE
        //          )
        //          VALUES
        //          (
        //              @LineNumber,
        //              @IPAddress,
        //              @StatusActive
        //          )";

        //          using (SqlConnection conn =
        //              new SqlConnection(connectionString))
        //          {
        //              conn.Open();

        //              using (SqlCommand cmd =
        //                  new SqlCommand(sql, conn))
        //              {
        //                  cmd.Parameters.AddWithValue(
        //                      "@LineNumber",
        //                      lineNumber);

        //                  cmd.Parameters.AddWithValue(
        //                      "@IPAddress",
        //                      ipAddress);

        //                  cmd.Parameters.AddWithValue(
        //                      "@StatusActive",
        //                      isActive);

        //                  cmd.ExecuteNonQuery();
        //              }
        //          }
        //          ActivityLogger.Log(
        //              connectionString,
        //              "INSERT NEW LINE",
        //              $"Line {lineNumber}",
        //              $"INSERT IP={ipAddress}, Active={isActive}");
        //      }
        //      catch (Exception ex)
        //      {
        //          MessageBox.Show(
        //                 $"Insert Line failed:\n{ex.Message}",
        //                 "Setup Line Error",
        //                 MessageBoxButtons.OK,
        //                 MessageBoxIcon.Error);
        //      }

        //  }

        //private void UpdateLine(
        //int lineID,
        //int lineNumber,
        //string ipAddress,
        //bool isActive)
        //{
        //    try
        //    {
        //        //string password =
        //        //    PasswordEncryption.Decrypt(
        //        //        config.DashboardDatabase.Password);

        //        //string connectionString =
        //        //    ConnectionMain.GetSqlConnectionString(
        //        //        config.DashboardDatabase.Server,
        //        //        config.DashboardDatabase.Instance,
        //        //        config.DashboardDatabase.Username,
        //        //        password,
        //        //        config.DashboardDatabase.Database);
        //        connectionString = ConnectionMain.GetDashboardConnection();

        //        string sql = @"
        //    UPDATE DC_LINEDETAIL
        //    SET
        //        LINE_NUM = @LineNumber,
        //        IP_ADDRESS = @IPAddress,
        //        STATUS_ACTIVE = @StatusActive,
        //        DATEMODIFIED = SYSDATETIME()
        //    WHERE LINEID = @LineID";

        //        using (SqlConnection conn =
        //            new SqlConnection(connectionString))
        //        {
        //            conn.Open();

        //            using (SqlCommand cmd =
        //                new SqlCommand(sql, conn))
        //            {
        //                cmd.Parameters.AddWithValue(
        //                    "@LineID",
        //                    lineID);

        //                cmd.Parameters.AddWithValue(
        //                    "@LineNumber",
        //                    lineNumber);

        //                cmd.Parameters.AddWithValue(
        //                    "@IPAddress",
        //                    ipAddress);

        //                cmd.Parameters.AddWithValue(
        //                    "@StatusActive",
        //                    isActive);

        //                cmd.ExecuteNonQuery();
        //            }
        //        }
        //        ActivityLogger.Log(
        //         connectionString,
        //         "UPDATE LINE",
        //         $"Line {lineNumber}",
        //         $"Updated IP={ipAddress}, Active={isActive}");
        //    }
        //    catch (Exception ex)
        //    {
        //        {
        //            //ActivityLogger.Log(connectionString, "LOGIN", "Error", "Login Failed");
        //            MessageBox.Show(
        //                $"Update failed:\n{ex.Message}",
        //                "Update Error",
        //                MessageBoxButtons.OK,
        //                MessageBoxIcon.Error);
        //        }
        //    }
        //}

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            editingLineID = 0;
            txtIpAddress.Clear();
            txtLine.Clear();
        }

        private void UC_MRT_DashCombine_Setup_Resize(object sender, EventArgs e)
        {
            UpdateSetupLayout();
            // LayoutSummaryCards();
            //if(flowLineList != null)
            //    Res
            PositionProcedureButton();
        }
        private void PositionProcedureButton()
        {
            if (pnlHeader == null ||
                glassProcedure == null)
                return;

            glassProcedure.Location =
                new Point(
                    pnlHeader.Width -
                    glassProcedure.Width -
                    25,
                    25);
        }

        private void flowLineList_Paint(object sender, PaintEventArgs e)
        {

        }

        private void chkMaster_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMaster.Checked)
            {
                txtMaster.Visible = true;
                txtMaster.Enabled = false;
                txtMaster.Text = txtLine.Text;
                //txtMaster.Focus();
            }
            else
            {
                txtMaster.Visible = true;
                txtMaster.Enabled = true;
                if (txtMaster.Text == txtLine.Text)
                {
                    txtMaster.Clear();
                }

            }
        }

        private void txtLine_Load(object sender, EventArgs e)
        {

        }

        private void txtLine_TextChanged(object sender, EventArgs e)
        {
            if (chkMaster.Checked)
            {
                txtMaster.Text = txtLine.Text;
                txtMaster.Enabled = false;
            }
            else
            {
                txtMaster.Clear();
                txtMaster.Enabled = true;
            }
        }
    }
}