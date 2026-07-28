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
            $"TrustServerCertificate=True;";
    }
}
}
