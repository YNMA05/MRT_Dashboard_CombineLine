using Microsoft.AspNetCore.SignalR;

namespace SmartMRTDashService_Combine;

// The hub itself stays empty — clients only ever *receive* the
// "KpiUpdate" event. Pushing happens from KpiPollingService via
// IHubContext<KpiHub>, not from client-invoked methods here.
public class MRTHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        // Nothing to do per-connection; the polling service broadcasts
        // to everyone on its own timer. If you want a fresh snapshot
        // immediately on connect (rather than waiting up to 10s), see
        // KpiPollingService.SendSnapshotTo().
        SignalRConnectionStatus.ClientConnected();
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(
        Exception? exception)
    {
        SignalRConnectionStatus.ClientDisconnected();
        await base.OnDisconnectedAsync(exception);
    }
}
