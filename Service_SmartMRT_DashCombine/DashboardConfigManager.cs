using System;
using System.IO;
using System.Text.Json;

namespace SmartMRTDashService_Combine
{
    public class DashboardConfigManager
    {
        public DashboardDatabaseConfig? DashboardDatabase { get; set; }
        public DashboardSettings? Dashboard { get; set; }

        public static DashboardConfigManager Load()
        {
            string configPath = Path.Combine(
                Environment.GetFolderPath(
                    Environment.SpecialFolder.CommonApplicationData),
                "SmartMRT_Dashboard",
                "Config",
                "DashboardConfig.json");

            if (!File.Exists(configPath))
            {
                throw new FileNotFoundException(
                    "DashboardConfig.json was not found.",
                    configPath);
            }

            string json = File.ReadAllText(configPath);

            return JsonSerializer.Deserialize<DashboardConfigManager>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? throw new InvalidOperationException(
                    "Failed to load DashboardConfig.json.");
        }
    }

    public class DashboardDatabaseConfig
    {
        public string Server { get; set; } = "";
        public string Instance { get; set; } = "";
        public string Database { get; set; } = "";
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
        public bool IsLocalDatabase { get; set; }
    }

    public class DashboardSettings
    {
        public int RefreshInterval { get; set; }
        public int SignalRPort { get; set; }
    }
}