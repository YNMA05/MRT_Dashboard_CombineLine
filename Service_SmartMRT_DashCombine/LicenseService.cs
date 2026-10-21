using Microsoft.Data.SqlClient;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text.Json;
using System.Text;

namespace SmartMRTDashService_Combine
{
    public class LicenseService : ILicenseProvider
    {
        private readonly ILogger<LicenseService> _logger;
        private readonly DashboardDatabaseOptions _dbOptions;

        public LicenseService(ILogger<LicenseService> logger, Microsoft.Extensions.Options.IOptions<DashboardDatabaseOptions> dbOptions)
        {
            _logger = logger;
            _dbOptions = dbOptions?.Value ?? new DashboardDatabaseOptions();
        }

        public LicenseModel? GetLicense()
        {
            // 1) Try file in ProgramData
            try
            {
                //  var file = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "SmartMRT_Dashboard","License", "license.lic");

                string licenseFolder = Path.Combine(
                    Environment.GetFolderPath(
                        Environment.SpecialFolder.CommonApplicationData),
                    "SmartMRT_Dashboard", "License");

                string file = Path.Combine(licenseFolder, "license.lic");

                if (File.Exists(file))
                {
                    try
                    {
                        string text = File.ReadAllText(file);

                        using (JsonDocument doc = JsonDocument.Parse(text))
                        {
                            JsonElement root = doc.RootElement;

                            if (!root.TryGetProperty(
                                    "LICENSE_PAYLOAD", out JsonElement payloadElement) ||
                                !root.TryGetProperty(
                                    "SIGNATURE", out JsonElement signatureElement))
                            {
                                _logger.LogWarning(
                                    "License file has an invalid format: {File}", file);

                                return null;
                            }

                            string payload = payloadElement.GetString() ?? string.Empty;
                            string signature = signatureElement.GetString() ?? string.Empty;

                            if (string.IsNullOrWhiteSpace(payload) ||
                                !VerifySignature(payload, signature))
                            {
                                _logger.LogWarning(
                                    "License signature verification failed: {File}", file);

                                return null;
                            }

                            LicenseModel? license =
                                JsonSerializer.Deserialize<LicenseModel>(
                                    payload,
                                    new JsonSerializerOptions
                                    {
                                        PropertyNameCaseInsensitive = true
                                    });

                            if (license == null)
                            {
                                _logger.LogWarning(
                                    "License payload could not be deserialized: {File}", file);

                                return null;
                            }

                            if (!IsLicenseValid(license))
                                return null;

                            return license;
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(
                            ex, "Failed to read or parse license file: {File}", file);

                        // Do not fall back to the database when a present license
                        // file is malformed or cannot be verified.
                        return null;
                    }


                    // Only reach the database fallback when no license file exists.
                }

                // 2) Fallback to DB
                try
                {
                    //string conn = MRT_ConnectionMain.GetGlobalDBConnection(
                    //    string.IsNullOrWhiteSpace(_dbOptions.Server) ? "localhost" : _dbOptions.Server,
                    //    string.IsNullOrWhiteSpace(_dbOptions.Instance) ? "SQLEXPRESS" : _dbOptions.Instance);
                    string conn =MRT_ConnectionMain.GetDashboardConnection();

                    using var connection = new SqlConnection(conn);
                    connection.Open();
                    using var cmd = new SqlCommand("SELECT TOP(1) LICENSE_PAYLOAD, SIGNATURE, HW_HASH, ALLOWED_MODULES, MAX_LINES, EXPIRY_UTC, ISSUED_UTC, IS_ACTIVE FROM DC_SETUP WHERE IS_ACTIVE = 1 ORDER BY DATEMODIFIED DESC", connection);
                    using var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        var payload = reader[0] == DBNull.Value ? null : reader.GetString(0);
                        var signature = reader[1] == DBNull.Value ? null : reader.GetString(1);
                        var hw = reader[2] == DBNull.Value ? null : reader.GetString(2);
                        var allowed = reader[3] == DBNull.Value ? null : reader.GetString(3);
                        var maxLines = reader[4] == DBNull.Value ? (int?)null : Convert.ToInt32(reader[4]);
                        var expiry = reader[5] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader[5]);
                        var issued = reader[6] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader[6]);
                        var isActive = reader[7] == DBNull.Value ? true : Convert.ToBoolean(reader[7]);

                        if (!string.IsNullOrWhiteSpace(payload))
                        {
                            // STRICT verification: require a valid signature and public key
                            if (VerifySignature(payload, signature))
                            {
                                var lic = JsonSerializer.Deserialize<LicenseModel>(payload);
                                if (lic != null)
                                    return lic;
                            }
                            else
                            {
                                _logger.LogWarning("DB license signature verification failed; rejecting license");
                                return null;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Failed to read license from DB");
                }

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Unexpected error while loading license");
                return null;
            }
        }

        private bool VerifySignature(string payload, string? signatureBase64)
        {
            if (string.IsNullOrWhiteSpace(signatureBase64))
                return false;

            try
            {
                var pubPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "SmartMRT_Dashboard","License", "publickey.pem");
                if (!File.Exists(pubPath))
                {
                    _logger.LogWarning("Public key not found at {Path}; cannot verify signatures", pubPath);
                    return false;
                }

                var pem = File.ReadAllText(pubPath);
                using var rsa = System.Security.Cryptography.RSA.Create();
                rsa.ImportFromPem(pem.ToCharArray());

                var sig = Convert.FromBase64String(signatureBase64);
                var data = Encoding.UTF8.GetBytes(payload);
                return rsa.VerifyData(data, sig, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Signature verification failed");
                return false;
            }
        }

        private bool IsLicenseValid(LicenseModel license)
        {
            // Check expiry only for TRIAL licenses.
            if (string.Equals(
                license.LicenseGroup,
                "TRIAL",
                StringComparison.OrdinalIgnoreCase))
            {
                if (!license.ExpiryUtc.HasValue)
                {
                    _logger.LogWarning(
                        "Trial license has no expiry date.");

                    return false;
                }

                if (DateTime.UtcNow >= license.ExpiryUtc.Value.ToUniversalTime())
                {
                    _logger.LogWarning(
                        "Trial license has expired.");

                    return false;
                }
            }

            return true;
        }
    }
}
