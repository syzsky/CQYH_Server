using System;
using System.Threading;
using System.Windows.Forms;

namespace 游戏登录器
{
    internal static class Program
    {
        private static Mutex myMutex;

        [STAThread]
        private static void Main()
        {
            System.Security.Principal.WindowsIdentity identity = System.Security.Principal.WindowsIdentity.GetCurrent();
            System.Security.Principal.WindowsPrincipal principal = new System.Security.Principal.WindowsPrincipal(identity);
            if (principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator))
            {
                myMutex = new Mutex(initiallyOwned: false, "CY_Launcher_Mutex", out var createdNew);
                if (createdNew)
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(defaultValue: false);
                    Application.Run(new 登录界面());
                }
                else
                {
                    MessageBox.Show("登录器已经在运行中");
                    Environment.Exit(0);
                }
            }
            else
            {

                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.UseShellExecute = true;
                startInfo.WorkingDirectory = Environment.CurrentDirectory;
                startInfo.FileName = Application.ExecutablePath;

                startInfo.Verb = "runas";
                try
                {
                    System.Diagnostics.Process.Start(startInfo);
                }
                catch
                {
                    return;
                }

                Application.Exit();
            }

        }
    }
}
