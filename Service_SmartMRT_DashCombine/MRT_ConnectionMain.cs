using System;
using System.IO;
using System.Text.Json;

namespace SmartMRTDashService_Combine
{
    public class MRT_ConnectionMain
    {
    public static string GetSqlConnectionString(
         string server,
         string instance,
         string username,
         string password,
         string database)
        {
            string dataSource = server;

            if (!string.IsNullOrWhiteSpace(instance))
            {
                dataSource += "\\" + instance;
            }

            return
                $"Server={dataSource};" +
                $"Database={database};" +
                $"User ID={username};" +
                $"Password={password};" +
                $"TrustServerCertificate=True;";
        }

    public static string GetGlobalDBConnection(
        string server,
        string instance)
    {
        return
            $"Server={server}\\{instance};" +
            $"Database=Dashboard_GlobalDB;" +
            $"Integrated Security=True;" +
            $"Connect Timeout=2;" +
            $"TrustServerCertificate=True;";
    }
    public static string GetLineGlobalDBConnection(
 string server)
    {
        return
            $"Server={server}\\SQLEXPRESS;" +
            $"Database=ONLINE_GLOBALDB;" +
            $"User ID=sa;" +
            $"Password=1234;" +
            $"Connect Timeout=2;"+
            $"TrustServerCertificate=True;";
    }
    public static string GetLineLocalDBConnection(
    string server)
    {
        return
            $"Server={server}\\SQLEXPRESS;" +
            $"Database=ONLINE_LOCALDB;" +
            $"User ID=sa;" +
            $"Password=1234;" +
            $"Connect Timeout=2;" +
            $"TrustServerCertificate=True;";
    }

        public static string GetDashboardConnection()
        {
            DashboardConfigManager config = DashboardConfigManager.Load();

            if (config.DashboardDatabase == null)
            {
                throw new InvalidOperationException(
                    "DashboardDatabase settings are missing.");
            }

            var db = config.DashboardDatabase;

            string password = PasswordEncryption.Decrypt(db.Password);

            return GetSqlConnectionString(
                db.Server,
                db.Instance,
                db.Username,
                password,
                db.Database);
        }
    }
}
