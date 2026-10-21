namespace SmartMRTDashService_Combine;

public class LineConfig
{
    public int LineId { get; set; }

    public int LineNum { get; set; }

    public string MasterIpAddress { get; set; } = "";
    public string LocalIpAddress { get; set; } = "";

    public bool StatusActive { get; set; }
    public bool IsOnline { get; set; }

    public List<string> Modules { get; set; } = new();
}

public class LineModuleRecord
{
    public int LineId { get; set; }
    public int LineNum { get; set; }
    public string MasterIpAddress { get; set; } = "";
    public string LocalIpAddress { get; set; } = "";
    public bool StatusActive { get; set; }
    public bool IsOnline { get; set; }
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

public class ProductionChartData
{
    public int LineNum { get; set; }

    public int Output { get; set; }

    public int Target { get; set; }

    public int RunningTarget { get; set; }
    public int Defect { get; set; }

    public int Loading { get; set; }

    public decimal Efficiency { get; set; }
    public bool IsOnline { get; set; }
}

public class ProductionMetrics
{
    public int TotalOutput { get; set; }
    public int Target { get; set; }
    public int Defect { get; set; }
    public decimal Efficiency { get; set; }
}

public class LocalDataBarChart
{
    public int TotalProduction { get; set; }
    public int TotalLoading { get; set; }
    public int Defect { get; set; }
    public int Shift { get; set; }
    public int MinWorked { get; set; }
    public TimeSpan ShiftStart { get; set; }
    public TimeSpan ShiftEnd { get; set; }
    public int ElapsedMin { get; set; }
    public int ShiftMin { get; set; }

    public List<ProductInfo> Products { get; set; } = new();
    public List<WorkerInfo> Workers { get; set; } = new();
    public List<StationAssignInfo> Assigns { get; set; } = new();
}
public class GlobalDataBarChart
{
    public decimal TotalSAM { get; set; }
    public int NumWorkers { get; set; }
}
public class ProductInfo
{
    public string Mono { get; set; } = "";
    public string Prodid { get; set; } ="";
    public string DesignCode { get; set; } = "";
}
public class WorkerInfo
{
    public string EmpId { get; set; } = "";
    public string OpCode { get; set; } = "";
    public string DesignCode { get; set; } = "";
    public string Prodid { get; set; } = "";
    public int PieceCnt { get; set; }
    public int SeqNo { get; set; }
    public string StationId { get; set; } = "";
    public int HangerIn { get; set; }
}
public class StationAssignInfo
{
    public string OpCode { get; set; } = "";
    public string DesignCode { get; set; } = "";
    public string Prodid { get; set; } = "";
}
public class DataInfo
{
    public string prodid { get; set; } = "";
    public string loadCode { get; set; } = "";
    public int loadSeq { get; set; }
    public string unloadCode { get; set; } = "";
    public int unloadSeq { get; set; }
    public int loadQty { get; set; }
    public int unloadQty { get; set; }
}

public class OpcodeInfo
{
    public string Opcode { get; set; } = "";
}

public class ProductionRunning
{
    public int TotalProduction { get; set; }
    public int Target { get; set; }
    public int RunningTarget { get; set; }
    public decimal Efficiency { get; set; }
    public int Defect { get; set; }
    public int Shift { get; set; }
    public TimeSpan ShiftStart { get; set; }
    public TimeSpan ShiftEnd { get; set; }
    public int ElapsedMin { get; set; }
    public int ShiftMin { get; set; }
}


public class LocalData
{
    public int TotalProduction { get; set; }
    public int TotalLoading { get; set; }
    public int Defect { get; set; }
    public int Shift { get; set; }
    public int MinWorked { get; set; }
    public TimeSpan ShiftStart { get; set; }
    public TimeSpan ShiftEnd { get; set; }
    public TimeSpan ShiftOTEnd { get; set; }
    public int ElapsedMin { get; set; }
    public List<ProductInfo> Products { get; set; } = new();
    public List<WorkerInfo> Workers { get; set; } = new();
    public List<StationAssignInfo> Assigns { get; set; } = new();
    public List<DataInfo> Datas { get; set; } = new();
}
public class GlobalData
{
    public decimal TotalSAM { get; set; }
    public int NumWorkers { get; set; }
    public List<OpcodeInfo> OpcodeShow { get; set; } = new();
}

public class AllProductionData
{
    public int LineNum { get; set; }
    public int Output { get; set; }
    public int TotalHangerIn { get; set; }
    public int Target { get; set; }
    public int RunningTarget { get; set; }
    public decimal Defect { get; set; }
    public int Loading { get; set; }
    public decimal Efficiency { get; set; }
    public bool IsOnline { get; set; }
}

public class Top3LineData
{
    public int LineNum { get; set; }
    public decimal LineEff { get; set; }
    public bool IsOnline { get; set; }
}

public class ProductionReportData
{
    public int LineNum { get; set; }
    public int Output { get; set; }
    public int Target { get; set; }
    public int RunningTarget { get; set; }
    public decimal Defect { get; set; }
    public decimal Efficiency { get; set; }
    public bool IsOnline { get; set; }
    public List<WorkerInfo> Workers { get; set; } = new();
    public List<ProductInfo> Products { get; set; } = new();
    public List<StationAssignInfo> Assigns { get; set; } = new();
}