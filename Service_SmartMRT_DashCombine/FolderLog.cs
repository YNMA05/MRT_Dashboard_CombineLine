
using System;
using System.IO;

namespace SmartMRTDashService_Combine
{
    public static class FolderLog
    {
        private static readonly object _lock = new object();

        private static readonly string logFilePath = Path.Combine(
            Environment.GetFolderPath(
                Environment.SpecialFolder.CommonApplicationData),
            "SmartMRT_Dashboard",
            "Logs");

        private static readonly string logFile = Path.Combine(
            logFilePath,
            "SmartMRT_ServiceLog.log");

        private static readonly string backupFile = Path.Combine(
            logFilePath,
            "SmartMRT_ServiceLog_old.log");

        private const long MaxSize = 10 * 1024 * 1024;

        public static void Info(string message)
        {
            Write("INFO", message);
        }

        public static void Warning(string message)
        {
            Write("WARN", message);
        }

        public static void Error(string message)
        {
            Write("ERROR", message);
        }

        public static void Error(string message, Exception ex)
        {
            Write("ERROR",
                message + Environment.NewLine + ex);
        }

        private static void Write(string level, string message)
        {
            try
            {
                lock (_lock)
                {
                    Directory.CreateDirectory(logFilePath);

                    RotateLogIfNeeded();

                    string logEntry =
                        $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} " +
                        $"[{level}] {message}" +
                        Environment.NewLine;

                    File.AppendAllText(logFile, logEntry);
                }
            }
            catch
            {
                // Logging failures must not crash the service.
            }
        }

        private static void RotateLogIfNeeded()
        {
            if (!File.Exists(logFile))
                return;

            var fileInfo = new FileInfo(logFile);

            if (fileInfo.Length < MaxSize)
                return;

            if (File.Exists(backupFile))
                File.Delete(backupFile);

            File.Move(logFile, backupFile);
        }
    }
}