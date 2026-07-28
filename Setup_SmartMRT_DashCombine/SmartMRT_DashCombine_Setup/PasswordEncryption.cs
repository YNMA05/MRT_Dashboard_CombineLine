using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SmartMRT_DashCombine_Setup
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
