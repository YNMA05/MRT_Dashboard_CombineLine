using Microsoft.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace SmartMRTDashService_Combine
{
    public class MRT_DashRepository
    {
        public async Task<List<LineConfig>> GetLineConfigsAsync(string globalDbConnectionString)
        {
            var lineConfigs = new List<LineConfig>();
            using (var connection = new SqlConnection(globalDbConnectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT LINEID, LINE_NUM, IP_ADDRESS,STATUS_ACTIVE FROM DC_LINEDETAIL", connection);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var lineConfig = new LineConfig
                        {
                            LineId = reader.GetInt32(0),
                            LineNum = reader.GetInt32(1),
                            IpAddress = reader.GetString(2),
                            StatusActive = reader.GetBoolean(3),
                        };
                        lineConfigs.Add(lineConfig);
                    }
                }
            }
            return lineConfigs;
        }

        public async Task<LineDashboardData> BuildDashboardDataAsync(LineConfig line)
        {
            string localConn = MRT_ConnectionMain.GetLineLocalDBConnection(line.IpAddress);
            string globalConn = MRT_ConnectionMain.GetLineGlobalDBConnection(line.IpAddress);
            var styles = await GetRunningStylesAsync(localConn, DateTime.Today);
            int output = await GetTodayOutputAsync(localConn, DateTime.Today);

            return new LineDashboardData
            {
                LineNum = line.LineNum,
                output = output
            };

        }

        public async Task<string> GetRunningStyleAsync(localConn, DateTime.Today);
        {
         using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

        string sql = @"
            SELECT COALESCE(SUM(PPH),0)
            FROM HANGERRECORD
            WHERE CURWKDATE = @Today";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Today", today.Date);

        }

        public async Task<int> GetTodayOutputAsync(string connectionString,DateTime today)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string sql = @"
            SELECT COALESCE(SUM(PPH),0)
            FROM HANGERRECORD
            WHERE CURWKDATE = @Today";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Today", today.Date);

                    return (int)await command.ExecuteScalarAsync();
                }
            }
        }
    }
}
