using SyncthingStatus.Data;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SyncthingStatus
{
    public class StatusChecker : IDisposable
    {
        private static readonly int INTERVAL_SHORT = 1000 / 2; // Half a second
        private static readonly int INTERVAL_LONG = 1000 * 5; // 5 seconds

        private Timer requestTimer;

        internal NotifyIcon TrayIcon { get; set; }
        internal ToolStripMenuItem TrayMenuItemVersion { get; set; }
        internal ToolStripMenuItem TrayMenuItemFolders { get; set; }

        public StatusChecker()
        {
            InitializeComponent();
        }
        
        private void InitializeComponent()
        {
            requestTimer = new Timer
            {
                Interval = INTERVAL_SHORT
            };
            requestTimer.Tick += async (object sender, EventArgs e) => {
                requestTimer.Stop();
                
                bool successfulCheck = await CheckAll(); 
                requestTimer.Interval = successfulCheck ? INTERVAL_LONG : INTERVAL_SHORT;
                
                requestTimer.Start();
            };
            requestTimer.Start();
        }

        public async Task<bool> CheckAll()
        {
            if (TrayIcon == null || TrayMenuItemVersion == null || TrayMenuItemFolders == null)
            {
                return false; // TODO handle this
            }

            string status = await CheckStatus();
            if (status == "OK")
            {
                TrayIcon.Icon = Properties.Resources.iconDefault;
                TrayIcon.Text = "Syncthing: OK";
            } else
            {
                TrayIcon.Icon = Properties.Resources.iconNotify;
                TrayIcon.Text = status != null ? "Syncthing: " + status : null;
            }

            FolderResponse[] folders = await CheckFolders();
            // Sort folders by name (Label)
            Array.Sort(folders, delegate (FolderResponse f1, FolderResponse f2) { return f1.Label.CompareTo(f2.Label); });

            if (folders != null)
            {
                TrayMenuItemFolders.DropDownItems.Clear();
                foreach (FolderResponse folder in folders)
                {
                    TrayMenuItemFolders.DropDownItems.Add(new ToolStripMenuItem(folder.Label, null,
                        (object sender, EventArgs e) => { Util.OpenFolder(folder.Path); })
                    );
                }
            }

            string version = await CheckVersion();
            if (version != null)
            {
                TrayMenuItemVersion.Text = "Syncthing " + version;
            } else
            {
                TrayMenuItemVersion.Text = "...";
            }

            return status == "OK" && version != null;
        }

        private async Task<string> CheckStatus()
        {
            var ping = await ApiClient.Ping();
            if (ping == null)
            {
                return "No response";
            }

            var errors = await ApiClient.Error();
            if (errors != null && errors?.Length > 0)
            {
                return "Reporting errors";
            }

            return "OK";
        }

        private async Task<string> CheckVersion()
        {
            var version = await ApiClient.Version();
            return version?.Version;
        }

        private async Task<FolderResponse[]> CheckFolders()
        {
            var config = await ApiClient.Config();
            return config?.Folders;
        }

        public void Dispose()
        {
            requestTimer.Dispose();
        }
    }
}
