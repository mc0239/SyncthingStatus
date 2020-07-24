using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SyncthingStatus
{
    class Util
    {

        public static string GetAboutString()
        {
            return "Syncthing Status " + Application.ProductVersion;
        }

        public static void OpenUrl(string url)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
        }

        public static string GetSyncthingAddress()
        {
            if (Properties.Settings.Default.UsingCustomStAddress)
            {
                return Properties.Settings.Default.StAddress;
            } else
            {
                return "http://localhost:8384/";
            }
        }

    }
}
