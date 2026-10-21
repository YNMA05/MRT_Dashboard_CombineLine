//using Org.BouncyCastle.OpenSsl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Crypto.Parameters;


namespace Dashboard_LicGenerator
{
    public static class LicenseSigner
    {
        public static string SignPayload(string payloadJson)
        {
            string privateKeyPath =
                @"C:\Users\pc\Documents\SmartMRT_LicenseKeys\privatekey.pem";

            if (!File.Exists(privateKeyPath))
                throw new FileNotFoundException(
                    "Private RSA key was not found.",
                    privateKeyPath);

            string privateKeyPem =
                File.ReadAllText(privateKeyPath);

            using (TextReader reader =
                new StringReader(privateKeyPem))
            {
                PemReader pemReader =
                    new PemReader(reader);

                object keyObject =
                    pemReader.ReadObject();

                AsymmetricKeyParameter privateKey;

                AsymmetricCipherKeyPair keyPair =
                    keyObject as AsymmetricCipherKeyPair;

                if (keyPair != null)
                {
                    privateKey = keyPair.Private;
                }
                else
                {
                    privateKey =
                        (AsymmetricKeyParameter)keyObject;
                }

                ISigner signer =
                    SignerUtilities.GetSigner(
                        "SHA256withRSA");

                signer.Init(true, privateKey);

                byte[] data =
                    Encoding.UTF8.GetBytes(payloadJson);

                signer.BlockUpdate(
                    data,
                    0,
                    data.Length);

                byte[] signature =
                    signer.GenerateSignature();

                return Convert.ToBase64String(signature);
            }
        }
    }
}
