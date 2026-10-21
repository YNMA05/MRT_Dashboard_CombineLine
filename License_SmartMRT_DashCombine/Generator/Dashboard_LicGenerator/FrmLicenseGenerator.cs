using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dashboard_LicGenerator
{
    public partial class FrmLicenseGenerator : Form
    {
        // LicensePackage matchedPackage;
        public FrmLicenseGenerator()
        {
            InitializeComponent();
            //    foreach (var package in packages)
            //    {
            //        string combinedText = $"{package.LicenseID} - {package.LicenseType} - {package.Description}";

            //        //var LicenseData = packages.Select(p => p.LicenseID).Distinct().ToArray();
            //        ddlPackage.Items.Add(combinedText);
            //    }
            //}
            foreach (var p in packages)
            {
                // Change this string format to change how the label looks!
                string labelText = $"{p.LicenseID} - {p.LicenseType} - {p.Description}";

                ddlPackage.Items.Add(new LicenseDropDownItem(p, labelText));
            }
        }

        public class LicenseDropDownItem
        {
            // The underlying package data
            public LicensePackage Package { get; set; }

            // The custom label format you want to show
            public string DisplayText { get; set; }

            public LicenseDropDownItem(LicensePackage package, string displayText)
            {
                Package = package;
                DisplayText = displayText;
            }

            // WinForms ComboBox uses ToString() to render the item label
            public override string ToString()
            {
                return DisplayText;
            }
        }
        private readonly List<LicensePackage> packages = new List<LicensePackage>()
        {
            new LicensePackage
            {
                LicenseID = 1,
                LicenseGroup = "FULL",
                LicenseType = "FBASIC_DC",
                Dashboard = "COMBINED",
                MaxLines = 2,
                MaxPages = "ALL",
                Description = "Connect 2 Lines"
            },
            new LicensePackage
            {
                LicenseID = 2,
                LicenseGroup = "FULL",
                LicenseType = "FSTANDARD_DC",
                Dashboard = "COMBINED",
                MaxLines = 5,
                MaxPages = "ALL",
                Description = "Connect 5 Lines"
            },
            new LicensePackage
            {
                LicenseID = 3,
                LicenseGroup = "FULL",
                LicenseType = "FFULL_DC",
                Dashboard = "COMBINED",
                MaxLines = 10,
                MaxPages = "ALL",
                Description = "Connect 10 Lines"
            },
            new LicensePackage
            {
                LicenseID = 4,
                LicenseGroup = "FULL",
                LicenseType = "FBASIC_DS",
                Dashboard = "STANDARD",
                MaxLines = 1,
                MaxPages = "2",
                Description = "Package for 2 Pages"
            },
            new LicensePackage
            {
                LicenseID = 5,
                LicenseGroup = "FULL",
                LicenseType = "FSTANDARD_DS",
                Dashboard = "STANDARD",
                MaxLines = 1,
                MaxPages = "5",
                Description = "Package for 5 Pages"
            },
            new LicensePackage
            {
                LicenseID = 6,
                LicenseGroup = "FULL",
                LicenseType = "FFULL_DS",
                Dashboard = "STANDARD",
                MaxLines = 1,
                MaxPages = "ALL",
                Description = "All pages (include defect)"
            },
            new LicensePackage
            {
                LicenseID = 7,
                LicenseGroup = "TRIAL",
                LicenseType = "TBASIC_DC",
                Dashboard = "COMBINED",
                MaxLines = 2,
                MaxPages = "ALL",
                Description = "Trial Connect 2 Lines"
            },
            new LicensePackage
            {
                LicenseID = 8,
                LicenseGroup = "TRIAL",
                LicenseType = "TBASIC_DS",
                Dashboard = "STANDARD",
                MaxLines = 1,
                MaxPages = "2",
                Description = "Trial Package for 2 Pages"
            },
            new LicensePackage
            {
                LicenseID = 9,
                LicenseGroup = "TRIAL",
                LicenseType = "TFULL_DC",
                Dashboard = "COMBINED",
                MaxLines = 10,
                MaxPages = "ALL",
                Description = "Trial Connect 10 Lines"
            },
            new LicensePackage
            {
                LicenseID = 10,
                LicenseGroup = "TRIAL",
                LicenseType = "TFULL_DS",
                Dashboard = "STANDARD",
                MaxLines = 1,
                MaxPages = "ALL",
                Description = "Trial All Pages"
            },
            new LicensePackage
            {
                LicenseID = 11,
                LicenseGroup = "TRIAL",
                LicenseType = "TSTANDARD_DC",
                Dashboard = "COMBINED",
                MaxLines = 5,
                MaxPages = "ALL",
                Description = "Trial Connect 5 Lines"
            },
            new LicensePackage
            {
                LicenseID = 12,
                LicenseGroup = "TRIAL",
                LicenseType = "TSTANDARD_DS",
                Dashboard = "STANDARD",
                MaxLines = 1,
                MaxPages = "5",
                Description = "Trial Package 5 Pages"
            },
        };

        private void btnLicenseReq_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    txtLicenseFiles.Text = dlg.FileName;
                    try
                    {
                        string jsonString = File.ReadAllText(dlg.FileName);
                        LicenseRequest package = JsonConvert.DeserializeObject<LicenseRequest>(jsonString);
                        if (package != null)
                        {
                            txtCompany.Text = package.Company.ToString();
                            txtHardwareHash.Text = package.HwHash;
                            txtRequestPackage.Text = package.LicenseID.ToString();
                            txtDateIssuedCustomer.Text = package.IssuedUtc.ToString("yyyy-MM-dd");

                            LicensePackage matchedPackage = packages.FirstOrDefault(p => p.LicenseID == package.LicenseID);

                            if (matchedPackage != null)
                            {
                                string targetPrefix = $"{package.LicenseID}";
                                bool itemFound = false;

                                for (int i = 0; i < ddlPackage.Items.Count; i++)
                                {
                                    // Cast the item back to your wrapper class
                                    if (ddlPackage.Items[i] is LicenseDropDownItem item)
                                    {
                                        if (item.Package.LicenseID == package.LicenseID)
                                        {
                                            ddlPackage.SelectedIndex = i; // Auto-selects the item in the UI
                                            itemFound = true;
                                            break;
                                        }
                                    }
                                }

                                lblLicenseGroup.Text = $"License Group : {matchedPackage.LicenseGroup}";
                                lblDashboard.Text = $"Dashboard : {matchedPackage.Dashboard}";
                                lblMaxLines.Text = $"Max Lines : {matchedPackage.MaxLines}";
                                lblMaxPages.Text = $"Max Pages : {matchedPackage.MaxPages}";

                                if (!itemFound)
                                {
                                    // If it wasn't in the list, you can fallback to assigning text directly
                                    ddlPackage.Text = $"{package.LicenseID} - {matchedPackage.Dashboard} - {matchedPackage.Description}";
                                }
                            
                        }
                            else
                            {
                                ddlPackage.Text = $"{package.LicenseID} - Unknown Package";
                            }
                        }

                    }
                    catch { }
                    //lblStatus.Text = "Selected license: " + dlg.FileName;
                }
            }
        }
        private void ddlPackage_SelectedIndexChanged(object sender, EventArgs e)
        {
            LicenseDropDownItem item =
                ddlPackage.SelectedItem as LicenseDropDownItem;

            if (item == null)
                return;

            LicensePackage package = item.Package;

            if (package == null)
                return;

            lblLicenseGroup.Text =
                $"License Group : {package.LicenseGroup}";

            lblDashboard.Text =
                $"Dashboard : {package.Dashboard}";

            lblMaxLines.Text =
                $"Max Lines : {package.MaxLines}";

            lblMaxPages.Text =
                $"Max Pages : {package.MaxPages}";
            if (string.Equals(package.LicenseGroup,"FULL",StringComparison.OrdinalIgnoreCase))
            {
                // Set a default expiry date
                dateTimePicker2.Value = new DateTime(
                    2099, 12, 31);

                dateTimePicker2.Value = new DateTime(
                    2099, 12, 31,
                    23, 59, 59);

                // Customer cannot change it
                dateTimePicker2.Enabled = false;
                dateTimePicker2.Enabled = false;

                lblExpiryStatus.Text = "PERPETUAL LICENSE";
            }
            else
            {
                // Trial license
                dateTimePicker2.Enabled = true;
                dateTimePicker2.Enabled = true;

                lblExpiryStatus.Text = "TRIAL LICENSE";
            }
        }

        private void btnGenerateLicense_Click(object sender, EventArgs e)
        {
            try
            {
                LicenseDropDownItem item =
                    ddlPackage.SelectedItem as LicenseDropDownItem;

                if (item == null || item.Package == null)
                {
                    MessageBox.Show(
                        "Please select a license package.",
                        "License Generator",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                LicensePackage package = item.Package;

                if (string.IsNullOrWhiteSpace(txtCompany.Text))
                {
                    MessageBox.Show(
                        "Company name is required.",
                        "License Generator",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                if (string.IsNullOrWhiteSpace(txtHardwareHash.Text))
                {
                    MessageBox.Show(
                        "Hardware hash is required.",
                        "License Generator",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                // -----------------------------------------
                // Issue Date
                // -----------------------------------------

                DateTime issuedUtc = DateTime.UtcNow;

                // -----------------------------------------
                // Expiry
                // -----------------------------------------

                string expiryUtc = null;

                if (string.Equals(
                    package.LicenseGroup,
                    "TRIAL",
                    StringComparison.OrdinalIgnoreCase))
                {
                    DateTime expiryLocal =
                        dateTimePicker2.Value.Date +
                        dateTimePicker2.Value.TimeOfDay;

                    expiryUtc = expiryLocal.ToUniversalTime()
                        .ToString("yyyy-MM-ddTHH:mm:ssZ");
                }

                // -----------------------------------------
                // Create payload
                // -----------------------------------------

                LicensePayload payload = new LicensePayload
                {
                    Company = txtCompany.Text.Trim(),
                    HwHash = txtHardwareHash.Text.Trim(),

                    LicenseID = package.LicenseID,
                    LicenseGroup = package.LicenseGroup,
                    LicenseType = package.LicenseType,
                    Dashboard = package.Dashboard,

                    MaxLines = package.MaxLines,
                    MaxPages = package.MaxPages,

                    IssuedUtc = issuedUtc
                        .ToString("yyyy-MM-ddTHH:mm:ssZ"),

                    ExpiryUtc = expiryUtc
                };

                // -----------------------------------------
                // Serialize
                // -----------------------------------------

                string payloadJson =
                    JsonConvert.SerializeObject(payload);

                // -----------------------------------------
                // Sign
                // -----------------------------------------

                string signature =
                    LicenseSigner.SignPayload(payloadJson);

                // -----------------------------------------
                // Create license wrapper
                // -----------------------------------------

                var licenseFile = new
                {
                    LICENSE_PAYLOAD = payloadJson,
                    SIGNATURE = signature
                };

                string licenseJson =
                    JsonConvert.SerializeObject(
                        licenseFile,
                        Formatting.Indented);

                // -----------------------------------------
                // Save
                // -----------------------------------------

                using (SaveFileDialog dlg = new SaveFileDialog())
                {
                    dlg.Filter =
                        "SmartMRT License (*.lic)|*.lic";

                    dlg.FileName =
                        "SmartMRT_License.lic";

                    if (dlg.ShowDialog() != DialogResult.OK)
                        return;

                    File.WriteAllText(
                        dlg.FileName,
                        licenseJson,
                        Encoding.UTF8);

                    MessageBox.Show(
                        "License generated successfully.\n\n" +
                        "File:\n" + dlg.FileName,
                        "License Generated",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Failed to generate license.\n\n" +
                    ex.Message,
                    "License Generator",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
