using SmartMRTDashService_Combine;

var builder = WebApplication.CreateBuilder(args);

// Lets `dotnet publish` + a plain `sc create` register this as a real
// Windows service — no external wrapper library needed (unlike Node,
// where node-windows plays this role).
builder.Host.UseWindowsService(options =>
{
    options.ServiceName = "SmartMRT Std Dashboard Combine";
});

builder.Services.AddSignalR();
builder.Services.AddCors(o => o.AddDefaultPolicy(p =>
    p.AllowAnyHeader().AllowAnyMethod().SetIsOriginAllowed(_ => true).AllowCredentials()));

builder.Services.AddHostedService<MRTPollingService>();
builder.Services.AddSingleton<MRT_DashRepository>();
var app = builder.Build();

app.UseCors();
app.MapHub<MRTHub>("/hubs/MRTdashCombine");
app.MapGet("/health", () => Results.Ok(new { status = "ok" }));

app.Run();
