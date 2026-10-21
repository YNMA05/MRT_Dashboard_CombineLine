using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard_LicGenerator
{
    public class LicensePackage
    {
        public int LicenseID { get; set; }
        public string LicenseGroup { get; set; }
        public string LicenseType { get; set; }
        public string Dashboard { get; set; }
        public int MaxLines { get; set; }
        public string MaxPages { get; set; }
        public string Description { get; set; }
    }
}
