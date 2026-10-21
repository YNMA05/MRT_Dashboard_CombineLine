namespace SmartMRTDashService_Combine
{
    public interface ILicenseProvider
    {
        /// <summary>
        /// Returns the active license (or null if none available)
        /// This method may perform I/O; callers should avoid calling it extremely frequently.
        /// </summary>
        LicenseModel? GetLicense();
    }
}
