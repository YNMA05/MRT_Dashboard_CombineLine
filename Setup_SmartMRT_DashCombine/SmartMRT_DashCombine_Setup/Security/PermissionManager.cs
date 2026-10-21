using Newtonsoft.Json;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using SmartMRT_DashCombine_Setup.Logging;
using SmartMRT_DashCombine_Setup.Security;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SmartMRT_DashCombine_Setup
{
    public static class PermissionManager
    {
        private static readonly HashSet<string>
            _permissions =
                new HashSet<string>(
                    StringComparer.OrdinalIgnoreCase);

        public static void LoadPermissions(
            string connectionString,
            int roleID)
        {
            _permissions.Clear();

            string sql = @"
            SELECT P.PERMISSIONCODE
            FROM DC_ROLE_PERMISSION RP
            INNER JOIN DC_PERMISSION P
                ON RP.PERMISSIONID = P.PERMISSIONID
            WHERE RP.ROLEID = @RoleID";

            using (SqlConnection conn =
                new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlCommand cmd =
                    new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue(
                        "@RoleID",
                        roleID);

                    using (SqlDataReader reader =
                        cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            _permissions.Add(
                                reader["PERMISSIONCODE"]
                                    .ToString());
                        }
                    }
                }
            }
        }

        public static bool HasPermission(
            string permissionCode)
        {
            return _permissions.Contains(
                permissionCode);
        }

        public static void Clear()
        {
            _permissions.Clear();
        }

        public static bool TryGetValidLicense(
    out LicensePayload payload)
        {
            payload = null;

            try
            {
                string licensePath = Path.Combine(
                    Environment.GetFolderPath(
                        Environment.SpecialFolder.CommonApplicationData),
                    "SmartMRT_Dashboard",
                    "License",
                    "license.lic");

                if (!File.Exists(licensePath))
                {
                    FolderLog.Info(
                        "License validation failed: license file not found.");

                    return false;
                }

                string json = File.ReadAllText(licensePath);

                LicenseFile licenseFile =
                    JsonConvert.DeserializeObject<LicenseFile>(json);

                if (licenseFile == null)
                {
                    FolderLog.Info(
                        "License validation failed: invalid license file.");

                    return false;
                }

                if (string.IsNullOrWhiteSpace(
                        licenseFile.LICENSE_PAYLOAD) ||
                    string.IsNullOrWhiteSpace(
                        licenseFile.SIGNATURE))
                {
                    FolderLog.Info(
                        "License validation failed: missing payload or signature.");

                    return false;
                }

                // ============================================
                // 1. Verify RSA signature FIRST
                // ============================================

                if (!VerifySignature(
                        licenseFile.LICENSE_PAYLOAD,
                        licenseFile.SIGNATURE))
                {
                    FolderLog.Info(
                        "License validation failed: invalid signature.");

                    return false;
                }

                // ============================================
                // 2. Deserialize the EXACT verified payload
                // ============================================

                LicensePayload verifiedPayload =
                    JsonConvert.DeserializeObject<LicensePayload>(
                        licenseFile.LICENSE_PAYLOAD);

                if (verifiedPayload == null)
                {
                    FolderLog.Info(
                        "License validation failed: invalid payload.");

                    return false;
                }

                // ============================================
                // 3. Verify hardware hash
                // ============================================

                string currentHwHash = GetHardwareHash();

                if (!string.Equals(
                        verifiedPayload.HwHash,
                        currentHwHash,
                        StringComparison.OrdinalIgnoreCase))
                {
                    FolderLog.Info(
                        "License validation failed: hardware mismatch.");

                    return false;
                }

                // ============================================
                // 4. Verify expiry
                // ============================================

                if (!string.IsNullOrWhiteSpace(
                        verifiedPayload.ExpiryUtc))
                {
                    DateTime expiryUtc;

                    if (!DateTime.TryParse(
                            verifiedPayload.ExpiryUtc,
                            null,
                            DateTimeStyles.AdjustToUniversal |
                            DateTimeStyles.AssumeUniversal,
                            out expiryUtc))
                    {
                        FolderLog.Info(
                            "License validation failed: invalid expiry date.");

                        return false;
                    }

                    if (DateTime.UtcNow > expiryUtc)
                    {
                        FolderLog.Info(
                            "License validation failed: license expired.");

                        return false;
                    }
                }

                // ============================================
                // License is valid
                // ============================================

                payload = verifiedPayload;

                FolderLog.Info(
                    "License validation successful: " +
                    verifiedPayload.LicenseType);

                return true;
            }
            catch (Exception ex)
            {
                FolderLog.Info(
                    "License validation error: " +
                    ex.Message);

                return false;
            }
        }

        public static bool ValidateLicense()
        {
            try
            {
                string licensePath = Path.Combine(
                    Environment.GetFolderPath(
                        Environment.SpecialFolder.CommonApplicationData),
                    "SmartMRT_Dashboard",
                    "License",
                    "license.lic");

                if (!File.Exists(licensePath))
                {
                    FolderLog.Info(
                        "License validation failed: license file not found.");

                    return false;
                }

                string json =
                    File.ReadAllText(licensePath);

                LicenseFile licenseFile =
                    JsonConvert.DeserializeObject<LicenseFile>(json);

                if (licenseFile == null)
                {
                    FolderLog.Info(
                        "License validation failed: invalid license file.");

                    return false;
                }

                if (string.IsNullOrWhiteSpace(
                        licenseFile.LICENSE_PAYLOAD) ||
                    string.IsNullOrWhiteSpace(
                        licenseFile.SIGNATURE))
                {
                    FolderLog.Info(
                        "License validation failed: missing payload or signature.");

                    return false;
                }

                LicensePayload payload =
                    JsonConvert.DeserializeObject<LicensePayload>(
                        licenseFile.LICENSE_PAYLOAD);

                if (payload == null)
                {
                    FolderLog.Info(
                        "License validation failed: invalid payload.");

                    return false;
                }

                // ============================================
                // 1. Verify RSA signature
                // ============================================

                if (!VerifySignature(
                        licenseFile.LICENSE_PAYLOAD,
                        licenseFile.SIGNATURE))
                {
                    FolderLog.Info(
                        "License validation failed: invalid signature.");

                    return false;
                }

                // ============================================
                // 2. Verify hardware hash
                // ============================================

                string currentHwHash =
                    GetHardwareHash();

                if (!string.Equals(
                        payload.HwHash,
                        currentHwHash,
                        StringComparison.OrdinalIgnoreCase))
                {
                    FolderLog.Info(
                        "License validation failed: hardware mismatch.");

                    return false;
                }

                // ============================================
                // 3. Verify expiry
                // ============================================

                if (!string.IsNullOrWhiteSpace(
                        payload.ExpiryUtc))
                {
                    DateTime expiryUtc;

                    if (!DateTime.TryParse(
                            payload.ExpiryUtc,
                            null,
                            DateTimeStyles.AdjustToUniversal |
                            DateTimeStyles.AssumeUniversal,
                            out expiryUtc))
                    {
                        FolderLog.Info(
                            "License validation failed: invalid expiry date.");

                        return false;
                    }

                    if (DateTime.UtcNow > expiryUtc)
                    {
                        FolderLog.Info(
                            "License validation failed: license expired.");

                        return false;
                    }
                }

                // ============================================
                // License is valid
                // ============================================

                FolderLog.Info(
                    "License validation successful: " +
                    payload.LicenseType);

                return true;
            }
            catch (Exception ex)
            {
                FolderLog.Info(
                    "License validation error: " +
                    ex.Message);

                return false;
            }
        }

        private static bool VerifySignature(
            string payload,
            string signature)
        {
            try
            {
                // Load the public key from a PEM file
                string publicKeyPath = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,"Security",
                    "publickey.pem");
                if (!File.Exists(publicKeyPath))
                {
                    FolderLog.Info(
                        "Public key file not found.");
                    return false;
                }
                using (var reader =
                    File.OpenText(publicKeyPath))
                {
                    var pemReader = new PemReader(reader);
                    var publicKey = (AsymmetricKeyParameter)pemReader.ReadObject();
                    var signer = SignerUtilities.GetSigner("SHA256withRSA");
                    signer.Init(false, publicKey);
                    byte[] payloadBytes =
                        Encoding.UTF8.GetBytes(payload);
                    signer.BlockUpdate(payloadBytes, 0, payloadBytes.Length);
                    byte[] signatureBytes =
                        Convert.FromBase64String(signature);
                    return signer.VerifySignature(signatureBytes);
                }
            }
            catch (Exception ex)
            {
                FolderLog.Info(
                    "Signature verification error: " +
                    ex.Message);
                return false;
            }
        }
        private static string GetHardwareHash()
        {
            var comp = new
            {
                SystemUuid = QueryFirst("Win32_ComputerSystemProduct", "UUID"),
                BaseBoardSN = QueryFirst("Win32_BaseBoard", "SerialNumber"),
                BiosSN = QueryFirst("Win32_BIOS", "SerialNumber"),
                ProcessorId = QueryFirst("Win32_Processor", "ProcessorId")
            };

            string hwConcat = string.Join("|",
                new[]
                {
            comp.SystemUuid,
            comp.BaseBoardSN,
            comp.BiosSN,
            comp.ProcessorId
                }
                .Where(s => !string.IsNullOrWhiteSpace(s)));

            string hwHash;

            using (var sha = SHA256.Create())
            {
                var hash = sha.ComputeHash(
                    Encoding.UTF8.GetBytes(hwConcat));

                hwHash = BitConverter
                    .ToString(hash)
                    .Replace("-", "")
                    .ToLowerInvariant();
            }

            return hwHash;
        }

        private static string QueryFirst(string wmiClass, string property)
        {
            try
            {
                using (var searcher = new ManagementObjectSearcher($"SELECT {property} FROM {wmiClass}"))
                {
                    foreach (ManagementObject mo in searcher.Get())
                    {
                        var val = mo[property]?.ToString();
                        if (!string.IsNullOrWhiteSpace(val))
                            return val.Trim();
                    }
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
