using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Egscape_gui
{
    public class TcpClientWithTimeout
    {
        // this is pretty much a hack to implement a timeout on the TcpClient class
        /* i stole most of this code
        http://www.splinter.com.au/opening-a-tcp-connection-in-c-with-a-custom-t/
        */
        protected TcpClient connection;
        protected string _hostname;
        protected int _port;
        protected int _timeout_milliseconds;

        public TcpClientWithTimeout(string hostname, int port, int timeout_milliseconds)
        {
            _hostname = hostname;
            _port = port;
            _timeout_milliseconds = timeout_milliseconds;
        }

        public TcpClient Connect()
        {

            Thread thread = new Thread(new ThreadStart(BeginConnect));
            thread.IsBackground = true;
            thread.Start();
            thread.Join(_timeout_milliseconds);
            thread.Abort();
            return connection;
        }


        protected void BeginConnect()
        {
            try
            {
                connection = new TcpClient(_hostname, _port);
            }
            catch (Exception)
            {

            }
        }

    }
}
