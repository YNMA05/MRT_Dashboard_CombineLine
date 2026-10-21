using System.Security.Cryptography;
using System.Text;

namespace SmartMRTDashService_Combine
{
    public static class PasswordEncryption
    {
        public static string Encrypt(string password)
        {
            byte[] plainBytes =
                Encoding.UTF8.GetBytes(password);

            byte[] encryptedBytes =
                ProtectedData.Protect(
                    plainBytes,
                    null,
                    DataProtectionScope.LocalMachine);

            return Convert.ToBase64String(
                encryptedBytes);
        }

        public static string Decrypt(string encryptedPassword)
        {
            byte[] encryptedBytes =
                Convert.FromBase64String(
                    encryptedPassword);

            byte[] decryptedBytes =
                ProtectedData.Unprotect(
                    encryptedBytes,
                    null,
                    DataProtectionScope.LocalMachine);

            return Encoding.UTF8.GetString(
                decryptedBytes);
        }
    }
    }
