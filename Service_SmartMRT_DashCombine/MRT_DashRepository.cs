using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using System.Data;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
                SELECT
                L.LINEID,
                L.LINE_NUM,
                COALESCE(M.IP_ADDRESS, L.IP_ADDRESS) AS DB_IP,
                L.IP_ADDRESS AS PLC_IP,
                L.STATUS_ACTIVE,
                V.MODULECODE
            FROM DC_LINEDETAIL L
            LEFT JOIN DC_VIEWLINE V
                ON L.LINEID = V.LINEID
            LEFT JOIN DC_LINEDETAIL M
                ON M.LINE_NUM = L.MASTER_LINE_ID
            WHERE L.STATUS_ACTIVE = 1;";

            using var command = new SqlCommand(sql, connection);
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                records.Add(new LineModuleRecord
                {
                    LineId = reader.GetInt32(0),
                    LineNum = reader.GetInt32(1),
                    MasterIpAddress = reader.GetString(2),
                    LocalIpAddress = reader.GetString(3),
                    StatusActive = reader.GetBoolean(4),
                    // MODULECODE can be NULL (LEFT JOIN) — handle it safely
                    ModuleCode = reader.IsDBNull(5) ? "" : reader.GetString(5)
                });
            }         
            return records;
        }
        public async Task<bool> CheckLocalConnection(LineConfig line)
        {
            try
            {
                string conn =
                    MRT_ConnectionMain.GetLineLocalDBConnection(line.LocalIpAddress);

                using var connection = new SqlConnection(conn);

                await connection.OpenAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<LineDashboardData> BuildProductionOutput(LineConfig line)
        {
            string localConn =
                MRT_ConnectionMain.GetLineLocalDBConnection(line.LocalIpAddress);

            //string globalConn =
            //    MRT_ConnectionMain.GetLineGlobalDBConnection(line.MasterIpAddress);

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
            // var metrics = await DC_GetBarChartDataAsync(line);
            // var running = await DC_GetRunningTargetAsync(line);
            try
            {
                var localMetrics = await DC_GetBarChartLocalAsync(line);
                var globalMetrics = await DC_GetBarChartGlobalAsync(line, localMetrics.Products, localMetrics.Workers);

                int target = CalculateTarget(
                    globalMetrics.TotalSAM,
                    globalMetrics.NumWorkers,
                    localMetrics.MinWorked,
                    localMetrics.Products.Count);

                int runningTarget = CalculateRunningTarget(
                    target,
                    localMetrics.ElapsedMin,
                    localMetrics.ShiftMin);

                decimal efficiency =
                    target == 0
                        ? 0
                        : Math.Round(
                            (decimal)localMetrics.TotalProduction / target * 100,
                            2);

                return new ProductionChartData
                {
                    LineNum = line.LineNum,
                    Output = localMetrics.TotalProduction,
                    Target = target,
                    RunningTarget = runningTarget,
                    Defect = localMetrics.Defect,
                    Efficiency = efficiency,
                    IsOnline = true
                };
            }
            catch(Exception ex)
            {
                return new ProductionChartData
                {
                    LineNum = line.LineNum,
                    Output = 0,
                    Target = 0,
                    RunningTarget = 0,
                    Defect = 0,
                    Efficiency = 0,
                    IsOnline = false
                };
            }

            }

        private async Task<LocalDataBarChart> DC_GetBarChartLocalAsync(LineConfig line)
        {
            string localConn = MRT_ConnectionMain.GetLineGlobalDBConnection(line.LocalIpAddress);

            using var connection = new SqlConnection(localConn);
            await connection.OpenAsync();
            
            using var cmd = new SqlCommand("DC_GetBarChartLocal", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Date", DateTime.Today);
            cmd.Parameters.AddWithValue("@Line", line.LineNum);
            cmd.Parameters.Add("@TotalProduction", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@MinWorked", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@Defect", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@TotalLoading", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@Shift", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@ShiftStart", SqlDbType.Time).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@ShiftEnd", SqlDbType.Time).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@ElapsedMin", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@ShiftMin", SqlDbType.Int).Direction = ParameterDirection.Output;

            await cmd.ExecuteNonQueryAsync();

            var shift =
            cmd.Parameters["@Shift"].Value == DBNull.Value
                ? 0
                : Convert.ToInt32(cmd.Parameters["@Shift"].Value);

            TimeSpan shiftStart =
            cmd.Parameters["@ShiftStart"].Value == DBNull.Value
                ? TimeSpan.Zero
                : (TimeSpan)cmd.Parameters["@ShiftStart"].Value;

            TimeSpan shiftEnd =
             cmd.Parameters["@ShiftEnd"].Value == DBNull.Value
                 ? TimeSpan.Zero
                 : (TimeSpan)cmd.Parameters["@ShiftEnd"].Value;

            var elapsedMin =
                Convert.ToInt32(cmd.Parameters["@ElapsedMin"].Value);

            var shiftMin =
                Convert.ToInt32(cmd.Parameters["@ShiftMin"].Value);

            using var reader = await cmd.ExecuteReaderAsync();

            var productInfo = new List<ProductInfo>();

            var workers = new List<WorkerInfo>();

            //while (await reader.ReadAsync())
            //{
            //    productInfo.Add(new ProductInfo
            //    {
            //        Mono = reader["MONO"].ToString(),
            //        Prodid = reader["PRODID"].ToString(),
            //        DesignCode = reader["DESIGNCODE"].ToString()

            //    });
            //    workers.Add(new WorkerInfo
            //    {
            //        EmpId = reader["EMPID"].ToString(),
            //        OpCode = reader["OPCODE"].ToString()
            //    });
            //}
            while (await reader.ReadAsync())
            {
                productInfo.Add(new ProductInfo
                {
                    Mono = reader["MONO"].ToString(),
                    DesignCode = reader["DESIGNCODE"].ToString(),
                    Prodid = reader["PRODID"].ToString()
                });
            }
            await reader.NextResultAsync();
            while (await reader.ReadAsync())
            {
                workers.Add(new WorkerInfo
                {
                    EmpId = reader["EMPID"].ToString(),
                    OpCode = reader["OPCODE"].ToString()
                });
            }

            // Very important!
            reader.Close();            

            return new LocalDataBarChart
            {
                TotalProduction = Convert.ToInt32(cmd.Parameters["@TotalProduction"].Value),
                TotalLoading = Convert.ToInt32(cmd.Parameters["@TotalLoading"].Value),
                MinWorked = Convert.ToInt32(cmd.Parameters["@MinWorked"].Value),
                Defect = Convert.ToInt32(cmd.Parameters["@Defect"].Value),
                Shift = shift,
                ShiftStart = shiftStart,
                ShiftEnd = shiftEnd,
                ElapsedMin = elapsedMin,
                ShiftMin = shiftMin,
                Products =productInfo,
                Workers = workers,
            };
        }

        private async Task<GlobalDataBarChart> DC_GetBarChartGlobalAsync(
            LineConfig line,
            List<ProductInfo> products,
            List<WorkerInfo> workers)
        {
            string globalConn = MRT_ConnectionMain.GetLineGlobalDBConnection(line.MasterIpAddress);

            using var connection = new SqlConnection(globalConn);
            await connection.OpenAsync();

            var productTable = new DataTable();
            productTable.Columns.Add("MONO", typeof(string));
            productTable.Columns.Add("DESIGNCODE", typeof(string));
            productTable.Columns.Add("PRODID", typeof(string));

            foreach (var item in products)
            {
                productTable.Rows.Add(
                    item.Mono,
                    item.DesignCode,
                    item.Prodid);
            }

            var workerTable = new DataTable();
            workerTable.Columns.Add("EMPID", typeof(string));
            workerTable.Columns.Add("OPCODE", typeof(string));

            foreach (var item in workers)
            {
                workerTable.Rows.Add(
                    item.EmpId,
                    item.OpCode);
            }

            using var cmd = new SqlCommand("DC_GetBarChartGlobal", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            var p1 = cmd.Parameters.AddWithValue("@Products", productTable);
            p1.SqlDbType = SqlDbType.Structured;
            p1.TypeName = "dbo.ProductInfoType";

            var p2 = cmd.Parameters.AddWithValue("@Workers", workerTable);
            p2.SqlDbType = SqlDbType.Structured;
            p2.TypeName = "dbo.WorkerInfoType";

            var sam = cmd.Parameters.Add("@TotalSAM", SqlDbType.Decimal);
            sam.Precision = 18;
            sam.Scale = 2;
            sam.Direction = ParameterDirection.Output;

            cmd.Parameters.Add("@NumWorkers", SqlDbType.Int)
                          .Direction = ParameterDirection.Output;

            await cmd.ExecuteNonQueryAsync();

            return new GlobalDataBarChart
            {
                TotalSAM = Convert.ToDecimal(cmd.Parameters["@TotalSAM"].Value),
                NumWorkers = Convert.ToInt32(cmd.Parameters["@NumWorkers"].Value)
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
            var results = new List<(string, string)>();

           // using var connection = new SqlConnection(localConn);
           // await connection.OpenAsync();

            const string sql = @"
            SELECT STATIONID, OPCODE
            FROM   [ONLINE_LOCALDB].[dbo].[STATIONASSIGN]
            WHERE  PRODID      = @ProdId
            AND  DESIGNCODE  = @DesignCode AND SEQNO = (SELECT TOP 1 (SA.SEQNO) FROM  [ONLINE_LOCALDB].[dbo].[STATIONASSIGN] SA LEFT JOIN [ONLINE_LOCALDB].[dbo].[BRIDGESTNID] B ON B.PRODLINE = SA.PRODLINE
            AND B.BRIDGESTNID = SA.STATIONID WHERE  SA.PRODID  = @ProdId
            AND  SA.DESIGNCODE  = @DesignCode AND B.BRIDGESTNID IS NULL ORDER BY SA.SEQNO DESC)";

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

        private int CalculateTarget(decimal totalSAM,int numWorkers,int minWorked,int monoCount)
        {
            if (totalSAM <= 0 || numWorkers <= 0 || minWorked <= 0 || monoCount <= 0)
                return 0;

            decimal totalHourWork = numWorkers * minWorked;

            int target = (int)((totalHourWork / totalSAM) * monoCount * 0.85m);

            return target;
        }

        private int CalculateRunningTarget(int Target, int ElapsedMin, int ShiftMin)
        {
            if (Target <= 0 || ElapsedMin <= 0 || ShiftMin <= 0)
                return 0;
            decimal runningTarget =((decimal)Target / ShiftMin) * ElapsedMin;

            return (int)runningTarget;
        }

    }


}
