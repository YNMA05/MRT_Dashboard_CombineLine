# KPI dashboard — C# / .NET version

Same architecture as the Node example, built with ASP.NET Core's native
Worker Service + SignalR instead of Express + socket.io.

## Pieces

- `KpiDashboardService/Program.cs` — app entry point, registers SignalR,
  the background poller, and Windows Service hosting.
- `KpiDashboardService/KpiPollingService.cs` — `BackgroundService` that
  queries SQL Server every 10s and broadcasts over SignalR. Equivalent to
  `server.js`'s `setInterval` loop.
- `KpiDashboardService/KpiHub.cs` — the SignalR hub clients connect to.
- `KpiDashboardService/KpiModels.cs` — plain records for the payload shape.
- `KpiDashboardService/appsettings.json` — connection string and port.
- `Dashboard.jsx` — React client using `@microsoft/signalr` instead of
  `socket.io-client`.

## Why this is more "native" than the Node version

- `builder.Host.UseWindowsService()` is built into `Microsoft.Extensions.Hosting.WindowsServices`
  — no third-party wrapper needed (Node needed `node-windows` for the same job).
- `Microsoft.Data.SqlClient` is Microsoft's own SQL Server driver.
- SignalR is Microsoft's WebSocket library, built for exactly this
  "server pushes state to many clients" pattern.

## Setup

1. Install the .NET 8 SDK if you don't have it.
2. Edit the SQL in `KpiPollingService.cs` to match your schema.
3. Set your real connection string in `appsettings.json` (or better, use
   `dotnet user-secrets` / environment variables for the password).
4. Run locally to test:
   ```
   cd KpiDashboardService
   dotnet run
   ```
   You should see it listening on `http://0.0.0.0:5000`.

## Installing as a Windows service

```
cd KpiDashboardService
dotnet publish -c Release -o C:\Services\KpiDashboard

sc create "KPI Dashboard Service" binPath="C:\Services\KpiDashboard\KpiDashboardService.exe"
sc start "KPI Dashboard Service"
```

Because `UseWindowsService()` is already wired into `Program.cs`, the
published .exe behaves correctly as a service the moment it's registered —
no extra install script needed (unlike the Node version's
`install-service.js`). Logs go to Windows Event Viewer automatically.

## Frontend

```
npm install @microsoft/signalr recharts
```
Update `HUB_URL` in `Dashboard.jsx` to match your server's host/port —
it must match the path in `app.MapHub<KpiHub>("/hubs/kpi")`.

Note: .NET's default JSON serializer sends PascalCase-derived camelCase
(`metrics.revenueToday`) automatically for Web API/SignalR — the client
code doesn't need any special casing logic.
