// egscape-cli.exe
// for use with egressive-server.py
// authors: Tom Steele (tom@huptwo34.co), Dan Kottmann
// 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace Egscape_cli
{
    public class TcpClientWithTimeout
    {
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


    class Program
    {
        static void Usage()
        {
            String progUsage = @"
egressive-client.exe <scan type> <host> <port string>

scan types:   tcp - send tcp connection
              udp - send udp connections    
              proxy - connect to server through system proxy

host:         ip address or hostname

port string:  supported port strings '1-65535', '1,2,3,4'

";
            Console.Write(progUsage);
            Console.Read(); // remove
        }

        static List<int> ParsePortString(String portString)
        {
            var ports = new List<int>();
            
            if (portString.IndexOfAny(",".ToCharArray()) > -1)
            {
                if (portString.IndexOfAny("-".ToCharArray()) != -1)
                {
                    Console.Write("Invalid Port Specification\n");
                    Environment.Exit(1);
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
                    Console.WriteLine("Invalid Port Specification\n");
                    Environment.Exit(1);
                }

                if (endPort > 65535)
                {
                    Console.WriteLine("Invalid Port Specification\n");
                    Environment.Exit(1);
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
                Console.Write("Invalid Port Specification\n");
                Environment.Exit(1);
            }
            
            return ports;
        }

        static void TcpScan(List<int> ports, String host)
        {
            foreach (int port in ports)
            {

                Console.Write("\rsending port {0}", port);
                // send one request every 50 milliseconds
                // hack here using threads to create a false timeout
                TcpClient connection = new TcpClientWithTimeout(host, port, 50).Connect();
            }
            Console.WriteLine();

        }

        static void UdpScan(List<int> ports, String host)
        {
            foreach (int port in ports)
            {
                Console.Write("\rsending port {0}", port);
                UdpClient udpClient = new UdpClient();
                udpClient.Connect(host, port);
                // Sends a message to the host to which you have connected.
                Byte[] sendBytes = Encoding.ASCII.GetBytes("egress check");
                udpClient.Send(sendBytes, sendBytes.Length);
                udpClient.Close();
                Thread.Sleep(10);
            }
            Console.WriteLine();
        }

        // TODO: this needs to use a thread pool
        static void ProxyScan(List<int> ports, String host)
        {

            foreach (int port in ports)
            {
                Console.Write("\rsending port {0}", port);
                String url = "http://" + host + ":" + Convert.ToString(port);
                try
                {
                    WebRequest request = WebRequest.Create(url);
                    request.Timeout = 1000;
                    WebResponse response = request.GetResponse();
                    new StreamReader(response.GetResponseStream()).ReadToEnd(); 
                }
                catch (Exception)
                {
                }
            }
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            if (args.Length == 1)
            {
                if (args[0] == "--help")
                {
                    Usage();
                }
            }
  

            if (args.Length != 3)
            {
                Console.Write("Not enough arguments\n");
                Usage();
                Environment.Exit(1);
            }

            String scanType = args[0];
            String servAddr = args[1];
            String portString = args[2];

            String[] validScanTypes = { "tcp", "udp", "proxy" };
            int testPos = Array.IndexOf(validScanTypes, scanType);
            if (testPos == -1)
            {
                Console.Write("Invalid scan type\n");
                Environment.Exit(1);

            }

            var ports = ParsePortString(portString);

            if (scanType == "tcp")
            {
                Console.WriteLine("starting tcp check");
                TcpScan(ports, servAddr);
            }
            else if (scanType == "udp")
            {
                Console.WriteLine("starting udp check");
                UdpScan(ports, servAddr);
            }
            else if (scanType == "proxy")
            {
                Console.WriteLine("starting proxy check");
                ProxyScan(ports, servAddr);
            }
            else
            {
                Console.Write("Something went wrong\n");
            }

            Console.WriteLine("Complete!");
        }
    }
}
