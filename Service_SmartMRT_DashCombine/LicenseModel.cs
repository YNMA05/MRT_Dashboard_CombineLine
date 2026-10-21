using System;

namespace SmartMRTDashService_Combine
{
    public class LicenseModel
    {
        public string? Company { get; set; }
        public string? HwHash { get; set; }

        public int LicenseID { get; set; }

        public string? LicenseGroup { get; set; }
        public string? LicenseType { get; set; }
        public string? Dashboard { get; set; }

        public int? MaxLines { get; set; }
        public string? MaxPages { get; set; }

        public DateTime? IssuedUtc { get; set; }
        public DateTime? ExpiryUtc { get; set; }

        public bool IsActive { get; set; }
    }
}
