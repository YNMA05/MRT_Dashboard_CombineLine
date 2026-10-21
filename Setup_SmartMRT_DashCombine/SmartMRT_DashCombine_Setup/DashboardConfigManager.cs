using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows.Forms;

namespace SmartMRT_DashCombine_Setup
{
    public static class DashboardConfigManager
    {
        private static readonly string ConfigDirectory =
            Path.Combine(
                Environment.GetFolderPath(
                    Environment.SpecialFolder.CommonApplicationData),
                "SmartMRT_Dashboard",
                "Config");

        private static readonly string ConfigFile =
            Path.Combine(
                ConfigDirectory,
                "DashboardConfig.json");

        //public static DashboardConfig Load()
        //{
        //    if (!File.Exists(ConfigFile))
        //    {
        //        MessageBox.Show(
        //            "Configuration file not found:\n\n" +
        //            ConfigFile);

        //        return null;
        //    }

        //    string json =
        //        File.ReadAllText(ConfigFile);

        //    if (string.IsNullOrWhiteSpace(json))
        //    {
        //        MessageBox.Show(
        //            "DashboardConfig.json is empty.");

        //        return null;
        //    }

        //    DashboardConfig config =
        //        JsonConvert.DeserializeObject<DashboardConfig>(
        //            json);

        //    return config;
        //}

        public static DashboardConfig Load()
        {
            if (!File.Exists(ConfigFile))
                return null;

            string json =
                File.ReadAllText(ConfigFile);

            if (string.IsNullOrWhiteSpace(json))
                return null;

            return JsonConvert.DeserializeObject<DashboardConfig>(
                json);
        }

        public static bool Exists()
        {
            return File.Exists(ConfigFile);
        }

        //public static CheckExist()
        //{
        //    if (!DashboardConfigManager.Exists())
        //    {
        //        MessageBox.Show(
        //            "Dashboard configuration was not found.\n\n" +
        //            "Please configure the database connection.",
        //            "Configuration Required",
        //            MessageBoxButtons.OK,
        //            MessageBoxIcon.Warning);

        //        UC_MRT_DashCombine_DB dbPage =
        //            new UC_MRT_DashCombine_DB();

        //        LoadPage(dbPage);

        //        dbPage.SetEditMode(true);

        //        return;
        //    }
        //}
        public static bool Save(DashboardConfig config)
        {
            try
            {
                if (config == null)
                    return false;

                if (!Directory.Exists(ConfigDirectory))
                {
                    Directory.CreateDirectory(ConfigDirectory);
                }

                string json =
                    JsonConvert.SerializeObject(
                        config,
                        Formatting.Indented);

                File.WriteAllText(
                    ConfigFile,
                    json);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Failed to save DashboardConfig.json.\n\n" +
                    ex.Message,
                    "Configuration Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return false;
            }
        }
    }
}