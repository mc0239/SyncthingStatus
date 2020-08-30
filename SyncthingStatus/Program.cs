using System;
using System.Windows.Forms;

namespace SyncthingStatus
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            // TODO: settings migration between versions.
            Properties.Settings.Default.Upgrade();

            ApiClient.Initialize();
            TrayIconContainer trayIconContainer = new TrayIconContainer();

            Application.Run();
        }
    }
}
