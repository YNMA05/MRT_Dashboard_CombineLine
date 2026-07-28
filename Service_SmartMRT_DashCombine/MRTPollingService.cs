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

    public MRTPollingService(
        IHubContext<MRTHub> hub,
        ILogger<MRTPollingService> logger,
        MRT_DashRepository repository)
    {
        _hub = hub;
        _logger = logger;
        _repository = repository;
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
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var timer = new PeriodicTimer(TimeSpan.FromSeconds(10));

        do
        {
            try
            {
                string dashboardConn =
                    MRT_ConnectionMain.GetGlobalDBConnection(
                        "192.168.0.126",
                        "SQLEXPRESS");

                var lines = await _repository.GetLineConfigsAsync(dashboardConn);

                List<LineDashboardData> dashboards = new();

                foreach (var line in lines.Where(x => x.StatusActive))
                {
                    dashboards.Add(
                        await _repository.BuildDashboardDataAsync(line));
                }

                await _hub.Clients.All.SendAsync(
                    "DashboardUpdate",
                    dashboards,
                    stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Polling failed.");
            }

        } while (await timer.WaitForNextTickAsync(stoppingToken));
    }
}
