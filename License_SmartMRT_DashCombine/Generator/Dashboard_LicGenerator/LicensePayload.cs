using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard_LicGenerator
{
    public class LicensePayload
    {
        public string Company { get; set; }

        public string HwHash { get; set; }

        public int LicenseID { get; set; }

        public string LicenseGroup { get; set; }

        public string LicenseType { get; set; }

        public string Dashboard { get; set; }

        public int MaxLines { get; set; }

        public string MaxPages { get; set; }

        public string IssuedUtc { get; set; }

        public string ExpiryUtc { get; set; }
    }
}
