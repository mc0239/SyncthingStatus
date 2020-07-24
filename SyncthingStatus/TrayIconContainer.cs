using System;
using System.Windows.Forms;

namespace SyncthingStatus
{
    class TrayIconContainer : IDisposable
    {
        private NotifyIcon trayIcon;
        private ContextMenuStrip trayMenu;
        
        private ToolStripItem[] trayMenuItems;
        private ToolStripMenuItem trayMenuItemAbout;
        private ToolStripMenuItem trayMenuItemOpen;
        private ToolStripMenuItem trayMenuItemSettings;
        private ToolStripMenuItem trayMenuItemExit;

        private SettingsForm settingsForm;
        private Timer requestTimer;

        public TrayIconContainer()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            InitializeTrayMenu();
            InitializeTrayIcon();
            InitializeRequestTimer();
            settingsForm = new SettingsForm();
        }

        private void InitializeTrayMenu()
        {
            trayMenuItemAbout = new ToolStripMenuItem(Util.GetAboutString()) { Enabled = false };
            trayMenuItemOpen = new ToolStripMenuItem("Open in browser", null, TrayMenuItemOpenClickHandler);
            trayMenuItemSettings = new ToolStripMenuItem("Settings", null, TrayMenuItemSettingsClickHandler);
            trayMenuItemExit = new ToolStripMenuItem("Exit", null, TrayMenuItemExitClickHandler);

            trayMenuItems = new ToolStripItem[] 
            {
                trayMenuItemAbout, new ToolStripSeparator(), trayMenuItemOpen, trayMenuItemSettings, trayMenuItemExit
            };

            trayMenu = new ContextMenuStrip() { ShowImageMargin = false };
            trayMenu.Items.AddRange(trayMenuItems);
        }

        private void InitializeTrayIcon()
        {
            trayIcon = new NotifyIcon
            {
                Icon = Properties.Resources.iconDefault,
                Visible = true,
                ContextMenuStrip = trayMenu
            };
            trayIcon.DoubleClick += TrayMenuItemOpenClickHandler;
        }

        public void Dispose()
        {
            foreach(ToolStripItem i in trayMenuItems)
            {
                i.Dispose();
            }
            trayMenu.Dispose();
            trayIcon.Dispose();
            timer.Dispose();
            settingsForm.Dispose();
        }

        private void InitializeRequestTimer()
        {
            requestTimer = new Timer();
            requestTimer.Interval = 1000 * 10;
            requestTimer.Tick += async (object sender, EventArgs e) =>
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
            };
            requestTimer.Start();
        }

        private void TrayMenuItemOpenClickHandler(object sender, EventArgs e)
        {
            Util.OpenUrl(Util.GetSyncthingAddress());
        }

        private void TrayMenuItemSettingsClickHandler(object sender, EventArgs e)
        {
            if (settingsForm.IsDisposed)
            {
                settingsForm = new SettingsForm();
            }
            settingsForm.Show();
        }

        private void TrayMenuItemExitClickHandler(object sender, EventArgs e)
        {
            // Tray icon sometimes just hangs around until user hovers over it, so
            // explicitly hide it before actual exit.
            trayIcon.Visible = false;
            Application.Exit();
        }

    }
}
