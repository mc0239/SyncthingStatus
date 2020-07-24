using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SyncthingStatus
{
    class TrayIconContainer
    {
        private NotifyIcon trayIcon;
        private ContextMenuStrip trayMenu;
        private SettingsForm settingsForm;

        private ToolStripMenuItem trayMenuItemAbout;
        private ToolStripSeparator trayMenuSeparator1;
        private ToolStripMenuItem trayMenuItemOpen;
        private ToolStripMenuItem trayMenuItemSettings;
        private ToolStripMenuItem trayMenuItemExit;

        public TrayIconContainer()
        {
            InitializeComponents();
            yeet();
        }

        async void yeet()
        {
            Console.WriteLine("Current status is:" + await ApiClient.ping());    
        }

        private void InitializeComponents()
        {
            trayMenuItemAbout = new ToolStripMenuItem();
            trayMenuItemAbout.Text = Util.GetAboutString();
            trayMenuItemAbout.Enabled = false;

            trayMenuSeparator1 = new ToolStripSeparator();

            trayMenuItemOpen = new ToolStripMenuItem();
            trayMenuItemOpen.Text = "Open in browser";
            trayMenuItemOpen.Click += TrayMenuItemOpenClickHandler;

            trayMenuItemSettings = new ToolStripMenuItem();
            trayMenuItemSettings.Text = "Settings";
            trayMenuItemSettings.Click += TrayMenuItemSettingsClickHandler;

            trayMenuItemExit = new ToolStripMenuItem();
            trayMenuItemExit.Text = "Exit";
            trayMenuItemExit.Click += TrayMenuItemExitClickHandler;

            trayMenu = new ContextMenuStrip();
            trayMenu.ShowImageMargin = false;

            trayMenu.Items.Add(trayMenuItemAbout);
            trayMenu.Items.Add(trayMenuSeparator1);
            trayMenu.Items.Add(trayMenuItemOpen);
            trayMenu.Items.Add(trayMenuItemSettings);
            trayMenu.Items.Add(trayMenuItemExit);

            trayIcon = new NotifyIcon();
            trayIcon.Icon = Properties.Resources.iconDefault;
            trayIcon.Visible = true;
            trayIcon.ContextMenuStrip = trayMenu;
            trayIcon.DoubleClick += TrayMenuItemOpenClickHandler;

            settingsForm = new SettingsForm();
        }

        private void TrayMenuItemOpenClickHandler(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://localhost:8384");
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
