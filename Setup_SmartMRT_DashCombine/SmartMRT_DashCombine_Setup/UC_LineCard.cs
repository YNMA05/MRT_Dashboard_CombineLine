using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartMRT_DashCombine_Setup
{
    public partial class UC_LineCard : UserControl
    {
        public event EventHandler EditClicked;
        public event EventHandler DeleteClicked;
        private int _lineNumber;
        private string _ipAddress;
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

        public bool IsActive
        {
            get => chkActive.Checked;
            set => chkActive.Checked = value;
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
        }
        public void SetEditMode(bool editMode)
        {
            IsEditMode = editMode;

            chkActive.Enabled = editMode;
            btnEdit.Enabled = editMode;
            btnDelete.Enabled = editMode;
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
            }
        }

        public async Task CheckLineStatusAsync()
        {
            // Dashboard setup says this line is not enabled
            if (!IsActive)
            {
                lblDatabaseStatus.Text =
                    "⚪ Not Checked";

                lblMotorStatus.Text =
                    "⚪ Not Checked";

                return;
            }

            // Check database
            lblDatabaseStatus.Text =
                "🟡 Connecting...";

            bool connected =
                await TestConnectionAsync();

            if (!connected)
            {
                lblDatabaseStatus.Text =
                    "🔴 Offline";

                lblMotorStatus.Text =
                    "⚪ Unknown";

                return;
            }

            // Database connected
            lblDatabaseStatus.Text =
                "🟢 Connected";

            // Check motor
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
        }

        private async Task<bool> TestConnectionAsync()
        {
            try
            {
                string connectionString =
                    ConnectionMain.GetLineLocalDBConnection(
                        IPAddress);

                return await Task.Run(() =>
                {
                    using (SqlConnection conn =
                        new SqlConnection(connectionString))
                    {
                        conn.Open();
                        return true;
                    }
                });
            }
            catch
            {
                return false;
            }
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

                    string query = @"
                SELECT MotorStatus
                FROM PUSHERCOUNTER
                WHERE Prodline = @LineID";

                    using (SqlCommand cmd =
                        new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue(
                            "@LineID",
                            LineNumber);

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
    }
}