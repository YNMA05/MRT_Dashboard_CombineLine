using Newtonsoft.Json;
using System.IO;
using System.Windows.Forms;

namespace SmartMRT_DashCombine_Setup
{
    public static class DashboardConfigManager
    {
        private static readonly string ConfigFile =
            Path.Combine(
                Application.StartupPath,
                "DashboardConfig.json");

        public static DashboardConfig Load()
        {
            if (!File.Exists(ConfigFile))
            {
                MessageBox.Show(
                    "Configuration file not found:\n\n" +
                    ConfigFile);

                return null;
            }

            string json =
                File.ReadAllText(ConfigFile);

            if (string.IsNullOrWhiteSpace(json))
            {
                MessageBox.Show(
                    "DashboardConfig.json is empty.");

                return null;
            }

            DashboardConfig config =
                JsonConvert.DeserializeObject<DashboardConfig>(
                    json);

            return config;
        }
    }
}