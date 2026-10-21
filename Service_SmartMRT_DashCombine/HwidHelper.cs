using System;
using System.Linq;
using System.Management;
using System.Security.Cryptography;
using System.Text;

namespace SmartMRTDashService_Combine
{
    public static class HwidHelper
    {
        public static string ComputeHwHash()
        {
            var parts = new[]
            {
                QueryFirst("Win32_ComputerSystemProduct", "UUID"),
                QueryFirst("Win32_BaseBoard", "SerialNumber"),
                QueryFirst("Win32_BIOS", "SerialNumber"),
                QueryFirst("Win32_Processor", "ProcessorId")
            }
            .Where(s => !string.IsNullOrWhiteSpace(s))
            .ToArray();

            string concat = string.Join("|", parts);

            using var sha = SHA256.Create();
            var hash = sha.ComputeHash(Encoding.UTF8.GetBytes(concat));
            return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
        }

        private static string QueryFirst(string wmiClass, string property)
        {
            try
            {
                using var searcher = new ManagementObjectSearcher($"SELECT {property} FROM {wmiClass}");
                foreach (ManagementObject mo in searcher.Get())
                {
                    var val = mo[property]?.ToString();
                    if (!string.IsNullOrWhiteSpace(val))
                        return val.Trim();
                }
            }
            catch
            {
                // ignore
            }
            return string.Empty;
        }
    }
}
