using System;
using System.IO;
using System.Text;
using System.Text.Json;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;

class Program
{
    static int Main(string[] args)
    {
        string payloadPath = null;
        string privPath = null;
        string outFolder = Directory.GetCurrentDirectory();

        for (int i = 0; i < args.Length; i++)
        {
            var a = args[i];
            if (a == "--in" && i + 1 < args.Length) payloadPath = args[++i];
            else if (a == "--privatekey" && i + 1 < args.Length) privPath = args[++i];
            else if (a == "--out" && i + 1 < args.Length) outFolder = args[++i];
            else if (a == "--help" || a == "-h") { PrintHelp(); return 0; }
        }

        if (string.IsNullOrWhiteSpace(payloadPath) || string.IsNullOrWhiteSpace(privPath))
        {
            Console.Error.WriteLine("Usage: SignerToolNet472 --in payload.json --privatekey privatekey.pem [--out outFolder]");
            return 2;
        }

        try
        {
            var payloadJson = File.ReadAllText(payloadPath, Encoding.UTF8);
            var pem = File.ReadAllText(privPath);

            AsymmetricKeyParameter keyParam;
            using (var str = new StringReader(pem))
            {
                var pr = new PemReader(str);
                var obj = pr.ReadObject();
                if (obj is AsymmetricCipherKeyPair pair) keyParam = pair.Private;
                else if (obj is AsymmetricKeyParameter akp) keyParam = akp;
                else throw new InvalidOperationException("Unsupported PEM private key format");
            }

            var signer = SignerUtilities.GetSigner("SHA256withRSA");
            signer.Init(true, keyParam);
            var data = Encoding.UTF8.GetBytes(payloadJson);
            signer.BlockUpdate(data, 0, data.Length);
            var sig = signer.GenerateSignature();
            var sigB64 = Convert.ToBase64String(sig);

            Console.WriteLine("Signature (base64):");
            Console.WriteLine(sigB64);

            Directory.CreateDirectory(outFolder);
            var wrapper = new { LICENSE_PAYLOAD = payloadJson, SIGNATURE = sigB64 };
            var wrapperPath = Path.Combine(outFolder, "license_wrapper.json");
            File.WriteAllText(wrapperPath, JsonSerializer.Serialize(wrapper, new JsonSerializerOptions { WriteIndented = true }), Encoding.UTF8);
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
        Console.WriteLine("SignerToolNet472 - signing helper for .NET Framework");
    }
}
