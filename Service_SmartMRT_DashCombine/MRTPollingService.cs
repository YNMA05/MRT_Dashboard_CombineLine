using DocumentFormat.OpenXml.Vml;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace SmartMRTDashService_Combine;

// The Windows-service-friendly equivalent of the Node setInterval loop.
// BackgroundService is the standard .NET pattern for "run this forever
// alongside the web host" — it starts with the app and stops cleanly on
// shutdown, which matters when Windows sends a stop signal to the service.
public class MRTPollingService : BackgroundService
{
    private static readonly TimeSpan PollInterval = TimeSpan.FromSeconds(10);

    private readonly IHubContext<MRTHub> _hub;
    private readonly ILogger<MRTPollingService> _logger;
    private readonly MRT_DashRepository _repository;
    private readonly DashboardDatabaseOptions _dbOptions;
    private List<LineConfig> _lines = new();
    // Cache for previous production lookups to avoid repeated DB calls
    private Dictionary<int,int> _prevProductionCache = new();
    private string _cacheShift = string.Empty;
    private string _cacheLineKey = string.Empty;
    private DateTime _cacheExpiry = DateTime.MinValue;
    private DateTime _lastHeartbeat = DateTime.MinValue;
    private readonly Dictionary<int, bool> _lastOnline = new();
    private readonly HashSet<int> _failedLines = new();


    private readonly ILicenseProvider _licenseProvider;

    public MRTPollingService(
        IHubContext<MRTHub> hub,
        ILogger<MRTPollingService> logger,
        MRT_DashRepository repository,
        IOptions<DashboardDatabaseOptions> dbOptions,
        ILicenseProvider licenseProvider)
    {
        _hub = hub;
        _logger = logger;
        _repository = repository;
        _dbOptions = dbOptions?.Value ?? new DashboardDatabaseOptions();
        _licenseProvider = licenseProvider;

        //_logger.LogInformation("MRTPollingService CREATED");
    }


    //protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    //{
    //    using var timer = new PeriodicTimer(TimeSpan.FromSeconds(10));

    //    do
    //    {
    //        try
    //        {
    //            string dashboardConn =
    //                MRT_ConnectionMain.GetGlobalDBConnection(
    //                    "192.168.0.126",
    //                    "SQLEXPRESS");

    //            List<LineConfig> lines =
    //                await _repository.GetLineConfigsAsync(dashboardConn);

    //            foreach (var line in lines)
    //            {
    //                _logger.LogInformation(
    //                    "Line: {Line}, IP: {IP}, Active: {Status}",
    //                    line.LineNum,
    //                    line.IpAddress,
    //                    line.StatusActive);
    //            }
    //            List<LineKpi> dashboardData = new List<LineKpi>();
    //            foreach (var line in lines.Where(x => x.StatusActive))
    //            {
    //                string lineConn =
    //                    MRT_ConnectionMain.GetSqlConnectionString(
    //                        line.IpAddress,
    //                        "",
    //                        "sa",
    //                        "1234",
    //                        "ONLINE_LOCALDB");

    //                int output =
    //                    await _repository.GetTodayOutputAsync(
    //                        lineConn,
    //                        DateTime.Today);

    //                _logger.LogInformation(
    //                    "Line {Line} Output = {Output}",
    //                    line.LineNum,
    //                    output);

    //                dashboardData.Add(new LineKpi
    //                {
    //                    LineNum = line.LineNum,
    //                    Output = output
    //                });
    //                int totalOutput = dashboardData.Sum(x => x.Output);

    //                _logger.LogInformation("Total Output : {Total}", totalOutput);

    //                DashboardData dashboard = new DashboardData
    //                {
    //                    TotalOutput = totalOutput,
    //                    Time = DateTime.Now.ToString("HH:mm:ss")
    //                };

    //                await _hub.Clients.All.SendAsync(
    //                    "DashboardUpdate",
    //                    dashboard,
    //                    stoppingToken);
    //            }

    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.LogError(ex, "Polling failed.");
    //        }


    //    } while (await timer.WaitForNextTickAsync(stoppingToken));
    //}
    //protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    //{
    //    using var timer = new PeriodicTimer(TimeSpan.FromSeconds(10));

    //    do
    //    {
    //        try
    //        {
    //            string globalConn = MRT_ConnectionMain.GetGlobalDBConnection(
    //                "192.168.0.126", "SQLEXPRESS");

    //            // ── 1. Fetch flat rows (one per line-module pair) ──────────
    //            var records = await _repository.GetLineConfigsAsync(globalConn);

    //            // ── 2. Group into LineConfig (one per line, modules merged) ─
    //            var result = new List<LineConfig>(); // ← was never declared before!

    //            foreach (var g in records.GroupBy(x => x.LineId))
    //            {
    //                var line = new LineConfig
    //                {
    //                    LineId = g.Key,
    //                    LineNum = g.First().LineNum,
    //                    IpAddress = g.First().IpAddress,
    //                    StatusActive = g.First().StatusActive
    //                };

    //                // Collect all module codes this line belongs to
    //                line.Modules.AddRange(
    //                    g.Where(x => !string.IsNullOrEmpty(x.ModuleCode))
    //                     .Select(x => x.ModuleCode)
    //                     .Distinct());

    //                result.Add(line);
    //            }

    //            // ── 3. Build dashboard data per line ───────────────────────
    //            var dashboards = new List<LineDashboardData>();

    //            foreach (var line in result.Where(x => x.StatusActive))
    //            {
    //                if (line.Modules.Contains("V_PRODOUTPUT"))
    //                {
    //                    var dashboard = await BuildProductionOutput(line);
    //                    dashboards.Add(dashboard);
    //                }

    //                //if (line.Modules.Contains("V_KPI"))
    //                //{
    //                //    var kpi = await BuildKPI(line);
    //                //    dashboards.Add(kpi);
    //                //}

    //                //if (line.Modules.Contains("V_TARGET"))
    //                //{
    //                //    var target = await BuildTarget(line);
    //                //    dashboards.Add(target);
    //                //}
    //            }

    //            // ── 4. Push to clients ─────────────────────────────────────
    //            // Each LineDashboardData.Modules tells the front-end
    //            // which module panel this line should appear in
    //            await _hub.Clients.All.SendAsync(
    //                "DashboardUpdate",
    //                dashboards,
    //                stoppingToken);
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.LogError(ex, "Dashboard polling failed.");
    //        }

    //    } while (await timer.WaitForNextTickAsync(stoppingToken));
    //}

    private List<LineConfig> GetModuleLines(
    List<LineConfig> lines,
    string moduleCode)
    {
        return lines
            .Where(x =>
                x.StatusActive &&
                x.Modules.Contains(moduleCode))
            .ToList();
    }


    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        FolderLog.Info($"{DateTime.Now} Execute started\r\n");
       // _logger.LogInformation("MRTPollingService ExecuteAsync STARTED");
        //var license = _licenseProvider.GetLicense();

        //if (license == null)
        //{
        //    _logger.LogError(
        //        "No valid license found. Dashboard service operations are disabled.");

        //    return;
        //}

        //_logger.LogInformation(
        //    "License validated. Dashboard polling is starting.");

        using var timer = new PeriodicTimer(PollInterval);

        do
        {
           // FolderLog.Info("MRTPollingService tick.");

            try
            {
                bool logNow = DateTime.UtcNow - _lastHeartbeat > TimeSpan.FromMinutes(10);
                if (logNow)
                {
                    FolderLog.Info("MRTPollingService heartbeat.");
                    _lastHeartbeat = DateTime.UtcNow;
                }

                //if (string.Equals(
                //        license.LicenseGroup,
                //        "TRIAL",
                //        StringComparison.OrdinalIgnoreCase))
                //{
                //    if (!license.ExpiryUtc.HasValue)
                //    {
                //        FolderLog.Error(
                //            "TRIAL license has no expiry date.");

                //        return;
                //    }

                //    TimeSpan remaining =
                //        license.ExpiryUtc.Value.ToUniversalTime() - DateTime.UtcNow;

                //    if (remaining <= TimeSpan.Zero)
                //    {
                //        FolderLog.Error(
                //            $"TRIAL license expired at {license.ExpiryUtc.Value:yyyy-MM-dd HH:mm:ss} UTC.");

                //        return;
                //    }
                var license = _licenseProvider.GetLicense();

                if (license == null)
                {
                    FolderLog.Error("No valid license found. Dashboard polling is disabled.");
                    return;
                }

                if (string.Equals(license.LicenseGroup, "TRIAL", StringComparison.OrdinalIgnoreCase))
                {
                    if (!license.ExpiryUtc.HasValue)
                    {
                        FolderLog.Error("TRIAL license has no expiry date.");
                        return;
                    }

                    TimeSpan remaining = license.ExpiryUtc.Value.ToUniversalTime() - DateTime.UtcNow;

                    if (remaining <= TimeSpan.Zero)
                    {
                        FolderLog.Error(
                            $"TRIAL license expired at {license.ExpiryUtc.Value:yyyy-MM-dd HH:mm:ss} UTC.");
                        return;
                    }

                    if (logNow)
                    {
                        FolderLog.Info(
                            $"TRIAL license valid. Time remaining: " +
                            $"{remaining.Days} days, {remaining.Hours} hours, " +
                            $"{remaining.Minutes} minutes. " +
                            $"Expiry UTC: {license.ExpiryUtc.Value.ToUniversalTime():yyyy-MM-dd HH:mm:ss}");
                    }
                }
                else if (logNow)
                {
                    FolderLog.Info($"License validated. License group: {license.LicenseGroup}.");
                }


                //string globalConn = MRT_ConnectionMain.GetGlobalDBConnection(
                //    string.IsNullOrWhiteSpace(_dbOptions.Server) ? "localhost" : _dbOptions.Server,
                //    string.IsNullOrWhiteSpace(_dbOptions.Instance) ? "SQLEXPRESS" : _dbOptions.Instance);
                string globalConn = MRT_ConnectionMain.GetDashboardConnection();
                var records = await _repository.GetLineConfigsAsync(globalConn);


                var lines = records
                    .GroupBy(x => x.LineId)
                    .Select(g => new LineConfig
                    {
                        LineId = g.Key,
                        LineNum = g.First().LineNum,
                        MasterIpAddress = g.First().MasterIpAddress,
                        LocalIpAddress = g.First().LocalIpAddress,
                        StatusActive = g.First().StatusActive,
                        Modules = g
                            .Where(x => !string.IsNullOrEmpty(x.ModuleCode))
                            .Select(x => x.ModuleCode)
                            .Distinct()
                            .ToList()
                    })
                    .ToList();


                var dashboards = new List<DashboardData>();

                foreach (var line in lines)
                {
                    line.IsOnline = await _repository.CheckLocalConnection(line);
                }

                var lineStatus = lines.Select(x => new
                {
                    LineNum = x.LineNum,
                    IsOnline = x.IsOnline
                }).ToList();

                dashboards.Add(new DashboardData
                {
                    ModuleCode = "V_LINESTATUS",
                    Data = lineStatus,
                    UpdateTime = DateTime.Now
                });


                var lineData = new Dictionary<int, (LocalData Local, GlobalData Global)>();

                foreach (var line in lines)
                {
                    //_logger.LogInformation(
                    //    "Processing Line {Line}, LineId={LineId}, IsOnline={IsOnline}",
                    //    line.LineNum,
                    //    line.LineId,
                    //    line.IsOnline);
                    bool wasOnline = _lastOnline.TryGetValue(line.LineId, out var prev) ? prev : true;

                    if (!line.IsOnline)
                    {
                        //_logger.LogWarning(
                        //    "Line {Line} is offline.",
                        if (wasOnline)
                            FolderLog.Error($"Line {line.LineNum} went offline.");

                        _lastOnline[line.LineId] = false;
                        continue;

                        if (!wasOnline)
                            FolderLog.Info($"Line {line.LineNum} is back online.");

                        _lastOnline[line.LineId] = true;
                    }

                    try
                    {
                        var data = await _repository.GetLineDataAsync(line);

                        lineData[line.LineId] = data;

                        if (_failedLines.Remove(line.LineId))
                            FolderLog.Info($"Line {line.LineNum} recovered.");

                        //_logger.LogInformation(
                        //    "Line {Line}: Output={Output}, SAM={SAM}, Workers={Workers}, OPCODEs={Opcodes}",
                        //    line.LineNum,
                        //    data.Local.TotalProduction,
                        //    data.Global.TotalSAM,
                        //    data.Global.NumWorkers,
                        //    data.Global.OpcodeShow.Count);

                        if (logNow)
                        {
                            FolderLog.Info(
                                $"Line {line.LineNum}: Output={data.Local.TotalProduction}, " +
                                $"SAM={data.Global.TotalSAM}, Workers={data.Global.NumWorkers}");
                        }

                        // Save production count to global DB (update daily production)
                        try
                        {
                            await _repository.SaveProductionCountAsync(
                                line,
                                DateTime.Today,
                                data.Local.Shift.ToString(),
                                data.Local.TotalProduction,
                                _dbOptions.Server,
                                _dbOptions.Instance);
                        }
                        catch (Exception ex)
                        {
                            // _logger.LogError(ex, "Failed to save production for Line {Line}", line.LineNum);
                            FolderLog.Error($"Failed to save production for Line {line.LineNum}: {ex.Message}");
                        }                   
                    }
                    catch (Exception ex)
                    {
                        //FolderLog.Error(
                        //    $"FAILED Line {line.LineNum}, LineId={line.LineId}, " +
                        //    $"LocalIP={line.LocalIpAddress}, MasterIP={line.MasterIpAddress}: {ex.Message}");
                        if (!_failedLines.Contains(line.LineId))
                        {
                            FolderLog.Error(
                                $"FAILED Line {line.LineNum}, LineId={line.LineId}: {ex.Message}");
                            _failedLines.Add(line.LineId);
                        }

                        // show empty data instead of removing the line
                        lineData[line.LineId] = (new LocalData(), new GlobalData());

                       // line.IsOnline = false;
                    }
                }
                var outputLines = GetModuleLines(lines, "V_PRODOUTPUT");
                
                int prevOutput = 0;
                int totalOutput = 0;

                // Prepare list of lines that have data and are in the output module
                var outputLineNums = outputLines
                    .Where(l => l.IsOnline && lineData.ContainsKey(l.LineId))
                    .Select(l => l.LineNum)
                    .OrderBy(n => n)
                    .ToList();

                // Determine current shift (same shift for all lines as enforced)
                string currentShift = string.Empty;
                if (outputLineNums.Count > 0)
                {
                    var anyLine = outputLines.First(l => outputLineNums.Contains(l.LineNum));
                    if (lineData.TryGetValue(anyLine.LineId, out var anyData))
                        currentShift = anyData.Local.Shift.ToString();
                }

                // Build cache key
                string key = string.Join(',', outputLineNums);

                if (currentShift != _cacheShift || key != _cacheLineKey || DateTime.UtcNow >= _cacheExpiry)
                {
                    try
                    {
                        var prevs = await _repository.GetPreviousProductionsAsync(
                            outputLineNums,
                            currentShift,
                            DateTime.Today,
                            _dbOptions.Server,
                            _dbOptions.Instance);

                        _prevProductionCache = prevs;
                        _cacheShift = currentShift;
                        _cacheLineKey = key;
                        _cacheExpiry = DateTime.UtcNow.AddMinutes(10);
                    }
                    catch (Exception ex)
                    {
                        FolderLog.Error($"Error Failed to fetch previous production batch: {ex.Message}");
                        //_logger.LogError(ex, "Failed to fetch previous productions batch");
                        _prevProductionCache = new Dictionary<int,int>();
                    }
                }

                foreach (var line in outputLines)
                {
                    if (!line.IsOnline)
                        continue;

                    if (!lineData.TryGetValue(line.LineId, out var data))
                        continue;

                    totalOutput += data.Local.TotalProduction;

                    if (_prevProductionCache.TryGetValue(line.LineNum, out var prev))
                        prevOutput += prev;
                }
                // Check license allows V_PRODOUTPUT
                //var license = _licenseProvider.GetLicense();
                //var allowedModules = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                //if (license != null && !string.IsNullOrWhiteSpace(license.AllowedModules))
                //{
                //    foreach (var m in license.AllowedModules.Split(',', StringSplitOptions.RemoveEmptyEntries))
                //        allowedModules.Add(m.Trim());
                //}

                //if (allowedModules.Count == 0 || allowedModules.Contains("V_PRODOUTPUT"))
                //{
                    dashboards.Add(new DashboardData
                    {
                        ModuleCode = "V_PRODOUTPUT",
                        Data = new
                        {
                            TotalOutput = totalOutput,
                            PreviousOutput = prevOutput
                        },
                        UpdateTime = DateTime.Now
                    });
                //}

                var prodTable = GetModuleLines(lines, "V_PRODTABLE");

                var prodTableData = new List<object>();

                foreach (var line in prodTable)
                {
                    if (!lineData.TryGetValue(line.LineId, out var data))
                    {
                        prodTableData.Add(new
                        {
                            LineNum = line.LineNum,
                            TotalHangerIn = 0,
                            Defect = 0,
                            Efficiency = 0,
                            IsOnline = false
                        });

                        continue;
                    }

                    var production = _repository.CalculateAllProduction(
                        line,
                        data.Local,
                        data.Global);

                    prodTableData.Add(new
                    {
                        LineNum = line.LineNum,
                        TotalHangerIn = production.TotalHangerIn,
                        Defect = production.Defect,
                        Efficiency = production.Efficiency,
                        IsOnline = production.IsOnline
                    });
                }

                //if (allowedModules.Count == 0 || allowedModules.Contains("V_PRODTABLE"))
                //{
                    dashboards.Add(new DashboardData
                    {
                        ModuleCode = "V_PRODTABLE",
                        Data = prodTableData,
                        UpdateTime = DateTime.Now
                    });
               // }

                var prodChart = GetModuleLines(lines, "V_PRODCHART");

                var prodChartData = new List<object>();
                foreach (var line in prodChart)
                {
                    if (!lineData.TryGetValue(line.LineId, out var data))
                    {
                        prodChartData.Add(new
                        {
                            LineNum = line.LineNum,
                            Target = 0,
                            RunningTarget = 0,
                            Output = 0,
                            IsOnline = false
                        });
                        continue;
                    }
                    var production = _repository.CalculateAllProduction(
                        line,
                        data.Local,
                        data.Global);

                    prodChartData.Add(new
                    {
                        LineNum = line.LineNum,
                        Target = production.Target,
                        RunningTarget = production.RunningTarget,
                        Output = production.Output,
                        IsOnline = production.IsOnline

                    });
                }
               // if (allowedModules.Count == 0 || allowedModules.Contains("V_PRODCHART"))
              //  {
                    dashboards.Add(new DashboardData
                    {
                        ModuleCode = "V_PRODCHART",
                        Data = prodChartData,
                        UpdateTime = DateTime.Now
                    });
                //}

                var prodTop3 = GetModuleLines(lines, "V_TOP3");

                var prodTop3Data = new List<Top3LineData>();

                foreach (var line in prodTop3)
                {
                    if (!lineData.TryGetValue(line.LineId, out var data))
                    {
                        prodTop3Data.Add(new Top3LineData
                        {
                            LineNum = line.LineNum,
                            LineEff = 0,
                            IsOnline = false
                        });

                        continue;
                    }

                    var production = _repository.CalculateAllProduction(
                        line,
                        data.Local,
                        data.Global);

                    prodTop3Data.Add(new Top3LineData
                    {
                        LineNum = line.LineNum,
                        LineEff = production.Efficiency,
                        IsOnline = production.IsOnline
                    });
                }

                prodTop3Data = prodTop3Data
                    .Where(x => x.IsOnline)
                    .OrderByDescending(x => x.LineEff)
                    .Take(3)
                    .ToList();

              //  if (allowedModules.Count == 0 || allowedModules.Contains("V_TOP3"))
               // {
                    dashboards.Add(new DashboardData
                    {
                        ModuleCode = "V_TOP3",
                        Data = prodTop3Data,
                        UpdateTime = DateTime.Now
                    });
               // }

                // var lineMetrics = new List
                // ============================
                // V_PRODOUTPUT KPI
                // ============================

                //var outputLines = GetModuleLines(lines, "V_PRODOUTPUT");


                //int totalOutput = 0;


                //foreach (var line in outputLines)
                //{
                //    if (!line.IsOnline)
                //    {
                //        _logger.LogWarning("Line {line} is offline.", line.LineNum);
                //        continue;
                //    }

                //    var output =
                //        await _repository.BuildProductionOutput(line);

                //    totalOutput += output.TotalOutput;
                //}


                //dashboards.Add(new DashboardData
                //{
                //    ModuleCode = "V_PRODOUTPUT",
                //    Data = new
                //    {
                //        TotalOutput = totalOutput
                //    },
                //    UpdateTime = DateTime.Now
                //});

                // ============================
                // V_PRODCHART
                // ============================

                //var chartLines =GetModuleLines(lines, "V_PRODCHART");


                //var chartData = new List<ProductionChartData>();

                //foreach (var line in chartLines)
                //{
                //    if (!line.IsOnline)
                //    {
                //        chartData.Add(new ProductionChartData
                //        {
                //            LineNum = line.LineNum,
                //            IsOnline = false
                //        });

                //        continue;
                //    }

                //    var chart =
                //        await _repository.BuildProductionChart(line);

                //    chart.IsOnline = true;

                //    chartData.Add(chart);
                //}


                //dashboards.Add(new DashboardData
                //{
                //    ModuleCode = "V_PRODCHART",
                //    Data = chartData,
                //    UpdateTime = DateTime.Now
                //});


                // ============================
                // V_PRODTABLE
                // ============================

                await _hub.Clients.All.SendAsync(
                    "DashboardUpdate",
                    dashboards,
                    stoppingToken);

                DashboardBroadcastStatus.MarkBroadcastSuccessful();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Dashboard polling failed");
                FolderLog.Error(ex.ToString());
            }

        }

        while (await timer.WaitForNextTickAsync(stoppingToken));
    }
}

