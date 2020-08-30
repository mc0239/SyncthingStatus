using System;
using System.Windows.Forms;

namespace SyncthingStatus
{
    public class StatusChecker : IDisposable
    {
        private Timer requestTimer;

        private readonly NotifyIcon trayIcon;
        private readonly ContextMenuStrip trayMenu;

        public StatusChecker(NotifyIcon trayIcon, ContextMenuStrip trayMenu)
        {
            this.trayIcon = trayIcon;
            this.trayMenu = trayMenu;
            Initialize();
        }

        private void Initialize()
        {
            requestTimer = new Timer
            {
                Interval = 1000 * 10
            };
            requestTimer.Tick += CheckStatus;
            CheckNow();
            requestTimer.Start();
        }

        public void CheckNow()
        {
            CheckStatus(null, null);
            FetchVersion();
        }

        private async void CheckStatus(object sender, EventArgs e)
        {
            var ping = await ApiClient.Ping();
            if (ping == null)
            {
                trayIcon.Icon = Properties.Resources.iconNotify;
                trayIcon.Text = "Syncthing: No response";
                return;
            }

            var errors = await ApiClient.Error();
            if (errors != null && errors.Length > 0)
            {
                trayIcon.Icon = Properties.Resources.iconNotify;
                trayIcon.Text = "Syncthing: Reported errors.";
                return;
            }

            trayIcon.Icon = Properties.Resources.iconDefault;
            trayIcon.Text = "Syncthing: OK";
        }

        private async void FetchVersion()
        {
            var version = await ApiClient.Version();
            if (version != null)
            {
                trayMenu.Items[1].Text = "Syncthing " + version.Version;
            }
        }

        public void Dispose()
        {
            requestTimer.Dispose();
            
        }
    }
}
