using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

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

        protected List<int> ParsePortString(String portString)
        {
            var ports = new List<int>();

            // test if single port
            int test;
            bool isNumeric;
            if (isNumeric = int.TryParse(portString, out test))
            {
                ports.Add(Convert.ToInt32(portString));
                return ports;
            }

            // test if port string is ',' or '-'
            if (portString.IndexOfAny(",".ToCharArray()) > -1)
            {
                if (portString.IndexOfAny("-".ToCharArray()) != -1)
                {
                    MessageBox.Show("Invalid Port Specification\n");
                    //Environment.Exit(1);
                }

                String[] splitPorts = portString.Split(',');
                foreach (string port in splitPorts)
                {
                    ports.Add(Convert.ToInt32(port));
                }
            }
            else if (portString.IndexOfAny("-".ToCharArray()) > -1)
            {
                String[] splitPorts = portString.Split(new char[] { '-' }, 2);
                int startPort = Convert.ToInt32(splitPorts[0]);
                int endPort = Convert.ToInt32(splitPorts[1]);

                if (startPort > endPort)
                {
                    MessageBox.Show("Invalid Port Specification\n");
                    //Environment.Exit(1);
                }

                if (endPort > 65535)
                {
                    MessageBox.Show("Invalid Port Specification\n");
                    //Environment.Exit(1);
                }

                // do this until i find a replacement for Enumerable.Range()
                endPort = (endPort - startPort) + 1;
                IEnumerable<int> enumPorts = Enumerable.Range(startPort, endPort);
                foreach (int port in enumPorts)
                {
                    ports.Add(port);
                }
            }
            else
            {
                MessageBox.Show("Invalid Port Specification\n");
                //Environment.Exit(1);
            }
            return ports;
        }
    }
}
