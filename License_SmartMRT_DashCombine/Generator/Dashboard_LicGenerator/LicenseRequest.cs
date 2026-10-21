using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard_LicGenerator
{
    public class LicenseRequest
    {
        public string Company { get; set; }
        public string HwHash { get; set; }
        public int LicenseID { get; set; }
        public DateTime IssuedUtc { get; set; }
    }
}
