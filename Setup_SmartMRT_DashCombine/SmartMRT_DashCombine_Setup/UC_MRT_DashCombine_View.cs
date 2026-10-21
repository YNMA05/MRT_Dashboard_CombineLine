using SmartMRT_DashCombine_Setup.Controls;
using SmartMRT_DashCombine_Setup.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartMRT_DashCombine_Setup
{
    public partial class UC_MRT_DashCombine_View : UserControl
    {
        private string connectionString = string.Empty;
        private DashboardConfig config;
        private bool isEditMode = false;
        private int selectedModuleID = -1;
        private string selectedModuleCode = string.Empty;
        private Timer lineStatusTimer;
        private GlassButton btnAddModule;
        private GlassButton btnSaveModule;
        // public event EventHandler ImageChanged;

        // public EventHandler ModuleCard_Clicked { get; private set; }

        public UC_MRT_DashCombine_View()
        {
            InitializeComponent();
            ApplyModernStyle();
            flowModuleList.FlowDirection =
                    FlowDirection.LeftToRight;

            flowModuleList.WrapContents = true;

            flowModuleList.AutoScroll = true;

            flowModuleList.Padding =
                new Padding(5);
            flowLineList.AutoScroll = true;
            flowLineList.WrapContents = true;
            flowLineList.FlowDirection =
                FlowDirection.LeftToRight;
            flowLineList.Dock = DockStyle.Fill;
            this.Load += UC_MRT_DashCombine_View_Load;
            

        }



        private void ApplyModernStyle()
        {
            pnlModuleGlass.GlassOpacity = 55;
            pnlModuleGlass.GlassColor = Color.FromArgb(10, 20, 32);
            pnlModuleGlass.BorderColor = Color.FromArgb(70, 180, 230, 255);
            pnlModuleGlass.CornerRadius = 20;

            // =========================
            // ADD MODULE BUTTON
            // =========================
            btnAddModule = new GlassButton
            {
                Text = "+ Add Module",
                Size = new Size(125, 38),

                BorderColor = Color.FromArgb(0, 212, 255),
                GlassColor = Color.FromArgb(20, 50, 70),
                TextColor = Color.White,

                Font = new Font(
                    "Segoe UI",
                    10F,
                    FontStyle.Bold),

                Cursor = Cursors.Hand,

                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };

            // =========================
            // SAVE BUTTON
            // =========================
            btnSaveModule = new GlassButton
            {
                Text = "🔐 Save",
                Size = new Size(125, 38),

                BorderColor = Color.FromArgb(0, 212, 255),
                GlassColor = Color.FromArgb(20, 50, 70),
                TextColor = Color.White,

                Font = new Font(
                    "Segoe UI",
                    10F,
                    FontStyle.Bold),

                Cursor = Cursors.Hand,

                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };

            // =========================
            // POSITION BUTTONS
            // =========================

            int rightMargin = 20;
            int buttonGap = 10;

            int top =
                (pnlHeader.Height - 38) / 2;

            // SAVE = rightmost
            btnSaveModule.Location = new Point(
                pnlHeader.ClientSize.Width
                    - btnSaveModule.Width
                    - rightMargin,
                top);

            // ADD MODULE = immediately before SAVE
            btnAddModule.Location = new Point(
                btnSaveModule.Left
                    - btnAddModule.Width
                    - buttonGap,
                top);

            // =========================
            // ADD CONTROLS
            // =========================

            pnlHeader.Controls.Add(btnAddModule);
            pnlHeader.Controls.Add(btnSaveModule);

            // Make sure they stay visible
            btnAddModule.BringToFront();
            btnSaveModule.BringToFront();

            // =========================
            // CLICK EVENTS
            // =========================

            btnAddModule.Click += BtnAddModule_Click;
            btnSaveModule.Click += BtnSaveModule_Click;
        }

        public void SetEditMode(bool editMode)
        {
            isEditMode = editMode;
           // ModuleCard_Clicked.ed
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private async void UC_MRT_DashCombine_View_Load(
    object sender,
    EventArgs e)
        {
            await UpdateLineStatusAsync();

            lineStatusTimer = new Timer();
            lineStatusTimer.Interval = 10000; // 10 seconds

            lineStatusTimer.Tick += async (s, args) =>
            {
                await UpdateLineStatusAsync();
            };

            lineStatusTimer.Start();
            LoadModules();
        }

        private void LoadModules()
        {
           // ResizeModuleCards();
            //SetupModuleLayout();
            try
            {                
               // pnlModuleGlass.Controls.Clear();
                flowModuleList.Controls.Clear();

               // pnlModuleGlass.Dock = DockStyle.Fill;

                connectionString = ConnectionMain.GetDashboardConnection();

                string sql = @"
            SELECT
                MODULEID,
                MODULENAME,
                MODULECODE,
                IMAGEPATH
            FROM DC_DASHMODULE
            WHERE STATUS_ACTIVE = 1
            ORDER BY MODULEID";

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
                            int count = 0;
                            flowModuleList.Padding = new Padding(10, 10, 10, 20);
                            while (reader.Read())
                            {
                                UC_Line_ModuleCard card =
                                    new UC_Line_ModuleCard();

                                card.ModuleID =
                                    Convert.ToInt32(
                                        reader["MODULEID"]);

                                card.ModuleName =
                                    reader["MODULENAME"].ToString();

                                card.ModuleCode =
                                    reader["MODULECODE"].ToString();

                                if (reader["IMAGEPATH"] != DBNull.Value)
                                {
                                    card.ImagePath =
                                        reader["IMAGEPATH"].ToString();
                                }


                                card.ModuleClicked +=
                                    ModuleCard_Clicked;
                                card.ImageChanged += ModuleCard_ImageChanged;
                               // card.UploadImageRequested += ModuleCard_UploadImageRequested;
                                card.CanUploadImage = PermissionManager.HasPermission("UPLOAD_MODULE_IMAGE");
                                flowModuleList.Controls.Add(card);
                                count++;
                            }

                            //MessageBox.Show(
                            //    $"Modules loaded: {count}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "LoadModules failed:\n\n" +
                    ex.ToString());
            }
        }
        private void ModuleCard_Clicked(
            object sender,
            EventArgs e)
        {
            if (!(sender is UC_Line_ModuleCard selectedCard))
                return;

            foreach (Control control in flowModuleList.Controls)
            {
                if (control is UC_Line_ModuleCard card)
                {
                    card.IsSelected = false;
                }
            }

            selectedCard.IsSelected = true;

            selectedModuleID = selectedCard.ModuleID;
            selectedModuleCode = selectedCard.ModuleCode;

            //lblModule.Text =
            //    $"Selected Module: {selectedCard.ModuleName}";

            LoadLinesForModule(
                selectedCard.ModuleCode);
        }

        private void LoadLinesForModule(string moduleCode)
        {
            try
            {
                flowLineList.Controls.Clear();

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
            SELECT
                L.LINEID,
                L.LINE_NUM,
                L.IP_ADDRESS,
                CASE
                    WHEN LM.LINEID IS NOT NULL
                    THEN 1
                    ELSE 0
                END AS IS_SELECTED
            FROM DC_LINEDETAIL L

            LEFT JOIN DC_VIEWLINE LM
                ON L.LINEID = LM.LINEID
                AND LM.MODULECODE = @ModuleCode

            WHERE L.STATUS_ACTIVE = 1

            ORDER BY L.LINE_NUM ASC";

                using (SqlConnection conn =
                    new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd =
                        new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue(
                            "@ModuleCode",
                            moduleCode);

                        using (SqlDataReader reader =
                            cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                UC_Line_ViewCard card =
                                    new UC_Line_ViewCard();

                                card.LineID =
                                    Convert.ToInt32(
                                        reader["LINEID"]);

                                card.LineNumber =
                                    Convert.ToInt32(
                                        reader["LINE_NUM"]);

                                card.IPAddress =
                                    reader["IP_ADDRESS"]?.ToString();

                                card.IsSelected =
                                    Convert.ToInt32(
                                        reader["IS_SELECTED"]) == 1;

                                flowLineList.Controls.Add(card);
                            }

                            ResizeLineCards();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "LoadLinesForModule failed:\n\n" +
                    ex.Message);
            }
        }

        private void ModuleCard_ImageChanged(
    object sender,
    EventArgs e)
        {
            if (sender is UC_Line_ModuleCard card)
            {
                SaveModuleImagePath(
                    card.ModuleCode,
                    card.ImagePath);
            }
        }

        private void BtnAddModule_Click(object sender, EventArgs e)
        {
            using (FrmAddModule form = new FrmAddModule())
            {
                form.ShowDialog(this);
            }

            // Always refresh after FrmAddModule closes
            LoadModules();
        }

        private void SaveModuleImagePath(
    string moduleCode,
    string imagePath)
        {
            try
            {
                connectionString = ConnectionMain.GetDashboardConnection();

                string sql = @"
            UPDATE DC_DASHMODULE
            SET IMAGEPATH = @ImagePath
            WHERE MODULECODE = @ModuleCode";

                using (SqlConnection conn =
                    new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd =
                        new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue(
                            "@ImagePath",
                            imagePath);

                        cmd.Parameters.AddWithValue(
                            "@ModuleCode",
                            moduleCode);

                        cmd.ExecuteNonQuery();
                    }
                }
                ActivityLogger.Log(connectionString, "UPLOAD_MODULE_IMAGE", $"Module {moduleCode}",
           $"Image Update {imagePath}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Failed to save module image:\n{ex.Message}",
                    "Save Image",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void SaveModuleLines(int moduleID, string moduleCode)
        {
            string connectionString =
                ConnectionMain.GetDashboardConnection();

            using (SqlConnection conn =
                new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlTransaction transaction =
                    conn.BeginTransaction())
                {
                    try
                    {
                        // ==========================================
                        // 1. Remove existing line assignments
                        // ==========================================

                        string deleteSql = @"
                    DELETE FROM DC_VIEWLINE
                    WHERE MODULECODE = @ModuleCode";

                        using (SqlCommand cmd =
                            new SqlCommand(
                                deleteSql,
                                conn,
                                transaction))
                        {
                            cmd.Parameters.AddWithValue(
                                "@ModuleCode",
                                moduleCode);

                            cmd.ExecuteNonQuery();
                        }


                        // ==========================================
                        // 2. Insert selected lines
                        // ==========================================

                        foreach (Control control
                            in flowLineList.Controls)
                        {
                            if (control is UC_Line_ViewCard card)
                            {
                                if (!card.IsSelected)
                                    continue;

                                string insertSql = @"
                            INSERT INTO DC_VIEWLINE
                            (
                                LINEID,
                                MODULECODE
                            )
                            VALUES
                            (
                                @LineID,
                                @ModuleCode
                            )";

                                using (SqlCommand cmd =
                                    new SqlCommand(
                                        insertSql,
                                        conn,
                                        transaction))
                                {
                                    cmd.Parameters.AddWithValue(
                                        "@LineID",
                                        card.LineID);

                                    cmd.Parameters.AddWithValue(
                                        "@ModuleCode",
                                        moduleCode);

                                    cmd.ExecuteNonQuery();
                                }                          
                            }
                        }


                        // ==========================================
                        // 3. Commit
                        // ==========================================

                        transaction.Commit();

                        MessageBox.Show(
                            "Module configuration saved.",
                            "Success",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        ActivityLogger.Log(connectionString, "ASSIGN_MODULE_LINE", $"Module {moduleCode}",
                                   $"Assigned Line to Module {moduleCode}");
                       // LoadModules();

                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            transaction.Rollback();
                        }
                        catch
                        {
                            // Ignore rollback error
                        }

                        MessageBox.Show(
                            "Save failed:\n\n" + ex.Message,
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void BtnSaveModule_Click(
     object sender,
     EventArgs e)
        {
            if (selectedModuleID == -1)
            {
                MessageBox.Show(
                    "Please select a dashboard module.");

                return;
            }

            SaveModuleLines(
                selectedModuleID, selectedModuleCode);
        }

        private void cbLineNum_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbLineNum.SelectedValue == null)
                return;

            if (!int.TryParse(
                cbLineNum.SelectedValue.ToString(),
                out int lineID))
            {
                return;
            }

            // LoadModuleSettings(lineID);

        }

        private void flowModuleList_Paint(object sender, PaintEventArgs e)
        {

        }

        private void UC_MRT_DashCombine_View_Resize(
        object sender,
        EventArgs e)
        {
            int left = 45;
            int right = 45;

            int top = 95;
            int gap = 10;
            int bottomMargin = 20;

            int minModuleHeight = 350;
            int maxModuleHeight = 505;

            int lineHeight = 247;

            int labelLineHeight = 33;

            int panelWidth = Math.Max(0,this.ClientSize.Width - left - right);


            // =====================================================
            // LINE PANEL
            // =====================================================

            int lineTop =
                this.ClientSize.Height
                - lineHeight
                - bottomMargin
                - labelLineHeight;


            // =====================================================
            // MODULE PANEL
            // =====================================================

            int availableModuleHeight =
                lineTop
                - gap
                - top;

            int moduleHeight = Math.Min(
                 maxModuleHeight,
                 Math.Max(
                     minModuleHeight,
                     this.ClientSize.Height
                     - top
                     - gap
                     - labelLineHeight
                     - lineHeight
                     - bottomMargin));

            pnlModuleGlass.Location =
                new Point(left, top);

            pnlModuleGlass.Width =
                this.ClientSize.Width
                - left
                - right;

            pnlModuleGlass.Height =
                moduleHeight;

            // =========================================
            // LABEL PANEL
            // =========================================

            lblPanel.Location = new Point(
                left,
                pnlModuleGlass.Bottom + gap);

            lblPanel.Width = panelWidth;
            lblPanel.Height = labelLineHeight;

            // =====================================================
            // LINE PANEL
            // =====================================================

            lineTop =
                pnlModuleGlass.Bottom + gap + labelLineHeight;

            pnlLineGlass.Location =
                new Point(left, lineTop);

            pnlLineGlass.Width =
                this.ClientSize.Width
                - left
                - right;

            pnlLineGlass.Height =
                this.ClientSize.Height
                - lineTop
                - bottomMargin;


            // =====================================================
            // MODULE CARDS
            // =====================================================


            ResizeModuleCards();

            // =====================================================
            // LINE CARDS
            // =====================================================

            ResizeLineCards();
        }

        private void ResizeModuleCards()
        {
            if (flowModuleList.ClientSize.Width <= 0)
                return;

            int cardSpacing = 16;

            int minimumCardWidth = 180;
            int maximumCardWidth = 320;
            int minimumCardHeight = 203;
            int maximumCardHeight = 362;

            int availableWidth =
                flowModuleList.ClientSize.Width
                - flowModuleList.Padding.Horizontal;

            int cardCount =
                flowModuleList.Controls
                    .OfType<UC_Line_ModuleCard>()
                    .Count();

            if (cardCount == 0)
                return;


            // ============================================
            // Calculate how many cards can fit
            // ============================================

            int columns =
                Math.Max(
                    1,
                    (availableWidth + cardSpacing)
                    / (minimumCardWidth + cardSpacing));

            columns =
                Math.Min(columns, cardCount);


            // ============================================
            // Calculate responsive width
            // ============================================

            int cardWidth =
                (availableWidth
                - ((columns - 1) * cardSpacing))
                / columns;

            cardWidth = Math.Max(
                minimumCardWidth,
                Math.Min(cardWidth, maximumCardWidth));


            // ============================================
            // Keep image/card proportions
            // ============================================

            int cardHeight =
                (int)(cardWidth * 1.13);

            cardHeight = Math.Max(
                minimumCardHeight,
                Math.Min(cardHeight, maximumCardHeight));


            // ============================================
            // Apply
            // ============================================

            foreach (Control control in flowModuleList.Controls)
            {
                if (control is UC_Line_ModuleCard card)
                {
                    card.Width = cardWidth;
                    card.Height = cardHeight;

                    card.Margin =
                        new Padding(
                            cardSpacing / 2);
                }
            }

            flowModuleList.PerformLayout();
        }
        private void ResizeLineCards()
        {
            foreach (Control control in flowLineList.Controls)
            {
                if (control is UC_Line_ViewCard card)
                {
                    card.Width = 220;
                    card.Height = 80;
                    card.Margin = new Padding(8);
                }
            }
        }

        private async Task UpdateLineStatusAsync()
        {
            List<Task<bool>> tasks =
                new List<Task<bool>>();

            List<UC_Line_ViewCard> cards =
                new List<UC_Line_ViewCard>();

            foreach (Control control in flowLineList.Controls)
            {
                UC_Line_ViewCard card =
                    control as UC_Line_ViewCard;

                if (card == null)
                    continue;

                cards.Add(card);

                tasks.Add(
                    CheckLineOnlineAsync(
                        card.IPAddress));
            }

            bool[] results =
                await Task.WhenAll(tasks);

            for (int i = 0; i < cards.Count; i++)
            {
                cards[i].IsOnline =
                    results[i];
            }
        }

        private async Task<bool> CheckLineOnlineAsync(string ipAddress)
        {
            if (string.IsNullOrWhiteSpace(ipAddress))
                return false;

            try
            {
                using (Ping ping = new Ping())
                {
                    PingReply reply =
                        await ping.SendPingAsync(
                            ipAddress,
                            1000);

                    return reply.Status == IPStatus.Success;
                }
            }
            catch
            {
                return false;
            }
        }

        private void lblModule_Click(object sender, EventArgs e)
        {
           
        }

        public void SetUploadImagePermission(bool canUpload)
        {
            foreach (Control control in flowModuleList.Controls)
            {
                if (control is UC_Line_ModuleCard card)
                {
                    card.SetUploadImagePermission(canUpload);
                }
            }
        }

        public void SetCreateModulePermission(bool canCreate)
        {
            if (btnAddModule == null)
                return;

            btnAddModule.Visible = canCreate;
        }

        public void SetSaveModulePermission(bool canSave)
        {
            if (btnSaveModule == null)
                return;
            btnSaveModule.Visible = canSave;
        }

        private void flowLineList_Paint(object sender, PaintEventArgs e)
        {

        }

        //private void ShowModulePage()
        //{
        //    pnlLineGlass.Controls.Clear();
        //    pnlModuleGlass.Controls.Clear();

        //    UC_MRT_DashCombine_View page = new UC_MRT_DashCombine_View();

        //    pnlLineGlass.Controls.Add(page);
        //    pnlModuleGlass.Controls.Add(page);

        //    page.Dock = DockStyle.Fill;
        //}
    }
}
