using System;
using System.Net.NetworkInformation;
using System.Security.Policy;

namespace SmartMRT_DashCombine_Setup
{
    public static class AppSession
    {
        public static bool IsAuthenticated { get; private set; }

        public static int UserID { get; private set; }

        public static string UserName { get; private set; }
            = string.Empty;

        public static int RoleID { get; private set; }

        public static string RoleName { get; private set; }
            = string.Empty;

        public static string LicenseType { get; set; } = string.Empty;
        public static string LicenseGroup { get; set; } = string.Empty;
        public static string Dashboard {get; set;} = string.Empty;
        public static int MaxLines { get; set; } = 0;
        public static string MaxPages { get; set; } = string.Empty;
        public static string Company { get; set; } = string.Empty;
        public static string ExpiryUtc { get; set; } = string.Empty;

        public static event EventHandler SessionChanged;

        public static void SetSession(
            int userID,
            string userName,
            int roleID,
            string roleName)
        {
            IsAuthenticated = true;

            UserID = userID;

            UserName =
                userName ?? string.Empty;

            RoleID = roleID;

            RoleName =
                roleName ?? string.Empty;

            SessionChanged?.Invoke(
                null,
                EventArgs.Empty);
        }

        public static void ClearSession()
        {
            IsAuthenticated = false;

            UserID = 0;

            UserName = string.Empty;

            RoleID = 0;

            RoleName = string.Empty;

            PermissionManager.Clear();

            SessionChanged?.Invoke(
                null,
                EventArgs.Empty);
        }
    }
}