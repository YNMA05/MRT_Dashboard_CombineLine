using System;
using System.IO;
using System.Linq;
using System.Management;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

class Program
{
    static int Main(string[] args)
    {
        bool flagPrint = args.Contains("--print");
        bool flagSave = args.Contains("--save-local");
        bool flagJson = args.Contains("--json");
        bool flagLicenseTemplate = args.Contains("--license-template");
        bool flagGenerateKeys = args.Contains("--generate-keys");

        var comp = new
        {
            SystemUuid = QueryFirst("Win32_ComputerSystemProduct", "UUID"),
            BaseBoardSN = QueryFirst("Win32_BaseBoard", "SerialNumber"),
            BiosSN = QueryFirst("Win32_BIOS", "SerialNumber"),
            ProcessorId = QueryFirst("Win32_Processor", "ProcessorId")
        };

        string hwConcat = string.Join("|", new[] { comp.SystemUuid, comp.BaseBoardSN, comp.BiosSN, comp.ProcessorId }.Where(s => !string.IsNullOrWhiteSpace(s)));
        string hwHash;
        using (var sha = SHA256.Create())
        {
            var hash = sha.ComputeHash(Encoding.UTF8.GetBytes(hwConcat));
            hwHash = BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
        }

        var result = new
        {
            HwHash = hwHash,
            Components = comp
        };

        var jsonOptions = new JsonSerializerOptions { WriteIndented = true };

        if (flagPrint || !args.Any())
        {
            Console.WriteLine("HW_HASH: " + hwHash);
            Console.WriteLine("SystemUuid: " + comp.SystemUuid);
            Console.WriteLine("BaseBoardSN: " + comp.BaseBoardSN);
            Console.WriteLine("BiosSN: " + comp.BiosSN);
            Console.WriteLine("ProcessorId: " + comp.ProcessorId);
        }

        if (flagJson)
        {
            Console.WriteLine(JsonSerializer.Serialize(result, jsonOptions));
        }

        if (flagSave)
        {
            try
            {
                var folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "SmartMRT");
                Directory.CreateDirectory(folder);
                var path = Path.Combine(folder, "hw_report.json");
                File.WriteAllText(path, JsonSerializer.Serialize(result, jsonOptions), Encoding.UTF8);
                Console.WriteLine("Saved hw_report.json to " + path);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Failed to save hw_report.json: " + ex.Message);
            }
        }

        if (flagLicenseTemplate)
        {
            var payload = new
            {
                ProductCode = "FFULL_DC",
                AllowedModules = "V_PRODOUTPUT",
                MaxLines = 10,
                IssuedUtc = DateTime.UtcNow.ToString("o"),
                ExpiryUtc = DateTime.UtcNow.AddYears(1).ToString("o"),
                HwHash = hwHash
            };

            string payloadJson = JsonSerializer.Serialize(payload, jsonOptions);
            Console.WriteLine("LICENSE_PAYLOAD:\n" + payloadJson);

            // Emit both simple license.json and wrapper
            try
            {
                var folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "SmartMRT");
                Directory.CreateDirectory(folder);
                var licensePath = Path.Combine(folder, "license.json");
                File.WriteAllText(licensePath, payloadJson, Encoding.UTF8);

                var wrapper = new
                {
                    LICENSE_PAYLOAD = payloadJson,
                    SIGNATURE = "" // fill by signing server
                };
                var wrapperPath = Path.Combine(folder, "license_wrapper.json");
                File.WriteAllText(wrapperPath, JsonSerializer.Serialize(wrapper, jsonOptions), Encoding.UTF8);

                Console.WriteLine("Wrote license.json and license_wrapper.json to " + folder);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Failed to write license templates: " + ex.Message);
            }
        }

        if (flagGenerateKeys)
        {
            try
            {
                var outFolder = Path.Combine(Directory.GetCurrentDirectory(), "keys");
                Directory.CreateDirectory(outFolder);
                using var rsa = System.Security.Cryptography.RSA.Create(2048);
                var pub = rsa.ExportSubjectPublicKeyInfo();
                var priv = rsa.ExportPkcs8PrivateKey();

                File.WriteAllText(Path.Combine(outFolder, "publickey.pem"), ToPem(pub, "PUBLIC KEY"));
                File.WriteAllText(Path.Combine(outFolder, "privatekey.pem"), ToPem(priv, "PRIVATE KEY"));

                Console.WriteLine("Generated RSA keypair in " + outFolder);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Failed to generate keys: " + ex.Message);
            }
        }

        return 0;
    }

    private static string ToPem(byte[] data, string label)
    {
        var b64 = Convert.ToBase64String(data);
        var sb = new StringBuilder();
        sb.AppendLine($"-----BEGIN {label}-----");
        for (int i = 0; i < b64.Length; i += 64)
        {
            sb.AppendLine(b64.Substring(i, Math.Min(64, b64.Length - i)));
        }
        sb.AppendLine($"-----END {label}-----");
        return sb.ToString();
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
