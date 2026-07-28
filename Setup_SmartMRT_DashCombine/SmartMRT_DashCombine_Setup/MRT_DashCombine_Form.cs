using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartMRT_DashCombine_Setup
{
    public partial class MRT_DashCombine_Form : Form
    {
        private Timer editSessionTimer;

        private bool isEditMode = false;

        private string editingUsername = "";

        private DateTime sessionExpireTime;

        private const int EDIT_SESSION_MINUTES = 5;
        private UserControl currentPage;

        public MRT_DashCombine_Form()
        {
            InitializeComponent();

            InitializeUI();

            InitializeEditSession();

            LoadPage(new UC_MRT_DashCombine_Home());
        }

        private void InitializeUI()
        {
            this.Text = "SmartMRT Dashboard Manager";
            this.StartPosition = FormStartPosition.CenterScreen;

            this.BackColor = Color.FromArgb(8, 21, 47);

            this.MinimumSize = new Size(1400, 850);
        }

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

            btnUnlock.Text = "Unlock";

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

        private void btnAbout_Click(object sender, EventArgs e)
        {
            SelectMenu(btnAbout);
            LoadPage(new UC_MRT_DashCombine_About());
        }

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
            LoadPage(new UC_MRT_DashCombine_Home());
        }

        private void lblLicense_Click(object sender, EventArgs e)
        {

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

            using (FrmUnlockLogin popup =
                new FrmUnlockLogin())
            {
                if (popup.ShowDialog() ==
                    DialogResult.OK)
                {
                    StartEditSession(popup.Username);
                }
            }
        }

        private void StartEditSession(
    string username)
        {
            isEditMode = true;

            editingUsername = username;

            sessionExpireTime =
                DateTime.Now.AddMinutes(
                    EDIT_SESSION_MINUTES);

            lblMode.Text =
                "Edit Mode";

            lblUser.Text =
                $"Editing As: {username}";

            btnUnlock.Text =
                "Lock";

            editSessionTimer.Start();

            ApplyCurrentPagePermissions();
        }
        private void LockEditing()
        {
            isEditMode = false;

            editingUsername = "";

            editSessionTimer.Stop();

            lblMode.Text =
                "View Mode";

            lblUser.Text =
                "Editing As: -";

            lblExp.Text =
                "Session Expires In: -";

            btnUnlock.Text =
                "Unlock";

            ApplyCurrentPagePermissions();
        }

        private void ApplyCurrentPagePermissions()
        {
            if (currentPage == null)
                return;

            bool canEdit =
                isEditMode;

            if (currentPage is UC_MRT_DashCombine_DB dbPage)
            {
                dbPage.SetEditMode(canEdit);
            }
            else if (currentPage is UC_MRT_DashCombine_Setup setupPage)
            {
                setupPage.SetEditMode(canEdit);
            }
            //else if (currentPage is UC_MRT_DashCombine_Service servicePage)
            //{
            //    servicePage.SetEditMode(canEdit);
            //}
        }

    }
}
