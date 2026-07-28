using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMRT_DashCombine_Setup
{
    public class DashboardConfig
    {
        public DashboardDatabase DashboardDatabase { get; set; }

        public DashboardSetting Dashboard { get; set; }
    }

    public class DashboardDatabase
    {
        public string Server { get; set; }

        public string Instance { get; set; }

        public string Database { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }

    public class DashboardSetting
    {
        public int RefreshInterval { get; set; }

        public int SignalRPort { get; set; }
    }
}
