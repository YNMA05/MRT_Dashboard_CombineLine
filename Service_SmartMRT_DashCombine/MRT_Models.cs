namespace SmartMRTDashService_Combine;

public class LineConfig
{
    public int LineId { get; set; }

    public int LineNum { get; set; }

    public string IpAddress { get; set; } = "";

    public bool StatusActive { get; set; }

    public List<string> Modules { get; set; } = new();
}

public class LineModuleRecord
{
    public int LineId { get; set; }
    public int LineNum { get; set; }
    public string IpAddress { get; set; } = "";
    public bool StatusActive { get; set; }
    public string ModuleCode { get; set; } = "";
   // public List<string> Modules { get; set; } = new();
}

//public class LineDashboardData
//{
//    public int LineId { get; set; }
//    public int LineNum { get; set; }
//    public List<string> Modules { get; set; } = new(); // which modules this line belongs to
//    public int          Output    { get; set; }   
//    public List<RunningStyle> RunningStyles { get; set; } = new();
//}

public class LineDashboardData
{
    public int LineId { get; set; }

    public int LineNum { get; set; }

    public string IpAddress { get; set; } = "";

    public string ModuleCode { get; set; } = "";

    public int TotalOutput { get; set; }

    public List<RunningStyle> Styles { get; set; } = new();
}

public class DashboardData
{
    public string ModuleCode { get; set; } = "";

    public object Data { get; set; } = new();

    public DateTime UpdateTime { get; set; }
}

public class RunningStyle
{
    public string ProdId { get; set; } = "";

    public string DesignCode { get; set; } = "";

    public int Output { get; set; }

   // public List<StationOutput> Stations { get; set; } = new();
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
