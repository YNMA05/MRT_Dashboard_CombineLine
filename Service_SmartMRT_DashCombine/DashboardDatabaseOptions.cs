namespace SmartMRTDashService_Combine
{
    public class DashboardDatabaseOptions
    {
        public string? Server { get; set; }
        public string? Instance { get; set; }
        public bool UseIntegratedSecurity { get; set; } = true;
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}
