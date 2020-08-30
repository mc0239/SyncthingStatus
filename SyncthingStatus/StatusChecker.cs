using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SyncthingStatus
{
    public class StatusChecker : IDisposable
    {
        private static readonly int INTERVAL_LONG = 1000 * 5; // 5 seconds

        private Timer requestTimer;

        internal NotifyIcon TrayIcon { get; set; }
        internal ToolStripMenuItem TrayMenuItemVersion { get; set; }

        public StatusChecker()
        {
            InitializeComponent();
        }
        
        private void InitializeComponent()
        {
            requestTimer = new Timer
            {
                Interval = INTERVAL_LONG
            };
            requestTimer.Tick += (object sender, EventArgs e) => { CheckAll(); };
            requestTimer.Start();
        }

        public void CheckAll()
        {
            System.Diagnostics.Debug.WriteLine("Checking status...");
            CheckStatus().ConfigureAwait(false);
            CheckVersion().ConfigureAwait(false);
            System.Diagnostics.Debug.WriteLine("Check done.");
        }

        private async Task CheckStatus()
        {
            if (TrayIcon == null)
            {
                requestTimer.Interval = INTERVAL_LONG;
                return;
            }

            var ping = await ApiClient.Ping();
            if (ping == null)
            {
                TrayIcon.Icon = Properties.Resources.iconNotify;
                TrayIcon.Text = "Syncthing: No response";
                return;
            }

            var errors = await ApiClient.Error();
            if (errors != null && errors.Length > 0)
            {
                TrayIcon.Icon = Properties.Resources.iconNotify;
                TrayIcon.Text = "Syncthing: Reported errors";
                return;
            }

            TrayIcon.Icon = Properties.Resources.iconDefault;
            TrayIcon.Text = "Syncthing: OK";
        }

        private async Task CheckVersion()
        {
            if (TrayMenuItemVersion == null)
            {
                return;
            }

            var version = await ApiClient.Version();
            if (version != null)
            {
                TrayMenuItemVersion.Text = "Syncthing " + version.Version;
            }
        }

        public void Dispose()
        {
            requestTimer.Dispose();
        }
    }
}
