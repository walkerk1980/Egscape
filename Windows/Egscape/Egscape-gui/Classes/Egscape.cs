using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Egscape_gui
{
    class Egscape : CommonTools
    {
        public void RunScan(string protocol, string host, string ports, string portType)
        {
            MessageBox.Show("Scanning..." + Environment.NewLine + Environment.NewLine + "Protocol: " + protocol + Environment.NewLine + "Host: " + host + Environment.NewLine + "Ports: " + ports );
        }

    }
}
