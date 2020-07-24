using System;
using System.Collections.Generic;
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

    }
}
