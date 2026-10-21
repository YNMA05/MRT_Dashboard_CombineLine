using SmartMRT_DashCombine_Setup.Controls;
using SmartMRT_DashCombine_Setup.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace SmartMRT_DashCombine_Setup
{
    public partial class MRT_DashCombine_Form : Form
    {
        private Timer editSessionTimer;

        private bool isEditMode = false;

        private string currentLicenseType = string.Empty;
        private string editingUsername = "";
        private string editingRole = "";
        private int editingUserID = 0;

        private DateTime sessionExpireTime;

        private const int EDIT_SESSION_MINUTES = 5;
        private UserControl currentPage;
        private GlassButton glassLock;
        private GlassButton glassUser;

        public MRT_DashCombine_Form()
        {
            InitializeComponent();

            InitializeUI();

            InitializeEditSession();
            ApplyModernStyle();
           // btnAddUser.Visible = false;
            this.Resize += MRT_DashCombine_Form_Resize;

            MRT_DashCombine_Form_Resize(this, EventArgs.Empty);
            SelectMenu(btnHome);
            LoadPage(new UC_MRT_DashCombine_Home());

            //this.Shown += MRT_DashCombine_Form_Shown;
        }
        private void MRT_DashCombine_Form_Shown(object sender, EventArgs e)
        {
            RefreshApplicationState();
        }
        private void InitializeUI()
        {
            this.Text = "SmartMRT Dashboard Manager";
            this.StartPosition = FormStartPosition.CenterScreen;

            this.BackColor = Color.FromArgb(8, 21, 47);

            this.MinimumSize = new Size(1400, 850);

        }

        private void ApplyModernStyle()
        {
            // =========================
            // GLASS UNLOCK / LOCK BUTTON
            // =========================

            glassLock = new GlassButton
            {
                Name = "glassLock",
                Text = "🔓 Unlock",

                Size = btnUnlock.Size,
                Location = btnUnlock.Location,

                BorderColor =
                    Color.FromArgb(56, 211, 159),

                GlassColor =
                    Color.FromArgb(25, 55, 65),

                TextColor =
                    Color.White,

                Font = new Font(
                    "Segoe UI",
                    7F,
                    FontStyle.Bold)
            };

            // =========================
            // GLASS ADD USER BUTTON
            // =========================

            glassUser = new GlassButton
            {
                Name = "glassUser",
                Text = "👨 Add User",

                Size = btnAddUser.Size,
                Location = btnAddUser.Location,
                Visible = btnAddUser.Visible,

                BorderColor =
                    Color.FromArgb(120, 220, 255),

                GlassColor =
                    Color.FromArgb(20, 45, 65),

                TextColor =
                    Color.White,

                Font = new Font(
                    "Segoe UI",
                    7F,
                    FontStyle.Bold)
            };


            // =========================
            // EVENTS
            // =========================

            glassLock.Click += button2_Click;
            glassUser.Click += btnAddUser_Click;


            // =========================
            // ADD TO SAME PARENT
            // =========================

            btnUnlock.Parent.Controls.Add(glassLock);
            btnAddUser.Parent.Controls.Add(glassUser);


            // Put glass buttons in front
            glassLock.BringToFront();
            glassUser.BringToFront();


            // Hide original Designer buttons
            btnUnlock.Visible = false;
            btnAddUser.Visible = false;
        }

        private void MRT_DashCombine_Form_Resize(object sender, EventArgs e)
        {
            int menuWidth = 183;
            int headerHeight = panelHeader.Height;

            // Menu
            panelMenu.Location = new Point(0, headerHeight);
            panelMenu.Size = new Size(
                menuWidth,
                ClientSize.Height - headerHeight
            );

            // Header
            panelHeader.Location = new Point(0, 0);
            panelHeader.Size = new Size(
                ClientSize.Width,
                headerHeight
            );

            // Content
            panelContent.Location = new Point(
                menuWidth,
                headerHeight
            );

            panelContent.Size = new Size(
                ClientSize.Width - menuWidth,
                ClientSize.Height - headerHeight
            );

            panel1.Location = new Point(
                panelHeader.Width - panel1.Width - 20,
                10);
        }

        //private bool ValidateLicense()
        //{
        //    return PermissionManager.ValidateLicense();
        //}

        private void InitializeEditSession()
        {
            editSessionTimer = new Timer();

            // Update every 1 second
            editSessionTimer.Interval = 1000;

            editSessionTimer.Tick +=
                EditSessionTimer_Tick;

            // Start in View Mode
            EnterViewMode();
        }

        private void EditSessionTimer_Tick(
    object sender,
    EventArgs e)
        {
            if (!isEditMode)
                return;

            TimeSpan remaining =
                sessionExpireTime -
                DateTime.Now;

            if (remaining.TotalSeconds <= 0)
            {
                LockEditing();

                MessageBox.Show(
                    "Edit session has expired.",
                    "Session Locked",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            // Update your label
            lblExp.Text =
                $"Session Expires In: " +
                $"{remaining.Minutes:00}:" +
                $"{remaining.Seconds:00}";
        }

        private void EnterViewMode()
        {
            isEditMode = false;

            editingUsername = "";

            lblMode.Text = "View Mode";

            lblUser.Text =
                "Editing As: -";

            lblExp.Text =
                "Session Expires In: -";

            // Update glass button
            if (glassLock != null)
            {
                glassLock.Text = "🔓 Unlock";

                glassLock.BorderColor =
                    Color.FromArgb(56, 211, 159);
            }

            editSessionTimer.Stop();

            ApplyCurrentPagePermissions();
        }


        private void LoadPage(UserControl page)
        {
            panelContent.Controls.Clear();

            currentPage = page;

            page.Dock = DockStyle.Fill;

            panelContent.Controls.Add(page);

            ApplyCurrentPagePermissions();
        }

        private void ResetMenu()
        {
            Color normalBack = Color.FromArgb(16, 36, 71);
            Color normalFore = Color.White;

            foreach (Control c in panelMenu.Controls)
            {
                if (c is Button btn)
                {
                    btn.BackColor = normalBack;
                    btn.ForeColor = normalFore;
                }
            }
        }

        private void SelectMenu(Button btn)
        {
            ResetMenu();

            btn.BackColor = Color.FromArgb(0, 212, 255);   // Cyan
            btn.ForeColor = Color.Black;
        }


        private void panelContent_Paint(object sender, PaintEventArgs e)
        {

        }

        //private void btnAbout_Click(object sender, EventArgs e)
        //{
        //    SelectMenu(btnAbout);
        //    LoadPage(new UC_MRT_DashCombine_About());
        //}

        private void panelMenu_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            SelectMenu(btnHome);
            LoadPage(new UC_MRT_DashCombine_Home());
        }

        private void lblExp_Click(object sender, EventArgs e)
        {

        }

        private void lblHeader_Click(object sender, EventArgs e)
        {
            SelectMenu(btnHome);
            LoadPage(new UC_MRT_DashCombine_Home());
        }

        private void lblSQL_Click(object sender, EventArgs e)
        {

        }

        private void btnDatabase_Click(object sender, EventArgs e)
        {
            SelectMenu(btnDatabase);
            LoadPage(new UC_MRT_DashCombine_DB());
        }

        private void btnLicense_Click(object sender, EventArgs e)
        {
            SelectMenu(btnLicense);
            LoadPage(new UC_MRT_DashCombine_License());
        }

        private void lblConnected_Click(object sender, EventArgs e)
        {

        }

        private void btnLogs_Click(object sender, EventArgs e)
        {
            SelectMenu(btnLogs);
            LoadPage(new UC_MRT_DashCombine_Logs());
        }

        private void lblSignalIR_Click(object sender, EventArgs e)
        {

        }

        private void btnSetup_Click(object sender, EventArgs e)
        {
            SelectMenu(btnLineSetup);
            LoadPage(new UC_MRT_DashCombine_Setup());
        }

        private void lblLastBroadcast_Click(object sender, EventArgs e)
        {

        }

        private void lblSystemOverview_Click(object sender, EventArgs e)
        {

        }

        private void panelHeader_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnView_Click(object sender, EventArgs e)
        {
            SelectMenu(btnView);
            LoadPage(new UC_MRT_DashCombine_View());
        }

        private void btnService_Click(object sender, EventArgs e)
        {
            SelectMenu(btnService);
            LoadPage(new UC_MRT_DashCombine_Service());
        }

        private void lblSystemService_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(
     object sender,
     EventArgs e)
        {
            if (isEditMode)
            {
                LockEditing();

                return;
            }

            //using (FrmUnlockLogin popup =
            //    new FrmUnlockLogin())
            //{
            //    if (popup.ShowDialog() ==
            //        DialogResult.OK)
            //    {
            //        StartEditSession(popup.Username);
            //    }
            //}

            using (FrmUnlockLogin popup =
    new FrmUnlockLogin())
            {
                if (popup.ShowDialog() ==
                    DialogResult.OK)
                {
                    StartEditSession(
                        popup.UserID,
                        popup.Username,
                        popup.Role);
                }
            }


        }

        private void StartEditSession(
            int userID,
            string username,
            string role)
        {
            isEditMode = true;

            editingUserID = userID;
            editingUsername = username;

            sessionExpireTime =
                DateTime.Now.AddMinutes(
                    EDIT_SESSION_MINUTES);

            lblMode.Text = "Edit Mode";

            lblUser.Text =
                $"Editing As: {username}";

            glassLock.Text = "🔒 Lock";

            glassLock.BorderColor =
                Color.FromArgb(255, 181, 71);

            editSessionTimer.Start();

            ApplyCurrentPagePermissions();
            ApplyMainFormPermissions();
        }
        private void LockEditing()
        {
    //        MessageBox.Show(
    //$"Edit Mode: {isEditMode}\n" +
    //$"CREATE_USER: {PermissionManager.HasPermission("CREATE_USER")}\n" +
    //$"Glass User Visible: {glassUser?.Visible}");
            isEditMode = false;

            editingUsername = "";

            editSessionTimer.Stop();

            lblMode.Text =
                "View Mode";

            lblUser.Text =
                "Editing As: -";

            lblExp.Text =
                "Session Expires In: -";


            // Update visible glass button
            if (glassLock != null)
            {
                glassLock.Text = "🔓 Unlock";

                glassLock.BorderColor =
                    Color.FromArgb(56, 211, 159);
            }


            ApplyCurrentPagePermissions();
            ApplyMainFormPermissions();
        }

        private void ApplyMainFormPermissions()
        {
            bool canCreateUser =
                isEditMode &&
                PermissionManager.HasPermission("CREATE_USER");

            // Original Designer button
            if (btnAddUser != null)
            {
                btnAddUser.Visible = false;
            }

            // Modern glass button
            if (glassUser != null)
            {
                glassUser.Visible = canCreateUser;
                glassUser.Enabled = canCreateUser;
            }
        }

        private void ApplyCurrentPagePermissions()
        {
            if (currentPage is UC_MRT_DashCombine_DB dbPage)
            {
                bool canEdit =
                    isEditMode &&
                    PermissionManager.HasPermission("EDIT_DATABASE");

                dbPage.SetEditMode(canEdit);
            }
            //if (currentPage is UC_MRT_DashCombine_DB dbPage)
            //{
            //    bool hasPermission =
            //        PermissionManager.HasPermission("EDIT_DATABASE");

            //    MessageBox.Show(
            //        "Main Form Edit Mode = " + isEditMode +
            //        "\nEDIT_DATABASE Permission = " + hasPermission);

            //    bool canEdit =
            //        isEditMode &&
            //        hasPermission;

            //    dbPage.SetEditMode(canEdit);
            //}
            else if (currentPage is UC_MRT_DashCombine_Setup setupPage)
            {
                bool canEdit =
                    isEditMode &&
                    PermissionManager.HasPermission("EDIT_LINE_SETUP");

                setupPage.SetEditMode(canEdit);

                bool canUpdateSP =
                    isEditMode &&
                    PermissionManager.HasPermission("UPDATE_SP");
                setupPage.SetUpdateSP(canUpdateSP);
            }
            else if (currentPage is UC_MRT_DashCombine_Service servicePage)
            {
                bool canInstall =
                    isEditMode &&
                    PermissionManager.HasPermission("INSTALL_SERVICE");
                servicePage.setCanInstall(canInstall);

                bool canStart =
                    isEditMode &&
                    PermissionManager.HasPermission("START_SERVICE");
                servicePage.setCanStart(canStart);
            }
            else if (currentPage is UC_MRT_DashCombine_View viewPage)
            {
                bool canUploadImage =
                    isEditMode &&
                    PermissionManager.HasPermission("UPLOAD_MODULE_IMAGE");

                bool canCreateModule =
                    isEditMode &&
                    PermissionManager.HasPermission("CREATE_MODULE");

                viewPage.SetUploadImagePermission(
                    canUploadImage);

                viewPage.SetCreateModulePermission(
                    canCreateModule);

                bool canEditViewLine =
                    isEditMode &&
                    PermissionManager.HasPermission("ASSIGN_MODULE_LINE");
                viewPage.SetSaveModulePermission(canEditViewLine);
            }
            else if (currentPage is UC_MRT_DashCombine_License licensePage)
            {
                bool canViewLicense =
                    isEditMode &&
                    PermissionManager.HasPermission("VIEW_LICENSE_KEY");
                licensePage.SetViewLicensePermission(canViewLicense);

                bool canAddLicense =
                    isEditMode &&
                    PermissionManager.HasPermission("ADD_LICENSE_KEY");
                licensePage.SetAddLicensePermission(canAddLicense);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            SelectMenu(btnHome);
            LoadPage(new UC_MRT_DashCombine_Home());
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            if (!PermissionManager.HasPermission("CREATE_USER"))
            {
                MessageBox.Show(
                    "You do not have permission to create users.",
                    "Access Denied",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }
            using (FrmAddUser popup = new FrmAddUser())
            {
                if (popup.ShowDialog() == DialogResult.OK) ;
            }
        }

        //    private void MRT_DashCombine_Form_Load(object sender, EventArgs e)
        //    {
        //        ApplyMainFormPermissions();
        //        btnAddUser.Visible =
        //PermissionManager.HasPermission("CREATE_USER");
        //    }

        //   private void MRT_DashCombine_Form_Load(
        //object sender,
        //EventArgs e)
        //   {
        //       LicensePayload payload;

        //       if (!PermissionManager.TryGetValidLicense(
        //               out payload))
        //       {
        //           MessageBox.Show(
        //               "Invalid license. Please check your license key.",
        //               "License Error",
        //               MessageBoxButtons.OK,
        //               MessageBoxIcon.Error);

        //           this.Close();
        //           return;
        //       }

        //       // ============================================
        //       // Store verified license information
        //       // ============================================

        //       AppSession.LicenseType = payload.LicenseType;
        //       AppSession.LicenseGroup = payload.LicenseGroup;
        //       AppSession.Dashboard = payload.Dashboard;
        //       AppSession.MaxLines = payload.MaxLines;
        //       AppSession.MaxPages = payload.MaxPages;
        //       AppSession.Company = payload.Company;
        //       AppSession.ExpiryUtc = payload.ExpiryUtc;
        //       lblLicense.Text =payload.LicenseType;
        //       // ============================================
        //       // Check configuration information
        //       // ============================================
        //       if (!DashboardConfigManager.Exists())
        //       {
        //           MessageBox.Show(
        //               "Dashboard configuration was not found.\n\n" +
        //               "Please configure the database connection before using the dashboard.",
        //               "Configuration Required",
        //               MessageBoxButtons.OK,
        //               MessageBoxIcon.Warning);

        //           UC_MRT_DashCombine_DB dbPage =
        //               new UC_MRT_DashCombine_DB();

        //           LoadPage(dbPage);

        //           // First-time configuration
        //           isEditMode = true;

        //           dbPage.SetEditMode(true);

        //           return;
        //       }

        //       // ============================================
        //       // Permission check
        //       // ============================================

        //       bool canCreateUser =
        //           PermissionManager.HasPermission("CREATE_USER");

        //       btnAddUser.Visible = canCreateUser;
        //   }


        private void SetStartupMode(string startup)
        {
            // Disable all navigation buttons first.
            btnHome.Enabled = false;
            btnDatabase.Enabled = false;
            btnLicense.Enabled = false;
            btnLineSetup.Enabled = false;
            btnService.Enabled = false;
            btnLogs.Enabled = false;
            btnView.Enabled = false; //btnAddUser.Enabled = false;

            switch (startup)
            {
                case "DATABASE":
                    // Allow database configuration and license activation.
                    btnDatabase.Enabled = true;
                    SelectMenu(btnDatabase);
                    //btnLicense.Enabled = true;
                    break;

                case "LICENSE":
                    // Allow only license activation.
                    btnLicense.Enabled = true;
                    SelectMenu(btnLicense);
                    btnDatabase.Enabled = true;
                    break;

                case "NORMAL":
                    // Allow normal navigation.
                    btnHome.Enabled = true;
                    btnDatabase.Enabled = true;
                    btnLicense.Enabled = true;
                    btnLineSetup.Enabled = true;
                    btnService.Enabled = true;
                    btnLogs.Enabled = true;
                    btnView.Enabled=true;
                    
                    break;
            }
        }

private void RefreshApplicationState()
        {
            // Disable navigation while checking the current state.
            SetStartupMode("CHECKING");

            // STEP 1: Check database configuration.
            if (!DashboardConfigManager.Exists())
            {
                SetStartupMode("DATABASE");

                MessageBox.Show(
                    "Configuration does not exist.\n\n" +
                    "Please configure the database connection and IP address first.",
                    "Initial Setup Required",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                UC_MRT_DashCombine_DB dbPage = CreateDatabasePage();

                isEditMode = true;

                LoadPage(dbPage);

                // Enable database configuration after loading the page.
                dbPage.SetEditMode(true);

                SelectMenu(btnDatabase);

                return;
            }

            // STEP 2: Check license.
            LicensePayload payload;

            if (!PermissionManager.TryGetValidLicense(out payload))
            {
                ClearLicenseSession();

                SetStartupMode("LICENSE");

                LoadPage(CreateLicensePage());
                SelectMenu(btnLicense);

                return;
            }

            // STEP 3: Store verified license information.
            AppSession.LicenseType = payload.LicenseType;
            AppSession.LicenseGroup = payload.LicenseGroup;
            AppSession.Dashboard = payload.Dashboard;
            AppSession.MaxLines = payload.MaxLines;
            AppSession.MaxPages = payload.MaxPages;
            AppSession.Company = payload.Company;
            AppSession.ExpiryUtc = payload.ExpiryUtc;

            lblLicense.Text = payload.LicenseType;

            // STEP 4: Enable navigation.
            SetStartupMode("NORMAL");

            // STEP 5: Apply permissions.
            btnAddUser.Visible =
                PermissionManager.HasPermission("CREATE_USER");

            // STEP 6: Show Home after successful activation.
            btnHome.PerformClick();
        }

        private void ClearLicenseSession()
        {
            AppSession.LicenseType = null;
            AppSession.LicenseGroup = null;
            AppSession.Dashboard = null;
            AppSession.MaxLines = 0;
            AppSession.MaxPages = null;
            AppSession.Company = null;
            AppSession.ExpiryUtc = null;

            lblLicense.Text = "Not Activated";
        }

        private UC_MRT_DashCombine_License CreateLicensePage()
        {
            var page = new UC_MRT_DashCombine_License();

            page.LicenseInstalled += (sender, e) =>
            {
                RefreshApplicationState();
            };

            return page;
        }

        private UC_MRT_DashCombine_DB CreateDatabasePage()
        {
            var page = new UC_MRT_DashCombine_DB();
            page.ConfigurationSaved += (sender, e) =>
            {
                RefreshApplicationState();
            };
            return page;
        }

        private void MRT_DashCombine_Form_Load(object sender, EventArgs e)
        {
            RefreshApplicationState();
            //// 1. Check database configuration first.
            //if (!DashboardConfigManager.Exists())
            //{
            //    SetStartupMode("DATABASE");

            //    MessageBox.Show(
            //        "Dashboard database configuration was not found.\n\n" +
            //        "Please configure and test the database connection first.",
            //        "Database Configuration Required",
            //        MessageBoxButtons.OK,
            //        MessageBoxIcon.Warning);

            //    UC_MRT_DashCombine_DB dbPage =
            //        new UC_MRT_DashCombine_DB();

            //    LoadPage(dbPage);

            //    isEditMode = true;
            //    dbPage.SetEditMode(true);

            //    return;
            //}

            //// 2. Check whether a valid license exists.
            //LicensePayload payload;

            //if (!PermissionManager.TryGetValidLicense(out payload))
            //{
            //    AppSession.LicenseType = null;
            //    AppSession.LicenseGroup = null;
            //    AppSession.Dashboard = null;
            //    AppSession.MaxLines = 0;
            //    AppSession.MaxPages = null;
            //    AppSession.Company = null;
            //    AppSession.ExpiryUtc = null;

            //    lblLicense.Text = "Not Activated";

            //    SetStartupMode("LICENSE");

            //    LoadPage(new UC_MRT_DashCombine_License());
            //    SelectMenu(btnLicense);

            //    return;
            //}

            //// 3. Store verified license information.
            //AppSession.LicenseType = payload.LicenseType;
            //AppSession.LicenseGroup = payload.LicenseGroup;
            //AppSession.Dashboard = payload.Dashboard;
            //AppSession.MaxLines = payload.MaxLines;
            //AppSession.MaxPages = payload.MaxPages;
            //AppSession.Company = payload.Company;
            //AppSession.ExpiryUtc = payload.ExpiryUtc;

            //lblLicense.Text = payload.LicenseType;

            //// 4. Enable normal navigation.
            //SetStartupMode("NORMAL");

            //// 5. Apply user permissions.
            //bool canCreateUser =
            //    PermissionManager.HasPermission("CREATE_USER");

            //btnAddUser.Visible = canCreateUser;
        
        }


        private void SetLicenseMode(bool hasValidLicense)
        {
            SetMenuButtonsEnabled(panelMenu, hasValidLicense);

            // Always allow the License page.
            btnLicense.Enabled = true;
        }

        private void SetMenuButtonsEnabled(
            Control parent,
            bool enabled)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is Button button)
                {
                    // Keep the License menu available.
                    if (button != btnLicense)
                    {
                        button.Enabled = enabled;
                    }
                }

                // Also handle buttons inside nested panels.
                if (control.HasChildren)
                {
                    SetMenuButtonsEnabled(control, enabled);
                }
            }
        }

        private bool RequirePermission(string permissionCode)
        {
            // Already has permission
            if (PermissionManager.HasPermission(permissionCode))
                return true;

            // Ask user to login
            using (FrmUnlockLogin loginForm = new FrmUnlockLogin())
            {
                if (loginForm.ShowDialog() != DialogResult.OK)
                    return false;
            }

            // Login succeeded - check permission again
            return PermissionManager.HasPermission(permissionCode);
        }
    }
}
