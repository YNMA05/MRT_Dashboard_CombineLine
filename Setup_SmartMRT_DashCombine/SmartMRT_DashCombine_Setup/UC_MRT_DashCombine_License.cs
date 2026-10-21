using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SmartMRT_DashCombine_Setup.Controls;
using SmartMRT_DashCombine_Setup.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using SmartMRT_DashCombine_Setup.Logging;


namespace SmartMRT_DashCombine_Setup
{
    public partial class UC_MRT_DashCombine_License : UserControl
    {
        private int editingLineID = 0;
        private bool isEditMode = false;
        private string licenseFilePath = string.Empty;
        private bool isFullLicense = true;
        private int selectedLicenseID = -1;
        private string selectedLicenseFilePath;
        private LicenseFile selectedLicenseFile;
        private LicensePayload selectedLicensePayload;
        private bool licenseExists = false;
        public event EventHandler LicenseInstalled;
        private string activeLicenseType = string.Empty;
        private bool isLoadingLicense = false;


        public UC_MRT_DashCombine_License()
        {
            InitializeComponent();
        }

        //private bool VerifySignatureWithPem(string payload, string signature, string pemFilePath)
        //{
        //    try
        //    {
        //        // Read the public key from the PEM file
        //        using (var reader = File.OpenText(pemFilePath))
        //        {
        //            var pemReader = new PemReader(reader);
        //            var publicKey = (RsaKeyParameters)pemReader.ReadObject();
        //            // Convert the payload and signature to byte arrays
        //            byte[] payloadBytes = Encoding.UTF8.GetBytes(payload);
        //            byte[] signatureBytes = Convert.FromBase64String(signature);
        //            // Create a SHA256 hash of the payload
        //            using (var sha256 = SHA256.Create())
        //            {
        //                byte[] hash = sha256.ComputeHash(payloadBytes);
        //                // Verify the signature using the public key
        //                var signer = SignerUtilities.GetSigner("SHA256withRSA");
        //                signer.Init(false, publicKey);
        //                signer.BlockUpdate(hash, 0, hash.Length);
        //                return signer.VerifySignature(signatureBytes);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(
        //            "Error verifying signature: " + ex.Message,
        //            "Signature Verification Error",
        //            MessageBoxButtons.OK,
        //            MessageBoxIcon.Error);
        //        return false;
        //    }
        //}



        private void btnBrowseLicense_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter =
                    "SmartMRT License (*.lic)|*.lic|" +
                    "JSON files (*.json)|*.json|" +
                    "All files (*.*)|*.*";

                if (dlg.ShowDialog() != DialogResult.OK)
                    return;

                try
                {
                    string json = File.ReadAllText(dlg.FileName);

                    //LicenseFile licenseFile =
                    //    JsonConvert.DeserializeObject<LicenseFile>(json);

                    //if (licenseFile == null)
                    //{
                    //    MessageBox.Show(
                    //        "Invalid license file.",
                    //        "License",
                    //        MessageBoxButtons.OK,
                    //        MessageBoxIcon.Error);

                    //    return;
                    //}

                    //if (string.IsNullOrWhiteSpace(licenseFile.LICENSE_PAYLOAD) ||
                    //    string.IsNullOrWhiteSpace(licenseFile.SIGNATURE))
                    //{
                    //    MessageBox.Show(
                    //        "License file is missing payload or signature.",
                    //        "License",
                    //        MessageBoxButtons.OK,
                    //        MessageBoxIcon.Error);

                    //    return;
                    //}

                    LicenseFile licenseFile = JsonConvert.DeserializeObject<LicenseFile>(json);

                    if (licenseFile == null)
                    {
                        MessageBox.Show(
                            "Invalid license file.",
                            "License",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);

                        return;
                    }

                    if (string.IsNullOrWhiteSpace(licenseFile.LICENSE_PAYLOAD) ||
                        string.IsNullOrWhiteSpace(licenseFile.SIGNATURE))
                    {
                        MessageBox.Show(
                            "License file is missing payload or signature.",
                            "License",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);

                        return;
                    }

                    LicensePayload payload =
                        JsonConvert.DeserializeObject<LicensePayload>(
                            licenseFile.LICENSE_PAYLOAD);

                    if (payload == null)
                    {
                        MessageBox.Show(
                            "Invalid license payload.",
                            "License",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);

                        return;
                    }

                    // Remember the selected license
                    selectedLicenseFile = licenseFile;
                    selectedLicensePayload = payload;
                    selectedLicenseFilePath = dlg.FileName;
                    if (payload == null)
                    {
                        MessageBox.Show(
                            "Invalid license payload.",
                            "License",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);

                        return;
                    }

                    // ----------------------------------------
                    // File
                    // ----------------------------------------

                    txtLicensePath.Text = dlg.FileName;

                    // ----------------------------------------
                    // Company / Hardware
                    // ----------------------------------------

                    txtCompany.Text = payload.Company;
                    txtHash.Text = payload.HwHash;

                    // ----------------------------------------
                    // License Group
                    // ----------------------------------------

                    if (string.Equals(
                        payload.LicenseGroup,
                        "FULL",
                        StringComparison.OrdinalIgnoreCase))
                    {
                        btnFull.Selected = true;
                        btnTrial.Selected = false;
                    }
                    else
                    {
                        btnFull.Selected = false;
                        btnTrial.Selected = true;
                    }

                    btnFull.Invalidate();
                    btnFull.Update();
                    btnTrial.Invalidate();
                    btnTrial.Update();

                    // ----------------------------------------
                    // Dashboard
                    // ----------------------------------------

                    //if (!string.IsNullOrWhiteSpace(payload.Dashboard))
                    //{
                    //    int dashboardIndex = ddlDashGroup.FindStringExact(payload.Dashboard);

                    //    if (dashboardIndex >= 0)
                    //    {
                    //        ddlDashGroup.SelectedIndex =
                    //            dashboardIndex;
                    //    }
                    //}
                   

                    // ----------------------------------------
                    // License Product
                    // ----------------------------------------

                    SelectLicenseProduct(payload);
                    ddlDashGroup.SelectedItem = payload.Dashboard;
                    // ----------------------------------------
                    // Status
                    // ----------------------------------------
                    DateTime issuedDate;

                    if (DateTime.TryParse(payload.IssuedUtc, out issuedDate))
                    {
                        glassDatePicker2.DateText =
                            issuedDate.ToLocalTime().ToString("yyyy-MM-dd");
                    }

                    if (!string.IsNullOrWhiteSpace(payload.ExpiryUtc))
                    {
                        DateTime expiryDate;

                        if (DateTime.TryParse(payload.ExpiryUtc, out expiryDate))
                        {
                            glassDatePicker1.DateText =
                                expiryDate.ToLocalTime().ToString("yyyy-MM-dd");
                        }
                    }
                    else
                    {
                        glassDatePicker1.DateText = "N/A";
                    }
                    lblStatus.Text =
                        "License loaded: " + dlg.FileName;


                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "Unable to read license file.\n\n" +
                        ex.Message,
                        "License",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        //private void SelectLicenseProduct(LicensePayload payload)
        //{
        //    dgvLicenseProduct.ClearSelection();

        //    foreach (DataGridViewRow row in dgvLicenseProduct.Rows)
        //    {
        //        string idText =
        //            row.Cells["LICENSEID"].Value?.ToString();

        //        string typeText =
        //            row.Cells["LICENSE_TYPE"].Value?.ToString();

        //        if (row.IsNewRow)
        //            continue;

        //        int licenseID;

        //        if (int.TryParse(
        //            row.Cells["LICENSEID"].Value?.ToString(),
        //            out licenseID)
        //            && licenseID == payload.LicenseID)
        //        {
        //            row.Selected = true;
        //            // selectedLicenseID = licenseID;

        //            txtMaxLine.Text =
        //                row.Cells["MAX_LINES"].Value?.ToString() ?? "";

        //            txtMaxPage.Text =
        //                row.Cells["MAX_PAGES"].Value?.ToString() ?? "";
        //            return;
        //        }



        //    }
        //}

        private void SelectLicenseProduct(LicensePayload payload)
        {
            if (payload == null)
                return;

            dgvLicenseProduct.ClearSelection();

            foreach (DataGridViewRow row in dgvLicenseProduct.Rows)
            {
                if (row.IsNewRow)
                    continue;

                string licenseType =
                    row.Cells["LICENSE_TYPE"].Value?.ToString();

                if (string.Equals(
                    licenseType?.Trim(),
                    payload.LicenseType?.Trim(),
                    StringComparison.OrdinalIgnoreCase))
                {
                    row.Selected = true;

                    // Set current cell to make the selection visible.
                    if (dgvLicenseProduct.Columns.Contains("LICENSE_TYPE"))
                    {
                        dgvLicenseProduct.CurrentCell =
                            row.Cells["LICENSE_TYPE"];
                    }

                    // Store the selected product ID if other code needs it.
                    int.TryParse(
                        row.Cells["LICENSEID"].Value?.ToString(),
                        out selectedLicenseID);

                    txtMaxLine.Text =
                        row.Cells["MAX_LINES"].Value?.ToString() ?? "";

                    txtMaxPage.Text =
                        row.Cells["MAX_PAGES"].Value?.ToString() ?? "";

                    if (row.Index >= 0)
                    {
                        dgvLicenseProduct.FirstDisplayedScrollingRowIndex =
                            row.Index;
                    }

                    return;
                }
            }

            MessageBox.Show(
                "License product not found.\n\n" +
                "License Type: " + payload.LicenseType,
                "License Product",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }

        private void btnInstallLicense_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtLicensePath.Text) || !System.IO.File.Exists(txtLicensePath.Text))
                {
                    MessageBox.Show("Please select a valid license_wrapper.json file first.", "No License", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var folder = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "SmartMRT_Dashboard");
                System.IO.Directory.CreateDirectory(folder);
                var dest = System.IO.Path.Combine(folder, "license.json");
                System.IO.File.Copy(txtLicensePath.Text, dest, true);
                //FolderLog.Info
                //lblStatus.Text = "Installed license to: " + dest + ". Please ensure publickey.pem is present at %ProgramData%\\SmartMRT\\publickey.pem";
               // MessageBox.Show("License installed. Restart the service to apply.", "Installed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to install license: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }   
        private void DesignStyle()
        {
            // Regular user: center the signature panel
            if (!panelGenerateKey.Visible &&
                !panelBottom.Visible &&
                !panelLicenseDetail.Visible)
            {
                int panelWidth = Math.Min(
                    700,
                    this.ClientSize.Width - 40);

                int panelHeight = Math.Min(
                    300,
                    this.ClientSize.Height - pnlHeader.Bottom - 40);

                glassPanelSignature.Size = new Size(
                    panelWidth,
                    panelHeight);

                glassPanelSignature.Location = new Point(
                    (this.ClientSize.Width - panelWidth) / 2,
                    pnlHeader.Bottom +
                    (this.ClientSize.Height - pnlHeader.Bottom - panelHeight) / 2);
                RequestLicenseLayout();
                return;
            }
        }

        private void UC_MRT_DashCombine_License_Load(object sender, EventArgs e)
        {
            ResizeLicensePanels();
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;
            LoadHardwareHash();
            getDashGroup();
            
            btnFull.Selected = true;
            btnTrial.Selected = false;

            btnFull.Invalidate();
            btnTrial.Invalidate();
            LoadLicense();
           
            DesignStyle();
        }

        private void LoadLicense()
        {
            try
            {
                string connectionString = ConnectionMain.GetDashboardConnection();

                string getSign = @"SELECT TOP 1
                D.COMPANY_NAME,
                D.SIGNATURE,
                D.LICENSE_TYPE,
				L.LICENSE_GROUP,
				L.DASHBOARD,
				L.MAX_LINES,
				L.MAX_PAGES,
                D.HW_HASH,
                D.LICENSE_PAYLOAD,
                D.ISSUED_UTC,
                D.EXPIRY_UTC,
                D.IS_ACTIVE
            FROM DC_SETUP D INNER JOIN DC_LICENSEDETAIL L ON D.LICENSE_TYPE = L.LICENSE_TYPE
            WHERE D.IS_ACTIVE = 1";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(getSign, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                licenseExists = true;
                                txtCompany.Text = reader["COMPANY_NAME"]?.ToString();
                                txtHash.Text = reader["HW_HASH"]?.ToString();
                                string licenseGroup = reader["LICENSE_GROUP"]?.ToString();
                                if (licenseGroup.Equals("FULL", StringComparison.OrdinalIgnoreCase))
                                {
                                    btnFull.Selected = true;
                                    btnTrial.Selected = false;
                                    btnFull.Invalidate();
                                    btnTrial.Invalidate();
                                }
                                else
                                {
                                    btnFull.Selected = false;
                                    btnTrial.Selected = true;
                                    btnFull.Invalidate();
                                    btnTrial.Invalidate();
                                }
                                modernTextSign.Text = reader["SIGNATURE"]?.ToString();
                               // glassDatePicker3.DateText = reader["EXPIRY_UTC"]?.ToString();
                                object expiryValue = reader["EXPIRY_UTC"];

                                if (expiryValue == DBNull.Value ||
                                    string.IsNullOrWhiteSpace(expiryValue.ToString()))
                                {
                                    glassDatePicker3.DateText = "N/A";
                                    modernTxtDays.Text = "PERPETUAL";
                                }
                                else
                                {
                                    DateTime expiryUtc;

                                    if (DateTime.TryParse(
                                        expiryValue.ToString(),
                                        out expiryUtc))
                                    {
                                        glassDatePicker3.DateText =
                                            expiryUtc.ToLocalTime().ToString("yyyy-MM-dd");

                                        modernTxtDays.Text =
                                            Math.Max(
                                                0,
                                                (expiryUtc.ToUniversalTime() -
                                                 DateTime.UtcNow).Days)
                                            .ToString();
                                    }
                                }
                              isLoadingLicense = true;

string dashboard =
    Convert.ToString(reader["DASHBOARD"]).Trim();

activeLicenseType =
    Convert.ToString(reader["LICENSE_TYPE"]).Trim();

// Set the dashboard before loading the product grid.
ddlDashGroup.SelectedItem = dashboard;

// Load the matching group and dashboard products.
LoadLicenseProducts(licenseGroup, dashboard);

// Apply the SQL selection after the current UI events finish.
this.BeginInvoke((MethodInvoker)delegate
{
    try
    {
        SelectActiveLicenseProduct(activeLicenseType);
    }
    finally
    {
        isLoadingLicense = false;
    }
});

                                //if (selectedRow != null)
                                //{
                                //    selectedRow.Selected = true;

                                //    dgvLicenseProduct.CurrentCell =
                                //        selectedRow.Cells["LICENSE_TYPE"];
                                //}
                                glassDatePicker1.DateText = reader["ISSUED_UTC"]?.ToString();
                                glassDatePicker2.DateText = reader["EXPIRY_UTC"]?.ToString();
                                lblStatusResult.Text = "Active license found.";
                            }
                            else
                            {
                                isLoadingLicense = false;
                                //modernTextSign.Text = string.Empty;
                                //modernTxtDays.Text = string.Empty;
                                //MessageBox.Show("No active license found. You may add or update the license.", "No Active License", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                glassDatePicker1.DateText = DateTime.Today.ToString("yyyy-MM-dd");
                                glassDatePicker2.DateText = DateTime.Today.ToString("yyyy-MM-dd");
                                licenseExists = false;
                                // lblStatusResult.Text = "No active license found. You may add or update the license.";
                                SetNoLicenseMode();
                                
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load license: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SelectActiveLicenseProduct(string licenseType)
        {
            if (string.IsNullOrWhiteSpace(licenseType))
                return;

            foreach (DataGridViewRow row in dgvLicenseProduct.Rows)
            {
                if (row.IsNewRow)
                    continue;

                string gridLicenseType =
                    Convert.ToString(row.Cells["LICENSE_TYPE"].Value)?.Trim();

                if (string.Equals(
                    gridLicenseType,
                    licenseType.Trim(),
                    StringComparison.OrdinalIgnoreCase))
                {
                    dgvLicenseProduct.ClearSelection();

                    dgvLicenseProduct.CurrentCell = null;
                    row.Selected = true;
                    dgvLicenseProduct.CurrentCell =
                        row.Cells["LICENSE_TYPE"];

                    selectedLicenseID = Convert.ToInt32(
                        row.Cells["LICENSEID"].Value);

                    txtMaxLine.Text =
                        Convert.ToString(row.Cells["MAX_LINES"].Value) ?? "";

                    txtMaxPage.Text =
                        Convert.ToString(row.Cells["MAX_PAGES"].Value) ?? "";

                    return;
                }
            }

            MessageBox.Show(
                "Cannot find license type: " + licenseType +
                "\nRows loaded: " + dgvLicenseProduct.Rows.Count,
                "License Selection Debug");
        }

        private void SetNoLicenseMode()
        {
            lblStatusResult.Text = "No active license found. You may add or update the license.";

            txtCompany.Enabled = true;
            txtHash.Enabled = false;

            dgvLicenseProduct.Enabled = true;

            btnFull.Enabled = true;
            btnTrial.Enabled = true;

            modernTextSign.Text = "";
            modernTxtDays.Text = "";

            glassDatePicker3.DateText = "";
        }

        private void LoadHardwareHash()
        {
            var comp = new
            {
                SystemUuid = QueryFirst("Win32_ComputerSystemProduct", "UUID"),
                BaseBoardSN = QueryFirst("Win32_BaseBoard", "SerialNumber"),
                BiosSN = QueryFirst("Win32_BIOS", "SerialNumber"),
                ProcessorId = QueryFirst("Win32_Processor", "ProcessorId")
            };

            string hwConcat = string.Join("|",
                new[]
                {
            comp.SystemUuid,
            comp.BaseBoardSN,
            comp.BiosSN,
            comp.ProcessorId
                }
                .Where(s => !string.IsNullOrWhiteSpace(s)));

            string hwHash;

            using (var sha = SHA256.Create())
            {
                var hash = sha.ComputeHash(
                    Encoding.UTF8.GetBytes(hwConcat));

                hwHash = BitConverter
                    .ToString(hash)
                    .Replace("-", "")
                    .ToLowerInvariant();
            }

            txtHash.Text = hwHash;
        }


        private static string QueryFirst(string wmiClass, string property)
        {
            try
            {
                using (var searcher = new ManagementObjectSearcher($"SELECT {property} FROM {wmiClass}"))
                {
                    foreach (ManagementObject mo in searcher.Get())
                    {
                        var val = mo[property]?.ToString();
                        if (!string.IsNullOrWhiteSpace(val))
                            return val.Trim();
                    }
                }
            }
            catch
            {
                // ignore
            }
            return string.Empty;
        }

        private void StyleToggleButton(Button btn, bool selected)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 1;
            btn.UseVisualStyleBackColor = false;

            btn.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;

            if (selected)
            {
                btn.BackColor = Color.FromArgb(0, 140, 190);
                btn.ForeColor = Color.Red;
                btn.FlatAppearance.BorderColor =
                    Color.FromArgb(0, 212, 255);
            }
            else
            {
                btn.BackColor = Color.FromArgb(16, 36, 71);
                btn.ForeColor = Color.FromArgb(168, 179, 199);
                btn.FlatAppearance.BorderColor =
                    Color.FromArgb(40, 75, 122);
            }
        }

        private void btnFull_Click(
            object sender,
            EventArgs e)
        {
            isFullLicense = true;

            btnFull.Selected = true;
            btnTrial.Selected = false;

            btnFull.Invalidate();
            btnTrial.Invalidate();
            getDashGroup();

            // LoadLicenseProducts("FULL");
        }

        private void btnTrial_Click(
            object sender,
            EventArgs e)
        {
            isFullLicense = false;

            btnFull.Selected = false;
            btnTrial.Selected = true;

            btnFull.Invalidate();
            btnTrial.Invalidate();
            getDashGroup();

            // LoadLicenseProducts("TRIAL");
        }

        private void getDashGroup()
        {
            ddlDashGroup.Enabled = true;

            string connectionString =
                ConnectionMain.GetDashboardConnection();

            try
            {
                using (var conn = new SqlConnection(connectionString))
                using (var cmd = new SqlCommand(
                    @"SELECT DISTINCT DASHBOARD
              FROM [DASHBOARD_GLOBALDB].[dbo].[DC_LICENSEDETAIL]
              WHERE DASHBOARD IS NOT NULL
                AND LTRIM(RTRIM(DASHBOARD)) <> ''
              ORDER BY DASHBOARD",
                    conn))
                {
                    conn.Open();

                    ddlDashGroup.Items.Clear();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string dashboard =
                                reader["DASHBOARD"]?.ToString()?.Trim();

                            if (!string.IsNullOrWhiteSpace(dashboard))
                            {
                                ddlDashGroup.Items.Add(dashboard);
                            }
                        }
                    }
                }

                if (ddlDashGroup.Items.Count > 0)
                {
                    ddlDashGroup.SelectedIndex = 0;


                    string licenseGroup =
                        btnFull.Selected ? "FULL" : "TRIAL";

                    string dashboardSelected =
                        ddlDashGroup.SelectedItem?.ToString() ?? "";

                    LoadLicenseProducts(licenseGroup, dashboardSelected);
                }
                else
                {
                    dgvLicenseProduct.DataSource = null;
                    txtMaxLine.Clear();
                    txtMaxPage.Clear();
                }
                // if (!string.IsNullOrWhiteSpace(dashboardSelected))
                //{
                //    LoadLicenseProducts(
                //        licenseGroup,
                //        dashboardSelected);
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Failed to load dashboard groups: " +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        private void dgvLicenseProduct_CellClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            dgvLicenseProduct.ClearSelection();

            dgvLicenseProduct.Rows[e.RowIndex].Selected = true;

            UpdateSelectedProductInfo();
        }

        private void LoadLicenseProducts(string licenseGroup, string dashboard)
        {
            dgvLicenseProduct.Visible = true;
            string connectionString = ConnectionMain.GetDashboardConnection();
            try
            {
                using (var conn = new SqlConnection(connectionString))
                using (var cmd = new SqlCommand(
                    @"SELECT 
                LICENSEID,
                LICENSE_GROUP,  
                LICENSE_TYPE,
                DASHBOARD,
                MAX_LINES,
                MAX_PAGES,
                LICENSE_DESC
              FROM [DASHBOARD_GLOBALDB].[dbo].[DC_LICENSEDETAIL]
              WHERE LICENSE_GROUP = @LicenseGroup AND DASHBOARD = @Dashboard
              ORDER BY LICENSE_GROUP, LICENSE_TYPE",
                    conn))
                {
                    cmd.Parameters.AddWithValue("@LicenseGroup", licenseGroup);
                    cmd.Parameters.AddWithValue("@Dashboard", dashboard);
                    // cmd.ExecuteNonQuery(); // Not needed when using SqlDataAdapter
                    using (var adapter = new SqlDataAdapter(cmd))
                    {
                        var dt = new DataTable();
                        adapter.Fill(dt);
                        dgvLicenseProduct.DataSource = dt;
                    }
                }
                if (dgvLicenseProduct.Columns.Contains("LICENSEID"))
                    dgvLicenseProduct.Columns["LICENSEID"].Visible = false;
                if (dgvLicenseProduct.Columns.Contains("DATEMODIFIED"))
                    dgvLicenseProduct.Columns["DATEMODIFIED"].Visible = false;
                if (dgvLicenseProduct.Columns.Contains("MAX_LINES"))
                    dgvLicenseProduct.Columns["MAX_LINES"].Visible = false;
                if (dgvLicenseProduct.Columns.Contains("MAX_PAGES"))
                    dgvLicenseProduct.Columns["MAX_PAGES"].Visible = false;
                dgvLicenseProduct.Columns["LICENSE_GROUP"].Visible = false;
                dgvLicenseProduct.Columns["LICENSE_TYPE"].HeaderCell.Value = "Product";
                dgvLicenseProduct.Columns["DASHBOARD"].Visible = false;
                dgvLicenseProduct.Columns["LICENSE_DESC"].HeaderText = "License Description";
                dgvLicenseProduct.Columns["LICENSE_DESC"].Visible = false;
                dgvLicenseProduct.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
                dgvLicenseProduct.AllowUserToResizeRows = false;
                dgvLicenseProduct.ReadOnly = true;
                dgvLicenseProduct.RowTemplate.Height = 30;

                foreach (DataGridViewColumn column in dgvLicenseProduct.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }

                foreach (DataGridViewRow row in dgvLicenseProduct.Rows)
                {
                    row.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                    row.Height = 30;
                }


                StyleProductGrid();
                dgvLicenseProduct.ClearSelection();
                dgvLicenseProduct.CurrentCell = null;
                dgvLicenseProduct.AutoSizeColumnsMode =
DataGridViewAutoSizeColumnsMode.None;
                dgvLicenseProduct.Columns["LICENSE_TYPE"].Width = 200;
                dgvLicenseProduct.Columns["LICENSE_DESC"].Width = 150;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Failed to load license products: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            //if (dgvLicenseProduct.Rows.Count > 0)
            //{
            //    dgvLicenseProduct.ClearSelection();

            //    DataGridViewRow firstRow =
            //        dgvLicenseProduct.Rows[0];

            //    firstRow.Selected = true;

            //    dgvLicenseProduct_CellClick(
            //        dgvLicenseProduct,
            //        new DataGridViewCellEventArgs(
            //            0,
            //            0));
            //}

        }

        private void LayoutLicenseDetails()
        {
            panelLicenseDetail.SuspendLayout();

            try
            {
                panelLicenseDetail.AutoScroll = true;

                dgvLicenseDetail.Dock = DockStyle.None;
                dgvLicenseDetail.Anchor = AnchorStyles.Top | AnchorStyles.Left;

                // Always use the same starting position
                dgvLicenseDetail.Location = new Point(10, 55);

                dgvLicenseDetail.ScrollBars = ScrollBars.None;
                dgvLicenseDetail.AutoSizeRowsMode =
                    DataGridViewAutoSizeRowsMode.None;

                // Fix row heights
                foreach (DataGridViewRow row in dgvLicenseDetail.Rows)
                {
                    if (!row.IsNewRow)
                        row.Height = 45;
                }

                // Calculate content dimensions
                int totalWidth = 0;

                foreach (DataGridViewColumn column in dgvLicenseDetail.Columns)
                {
                    if (column.Visible)
                        totalWidth += column.Width;
                }

                int totalHeight =
                    dgvLicenseDetail.ColumnHeadersHeight +
                    (dgvLicenseDetail.Rows.Count * 45) + 5;

                dgvLicenseDetail.Size = new Size(
                    Math.Max(totalWidth + 5, panelLicenseDetail.ClientSize.Width - 30),
                    Math.Max(totalHeight, panelLicenseDetail.ClientSize.Height - 70)
                );

                panelLicenseDetail.AutoScrollMinSize = new Size(
                    dgvLicenseDetail.Right + 10,
                    dgvLicenseDetail.Bottom + 10
                );
            }
            finally
            {
                panelLicenseDetail.ResumeLayout(true);
            }
        }

        private void btnCheckLicense_Click(object sender, EventArgs e)
        {
            if (!dgvLicenseDetail.Visible)
            {
                LoadLicenseDetails();

                // Set layout only after data and columns are loaded
                LayoutLicenseDetails();

                dgvLicenseDetail.Visible = true;

                btnCheckLicense.Text = "Hide License Details";
                btnCheckLicense.Selected = true;
            }
            else
            {
                dgvLicenseDetail.Visible = false;

                panelLicenseDetail.AutoScrollPosition = Point.Empty;
                panelLicenseDetail.AutoScrollMinSize = Size.Empty;

                btnCheckLicense.Text = "Show License Details";
                btnCheckLicense.Selected = false;
            }
        }

        private void LoadLicenseDetails()
        {
            dgvLicenseDetail.Visible = true;
            string connectionString = ConnectionMain.GetDashboardConnection();

            try
            {
                using (var conn = new SqlConnection(connectionString))
                using (var cmd = new SqlCommand(
                    @"SELECT 
                LICENSEID,
                LICENSE_GROUP,
                LICENSE_TYPE,
                DASHBOARD,
                MAX_LINES,
                MAX_PAGES,
                LICENSE_DESC,
                DATEMODIFIED
              FROM [DASHBOARD_GLOBALDB].[dbo].[DC_LICENSEDETAIL]
              ORDER BY LICENSE_GROUP, LICENSE_TYPE",
                    conn))
                using (var adapter = new SqlDataAdapter(cmd))
                {
                    var dt = new DataTable();

                    conn.Open();
                    adapter.Fill(dt);

                    dgvLicenseDetail.DataSource = dt;
                }

                if (dgvLicenseDetail.Columns.Contains("LICENSEID"))
                    dgvLicenseDetail.Columns["LICENSEID"].Visible = false;

                if (dgvLicenseDetail.Columns.Contains("DATEMODIFIED"))
                    dgvLicenseDetail.Columns["DATEMODIFIED"].Visible = false;

                dgvLicenseDetail.Columns["LICENSE_GROUP"].HeaderText = "License Group";
                dgvLicenseDetail.Columns["LICENSE_TYPE"].HeaderText = "Product";
                dgvLicenseDetail.Columns["DASHBOARD"].HeaderText = "Dashboard";
                dgvLicenseDetail.Columns["MAX_LINES"].HeaderText = "Max Lines";
                dgvLicenseDetail.Columns["MAX_PAGES"].HeaderText = "Max Pages";
                dgvLicenseDetail.Columns["LICENSE_DESC"].HeaderText = "License Description";
                dgvLicenseDetail.Columns["LICENSE_DESC"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                dgvLicenseDetail.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
                dgvLicenseDetail.AllowUserToResizeRows = false;
                dgvLicenseDetail.ReadOnly = true;
                dgvLicenseDetail.RowTemplate.Height = 45;

                foreach (DataGridViewColumn column in dgvLicenseDetail.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }

                //foreach (DataGridViewRow row in dgvLicenseDetail.Rows)
                //{
                //    row.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                //    row.Height = 45;
                //}

                dgvLicenseDetail.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                dgvLicenseDetail.Columns["LICENSE_GROUP"].Width = 150;
                dgvLicenseDetail.Columns["LICENSE_TYPE"].Width = 150;
                dgvLicenseDetail.Columns["DASHBOARD"].Width = 120;
                // dgvLicenseDetail.Columns["LICENSE_DESC"].Width = 350;
                dgvLicenseDetail.Columns["LICENSE_DESC"].MinimumWidth = 200;
                dgvLicenseDetail.Columns["MAX_LINES"].Width = 80;
                dgvLicenseDetail.Columns["MAX_PAGES"].Width = 80;
                dgvLicenseDetail.Columns["LICENSE_DESC"].AutoSizeMode =
    DataGridViewAutoSizeColumnMode.None;

                dgvLicenseDetail.Columns["LICENSE_DESC"].Width = 400;
                dgvLicenseDetail.Columns["LICENSE_DESC"].MinimumWidth = 300;


                StyleModuleGrid();

                // Let the parent panel handle scrolling
                panelLicenseDetail.AutoScroll = true;

                // Disable grid docking and anchoring
                dgvLicenseDetail.Dock = DockStyle.None;
                dgvLicenseDetail.Anchor = AnchorStyles.Top | AnchorStyles.Left;

                // Disable the DataGridView's own scrollbars
                dgvLicenseDetail.ScrollBars = ScrollBars.None;

                // Position the grid inside the panel
                dgvLicenseDetail.Location = new Point(10, 55);

                // Set the grid width to the total of all visible column widths
                int totalWidth = 0;

                foreach (DataGridViewColumn column in dgvLicenseDetail.Columns)
                {
                    if (column.Visible)
                    {
                        totalWidth += column.Width;
                    }
                }

                dgvLicenseDetail.Width = totalWidth + 5;

                // Calculate the grid height from the number of rows
                int totalHeight =
                    dgvLicenseDetail.ColumnHeadersHeight +
                    (dgvLicenseDetail.Rows.Count * dgvLicenseDetail.RowTemplate.Height) +
                    5;

                dgvLicenseDetail.Height = Math.Max(
                    totalHeight,
                    panelLicenseDetail.ClientSize.Height - 70
                );

                // Enable the panel's horizontal and vertical scrollbars
                panelLicenseDetail.AutoScrollMinSize = new Size(
                    dgvLicenseDetail.Right + 10,
                    dgvLicenseDetail.Bottom + 10
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Failed to check license: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        private void StyleProductGrid()
        {

            // dgvLicenseProduct.ScrollBars = ScrollBars.Both;

            dgvLicenseProduct.BackgroundColor =
                Color.FromArgb(8, 21, 47);

            dgvLicenseProduct.BorderStyle =
                BorderStyle.None;

            dgvLicenseProduct.CellBorderStyle =
                DataGridViewCellBorderStyle.SingleHorizontal;

            dgvLicenseProduct.GridColor =
    Color.FromArgb(35, 65, 105);

            dgvLicenseProduct.GridColor =
                Color.FromArgb(40, 75, 122);

            dgvLicenseProduct.EnableHeadersVisualStyles = false;

            // ==============================
            // HEADER
            // ==============================

            dgvLicenseProduct.DefaultCellStyle.BackColor =
             Color.FromArgb(10, 25, 50);

            dgvLicenseProduct.DefaultCellStyle.ForeColor =
                Color.White;

            dgvLicenseProduct.DefaultCellStyle.SelectionBackColor =
                Color.FromArgb(0, 100, 140);

            dgvLicenseProduct.DefaultCellStyle.SelectionForeColor =
                Color.White;

            dgvLicenseProduct.ColumnHeadersDefaultCellStyle.BackColor =
    Color.FromArgb(16, 36, 71);

            dgvLicenseProduct.ColumnHeadersDefaultCellStyle.ForeColor =
                Color.White;

            dgvLicenseProduct.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 9F, FontStyle.Bold);

            dgvLicenseProduct.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleLeft;

            dgvLicenseProduct.ColumnHeadersHeight = 25;

            // ==============================
            // CELLS
            // ==============================

            dgvLicenseProduct.DefaultCellStyle.BackColor =
                Color.FromArgb(12, 29, 55);

            dgvLicenseProduct.DefaultCellStyle.ForeColor =
                Color.White;

            dgvLicenseProduct.DefaultCellStyle.Font =
                new Font(
                    "Segoe UI",
                    9.5F);

            dgvLicenseProduct.DefaultCellStyle.SelectionBackColor =
                Color.FromArgb(20, 52, 82);

            dgvLicenseProduct.DefaultCellStyle.SelectionForeColor =
                Color.White;

            dgvLicenseProduct.DefaultCellStyle.Padding =
                new Padding(12, 0, 12, 0);

            dgvLicenseProduct.RowTemplate.Height = 30;

            dgvLicenseProduct.AutoSizeRowsMode =
                DataGridViewAutoSizeRowsMode.None;
           // dgvLicenseProduct.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;


            dgvLicenseProduct.AllowUserToResizeRows = false;

            // ==============================
            // BEHAVIOR
            // ==============================

            dgvLicenseProduct.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvLicenseProduct.MultiSelect = false;

            dgvLicenseProduct.AllowUserToAddRows = false;

            dgvLicenseProduct.AllowUserToDeleteRows = false;

            dgvLicenseProduct.AllowUserToResizeRows = false;

            dgvLicenseProduct.ReadOnly = true;
            dgvLicenseProduct.RowHeadersVisible = false;

            dgvLicenseProduct.AutoGenerateColumns = true;

            dgvLicenseProduct.AutoSizeRowsMode =
                DataGridViewAutoSizeRowsMode.None;

            dgvLicenseProduct.SelectionMode =
    DataGridViewSelectionMode.FullRowSelect;

            dgvLicenseProduct.MultiSelect = false;

            dgvLicenseProduct.DefaultCellStyle.SelectionBackColor =
                Color.FromArgb(0, 100, 140);

            dgvLicenseProduct.DefaultCellStyle.SelectionForeColor =
                Color.White;

            //dgvLicenseDetail.CellFormatting -= dgvLicenseDetail_CellFormatting;
            //dgvLicenseDetail.CellFormatting += dgvLicenseDetail_CellFormatting;
        }

        private void StyleModuleGrid()
        {
            // dgvLicenseDetail.ScrollBars = ScrollBars.Both;

//            dgvLicenseDetail.AutoSizeColumnsMode =
//DataGridViewAutoSizeColumnsMode.AllCells;

//            dgvLicenseDetail.AutoSizeRowsMode =
//                DataGridViewAutoSizeRowsMode.AllCells;

            dgvLicenseDetail.BackgroundColor =
                Color.FromArgb(8, 21, 47);

            dgvLicenseDetail.BorderStyle =
                BorderStyle.None;

            dgvLicenseDetail.CellBorderStyle =
                DataGridViewCellBorderStyle.SingleHorizontal;

            dgvLicenseDetail.GridColor =
                Color.FromArgb(40, 75, 122);

            dgvLicenseDetail.EnableHeadersVisualStyles = false;

            // ==============================
            // HEADER
            // ==============================

            dgvLicenseDetail.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(16, 36, 71);

            dgvLicenseDetail.ColumnHeadersDefaultCellStyle.ForeColor =
                Color.FromArgb(168, 179, 199);

            dgvLicenseDetail.ColumnHeadersDefaultCellStyle.Font =
                new Font(
                    "Segoe UI",
                    9F,
                    FontStyle.Bold);

            dgvLicenseDetail.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleLeft;

            dgvLicenseDetail.ColumnHeadersHeight = 42;

            // ==============================
            // CELLS
            // ==============================

            dgvLicenseDetail.DefaultCellStyle.BackColor =
                Color.FromArgb(12, 29, 55);

            dgvLicenseDetail.DefaultCellStyle.ForeColor =
                Color.White;

            dgvLicenseDetail.DefaultCellStyle.Font =
                new Font(
                    "Segoe UI",
                    9.5F);

            dgvLicenseDetail.DefaultCellStyle.SelectionBackColor =
                Color.FromArgb(20, 52, 82);

            dgvLicenseDetail.DefaultCellStyle.SelectionForeColor =
                Color.White;

            dgvLicenseDetail.DefaultCellStyle.Padding =
                new Padding(12, 0, 12, 0);

            dgvLicenseDetail.RowTemplate.Height = 45;

            dgvLicenseDetail.AutoSizeRowsMode =
                DataGridViewAutoSizeRowsMode.None;

            dgvLicenseDetail.AllowUserToResizeRows = false;

            // ==============================
            // BEHAVIOR
            // ==============================

            dgvLicenseDetail.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvLicenseDetail.MultiSelect = false;

            dgvLicenseDetail.AllowUserToAddRows = false;

            dgvLicenseDetail.AllowUserToDeleteRows = false;

            dgvLicenseDetail.AllowUserToResizeRows = false;

            dgvLicenseDetail.ReadOnly = true;
            dgvLicenseDetail.RowHeadersVisible = false;

            dgvLicenseDetail.AutoGenerateColumns = true;

            //dgvLicenseDetail.AutoSizeRowsMode =
            //    DataGridViewAutoSizeRowsMode.None;
            // Use manual column widths
            dgvLicenseDetail.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.None;

            // Keep rows at a consistent height
            dgvLicenseDetail.AutoSizeRowsMode =
                DataGridViewAutoSizeRowsMode.None;

            dgvLicenseDetail.RowTemplate.Height = 45;

            // Allow horizontal and vertical scrolling
            dgvLicenseDetail.ScrollBars = ScrollBars.Both;

            //dgvLicenseDetail.CellFormatting -= dgvLicenseDetail_CellFormatting;
            //dgvLicenseDetail.CellFormatting += dgvLicenseDetail_CellFormatting;
        }

        private void SelectProductRow(int rowIndex)
        {
            if (rowIndex < 0 ||
                rowIndex >= dgvLicenseProduct.Rows.Count)
                return;

            DataGridViewRow row = dgvLicenseProduct.Rows[rowIndex];

            if (row.IsNewRow ||
                row.Cells["LICENSEID"].Value == null)
                return;

            if (!int.TryParse(
                row.Cells["LICENSEID"].Value.ToString(),
                out int licenseID))
                return;

            selectedLicenseID = licenseID;

            txtMaxLine.Text =
                row.Cells["MAX_LINES"].Value?.ToString() ?? "";

            txtMaxPage.Text =
                row.Cells["MAX_PAGES"].Value?.ToString() ?? "";

            dgvLicenseProduct.ClearSelection();
            row.Selected = true;

            if (dgvLicenseProduct.Columns.Contains("LICENSE_TYPE"))
            {
                dgvLicenseProduct.CurrentCell =
                    row.Cells["LICENSE_TYPE"];
            }
        }




        private void ddlDashGroup_SelectedIndexChanged(
    object sender,
    EventArgs e)
        {
            if (ddlDashGroup.SelectedIndex < 0)
                return;

            string licenseGroup =
                btnFull.Selected ? "FULL" : "TRIAL";

            string dashboard =
                ddlDashGroup.SelectedItem?.ToString()?.Trim();

            if (string.IsNullOrWhiteSpace(dashboard))
                return;

            LoadLicenseProducts(licenseGroup, dashboard);

            // Keep the active SQL license selected if it exists
            // in this dashboard's product list.
            bool activeFound = false;

            if (licenseExists &&
                !string.IsNullOrWhiteSpace(activeLicenseType))
            {
                activeFound =
                    SelectBoolActiveLicenseProduct(activeLicenseType);
            }

            // If the active license doesn't belong to this dashboard,
            // select the first product in the newly loaded list.
            if (!activeFound && dgvLicenseProduct.Rows.Count > 0)
            {
                SelectProductRow(0);
            }
        }

        private bool SelectBoolActiveLicenseProduct(string licenseType)
        {
            if (string.IsNullOrWhiteSpace(licenseType))
                return false;

            foreach (DataGridViewRow row in dgvLicenseProduct.Rows)
            {
                if (row.IsNewRow)
                    continue;

                string gridLicenseType =
                    Convert.ToString(
                        row.Cells["LICENSE_TYPE"].Value)?.Trim();

                if (!string.Equals(
                    gridLicenseType,
                    licenseType.Trim(),
                    StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                dgvLicenseProduct.ClearSelection();

                row.Selected = true;
                dgvLicenseProduct.CurrentCell =
                    row.Cells["LICENSE_TYPE"];

                int.TryParse(
                    Convert.ToString(row.Cells["LICENSEID"].Value),
                    out selectedLicenseID);

                txtMaxLine.Text =
                    Convert.ToString(row.Cells["MAX_LINES"].Value) ?? "";

                txtMaxPage.Text =
                    Convert.ToString(row.Cells["MAX_PAGES"].Value) ?? "";

                return true;
            }

            // Not found is normal when the user changes dashboard.
            return false;
        }


        private void UpdateSelectedProductInfo()
        {
            if (dgvLicenseProduct.Rows.Count == 0)
            {
                selectedLicenseID = 0;
                txtMaxLine.Clear();
                txtMaxPage.Clear();
                return;
            }

            int rowIndex = 0;

            // If user has selected a product, use that row
            if (dgvLicenseProduct.SelectedRows.Count > 0)
            {
                rowIndex = dgvLicenseProduct.SelectedRows[0].Index;
            }

            DataGridViewRow row =
                dgvLicenseProduct.Rows[rowIndex];

            if (row.Cells["LICENSEID"].Value != null)
            {
                int.TryParse(
                    row.Cells["LICENSEID"].Value.ToString(),
                    out selectedLicenseID);
            }

            txtMaxLine.Text =
                row.Cells["MAX_LINES"].Value?.ToString() ?? "";

            txtMaxPage.Text =
                row.Cells["MAX_PAGES"].Value?.ToString() ?? "";

            // Select the row visually
            dgvLicenseProduct.ClearSelection();
            row.Selected = true;
        }

        private void dgvLicenseProduct_SelectionChanged(
       object sender,
       EventArgs e)
        {
            if (isLoadingLicense)
                return;

            if (dgvLicenseProduct.CurrentRow == null)
                return;

            if (!dgvLicenseProduct.Columns.Contains("LICENSEID"))
                return;

            var value =
                dgvLicenseProduct.CurrentRow.Cells["LICENSEID"].Value;

            if (value == null || value == DBNull.Value)
                return;

            if (int.TryParse(value.ToString(), out int licenseID))
            {
                selectedLicenseID = licenseID;

                txtMaxLine.Text =
                    dgvLicenseProduct.CurrentRow
                        .Cells["MAX_LINES"]
                        .Value?.ToString() ?? "";

                txtMaxPage.Text =
                    dgvLicenseProduct.CurrentRow
                        .Cells["MAX_PAGES"]
                        .Value?.ToString() ?? "";
            }
        }

        private void btnGenerateKey_Click(object sender, EventArgs e)
        {
            if (txtCompany.Text.Trim() == "")
            {
                MessageBox.Show("Please enter a company name before generating a key.", "Missing Company Name", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtHash.Text))
            {
                MessageBox.Show("Hardware hash is empty. Cannot generate key.", "Missing Hardware Hash", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtMaxLine.Text) || string.IsNullOrWhiteSpace(txtMaxPage.Text))
            {
                MessageBox.Show("Please select a product to generate a key.", "Missing Product Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (selectedLicenseID <= 0)
            {
                MessageBox.Show("No valid product selected. Cannot generate key.", "Invalid Product Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var license = new
            {
                Company = txtCompany.Text.Trim(),
                HwHash = txtHash.Text.Trim(),
                //MaxLines = int.TryParse(txtMaxLine.Text.Trim(), out int ml) ? ml : 0,
                //MaxPages = int.TryParse(txtMaxPage.Text.Trim(), out int mp) ? mp : 0,
                // LicenseType = btnFull.Selected ? "FULL" : "TRIAL",
                LicenseID = selectedLicenseID,
                IssuedUtc = DateTime.UtcNow.ToString("o"),
                //ExpiryUtc = DateTime.UtcNow.AddYears(1).ToString("o")
            };

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
                sfd.FileName = "license_wrapper.json";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    SaveLicenseAsJson(sfd.FileName, license);
                    MessageBox.Show("License wrapper saved: " + sfd.FileName, "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
               // MessageBox.Show("License wrapper saved: " + sfd.FileName, "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void SaveLicenseAsJson(string outPath, object licenseObject)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(licenseObject, Newtonsoft.Json.Formatting.Indented);
            System.IO.File.WriteAllText(outPath, json, Encoding.UTF8);
        }

        //private void SaveLicenseAsJson(string licensePayload, string signature)
        //{
        //    var licenseObject = new
        //    {
        //        LICENSE_PAYLOAD = licensePayload,
        //        SIGNATURE = signature
        //    };
        //    string json = Newtonsoft.Json.JsonConvert.SerializeObject(licenseObject, Newtonsoft.Json.Formatting.Indented);
        //    using (SaveFileDialog sfd = new SaveFileDialog())
        //    {
        //        sfd.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
        //        sfd.FileName = "license_wrapper.json";
        //        if (sfd.ShowDialog() == DialogResult.OK)
        //        {
        //            System.IO.File.WriteAllText(sfd.FileName, json, Encoding.UTF8);
        //            lblStatus.Text = "Saved license to: " + sfd.FileName;
        //            MessageBox.Show("License saved: " + sfd.FileName, "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        }
        //    }
        //}

        private void btnBrowseLicense_Click_1(object sender, EventArgs e)
        {

        }

        private void glassDatePicker2_Load(object sender, EventArgs e)
        {

        }


        private void btnInstallLicense_Click(object sender, EventArgs e)
        {
            DialogResult warningResult = MessageBox.Show(
                "Installing a license will overwrite any existing license in the system.",
                "Warning",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Warning);

            if (warningResult != DialogResult.OK)
                return;

            if (selectedLicensePayload == null ||
                selectedLicenseFile == null ||
                string.IsNullOrWhiteSpace(selectedLicenseFilePath))
            {
                MessageBox.Show(
                    "No license selected.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            string licenseFolder = null;
            string licenseFilePath = null;
            string tempLicensePath = null;

            try
            {
                LicenseFile licenseFile = selectedLicenseFile;
                LicensePayload payload = selectedLicensePayload;

                // =========================================================
                // 1. Prepare license folder
                // =========================================================

                licenseFolder = Path.Combine(
                    Environment.GetFolderPath(
                        Environment.SpecialFolder.CommonApplicationData),
                    "SmartMRT_Dashboard",
                    "License");

                Directory.CreateDirectory(licenseFolder);

                licenseFilePath = Path.Combine(
                    licenseFolder,
                    "license.lic");

                tempLicensePath = Path.Combine(
                    licenseFolder,
                    "license.tmp");

                // =========================================================
                // 2. Copy new license to temporary file first
                // =========================================================

                File.Copy(
                    selectedLicenseFilePath,
                    tempLicensePath,
                    true);

                FolderLog.Info(
                    "Temporary license file created: " +
                    tempLicensePath);

                // =========================================================
                // 3. Connect to SQL
                // =========================================================

                string connectionString =
                    ConnectionMain.GetDashboardConnection();

                using (SqlConnection connection =
                    new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlTransaction transaction =
                        connection.BeginTransaction())
                    {
                        try
                        {
                            // =================================================
                            // 4. Replace current license in DC_SETUP
                            // =================================================

                            string sql = @"
                        DELETE FROM DC_SETUP;

                        INSERT INTO DC_SETUP
                        (
                            COMPANY_NAME,
                            SIGNATURE,
                            LICENSE_TYPE,
                            HW_HASH,
                            LICENSE_PAYLOAD,
                            ISSUED_UTC,
                            EXPIRY_UTC,
                            IS_ACTIVE,
                            DATEMODIFIED
                        )
                        VALUES
                        (
                            @Company,
                            @Signature,
                            @LicenseType,
                            @HwHash,
                            @LicensePayload,
                            @IssuedUtc,
                            @ExpiryUtc,
                            1,
                            GETDATE()
                        );";

                            using (SqlCommand cmd =
                                new SqlCommand(sql, connection, transaction))
                            {
                                cmd.Parameters.AddWithValue(
                                    "@Company",
                                    payload.Company);

                                cmd.Parameters.AddWithValue(
                                    "@Signature",
                                    licenseFile.SIGNATURE);

                                cmd.Parameters.AddWithValue(
                                    "@LicenseType",
                                    payload.LicenseType);

                                cmd.Parameters.AddWithValue(
                                    "@HwHash",
                                    payload.HwHash);

                                cmd.Parameters.AddWithValue(
                                    "@LicensePayload",
                                    licenseFile.LICENSE_PAYLOAD);

                                cmd.Parameters.AddWithValue(
                                    "@IssuedUtc",
                                    payload.IssuedUtc);

                                cmd.Parameters.AddWithValue(
                                    "@ExpiryUtc",
                                    (object)payload.ExpiryUtc ??
                                    DBNull.Value);

                                cmd.ExecuteNonQuery();
                            }

                            // =================================================
                            // 5. Commit SQL
                            // =================================================

                            transaction.Commit();
                        }
                        catch
                        {
                            transaction.Rollback();

                            throw;
                        }
                    }
                }

                // =========================================================
                // 6. Replace actual license file
                // =========================================================

                File.Copy(
                    tempLicensePath,
                    licenseFilePath,
                    true);

                FolderLog.Info(
                    "License file installed: " +
                    licenseFilePath);

                // =========================================================
                // 7. Remove temporary file
                // =========================================================

                if (File.Exists(tempLicensePath))
                {
                    File.Delete(tempLicensePath);
                }

                // =========================================================
                // 8. Log activity
                // =========================================================

                ActivityLogger.Log(
                    connectionString,
                    "LICENSE",
                    payload.LicenseType,
                    string.Format(
                        "License={0}; IssuedDate={1}; ExpiryDate={2}",
                        payload.LicenseType,
                        payload.IssuedUtc,
                        payload.ExpiryUtc));

                //LoadLicenseDetails();

                MessageBox.Show(
                    "License installed successfully.",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                LicenseInstalled?.Invoke(this,EventArgs.Empty);

                Application.Restart();
                Application.Exit();
            }
            catch (Exception ex)
            {
                // =========================================================
                // Cleanup temporary file
                // =========================================================

                try
                {
                    if (!string.IsNullOrWhiteSpace(tempLicensePath) &&
                        File.Exists(tempLicensePath))
                    {
                        File.Delete(tempLicensePath);
                    }
                }
                catch
                {
                    // Ignore cleanup failure
                }

                FolderLog.Info(
                    "License installation failed: " +
                    ex.Message);

                MessageBox.Show(
                    "Failed to install license.\n\n" +
                    ex.Message,
                    "License",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        public void SetViewLicensePermission(bool canViewLicense)
        {
            if (glassPanelSignature != null)
            {
                glassPanelSignature.Visible = canViewLicense;
            }
        }

        public void SetAddLicensePermission(bool canAdd)
        {
            if (panelBottom != null)
            {
                panelBottom.Visible = canAdd;
            }
            if (panelGenerateKey != null)
            {
                panelGenerateKey.Visible = canAdd;
            }
            if (panelLicenseDetail != null)
            {
                panelLicenseDetail.Visible = canAdd;
            }
        }

        //public void SetViewLicensePermission(bool canView)
        //{
        //    glassPanelSignature.Visible = canView;
        //}
        //public void SetAddLicensePermission(bool canAdd)
        //{
        //    panelBottom.Visible = canAdd;
        //    panelGenerateKey.Visible = canAdd;
        //    panelLicenseDetail.Visible = canAdd;
            
        //}

        private void ddlDashGroup_Load(object sender, EventArgs e)
        {

        }

        private void modernTextSign_Load(object sender, EventArgs e)
        {

        }

        private void glassPanelSignature_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblLicenseDetail_Click(object sender, EventArgs e)
        {

        }

        private void UC_MRT_DashCombine_License_Resize(object sender, EventArgs e)
        {
            RequestLicenseLayout();
        }

        private bool _layoutPending;

        private void RequestLicenseLayout()
        {
            if (_layoutPending || IsDisposed)
                return;

            _layoutPending = true;

            BeginInvoke((Action)(() =>
            {
                _layoutPending = false;

                if (IsDisposed)
                    return;

                ResizeLicensePanels();
            }));
        }

        private void ResizeLicensePanels()
        {
            // Admin layout: left and right columns
            int margin = 12;
            int gap = 12;

            int contentTop = pnlHeader.Bottom + gap;
            int contentHeight = this.ClientSize.Height - contentTop - margin;

            int leftWidth = (this.ClientSize.Width - (margin * 2) - gap) / 2;
            int rightWidth = leftWidth;

            // Left: signature panel
            glassPanelSignature.Location =
                new Point(margin, contentTop);

            glassPanelSignature.Size =
                new Size(leftWidth, 156);

            // Left: generate key panel below signature
            panelGenerateKey.Location =
                new Point(margin, glassPanelSignature.Bottom + gap);

            panelGenerateKey.Size =
                new Size(
                    leftWidth,
                    Math.Max(200, contentHeight - 156 - gap));

            // Right: bottom panel at the TOP
            int bottomPanelHeight = 190;

            panelBottom.Location =
                new Point(margin + leftWidth + gap, contentTop);

            panelBottom.Size =
                new Size(rightWidth, bottomPanelHeight);

            // Right: license details BELOW the bottom panel
            panelLicenseDetail.Location =
                new Point(
                    panelBottom.Left,
                    panelBottom.Bottom + gap);

            panelLicenseDetail.Size =
                new Size(
                    rightWidth,
                    Math.Max(
                        150,
                        contentHeight - bottomPanelHeight - gap));
        }

        private void dgvLicenseDetail_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void glassDatePicker1_Load(object sender, EventArgs e)
        {

        }

        private void panelGenerateKey_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dgvLicenseProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            SelectProductRow(e.RowIndex);
        }

    }
}

