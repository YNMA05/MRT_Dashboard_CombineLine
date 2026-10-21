using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace SmartMRT_DashCombine_Setup
{
    public partial class UC_MRT_DashCombine_Logs : UserControl
    {
        private DashboardConfig config;
        private string connectionString = string.Empty;

        private Panel pnlHeader;
       // private Label lblTitle;
        //private Label lblSubtitle;

        private Panel pnlSummary;
        private Panel cardToday;
        private Panel cardLogins;
        private Panel cardChanges;
        private Panel cardUsers;

        private Label lblTodayCount;
        private Label lblLoginCount;
        private Label lblChangeCount;
        private Label lblUserCount;

        private TextBox txtSearch;
        private ComboBox ddlAction;
        private Button btnRefresh;

        private FlowLayoutPanel flowLogs;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SendMessage(IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

        private const int EM_SETCUEBANNER = 0x1501;

        public UC_MRT_DashCombine_Logs()
        {
            InitializeComponent();

            this.BackColor = Color.FromArgb(8, 21, 47);
            this.Dock = DockStyle.Fill;

            Load += UC_MRT_DashCombine_Logs_Load;
            Resize += UC_MRT_DashCombine_Logs_Resize;
        }

        private void UC_MRT_DashCombine_Logs_Load(object sender, EventArgs e)
        {
            BuildUI();
            LoadConnection();
            LoadLogs();
        }

        private void LoadConnection()
        {
            try
            {
                config = DashboardConfigManager.Load();

                if (config == null)
                {
                    MessageBox.Show(
                        "DashboardConfig.json was not found.",
                        "Logs",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    return;
                }
                //string connectionString = ConnectionMain.GetDashboardConnection();

                string password =
                    PasswordEncryption.Decrypt(
                        config.DashboardDatabase.Password);

                connectionString =
                    ConnectionMain.GetSqlConnectionString(
                        config.DashboardDatabase.Server,
                        config.DashboardDatabase.Instance,
                        config.DashboardDatabase.Username,
                        password,
                        config.DashboardDatabase.Database);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Failed to load database configuration.\n\n" +
                    ex.Message,
                    "Logs",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void BuildUI()
        {
            Controls.Clear();

            // =========================================================
            // HEADER
            // =========================================================

            pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 75,
                BackColor = Color.Transparent
            };

            lblTitle = new Label
            {
                Text = "Activity Log",
                Font = new Font(
                    "Segoe UI",
                    22,
                    FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(25, 12)
            };

            lblSubtitle = new Label
            {
                Text = "Monitor system activity and administrator actions",
                Font = new Font(
                    "Segoe UI",
                    9.5f),
                ForeColor = Color.FromArgb(150, 180, 210),
                AutoSize = true,
                Location = new Point(27, 48)
            };

            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Controls.Add(lblSubtitle);

            btnRefresh = new Button
            {
                Text = "↻  Refresh",
                Width = 105,
                Height = 36,
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(
                    Width - 130,
                    20),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(16, 36, 71),
                ForeColor = Color.FromArgb(0, 212, 255),
                Font = new Font(
                    "Segoe UI",
                    9,
                    FontStyle.Bold),
                Cursor = Cursors.Hand
            };

            btnRefresh.FlatAppearance.BorderColor =
                Color.FromArgb(40, 75, 122);

            btnRefresh.Click += (s, e) =>
            {
                LoadLogs();
            };

            pnlHeader.Controls.Add(btnRefresh);

            // =========================================================
            // SUMMARY
            // =========================================================

            pnlSummary = new Panel
            {
                Dock = DockStyle.Top,
                Height = 105,
                Padding = new Padding(25, 5, 25, 10),
                BackColor = Color.Transparent
            };

            cardToday = CreateSummaryCard(
                "TODAY",
                "0",
                out lblTodayCount);

            cardLogins = CreateSummaryCard(
                "LOGINS",
                "0",
                out lblLoginCount);

            cardChanges = CreateSummaryCard(
                "CHANGES",
                "0",
                out lblChangeCount);

            cardUsers = CreateSummaryCard(
                "USERS",
                "0",
                out lblUserCount);

            pnlSummary.Controls.Add(cardToday);
            pnlSummary.Controls.Add(cardLogins);
            pnlSummary.Controls.Add(cardChanges);
            pnlSummary.Controls.Add(cardUsers);

            // =========================================================
            // FILTER PANEL
            // =========================================================

            Panel pnlFilter = new Panel
            {
                Dock = DockStyle.Top,
                Height = 55,
                Padding = new Padding(25, 5, 25, 5),
                BackColor = Color.Transparent
            };

            txtSearch = new TextBox
            {
                Width = 300,
                Height = 32,
                Font = new Font("Segoe UI", 10),
                BackColor = Color.FromArgb(16, 36, 71),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                //PlaceholderText = "Search activity..."
            };

            this.Controls.Add(txtSearch);
            SendMessage(txtSearch.Handle, EM_SETCUEBANNER, 0, "Search activity...");

            txtSearch.TextChanged += (s, e) =>
            {
                LoadLogs();
            };

            ddlAction = new ComboBox
            {
                Width = 180,
                Height = 32,
                Font = new Font("Segoe UI", 9),
                BackColor = Color.FromArgb(16, 36, 71),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            ddlAction.Items.Add("All Activities");
            ddlAction.Items.Add("LOGIN");
            ddlAction.Items.Add("LOGOUT");
            ddlAction.Items.Add("LOGIN_FAILED");
            ddlAction.Items.Add("CREATE_USER");
            ddlAction.Items.Add("EDIT_USER");
            ddlAction.Items.Add("DISABLE_USER");
            ddlAction.Items.Add("EDIT_DATABASE");
            ddlAction.Items.Add("CREATE_MODULE");
            ddlAction.Items.Add("EDIT_MODULE");
            ddlAction.Items.Add("DELETE_MODULE");
            ddlAction.Items.Add("UPLOAD_MODULE_IMAGE");
            ddlAction.Items.Add("ASSIGN_MODULE_LINE");
            ddlAction.Items.Add("EDIT_LINE_SETUP");
            ddlAction.Items.Add("EDIT_LICENSE");

            ddlAction.SelectedIndex = 0;

            ddlAction.SelectedIndexChanged += (s, e) =>
            {
                LoadLogs();
            };

            txtSearch.Location = new Point(25, 8);
            ddlAction.Location = new Point(340, 8);

            pnlFilter.Controls.Add(txtSearch);
            pnlFilter.Controls.Add(ddlAction);

            // =========================================================
            // LOG LIST
            // =========================================================

            flowLogs = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                Padding = new Padding(25, 5, 25, 25),
                BackColor = Color.Transparent
            };

            Controls.Add(flowLogs);
            Controls.Add(pnlFilter);
            Controls.Add(pnlSummary);
            Controls.Add(pnlHeader);

            LayoutSummaryCards();
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

            int availableWidth =
                pnlSummary.ClientSize.Width - 50;

            int spacing = 12;

            int cardWidth =
                (availableWidth - spacing * 3) / 4;

            if (cardWidth < 100)
                cardWidth = 100;

            Panel[] cards =
            {
                cardToday,
                cardLogins,
                cardChanges,
                cardUsers
            };

            int x = 25;

            foreach (Panel card in cards)
            {
                card.Width = cardWidth;
                card.Location = new Point(x, 5);

                x += cardWidth + spacing;
            }
        }

        private void UC_MRT_DashCombine_Logs_Resize(
            object sender,
            EventArgs e)
        {
            if (btnRefresh != null)
            {
                btnRefresh.Location =
                    new Point(
                        ClientSize.Width - 130,


                        20);
            }

            LayoutSummaryCards();

            if (flowLogs != null)
                ResizeLogCards();
        }

        // =============================================================
        // LOAD LOGS
        // =============================================================

        private void LoadLogs()
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                return;

            try
            {
                string actionFilter =
                    ddlAction?.SelectedItem?.ToString();

                string search =
                    txtSearch?.Text.Trim() ?? "";

                string sql = @"
                    SELECT TOP 100
                        L.ID,
                        L.USERID,
                        U.USERNAME,
                        L.ACTION,
                        L.TARGET,
                        L.REMARK,
                        L.DATEMODIFIED
                    FROM DC_LOG L
                    LEFT JOIN DC_USER U
                        ON L.USERID = U.USERID
                    WHERE
                        (
                            @Search = ''
                            OR U.USERNAME LIKE '%' + @Search + '%'
                            OR L.ACTION LIKE '%' + @Search + '%'
                            OR L.TARGET LIKE '%' + @Search + '%'
                            OR L.REMARK LIKE '%' + @Search + '%'
                        )
                        AND
                        (
                            @Action = 'All Activities'
                            OR L.ACTION = @Action
                        )
                    ORDER BY L.DATEMODIFIED DESC;
                ";

                using (SqlConnection conn =
                    new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd =
                        new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.Add(
                            "@Search",
                            SqlDbType.NVarChar,
                            200).Value = search;

                        cmd.Parameters.Add(
                            "@Action",
                            SqlDbType.NVarChar,
                            100).Value =
                                actionFilter ??
                                "All Activities";

                        using (SqlDataReader reader =
                            cmd.ExecuteReader())
                        {
                            flowLogs.SuspendLayout();
                            flowLogs.Controls.Clear();

                            int count = 0;

                            while (reader.Read())
                            {
                                Panel card =
                                    CreateLogCard(reader);

                                flowLogs.Controls.Add(card);

                                count++;
                            }

                            flowLogs.ResumeLayout();

                            UpdateSummary();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Failed to load activity logs.\n\n" +
                    ex.Message,
                    "Activity Log",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =============================================================
        // CREATE ACTIVITY CARD
        // =============================================================

        private Panel CreateLogCard(SqlDataReader reader)
        {
            string action =
                reader["ACTION"]?.ToString() ?? "";

            string username =
                reader["USERNAME"]?.ToString() ?? "Unknown";

            string target =
                reader["TARGET"]?.ToString() ?? "";

            string remark =
                reader["REMARK"]?.ToString() ?? "";

            DateTime date =
                Convert.ToDateTime(
                    reader["DATEMODIFIED"]);

            Panel card = new Panel
            {
                Height = 100,
                Width = Math.Max(
                    600,
                    flowLogs.ClientSize.Width - 50),
                BackColor = Color.FromArgb(16, 36, 71),
                Margin = new Padding(0, 0, 0, 10),
                Padding = new Padding(15)
            };

            // Icon
            Panel icon = new Panel
            {
                Width = 42,
                Height = 42,
                Location = new Point(15, 25),
                BackColor = GetActionColor(action)
            };

            Label iconText = new Label
            {
                Text = GetActionIcon(action),
                Font = new Font(
                    "Segoe UI Symbol",
                    14,
                    FontStyle.Bold),
                ForeColor = Color.White,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };

            icon.Controls.Add(iconText);

            // Action
            Label lblAction = new Label
            {
                Text = FormatAction(action),
                Font = new Font(
                    "Segoe UI",
                    10.5f,
                    FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(75, 15)
            };

            // User
            Label lblUser = new Label
            {
                Text = username,
                Font = new Font(
                    "Segoe UI",
                    9,
                    FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 212, 255),
                AutoSize = true,
                Location = new Point(75, 40)
            };

            // Target
            string targetText =
                string.IsNullOrWhiteSpace(target)
                    ? ""
                    : " → " + target;

            Label lblTarget = new Label
            {
                Text = targetText,
                Font = new Font(
                    "Segoe UI",
                    9),
                ForeColor = Color.FromArgb(170, 195, 220),
                AutoSize = true,
                Location = new Point(
                    75 + lblUser.PreferredWidth + 5,
                    40)
            };

            // Remark
            Label lblRemark = new Label
            {
                Text = remark,
                Font = new Font(
                    "Segoe UI",
                    8.5f),
                ForeColor = Color.FromArgb(145, 170, 195),
                AutoSize = true,
                MaximumSize = new Size(
                    card.Width - 260,
                    35),
                Location = new Point(75, 65)
            };

            // Date
            Label lblDate = new Label
            {
                Text = FormatDate(date),
                Font = new Font(
                    "Segoe UI",
                    8.5f),
                ForeColor = Color.FromArgb(130, 160, 190),
                AutoSize = false,
                Width = 160,
                Height = 25,
                TextAlign = ContentAlignment.MiddleRight,
                Anchor =
                    AnchorStyles.Top |
                    AnchorStyles.Right,
                Location = new Point(
                    card.Width - 180,
                    15)
            };

            card.Controls.Add(icon);
            card.Controls.Add(lblAction);
            card.Controls.Add(lblUser);
            card.Controls.Add(lblTarget);
            card.Controls.Add(lblRemark);
            card.Controls.Add(lblDate);

            return card;
        }

        private void ResizeLogCards()
        {
            if (flowLogs == null)
                return;

            int width =
                Math.Max(
                    600,
                    flowLogs.ClientSize.Width - 50);

            foreach (Control control in flowLogs.Controls)
            {
                control.Width = width;

                foreach (Control child in control.Controls)
                {
                    if (child is Label label &&
                        label.Anchor.HasFlag(
                            AnchorStyles.Right))
                    {
                        label.Location =
                            new Point(
                                control.Width - 180,
                                label.Location.Y);
                    }
                }
            }
        }

        // =============================================================
        // SUMMARY
        // =============================================================

        private void UpdateSummary()
        {
            try
            {
                using (SqlConnection conn =
                    new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = @"
                        SELECT

                        (
                            SELECT COUNT(*)
                            FROM DC_LOG
                            WHERE CAST(DATEMODIFIED AS DATE)
                                  = CAST(GETDATE() AS DATE)
                        ) AS TodayCount,

                        (
                            SELECT COUNT(*)
                            FROM DC_LOG
                            WHERE ACTION = 'LOGIN'
                              AND CAST(DATEMODIFIED AS DATE)
                                  = CAST(GETDATE() AS DATE)
                        ) AS LoginCount,

                        (
                            SELECT COUNT(*)
                            FROM DC_LOG
                            WHERE ACTION IN
                            (
                                'CREATE_USER',
                                'EDIT_USER',
                                'DISABLE_USER',
                                'EDIT_DATABASE',
                                'CREATE_MODULE',
                                'EDIT_MODULE',
                                'DELETE_MODULE',
                                'UPLOAD_MODULE_IMAGE',
                                'ASSIGN_MODULE_LINE'
                            )
                            AND CAST(DATEMODIFIED AS DATE)
                                = CAST(GETDATE() AS DATE)
                        ) AS ChangeCount,

                        (
                            SELECT COUNT(*)
                            FROM DC_USER
                            WHERE STATUS_ACTIVE = 1
                        ) AS UserCount;
                    ";

                    using (SqlCommand cmd =
                        new SqlCommand(sql, conn))
                    {
                        using (SqlDataReader reader =
                            cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                lblTodayCount.Text =
                                    reader["TodayCount"].ToString();

                                lblLoginCount.Text =
                                    reader["LoginCount"].ToString();

                                lblChangeCount.Text =
                                    reader["ChangeCount"].ToString();

                                lblUserCount.Text =
                                    reader["UserCount"].ToString();
                            }
                        }
                    }
                }
            }
            catch
            {
                // Do not prevent the log page from loading
            }
        }

        // =============================================================
        // HELPERS
        // =============================================================

        private string FormatAction(string action)
        {
            if (string.IsNullOrWhiteSpace(action))
                return "Activity";

            return action
                .Replace("_", " ");
        }

        private string FormatDate(DateTime date)
        {
            if (date.Date == DateTime.Today)
                return date.ToString("hh:mm tt");

            if (date.Date == DateTime.Today.AddDays(-1))
                return "Yesterday " +
                       date.ToString("hh:mm tt");

            return date.ToString("dd MMM yyyy  hh:mm tt");
        }

        private Color GetActionColor(string action)
        {
            switch (action)
            {
                case "LOGIN":
                    return Color.FromArgb(0, 160, 190);

                case "LOGOUT":
                    return Color.FromArgb(100, 120, 140);

                case "LOGIN_FAILED":
                    return Color.FromArgb(210, 70, 70);

                case "CREATE_USER":
                case "CREATE_MODULE":
                    return Color.FromArgb(40, 170, 110);

                case "DELETE_MODULE":
                case "DISABLE_USER":
                    return Color.FromArgb(210, 80, 80);

                case "EDIT_USER":
                //case "EDIT_DATABASE":
                case "EDIT_MODULE":
                case "ASSIGN_MODULE_LINE":
                case "EDIT_LINE_SETUP":
                    return Color.FromArgb(60, 130, 210);

                case "EDIT DATABASE":
                    return Color.FromArgb(192, 64, 0);

                case "EDIT LICENSE":
                    return Color.FromArgb(222, 49, 99);

                default:
                    return Color.FromArgb(120, 90, 190);
            }
        }

        private string GetActionIcon(string action)
        {
            switch (action)
            {
                case "LOGIN":
                    return "→";

                case "LOGOUT":
                    return "←";

                case "LOGIN_FAILED":
                    return "!";

                case "CREATE_USER":
                    return "+";

                case "DELETE_MODULE":
                case "DISABLE_USER":
                    return "×";

                case "EDIT_USER": 
                    return "👨‍🦳";

                case "EDIT_MODULE":
                case "ASSIGN_MODULE_LINE":
                    return "✎";

                case "EDIT_DATABASE":
                    return "⚙";

                case "EDIT_LICENSE":
                    return "🔐";

                default:
                    return "•";
            }
        }
    }
}