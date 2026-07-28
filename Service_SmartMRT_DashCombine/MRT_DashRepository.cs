using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using System.Runtime.CompilerServices;

namespace SmartMRTDashService_Combine
{
    public class MRT_DashRepository
    {
        // ─────────────────────────────────────────────
        // Step 1 — Load flat records from global DB
        // ─────────────────────────────────────────────
        public async Task<List<LineModuleRecord>> GetLineConfigsAsync(string globalDbConnectionString)
        {
            var records = new List<LineModuleRecord>();

            using var connection = new SqlConnection(globalDbConnectionString);
            await connection.OpenAsync();

            const string sql = @"
            SELECT L.LINEID,
                   L.LINE_NUM,
                   L.IP_ADDRESS,
                   L.STATUS_ACTIVE,
                   V.MODULECODE
            FROM   DC_LINEDETAIL L
            LEFT JOIN DC_VIEWLINE V ON L.LINEID = V.LINEID
            WHERE  L.STATUS_ACTIVE = 1;";

            using var command = new SqlCommand(sql, connection);
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                records.Add(new LineModuleRecord
                {
                    LineId = reader.GetInt32(0),
                    LineNum = reader.GetInt32(1),
                    IpAddress = reader.GetString(2),
                    StatusActive = reader.GetBoolean(3),
                    // MODULECODE can be NULL (LEFT JOIN) — handle it safely
                    ModuleCode = reader.IsDBNull(4) ? "" : reader.GetString(4)
                });
            }

            return records;
        }

        public async Task<LineDashboardData> BuildProductionOutput(LineConfig line)
        {
            string localConn =
                MRT_ConnectionMain.GetLineLocalDBConnection(line.IpAddress);


            using var connection = new SqlConnection(localConn);

            await connection.OpenAsync();


            // 1. Get running styles
            var styles = await GetRunningStylesAsync(connection);


            int totalOutput = 0;

            foreach (var style in styles)
            {
                int styleOutput = 0;

                var stations = await GetStationOpcodeAsync(
                    connection,
                    style.DesignCode,
                    style.ProdId);


                foreach (var station in stations)
                {
                    int output = await GetTodayOutputAsync(
                        connection,
                        DateTime.Today,
                        style.DesignCode,
                        style.ProdId,
                        station.StationId,
                        station.OpCode);

                    styleOutput += output;
                }


                style.Output = styleOutput;

                totalOutput += styleOutput;
            }


            return new LineDashboardData
            {
                LineNum = line.LineNum,
                ModuleCode = "V_PRODOUTPUT",
                TotalOutput = totalOutput,
                Styles = styles
            };
        }

        public async Task<ProductionChartData> BuildProductionChart(LineConfig line)
        {
            string localConn =
                MRT_ConnectionMain.GetLineLocalDBConnection(line.IpAddress);


            using var connection = new SqlConnection(localConn);

            await connection.OpenAsync();


            var styles = await GetRunningStylesAsync(connection);


            int totalOutput = 0;
            int totalDefect = 0;


            foreach (var style in styles)
            {
                int styleOutput = 0;


                var stations = await GetStationOpcodeAsync(
                    connection,
                    style.DesignCode,
                    style.ProdId);


                foreach (var station in stations)
                {
                    int output =
                        await GetTodayOutputAsync(
                            connection,
                            DateTime.Today,
                            style.DesignCode,
                            style.ProdId,
                            station.StationId,
                            station.OpCode);


                    styleOutput += output;
                }


                style.Output = styleOutput;

                totalOutput += styleOutput;


                // Example
                // totalDefect += await GetDefectAsync(...)
            }


            int target = await GetTargetAsync(line);


            return new ProductionChartData
            {
                LineNum = line.LineNum,
                Output = totalOutput,
                Target = target,
                Defect = totalDefect,
                Efficiency = target == 0
                    ? 0
                    : (double)totalOutput / target * 100
            };
        }

        private async Task<List<RunningStyle>> GetRunningStylesAsync(SqlConnection connection)
        {
            var styles = new List<RunningStyle>();

            const string sql = @"
            SELECT DISTINCT PRODID, DESIGNCODE
            FROM   EWL
            WHERE  CURWKDATE = @Today";

            using var cmd = new SqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@Today", DateTime.Today);

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                styles.Add(new RunningStyle
                {
                    ProdId = reader["PRODID"].ToString() ?? "",
                    DesignCode = reader["DESIGNCODE"].ToString() ?? ""
                });
            }

            return styles;
        }

        /// <summary>Returns station assignments for a given style.</summary>
        private async Task<List<(string StationId, string OpCode)>> GetStationOpcodeAsync(
            SqlConnection connection,
            string designCode,
            string prodId)
        { 
            var results = new List<(String, string)>();

           // using var connection = new SqlConnection(localConn);
           // await connection.OpenAsync();

            const string sql = @"
            SELECT STATIONID, OPCODE
            FROM   [ONLINE_LOCALDB].[dbo].[STATIONASSIGN]
            WHERE  PRODID      = @ProdId
            AND  DESIGNCODE  = @DesignCode AND SEQNO = (SELECT TOP 1 (SEQNO) FROM   [ONLINE_LOCALDB].[dbo].[STATIONASSIGN] WHERE  PRODID  = @ProdId
            AND  DESIGNCODE  = @DesignCode ORDER BY SEQNO DESC)";

            using var cmd = new SqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@ProdId", prodId);
            cmd.Parameters.AddWithValue("@DesignCode", designCode);

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
                results.Add((reader.GetString(0), reader.GetString(1)));

            return results;
        }

        public async Task<int> GetTodayOutputAsync(
            SqlConnection connection,
            DateTime today,
            string designCode,
            string prodId,
            string stationId,
            string opcode)
        {
           // using var connection = new SqlConnection(connectionString);
            //await connection.OpenAsync();

            const string sql = @"
            SELECT COALESCE(SUM(PIECECNT),0)
            FROM EWL
            WHERE PRODID = @ProdId
            AND DESIGNCODE = @DesignCode
            AND STATIONID = @StationId
            AND OPCODE = @Opcode
            AND CURWKDATE = @Today";

            using var cmd = new SqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@ProdId", prodId);
            cmd.Parameters.AddWithValue("@DesignCode", designCode);
            cmd.Parameters.AddWithValue("@StationId", stationId);
            cmd.Parameters.AddWithValue("@Opcode", opcode);
            cmd.Parameters.AddWithValue("@Today", today.Date);

            var result = await cmd.ExecuteScalarAsync();
            return Convert.ToInt32(result);
        }
    }

}
