namespace SmartMRTDashService_Combine
{
    public static class SignalRConnectionStatus
    {
        private static int _connectedClients;

        public static int ConnectedClients =>
            Math.Max(0, Volatile.Read(ref _connectedClients));

        public static void ClientConnected()
        {
            Interlocked.Increment(ref _connectedClients);
        }

        public static void ClientDisconnected()
        {
            Interlocked.Decrement(ref _connectedClients);
        }
    }
}
