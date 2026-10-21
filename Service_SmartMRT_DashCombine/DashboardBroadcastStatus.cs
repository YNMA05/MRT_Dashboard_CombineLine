namespace SmartMRTDashService_Combine
{
    public static class DashboardBroadcastStatus
    {
        private static readonly object _lock = new();

        private static DateTime? _lastBroadcastUtc;

        public static DateTime? LastBroadcastUtc
        {
            get
            {
                lock (_lock)
                {
                    return _lastBroadcastUtc;
                }
            }
        }

        public static void MarkBroadcastSuccessful()
        {
            lock (_lock)
            {
                _lastBroadcastUtc = DateTime.UtcNow;
            }
        }
    }
}
