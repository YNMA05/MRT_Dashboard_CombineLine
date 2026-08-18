using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using System.Data;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Diagnostics;

namespace SmartMRTDashService_Combine
{

    public class MRT_DashRepository
    {
        private readonly ILogger<MRT_DashRepository> _logger;

        public MRT_DashRepository(
        ILogger<MRT_DashRepository> logger)
        {
            _logger = logger;
        }
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


        //public async Task<bool> CheckLocalConnection(LineConfig line)
        //{
        //    try
        //    {
        //        string conn =
        //            MRT_ConnectionMain.GetLineLocalDBConnection(line.LocalIpAddress);

        //        using var connection = new SqlConnection(conn);

        //        await connection.OpenAsync();

        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}

        public async Task<bool> CheckLocalConnection(LineConfig line)
        {
            var sw = Stopwatch.StartNew();

            try
            {
                string conn =
                    MRT_ConnectionMain.GetLineLocalDBConnection(line.LocalIpAddress);

                using var connection = new SqlConnection(conn);

                await connection.OpenAsync();

                sw.Stop();

                _logger.LogInformation(
                    "Line {Line}: SQL connection OK in {Ms} ms. IP={IP}",
                    line.LineNum,
                    sw.ElapsedMilliseconds,
                    line.LocalIpAddress);

                return true;
            }
            catch (Exception ex)
            {
                sw.Stop();

                _logger.LogError(
                    ex,
                    "Line {Line}: SQL connection FAILED after {Ms} ms. IP={IP}",
                    line.LineNum,
                    sw.ElapsedMilliseconds,
                    line.LocalIpAddress);

                return false;
            }
        }


        public async Task<AllProductionData> BuildAllProduction(LineConfig line)
        {
            try
            {
                var data = await GetLineDataAsync(line);

                return CalculateAllProduction(
                    line,
                    data.Local,
                    data.Global);
            }
            catch (Exception ex)
            {
                return new AllProductionData
                {
                    LineNum = line.LineNum,
                    Output = 0,
                    TotalHangerIn = 0,
                    Target = 0,
                    RunningTarget = 0,
                    Defect = 0,
                    Efficiency = 0,
                    IsOnline = false
                };
            }
        }

        public async Task<(LocalData Local, GlobalData Global)> GetLineDataAsync(LineConfig line)
        {
           // string localConn = MRT_ConnectionMain.GetLineLocalDBConnection(line.LocalIpAddress);
           // string globalConn = MRT_ConnectionMain.GetLineGlobalDBConnection(line.MasterIpAddress);

            LocalData localData = await DC_GetLocalDataAsync(line);
            GlobalData globalData = await DC_GetGlobalDataAsync(line,localData.Products,localData.Workers);

            return(localData,globalData);
        }

        public AllProductionData CalculateAllProduction(
        LineConfig line,
        LocalData local,
        GlobalData global)
        {
            int target = CalculateTarget(
                global.TotalSAM,
                global.NumWorkers,
                local.MinWorked,
                local.Products.Count);

            int runningTarget = CalculateRunningTarget(
                target,
                local.ElapsedMin,
                local.MinWorked);

            decimal efficiency =
                target == 0
                    ? 0
                    : Math.Round(
                        (decimal)local.TotalProduction / target * 100,
                        2);

            decimal defect =
                local.TotalProduction == 0
                    ? 0
                    : Math.Round(
                        (decimal)local.Defect / local.TotalProduction * 100,
                        2);

            var validOpcodes = new HashSet<string>(
                global.OpcodeShow.Select(x => x.Opcode),
                StringComparer.OrdinalIgnoreCase);

            var excludedOpcodes = new HashSet<string>(
                local.Datas
                    .SelectMany(x => new[]
                    {
            x.loadCode,
            x.unloadCode
                    })
                    .Where(x => !string.IsNullOrWhiteSpace(x)),
                StringComparer.OrdinalIgnoreCase);

            int totalHangerIn = local.Workers
                .Where(w => validOpcodes.Contains(w.OpCode))
                .Where(w => !excludedOpcodes.Contains(w.OpCode))
                .GroupBy(w => w.StnId)
                .Sum(g => g.First().HangerIn);

            //int totalPieceCnt = local.Workers
            //    .Where(w => validOpcodes.Contains(w.OpCode))
            //    .Sum(w => w.PieceCnt);


            return new AllProductionData
            {
                LineNum = line.LineNum,
                Output = local.TotalProduction,
                TotalHangerIn = totalHangerIn,
                Target = target,
                RunningTarget = runningTarget,
                Defect = defect,
                Efficiency = efficiency,
                IsOnline = true
            };
        }



        private async Task<LocalData> DC_GetLocalDataAsync(LineConfig line)
        {
            var sw = Stopwatch.StartNew();
            try
            {
                _logger.LogInformation(
                    "START DC_GetLocalData Line {Line}",
                    line.LineNum);

                string localConn = MRT_ConnectionMain.GetLineGlobalDBConnection(line.LocalIpAddress);
                using var connection = new SqlConnection(localConn); await connection.OpenAsync();

                _logger.LogInformation(
           "Line {Line}: SQL connection opened in {Ms} ms",
           line.LineNum,
           sw.ElapsedMilliseconds);


                using var cmd = new SqlCommand("DC_GetDataLocal", connection);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Date", DateTime.Today);
                cmd.Parameters.AddWithValue("@Line", line.LineNum);
                cmd.Parameters.Add("@TotalProduction", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@MinWorked", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@Defect", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@TotalLoading", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@Shift", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@ShiftStartTime", SqlDbType.Time).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@ShiftEndTime", SqlDbType.Time).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@ShiftOTEndTime", SqlDbType.Time).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@ElapsedMin", SqlDbType.Int).Direction = ParameterDirection.Output;
                await cmd.ExecuteNonQueryAsync();

                var shift =
                cmd.Parameters["@Shift"].Value == DBNull.Value
                    ? 0
                    : Convert.ToInt32(cmd.Parameters["@Shift"].Value);

                TimeSpan shiftStart =
                cmd.Parameters["@ShiftStartTime"].Value == DBNull.Value
                    ? TimeSpan.Zero
                    : (TimeSpan)cmd.Parameters["@ShiftStartTime"].Value;

                TimeSpan shiftEnd =
                 cmd.Parameters["@ShiftEndTime"].Value == DBNull.Value
                     ? TimeSpan.Zero
                     : (TimeSpan)cmd.Parameters["@ShiftEndTime"].Value;

                TimeSpan shiftOTEnd =
                cmd.Parameters["@ShiftOTEndTime"].Value == DBNull.Value
                    ? TimeSpan.Zero
                    : (TimeSpan)cmd.Parameters["@ShiftOTEndTime"].Value;

                var elapsedMin = cmd.Parameters["@ElapsedMin"].Value == DBNull.Value
                    ? 0 :
                    Convert.ToInt32(cmd.Parameters["@ElapsedMin"].Value);

                using var reader = await cmd.ExecuteReaderAsync();

                var productInfo = new List<ProductInfo>();

                var workers = new List<WorkerInfo>();

                var datas = new List<DataInfo>();

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
                        OpCode = reader["OPCODE"].ToString(),
                        DesignCode = reader["DESIGNCODE"].ToString(),
                        Prodid = reader["PRODID"].ToString(),
                        PieceCnt = Convert.ToInt32(reader["PIECECNT"].ToString()),
                        SeqNo = Convert.ToInt32(reader["SEQNO"].ToString()),
                        StnId = reader["STATIONID"].ToString(),
                        HangerIn = Convert.ToInt32(reader["HANGERIN"].ToString())

                    });
                }
                await reader.NextResultAsync();
                await reader.NextResultAsync();

                while (await reader.ReadAsync())
                {
                    datas.Add(new DataInfo
                    {
                        prodid = reader["ProdID"]?.ToString(),

                        loadCode = reader["LoadingOpcode"]?.ToString(),

                        loadSeq = reader["LoadingSeq"] == DBNull.Value
                            ? 0
                            : Convert.ToInt32(reader["LoadingSeq"]),

                        unloadCode = reader["UnloadingOpcode"]?.ToString(),

                        unloadSeq = reader["UnloadingSeq"] == DBNull.Value
                            ? 0
                            : Convert.ToInt32(reader["UnloadingSeq"]),

                        loadQty = reader["LoadingQty"] == DBNull.Value
                            ? 0
                            : Convert.ToInt32(reader["LoadingQty"]),

                        unloadQty = reader["ProductionQty"] == DBNull.Value
                            ? 0
                            : Convert.ToInt32(reader["ProductionQty"])
                    });
                }
                _logger.LogInformation(
    "Line {Line}: TotalProduction={TotalProduction}, MinWorked={MinWorked}, Defect={Defect}, TotalLoading={TotalLoading}, Shift={Shift}, ShiftStart={ShiftStart}, ShiftEnd={ShiftEnd}, ElapsedMin={ElapsedMin}",
    line.LineNum,
    cmd.Parameters["@TotalProduction"].Value,
    cmd.Parameters["@MinWorked"].Value,
    cmd.Parameters["@Defect"].Value,
    cmd.Parameters["@TotalLoading"].Value,
    cmd.Parameters["@Shift"].Value,
    cmd.Parameters["@ShiftStartTime"].Value,
    cmd.Parameters["@ShiftEndTime"].Value,
    cmd.Parameters["@ElapsedMin"].Value
    );

                // Very important!
                reader.Close();
                _logger.LogInformation(
            "Line {Line}: DC_GetDataLocal completed in {Ms} ms",
            line.LineNum,
            sw.ElapsedMilliseconds);

                sw.Stop();

                _logger.LogInformation(
                    "END DC_GetLocalData Line {Line}: {Ms} ms",
                    line.LineNum,
                    sw.ElapsedMilliseconds);

                return new LocalData
                {
                    TotalProduction = cmd.Parameters["@TotalProduction"].Value == DBNull.Value ? 0 : Convert.ToInt32(cmd.Parameters["@TotalProduction"].Value),
                    TotalLoading = cmd.Parameters["@TotalLoading"].Value == DBNull.Value ? 0 : Convert.ToInt32(cmd.Parameters["@TotalLoading"].Value),
                    MinWorked = cmd.Parameters["@MinWorked"].Value == DBNull.Value ? 0 : Convert.ToInt32(cmd.Parameters["@MinWorked"].Value),
                    Defect = cmd.Parameters["@Defect"].Value == DBNull.Value ? 0 : Convert.ToInt32(cmd.Parameters["@Defect"].Value),
                    Shift = shift,
                    ShiftStart = shiftStart,
                    ShiftEnd = shiftEnd,
                    ShiftOTEnd = shiftOTEnd,
                    ElapsedMin = elapsedMin,
                    Products = productInfo,
                    Workers = workers,
                    Datas = datas
                };

            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "DC_GetLocalData FAILED Line {Line}",
                    line.LineNum);

                throw;
            }
        }

        private async Task<GlobalData> DC_GetGlobalDataAsync(
          LineConfig line,
          List<ProductInfo> products,
          List<WorkerInfo> workers)
        {
            string globalConn = MRT_ConnectionMain.GetLineGlobalDBConnection(line.MasterIpAddress);

            using var connection = new SqlConnection(globalConn);
            await connection.OpenAsync();
            
            var sw = Stopwatch.StartNew();

            _logger.LogInformation(
                "START DC_GetGlobalData Line {Line}, MasterIP={IP}",
                line.LineNum,
                line.MasterIpAddress);

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

            using var cmd = new SqlCommand("DC_GetDataGlobal", connection);
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

            var numWorkers = cmd.Parameters.Add(
        "@NumWorkers",
        SqlDbType.Int);

            numWorkers.Direction =
                ParameterDirection.Output;

            var opcodes = new List<OpcodeInfo>();

            // Execute the stored procedure ONCE
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                // First result set
                while (await reader.ReadAsync())
                {
                    opcodes.Add(new OpcodeInfo
                    {
                        Opcode = reader["OPCODE"]?.ToString() ?? ""
                    });
                }
            }

            // Reader is closed now, so output parameters are available
            decimal totalSAM =
                sam.Value == DBNull.Value
                    ? 0
                    : Convert.ToDecimal(sam.Value);

            int totalWorkers =
                numWorkers.Value == DBNull.Value
                    ? 0
                    : Convert.ToInt32(numWorkers.Value);
            sw.Stop();

            _logger.LogInformation(
                "END DC_GetGlobalData Line {Line}: {Ms} ms",
                line.LineNum,
                sw.ElapsedMilliseconds);

            return new GlobalData
            {
                TotalSAM = totalSAM,
                NumWorkers = totalWorkers,
                OpcodeShow = opcodes
            };
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
