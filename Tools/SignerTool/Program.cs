using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Security.Cryptography;

class Program
{
    static int Main(string[] args)
    {
        string? payloadPath = null;
        string? privPath = null;
        string outFolder = Directory.GetCurrentDirectory();

        for (int i = 0; i < args.Length; i++)
        {
            var a = args[i];
            if (a == "--in" && i + 1 < args.Length) { payloadPath = args[++i]; }
            else if (a == "--privatekey" && i + 1 < args.Length) { privPath = args[++i]; }
            else if (a == "--out" && i + 1 < args.Length) { outFolder = args[++i]; }
            else if (a == "--help" || a == "-h") { PrintHelp(); return 0; }
        }

        if (string.IsNullOrWhiteSpace(payloadPath))
        {
            Console.Error.WriteLine("Missing --in <payload.json>");
            PrintHelp();
            return 2;
        }

        if (string.IsNullOrWhiteSpace(privPath))
        {
            Console.Error.WriteLine("Missing --privatekey <privatekey.pem>");
            PrintHelp();
            return 2;
        }

        try
        {
            var payloadJson = File.ReadAllText(payloadPath, Encoding.UTF8);
            var pem = File.ReadAllText(privPath);

            byte[] data = Encoding.UTF8.GetBytes(payloadJson);
            using var rsa = RSA.Create();
            rsa.ImportFromPem(pem.ToCharArray());
            var sig = rsa.SignData(data, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
            var sigB64 = Convert.ToBase64String(sig);

            Console.WriteLine("Signature (base64):");
            Console.WriteLine(sigB64);

            Directory.CreateDirectory(outFolder);
            var wrapper = new { LICENSE_PAYLOAD = payloadJson, SIGNATURE = sigB64 };
            var wrapperPath = Path.Combine(outFolder, "license_wrapper.json");
            File.WriteAllText(wrapperPath, JsonSerializer.Serialize(wrapper, new JsonSerializerOptions { WriteIndented = true }));
            Console.WriteLine("Wrote " + wrapperPath);

            return 0;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine("Error: " + ex.Message);
            return 3;
        }
    }

    static void PrintHelp()
    {
        Console.WriteLine("SignerTool - simple signing helper");
        Console.WriteLine("Usage: SignerTool --in payload.json --privatekey privatekey.pem [--out outFolder]");
        Console.WriteLine("Outputs base64 signature to stdout and writes license_wrapper.json to outFolder");
    }
}
