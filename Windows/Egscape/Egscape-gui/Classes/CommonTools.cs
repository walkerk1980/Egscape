using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Egscape_gui
{
    public class CommonTools
    {
        public bool InputIsNotNullOrVoid(string protocol, string host, string ports, string portType)
        {
            if (String.Equals(protocol,"Protocol") || String.IsNullOrEmpty(host) || String.IsNullOrEmpty(ports) || String.Equals(portType,"Port Input Type"))
            {
                System.Windows.Forms.MessageBox.Show("Input is not fully configured!" + Environment.NewLine + "You must supply a Protocol, Hostname or IP, Port List or Range, Port Input Type.");
                return false;
            }
            else
                return true;
        }

    }
}
