using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Security.Principal;
using System.ServiceProcess;

namespace SmartMRT_DashCombine_Setup
{
    public partial class UC_MRT_DashCombine_Service : UserControl
    {
        private bool isEditMode = false;
        private Timer serviceStatusTimer;
        public UC_MRT_DashCombine_Service()
        {
            InitializeComponent();
            serviceStatusTimer = new Timer();
            serviceStatusTimer.Interval = 5000;

            serviceStatusTimer.Tick += (s, e) =>
            {
                UpdateServiceStatus();
            };

            UpdateServiceStatus();
            serviceStatusTimer.Start();
        }


        private const string ServiceName = "SmartMRTDashService_Combine";

private string GetProgramFilesPath()
        {
            string programFiles64 =
                Environment.GetEnvironmentVariable("ProgramW6432");

            if (!string.IsNullOrEmpty(programFiles64))
            {
                return programFiles64;
            }

            return Environment.GetFolderPath(
                Environment.SpecialFolder.ProgramFiles);
        }

        private string GetServicePackagePath()
        {
            return Path.Combine(
                GetProgramFilesPath(),
                "SmartMRT_Dashboard",
                "Service");
        }

        private string GetServiceInstallPath()
        {
            return Path.Combine(
                GetProgramFilesPath(),
                "SmartMRT_Dashboard",
                "Service");
        }

        public void setCanInstall(bool canInstall)
        {
            if (btnInstall == null) { return; }
            btnInstall.Enabled = canInstall;
        }

        public void setCanStart(bool canStart)
        {
            if(btnStart == null) { return; }
            btnStart.Enabled = canStart;
            if(btnRestart == null) { return; }
            btnRestart.Enabled = canStart;
        }

        private bool IsAdministrator()
        {
            using (WindowsIdentity identity =
                   WindowsIdentity.GetCurrent())
            {
                WindowsPrincipal principal =
                    new WindowsPrincipal(identity);

                return principal.IsInRole(
                    WindowsBuiltInRole.Administrator);
            }
        }

        private void CopyDirectory(
            string source,
            string destination)
        {
            Directory.CreateDirectory(destination);

            foreach (string file in Directory.GetFiles(source))
            {
                string targetFile = Path.Combine(
                    destination,
                    Path.GetFileName(file));

                File.Copy(file, targetFile, true);
            }

            foreach (string directory in
                     Directory.GetDirectories(source))
            {
                string targetDirectory = Path.Combine(
                    destination,
                    Path.GetFileName(directory));

                CopyDirectory(directory, targetDirectory);
            }
        }

        private int RunScCommand(string arguments)
        {
            using (Process process = new Process())
            {
                process.StartInfo = new ProcessStartInfo
                {
                    FileName = Path.Combine(
                        Environment.SystemDirectory,
                        "sc.exe"),
                    Arguments = arguments,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                };

                process.Start();

                string output =
                    process.StandardOutput.ReadToEnd();

                string error =
                    process.StandardError.ReadToEnd();

                process.WaitForExit();

                if (process.ExitCode != 0)
                {
                    throw new InvalidOperationException(
                        "Windows Service command failed.\r\n" +
                        output + "\r\n" + error);
                }

                return process.ExitCode;
            }
        }


        //private void btnInstall_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (!IsAdministrator())
        //        {
        //            MessageBox.Show(
        //                "Please close Dashboard Manager and " +
        //                "run it as Administrator before installing " +
        //                "the Windows Service.",
        //                "Administrator Required",
        //                MessageBoxButtons.OK,
        //                MessageBoxIcon.Warning);

        //            return;
        //        }

        //        string source = GetServicePackagePath();
        //        string installPath = GetServiceInstallPath();

        //        string serviceExe = Path.Combine(
        //            installPath,
        //            "MRTDashService_Combine.exe");

        //        if (!Directory.Exists(source))
        //        {
        //            MessageBox.Show(
        //                "Service package was not found:\r\n" +
        //                source +
        //                "\r\n\r\nPlease include the published " +
        //                "ServicePackage folder in the installer.",
        //                "Package Not Found",
        //                MessageBoxButtons.OK,
        //                MessageBoxIcon.Error);

        //            return;
        //        }

        //        string sourceExe = Path.Combine(
        //            source,
        //            "MRTDashService_Combine.exe");

        //        if (!File.Exists(sourceExe))
        //        {
        //            MessageBox.Show(
        //                "The published service executable is missing:\r\n" +
        //                sourceExe,
        //                "Invalid Package",
        //                MessageBoxButtons.OK,
        //                MessageBoxIcon.Error);

        //            return;
        //        }

        //        DialogResult confirm = MessageBox.Show(
        //            "Install SmartMRT Dashboard Windows Service?\r\n\r\n" +
        //            "Installation path:\r\n" + installPath,
        //            "Install Service",
        //            MessageBoxButtons.YesNo,
        //            MessageBoxIcon.Question);

        //        if (confirm != DialogResult.Yes)
        //            return;

        //        btnInstall.Enabled = false;
        //        btnInstall.Text = "Installing...";

        //        // Copy all published files.
        //        CopyDirectory(source, installPath);

        //        // Register the service.
        //        string quotedExe = "\"" + serviceExe + "\"";

        //        RunScCommand(
        //            "create " + ServiceName +
        //            " binPath= " + quotedExe +
        //            " start= auto" +
        //            " DisplayName= \"SmartMRT Dashboard Combine Service\"");

        //        // Start the service.
        //        RunScCommand("start " + ServiceName);

        //        MessageBox.Show(
        //            "SmartMRT Dashboard Windows Service " +
        //            "was installed and started successfully.",
        //            "Installation Complete",
        //            MessageBoxButtons.OK,
        //            MessageBoxIcon.Information);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(
        //            "Service installation failed.\r\n\r\n" +
        //            ex.Message,
        //            "Installation Error",
        //            MessageBoxButtons.OK,
        //            MessageBoxIcon.Error);
        //    }
        //    finally
        //    {
        //        btnInstall.Enabled = true;
        //        btnInstall.Text = "Install Service";
        //    }
        //}

        private void UpdateServiceStatus()
        {
            try
            {
                using (ServiceController service =
                       new ServiceController(ServiceName))
                {
                    service.Refresh();

                    switch (service.Status)
                    {
                        case ServiceControllerStatus.Running:
                            lblServiceStatus.Text = "Running";
                            lblServiceStatus.ForeColor = Color.LimeGreen;
                            break;

                        case ServiceControllerStatus.Stopped:
                            lblServiceStatus.Text = "Stopped";
                            lblServiceStatus.ForeColor = Color.IndianRed;
                            break;

                        case ServiceControllerStatus.StartPending:
                            lblServiceStatus.Text = "Starting...";
                            lblServiceStatus.ForeColor = Color.Goldenrod;
                            break;

                        case ServiceControllerStatus.StopPending:
                            lblServiceStatus.Text = "Stopping...";
                            lblServiceStatus.ForeColor = Color.Goldenrod;
                            break;

                        case ServiceControllerStatus.Paused:
                            lblServiceStatus.Text = "Paused";
                            lblServiceStatus.ForeColor = Color.Goldenrod;
                            break;

                        default:
                            lblServiceStatus.Text = service.Status.ToString();
                            lblServiceStatus.ForeColor = Color.Gray;
                            break;
                    }
                }
            }
            catch (InvalidOperationException)
            {
                lblServiceStatus.Text = "Not Installed";
                lblServiceStatus.ForeColor = Color.IndianRed;
            }
            catch (System.ComponentModel.Win32Exception)
            {
                lblServiceStatus.Text = "Access Error";
                lblServiceStatus.ForeColor = Color.IndianRed;
            }
        }

        private void btnInstall_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsAdministrator())
                {
                    MessageBox.Show(
                        "Please run Dashboard Manager as Administrator.",
                        "Administrator Required",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                string installPath = GetServiceInstallPath();

                string serviceExe = Path.Combine(
                    installPath,
                    "MRTDashService_Combine.exe");

                if (!File.Exists(serviceExe))
                {
                    MessageBox.Show(
                        "Service executable was not found:\r\n" + serviceExe,
                        "Service File Missing",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    return;
                }

                DialogResult confirm = MessageBox.Show(
                    "Register and start the SmartMRT Dashboard Service?\r\n\r\n" +
                    "Service path:\r\n" + serviceExe,
                    "Install Service",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirm != DialogResult.Yes)
                    return;

                btnInstall.Enabled = false;
                btnInstall.Text = "Installing...";

                // Check whether the service is already registered.
                bool serviceExists = false;

                try
                {
                    RunScCommand("query \"" + ServiceName + "\"");
                    serviceExists = true;
                }
                catch (InvalidOperationException)
                {
                    // The service may not be registered yet.
                }

                if (!serviceExists)
                {
                    string quotedExe = "\"" + serviceExe + "\"";

                    RunScCommand(
                        "create \"" + ServiceName + "\"" +
                        " binPath= " + quotedExe +
                        " start= auto" +
                        " DisplayName= \"SmartMRT Dashboard Combine Service\"");
                }
                else
                {
                    // Update the registered service executable path.
                    string quotedExe = "\"" + serviceExe + "\"";

                    RunScCommand(
                        "config \"" + ServiceName + "\"" +
                        " binPath= " + quotedExe +
                        " start= auto");
                }

                RunScCommand(
                "description \"" + ServiceName + "\" " +
                "\"SmartMRT Dashboard Combine Service. " +
                "Provides dashboard data updates, SignalR communication, " +
                "and production monitoring for configured manufacturing lines.\"");

                // Start the service.
                RunScCommand("start \"" + ServiceName + "\"");

                MessageBox.Show(
                    "SmartMRT Dashboard Service was registered " +
                    "and started successfully.",
                    "Installation Complete",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Service installation failed.\r\n\r\n" + ex.Message,
                    "Installation Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                btnInstall.Enabled = true;
                btnInstall.Text = "Install Service";
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsServiceInstalled())
                {
                    MessageBox.Show(
                        "Please install the SmartMRT Dashboard Service first.",
                        "Service Not Installed",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }
                RunScCommand("start SmartMRTDashService_Combine");

                MessageBox.Show(
                    "SmartMRT Dashboard Service started successfully.",
                    "Service Started",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                UpdateServiceStatus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Failed to start the service.\r\n\r\n" + ex.Message,
                    "Start Service Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            try
            {
                // Stop the service
                RunScCommand("stop SmartMRTDashService_Combine");

                // Wait until the service has stopped
                bool stopped = false;

                for (int i = 0; i < 30; i++)
                {
                    using (Process process = new Process())
                    {
                        process.StartInfo = new ProcessStartInfo
                        {
                            FileName = Path.Combine(
                                Environment.SystemDirectory,
                                "sc.exe"),
                            Arguments = "query SmartMRTDashService_Combine",
                            UseShellExecute = false,
                            CreateNoWindow = true,
                            RedirectStandardOutput = true,
                            RedirectStandardError = true
                        };

                        process.Start();

                        string output = process.StandardOutput.ReadToEnd();

                        process.WaitForExit();

                        if (output.IndexOf(
                            "STOPPED",
                            StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            stopped = true;
                            break;
                        }
                    }

                    System.Threading.Thread.Sleep(1000);
                }

                if (!stopped)
                {
                    throw new InvalidOperationException(
                        "The service did not stop within 30 seconds.");
                }

                // Start the service again
                RunScCommand("start SmartMRTDashService_Combine");

                MessageBox.Show(
                    "SmartMRT Dashboard Service restarted successfully.",
                    "Service Restarted",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                UpdateServiceStatus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Failed to restart the service.\r\n\r\n" + ex.Message,
                    "Restart Service Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private bool IsServiceInstalled()
        {
            using (Process process = new Process())
            {
                process.StartInfo = new ProcessStartInfo
                {
                    FileName = Path.Combine(
                        Environment.SystemDirectory,
                        "sc.exe"),
                    Arguments = "query SmartMRTDashService_Combine",
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                };

                process.Start();

                string output = process.StandardOutput.ReadToEnd();

                process.WaitForExit();

                return process.ExitCode == 0 &&
                       output.IndexOf(
                           "SERVICE_NAME:",
                           StringComparison.OrdinalIgnoreCase) >= 0;
            }
        }

        private void btnRefreshStatus_Click(object sender, EventArgs e)
        {
            UpdateServiceStatus();
        }

    }
}
