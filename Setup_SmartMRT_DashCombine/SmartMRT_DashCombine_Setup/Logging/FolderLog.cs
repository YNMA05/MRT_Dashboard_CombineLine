using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace SmartMRT_DashCombine_Setup.Logging
{
    public static class FolderLog
    {
        private static readonly string logFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),"SmartMRT_Dashboard", "Logs");
        private static readonly string logFile = Path.Combine(logFilePath, "SmartMRT_ActivityLog.log");

        public static void Info(string message)
        {
            Write("INFO", message);
        }

        public static void Error(string message)
        {
            Write("ERROR", message);
        }

        public static void Error(string message, Exception ex)
        {
            Write(
                "ERROR",
                message + Environment.NewLine + ex.ToString());
        }

        private static void Write(string level, string message)
        {
            try
            {
                if (!Directory.Exists(logFilePath))
                {
                    Directory.CreateDirectory(logFilePath);
                }

                RotateLogIfNeeded();

                using (StreamWriter writer =
                    new StreamWriter(logFile, true))
                {
                    string logEntry =
                        string.Format(
                            "{0:yyyy-MM-dd HH:mm:ss} | {1} | {2}",
                            DateTime.Now,
                            level,
                            message);

                    writer.WriteLine(logEntry);
                }
            }
            catch
            {
                // Logging must never crash the application.
            }
        }

        private static void RotateLogIfNeeded()
        {
            try
            {
                if (!File.Exists(logFile))
                {
                    return;
                }

                FileInfo fileInfo = new FileInfo(logFile);

                // 10 MB
                long maxSize = 10 * 1024 * 1024;

                if (fileInfo.Length < maxSize)
                {
                    return;
                }

                string backupFile =
                    Path.Combine(
                        logFilePath,
                        "SmartMRT_old.log");

                if (File.Exists(backupFile))
                {
                    File.Delete(backupFile);
                }

                File.Move(logFile, backupFile);
            }
            catch
            {
                // Do not allow log rotation failure
                // to crash the application.
            }
        }

    }
}
