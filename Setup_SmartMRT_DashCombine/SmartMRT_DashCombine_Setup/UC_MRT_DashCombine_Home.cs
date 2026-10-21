using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using Newtonsoft.Json;

namespace SmartMRT_DashCombine_Setup
{
    public partial class UC_MRT_DashCombine_Home : UserControl
    {
        private System.Windows.Forms.Timer homeStatusTimer;
        private bool isCheckingHomeStatus = false;

        private readonly HttpClient serviceHttpClient = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5000/")
        };
        public UC_MRT_DashCombine_Home()
        {
            InitializeComponent();
            homeStatusTimer = new System.Windows.Forms.Timer();
            homeStatusTimer.Interval = 10000;
            homeStatusTimer.Tick += async (sender, e) =>
            {
                await RefreshHomeStatusAsync();
            };

            _ = RefreshHomeStatusAsync();
            homeStatusTimer.Start();

        }

        private class ServiceStatusResponse
        {
            public int ConnectedClients { get; set; }

            public DateTime? LastBroadcastUtc { get; set; }
        }

        private void UC_MRT_DashCombine_Home_Load(object sender, EventArgs e)
        {


        }


        private async Task RefreshHomeStatusAsync()
        {
            if (isCheckingHomeStatus)
                return;

            isCheckingHomeStatus = true;

            try
            {
                // Check SQL
                bool sqlConnected =
                    await CheckDashboardSqlConnectionAsync();

                lblSqlStatus.Text =
                    sqlConnected
                        ? "✔ Connected"
                        : "❌ Disconnected";


                // Check Dashboard Service
                ServiceStatusResponse serviceStatus =
                    await GetDashboardServiceStatusAsync();

                if (serviceStatus == null)
                {
                    lblServiceStatus.Text =
                        "❌ Disconnected";

                    lblSignalStatus.Text =
                        "0";

                    lblBroadcastStatus.Text =
                        "No connection";
                }
                else
                {
                    lblServiceStatus.Text =
                        "✔ Running";

                    lblSignalStatus.Text =
                        serviceStatus.ConnectedClients.ToString();

                    if (serviceStatus.LastBroadcastUtc.HasValue)
                    {
                        DateTime localTime =
                            serviceStatus.LastBroadcastUtc.Value.ToLocalTime();

                        lblBroadcastStatus.Text =
                            localTime.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    else
                    {
                        lblBroadcastStatus.Text =
                            "No broadcast yet";
                    }
                }

                bool License = await CheckLicenseTypeAsync();
                lblLicenseStatus.Text = License ? "✔ Valid" : "❌ Not Valid";

                lblExpiryStatus.Text = await CheckExpiryDateAsync();
            }
            finally
            {
                isCheckingHomeStatus = false;
            }
        }

        private async Task<bool> CheckDashboardSqlConnectionAsync()
        {
            try
            {
                string connectionString =
                    ConnectionMain.GetDashboardConnection();

                using (SqlConnection conn =
                    new SqlConnection(connectionString))
                {
                    await conn.OpenAsync();

                    using (SqlCommand cmd =
                        new SqlCommand("SELECT 1", conn))
                    {
                        await cmd.ExecuteScalarAsync();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Logging.FolderLog.Error(
                    "Dashboard SQL connection check failed: " +
                    ex.Message);

                return false;
            }

        }

        private async Task<ServiceStatusResponse> GetDashboardServiceStatusAsync()
        {
            try
            {
                using (HttpResponseMessage response =
                    await serviceHttpClient.GetAsync("api/status"))
                {
                    if (!response.IsSuccessStatusCode)
                        return null;

                    string json =
                        await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<ServiceStatusResponse>(json);

                }
            }
            catch (Exception ex)
            {
                Logging.FolderLog.Error(
                    "Dashboard Service status check failed: " +
                    ex.Message);

                return null;
            }
        }

        private async Task<bool> CheckLicenseTypeAsync()
        {
            try
            {
                string connectionString =
                                  ConnectionMain.GetDashboardConnection();

                using (SqlConnection conn =
                    new SqlConnection(connectionString))
                {
                    await conn.OpenAsync();

                    using (SqlCommand cmd =
                        new SqlCommand("SELECT TOP 1 ID FROM DC_SETUP", conn))
                    {
                        await cmd.ExecuteScalarAsync();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Logging.FolderLog.Error(
                    "Failed to check license type " + ": " + ex.Message);

                return false;
            }
        }

        private async Task<string> CheckExpiryDateAsync()
        {
            try
            {
                string connectionString =
                    ConnectionMain.GetDashboardConnection();

                using (SqlConnection conn =
                    new SqlConnection(connectionString))
                {
                    await conn.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand(
                        @"SELECT TOP 1 S.EXPIRY_UTC
                  FROM DC_SETUP S
                  INNER JOIN DC_LICENSEDETAIL D
                      ON S.LICENSE_TYPE = D.LICENSE_TYPE
                  WHERE D.LICENSE_GROUP = 'TRIAL'
                    AND S.IS_ACTIVE = 1
                  ORDER BY S.ID DESC", conn))
                    {
                        object result = await cmd.ExecuteScalarAsync();

                        if (result == null || result == DBNull.Value)
                            return "No Expiry Date";

                        DateTime expiryDate = Convert.ToDateTime(result);

                        return expiryDate.ToString("dd/MM/yyyy");
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.FolderLog.Error(
                    "Failed to check license expiry date: " + ex.Message);

                return "Unable to Check";
            }
        }
    }
    
}
