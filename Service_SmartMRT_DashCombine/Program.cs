using SmartMRTDashService_Combine;

//var builder = WebApplication.CreateBuilder(args);
var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    ContentRootPath = AppContext.BaseDirectory,
    WebRootPath = Path.Combine(AppContext.BaseDirectory, "wwwroot")
});
QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;
// Lets `dotnet publish` + a plain `sc create` register this as a real
// Windows service — no external wrapper library needed (unlike Node,
// where node-windows plays this role).
builder.Host.UseWindowsService(options =>
{
    options.ServiceName = "SmartMRT Std Dashboard Combine";
});
builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddCors(o => o.AddDefaultPolicy(p =>
    p.AllowAnyHeader().AllowAnyMethod().SetIsOriginAllowed(_ => true).AllowCredentials()));

// Bind Dashboard database configuration from appsettings (section: DashboardDatabase)
builder.Services.Configure<DashboardDatabaseOptions>(
    builder.Configuration.GetSection("DashboardDatabase"));

builder.Services.AddHostedService<MRTPollingService>();
builder.Services.AddSingleton<MRT_DashRepository>();
// License service (reads file in %ProgramData% or fallback to DB)
builder.Services.AddSingleton<ILicenseProvider, LicenseService>();
builder.Services.AddSingleton<System.Text.Json.JsonSerializerOptions>(_ => new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });
var app = builder.Build();

// --- DIAGNOSTIC LOGGING ---

// --- DIAGNOSTIC LOGGING ---
string logDirectory = Path.Combine(
    Environment.GetFolderPath(
        Environment.SpecialFolder.CommonApplicationData),
    "SmartMRT_Dashboard",
    "Logs");

string logPath = Path.Combine(
    logDirectory,
    "debug_startup.log");

try
{
    // Create the log folder if it does not exist.
    Directory.CreateDirectory(logDirectory);

    string wwwrootCheck = Path.Combine(
        AppContext.BaseDirectory,
        "wwwroot");

    string indexPath = Path.Combine(
        wwwrootCheck,
        "index.html");

    string logContent =
        $"Startup Time: {DateTime.Now:yyyy-MM-dd HH:mm:ss}\r\n" +
        $"BaseDirectory: {AppContext.BaseDirectory}\r\n" +
        $"CurrentDirectory: {Environment.CurrentDirectory}\r\n" +
        $"Expected wwwroot: {wwwrootCheck}\r\n" +
        $"wwwroot Exists?: {Directory.Exists(wwwrootCheck)}\r\n" +
        $"Expected index.html: {indexPath}\r\n" +
        $"index.html Exists?: {File.Exists(indexPath)}\r\n" +
        "----------------------------------------\r\n";

    File.AppendAllText(logPath, logContent);
}
catch (Exception ex)
{
    // Fallback in case ProgramData logging fails.
    try
    {
        string fallbackPath = Path.Combine(
            Path.GetTempPath(),
            "SmartMRT_debug_startup.log");

        File.AppendAllText(
            fallbackPath,
            $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} " +
            $"Logging error: {ex}\r\n");
    }
    catch
    {
        // Avoid preventing the service from starting
        // solely because diagnostic logging failed.
    }
}

app.MapGet("/api/status", () =>
{
    return Results.Ok(new
    {
        connectedClients =
            SignalRConnectionStatus.ConnectedClients,

        lastBroadcastUtc = DashboardBroadcastStatus.LastBroadcastUtc
    });
});
// --------------------------

// --------------------------

app.UseCors();
app.UseDefaultFiles();
app.UseStaticFiles();
app.MapControllers();
app.MapHub<MRTHub>("/hubs/MRTdashCombine");
app.MapGet("/health", () => Results.Ok(new { status = "ok" }));
app.MapFallbackToFile("index.html");
app.Run();
