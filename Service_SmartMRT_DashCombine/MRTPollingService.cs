using Microsoft.AspNetCore.SignalR;
using Microsoft.Data.SqlClient;

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
    private List<LineConfig> _lines = new();


    public MRTPollingService(
        IHubContext<MRTHub> hub,
        ILogger<MRTPollingService> logger,
        MRT_DashRepository repository)
    {
        _hub = hub;
        _logger = logger;
        _repository = repository;

        _logger.LogInformation("MRTPollingService CREATED");
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
        File.AppendAllText(
        @"C:\Temp\SmartMRT_Service.log",
        $"{DateTime.Now} Execute started\r\n");
        _logger.LogInformation("MRTPollingService ExecuteAsync STARTED");

        using var timer = new PeriodicTimer(PollInterval);

        do
        {
            File.AppendAllText(
           @"C:\Temp\SmartMRT_Service.log",
           $"{DateTime.Now} Tick\r\n");
            try
            {
                _logger.LogInformation("MRTPollingService tick");

                string globalConn = MRT_ConnectionMain.GetGlobalDBConnection(
                    "192.168.0.126", "SQLEXPRESS");
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
                // ============================
                // V_PRODOUTPUT KPI
                // ============================

                var outputLines = GetModuleLines(lines, "V_PRODOUTPUT");


                int totalOutput = 0;


                foreach (var line in outputLines)
                {
                    if (!line.IsOnline)
                    {
                        _logger.LogWarning("Line {line} is offline.", line.LineNum);
                        continue;
                    }

                    var output =
                        await _repository.BuildProductionOutput(line);

                    totalOutput += output.TotalOutput;
                }


                dashboards.Add(new DashboardData
                {
                    ModuleCode = "V_PRODOUTPUT",
                    Data = new
                    {
                        TotalOutput = totalOutput
                    },
                    UpdateTime = DateTime.Now
                });

                // ============================
                // V_PRODCHART
                // ============================

                var chartLines =GetModuleLines(lines, "V_PRODCHART");


                var chartData = new List<ProductionChartData>();

                foreach (var line in chartLines)
                {
                    if (!line.IsOnline)
                    {
                        chartData.Add(new ProductionChartData
                        {
                            LineNum = line.LineNum,
                            IsOnline = false
                        });

                        continue;
                    }

                    var chart =
                        await _repository.BuildProductionChart(line);

                    chart.IsOnline = true;

                    chartData.Add(chart);
                }


                dashboards.Add(new DashboardData
                {
                    ModuleCode = "V_PRODCHART",
                    Data = chartData,
                    UpdateTime = DateTime.Now
                });



                await _hub.Clients.All.SendAsync(
                    "DashboardUpdate",
                    dashboards,
                    stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Dashboard polling failed");
                File.AppendAllText(
                @"C:\Temp\SmartMRT_Service.log",
                ex.ToString());
            }

        }

        while (await timer.WaitForNextTickAsync(stoppingToken));
    }
}

