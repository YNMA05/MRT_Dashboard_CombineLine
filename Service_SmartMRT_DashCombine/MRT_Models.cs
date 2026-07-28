namespace SmartMRTDashService_Combine;

public class LineConfig
{
    public int LineId { get; set; }

    public int LineNum { get; set; }

    public string IpAddress { get; set; }

    public bool StatusActive { get; set; }
}

public class DashboardData
{
    public int TotalOutput { get; set; }

    public string Time { get; set; }
}

public class LineKpitotal
{
    public string LineName { get; set; }

    public int Output { get; set; }

    public int Target { get; set; }

    public double Efficiency { get; set; }
}
public class LineKpi
{
    public int LineNum { get; set; }

    public int Output { get; set; }
}

public record KpiSnapshot(
    DateTime Timestamp,
    KpiMetrics Metrics,
    List<HourlyRevenue> RevenueByHour,
    List<TopProduct> TopProducts
);

public record KpiMetrics(decimal RevenueToday, int ActiveUsers, int OrdersToday);

public record HourlyRevenue(int Hour, decimal Revenue);

public record TopProduct(string ProductName, int Units, decimal Revenue);
