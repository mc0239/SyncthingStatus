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
        private ToolStripMenuItem trayMenuItemAbout2;
        private ToolStripMenuItem trayMenuItemOpen;
        private ToolStripMenuItem trayMenuItemFolders;
        private ToolStripMenuItem trayMenuItemSettings;
        private ToolStripMenuItem trayMenuItemExit;

        private SettingsForm settingsForm;
        private StatusChecker statusChecker;

        public TrayIconContainer()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            InitializeTrayMenu();
            InitializeTrayIcon();

            statusChecker = new StatusChecker
            {
                TrayIcon = trayIcon,
                TrayMenuItemVersion = trayMenuItemAbout2,
                TrayMenuItemFolders = trayMenuItemFolders
            };

            settingsForm = new SettingsForm
            {
                StatusChecker = statusChecker
            };
        }

        private void InitializeTrayMenu()
        {
            trayMenuItemAbout = new ToolStripMenuItem(Util.GetAboutString()) { Enabled = false };
            trayMenuItemAbout2 = new ToolStripMenuItem("...") { Enabled = false };
            trayMenuItemOpen = new ToolStripMenuItem("Open in browser", null, TrayMenuItemOpenClickHandler);
            trayMenuItemFolders = new ToolStripMenuItem("Open in explorer");
            trayMenuItemSettings = new ToolStripMenuItem("Settings", null, TrayMenuItemSettingsClickHandler);
            trayMenuItemExit = new ToolStripMenuItem("Exit", null, TrayMenuItemExitClickHandler);

            trayMenuItemFolders.DropDownItems.Add(new ToolStripMenuItem("No folders") { Enabled = false });
            ((ToolStripDropDownMenu)trayMenuItemFolders.DropDown).ShowImageMargin = false;

            trayMenuItems = new ToolStripItem[] 
            {
                trayMenuItemAbout, trayMenuItemAbout2, new ToolStripSeparator(), trayMenuItemOpen, trayMenuItemFolders, trayMenuItemSettings, trayMenuItemExit
            };

            trayMenu = new ContextMenuStrip() { ShowImageMargin = false };
            trayMenu.Items.AddRange(trayMenuItems);
        }

        private void InitializeTrayIcon()
        {
            trayIcon = new NotifyIcon
            {
                Icon = Properties.Resources.iconThink,
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

            statusChecker.Dispose();
            settingsForm.Dispose();
        }

        private void TrayMenuItemOpenClickHandler(object sender, EventArgs e)
        {
            Util.OpenUrl(Util.GetSyncthingAddress());
        }

        private void TrayMenuItemSettingsClickHandler(object sender, EventArgs e)
        {
            if (settingsForm.IsDisposed)
            {
                settingsForm = new SettingsForm
                {
                    StatusChecker = statusChecker
                };
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
