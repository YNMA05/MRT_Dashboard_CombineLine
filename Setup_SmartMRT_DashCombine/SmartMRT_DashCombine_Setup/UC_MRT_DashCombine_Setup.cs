using System;
using System.Data.SqlClient;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartMRT_DashCombine_Setup
{
    public partial class UC_MRT_DashCombine_Setup : UserControl
    {
        private DashboardConfig config;
        private int editingLineID = 0;
        private bool isEditMode = false;
        public UC_MRT_DashCombine_Setup()
        {
            InitializeComponent();
            flowLineList.FlowDirection = FlowDirection.LeftToRight;

            flowLineList.WrapContents = true;

            flowLineList.AutoScroll = true;
            SetEditMode(false);
        }

        public void SetEditMode(bool editMode)
        {
            isEditMode = editMode;

            // Parent controls
            txtLine.ReadOnly = !editMode;
            txtIpAddress.ReadOnly = !editMode;
            chkStatus.Enabled = editMode;
            btnSave.Enabled = editMode;
            btnClear.Enabled = editMode;

            // Line cards
            foreach (Control control in flowLineList.Controls)
            {
                if (control is UC_LineCard lineCard)
                {
                    lineCard.SetEditMode(editMode);
                }
            }
        }

        private async void UC_MRT_DashCombine_Setup_Load(
            object sender,
            EventArgs e)
        {
            config = DashboardConfigManager.Load();

            if (config == null)
            {
                MessageBox.Show(
                    "DashboardConfig.json was not found.");

                return;
            }

            LoadLineCards();

            await CheckAllLineStatusAsync();
        }
        private async Task CheckAllLineStatusAsync()
        {
            foreach (Control control in flowLineList.Controls)
            {
                if (control is UC_LineCard card)
                {
                    await card.CheckLineStatusAsync();
                }
            }
        }
        private void LoadLineCards()
        {
            flowLineList.Controls.Clear();

            if (config == null)
            {
                MessageBox.Show(
                    "Configuration is not loaded.");

                return;
            }

            if (config.DashboardDatabase == null)
            {
                MessageBox.Show(
                    "DashboardDatabase configuration is missing.");

                return;
            }

            string password =
                PasswordEncryption.Decrypt(
                    config.DashboardDatabase.Password);

            string connectionString =
                ConnectionMain.GetSqlConnectionString(
                    config.DashboardDatabase.Server,
                    config.DashboardDatabase.Instance,
                    config.DashboardDatabase.Username,
                    password,
                    config.DashboardDatabase.Database);

            string sql = @"
                SELECT
                    LINEID,
                    LINE_NUM,
                    IP_ADDRESS,
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

                            // Active / inactive
                            card.IsActive =
                                Convert.ToBoolean(
                                    reader["STATUS_ACTIVE"]);

                            // IMPORTANT
                            // Connect Edit button
                            // to parent
                            card.EditClicked +=
                                LineCard_EditClicked;
                            card.DeleteClicked +=
                                LineCard_DeleteClicked;

                            flowLineList.Controls.Add(card);
                        }
                    }
                }
            }
        }

        private void LineCard_DeleteClicked(
    object sender,
    EventArgs e)
        {
            if (sender is UC_LineCard card)
            {
                int lineID =
                    card.LineID;

                DialogResult result =
                    MessageBox.Show(
                        $"Are you sure you want to delete Line {card.LineNumber}?",
                        "Confirm Delete",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    DeleteLine(lineID);

                    LoadLineCards();
                }
            }
        }

        private void DeleteLine(
    int lineID)
        {
            string password =
                PasswordEncryption.Decrypt(
                    config.DashboardDatabase.Password);

            string connectionString =
                ConnectionMain.GetSqlConnectionString(
                    config.DashboardDatabase.Server,
                    config.DashboardDatabase.Instance,
                    config.DashboardDatabase.Username,
                    password,
                    config.DashboardDatabase.Database);

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
        }
        private void LineCard_EditClicked(object sender,
    EventArgs e)
        {
            if (sender is UC_LineCard card)
            {
                // Remember which database row
                // we are editing
                editingLineID =
                    card.LineID;

                // Send values back to input controls
                txtLine.Text =
                    card.LineNumber.ToString();

                txtIpAddress.Text =
                    card.IPAddress;

                chkStatus.Checked =
                    card.IsActive;
            }
        }
        private async void btnSave_Click(
            object sender,
            EventArgs e)
        {
            if (!int.TryParse(
                txtLine.Text,
                out int lineNumber))
            {
                MessageBox.Show(
                    "Please enter a valid line number.");

                return;
            }

            string ipAddress =
                txtIpAddress.Text.Trim();

            if (string.IsNullOrWhiteSpace(
                ipAddress))
            {
                MessageBox.Show(
                    "Please enter IP address.");

                return;
            }

            if (editingLineID == 0)
            {
                // ADD NEW LINE
                InsertLine(
                    lineNumber,
                    ipAddress,
                    chkStatus.Checked);
            }
            else
            {
                // UPDATE EXISTING LINE
                UpdateLine(
                    editingLineID,
                    lineNumber,
                    ipAddress,
                    chkStatus.Checked);
            }

            // Reset edit mode
            editingLineID = 0;

            // Clear input fields
            txtLine.Clear();
            txtIpAddress.Clear();
            chkStatus.Checked = false;

            // Reload cards
            LoadLineCards();
       
        await CheckAllLineStatusAsync();
        }

        private void InsertLine(
      int lineNumber,
      string ipAddress,
      bool isActive)
        {
            string connectionString =
                ConnectionMain.GetSqlConnectionString(
                    config.DashboardDatabase.Server,
                    config.DashboardDatabase.Instance,
                    config.DashboardDatabase.Username,
                    PasswordEncryption.Decrypt(config.DashboardDatabase.Password),
                    config.DashboardDatabase.Database);
            string sql = @"
                INSERT INTO DC_LINEDETAIL
                (
                    LINE_NUM,
                    IP_ADDRESS,
                    STATUS_ACTIVE
                )
                VALUES
                (
                    @LineNumber,
                    @IPAddress,
                    @StatusActive
                )";

            using (SqlConnection conn =
                new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlCommand cmd =
                    new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue(
                        "@LineNumber",
                        lineNumber);

                    cmd.Parameters.AddWithValue(
                        "@IPAddress",
                        ipAddress);

                    cmd.Parameters.AddWithValue(
                        "@StatusActive",
                        isActive);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void UpdateLine(
        int lineID,
        int lineNumber,
        string ipAddress,
        bool isActive)
        {
            string password =
                PasswordEncryption.Decrypt(
                    config.DashboardDatabase.Password);

            string connectionString =
                ConnectionMain.GetSqlConnectionString(
                    config.DashboardDatabase.Server,
                    config.DashboardDatabase.Instance,
                    config.DashboardDatabase.Username,
                    password,
                    config.DashboardDatabase.Database);

            string sql = @"
            UPDATE DC_LINEDETAIL
            SET
                LINE_NUM = @LineNumber,
                IP_ADDRESS = @IPAddress,
                STATUS_ACTIVE = @StatusActive,
                DATEMODIFIED = SYSDATETIME()
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

                    cmd.Parameters.AddWithValue(
                        "@LineNumber",
                        lineNumber);

                    cmd.Parameters.AddWithValue(
                        "@IPAddress",
                        ipAddress);

                    cmd.Parameters.AddWithValue(
                        "@StatusActive",
                        isActive);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            editingLineID = 0;
            txtIpAddress.Clear();
            txtLine.Clear();
        }
    }
}