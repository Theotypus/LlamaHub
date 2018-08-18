using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows;
using System.Xml.Linq;

namespace LCP_API
{
    class ServerConnection
    {
        private TcpClient Client;  // Low-level connection to the server
        private Stream Stream;

        public IPAddress IpAddress;
        public int Port;

        public bool Connected { get; private set; } // To be connected or not to be connected !
        private Thread ThrReceive; // Receive incoming messages

        private LCPClient Controller;  // Needed to process incoming commands

        public ServerConnection(LCPClient controller)
        {
            Controller = controller;
        }

        // Initialize the connection with the server
        public void Connect(IPAddress ip, int port)
        {
            IpAddress = ip;
            Port = port;
            Client = new TcpClient(); // initialize a TCP connection with the server
            try
            {
                Client.Connect(IpAddress, port); // connecting using the ip and port number provided
            }
            catch (SocketException e)
            {
                Console.WriteLine(e);
                return;
            }

            // updating our variables
            Connected = true;
            Controller.RaiseStateChangeEvent(new ClientStateChangedEvent(Connected));
            Stream = Client.GetStream();

            // start the thread to receive messages
            ThrReceive = new Thread(new ThreadStart(Receive));
            ThrReceive.Start();
        }

        /*Encodes the xml as a string and then sends it
         * The length is sent first (followed by ':') to make sure the message in received entierely */

        public void Send(XDocument command)
        {
            string message = command.Declaration.ToString() + Environment.NewLine + command.ToString();
            string length = Encoding.UTF8.GetByteCount(message) + ":";

            Console.WriteLine(length);
            Console.WriteLine(message);
            byte[] lgth = Encoding.UTF8.GetBytes(length);
            byte[] msg = Encoding.UTF8.GetBytes(message);
            if (Connected)
            {
                Stream.Write(lgth, 0, lgth.Length);
                Stream.Write(msg, 0, msg.Length);
                Stream.Flush();
            }
        }

        // Thread to receive and process messages as they come in
        private void Receive()
        {
            while (Connected)
            {
                try
                {
                    // Read the length of the xml string that follows (until the ':')
                    char data = (char)Stream.ReadByte();
                    List<string> received = new List<string>
                    {
                        data.ToString()
                    };
                    char last;
                    do
                    {
                        last = (char)Stream.ReadByte();
                        received.Add(last.ToString());
                    } while (last != ':');

                    received.RemoveAt(received.Count - 1);
                    int bytes = int.Parse(string.Join("", received));

                    // Then receive the actual command
                    byte[] buffer = new byte[bytes];
                    Stream.Read(buffer, 0, bytes);
                    string xml_Str = Encoding.UTF8.GetString(buffer);
                    XDocument xml = XDocument.Parse(xml_Str);
                    Console.WriteLine(xml.ToString());

                    // And process it
                    CommandReceiver.Process(Controller, xml);
                }
                catch (IOException e)
                {
                    Console.WriteLine(e);
                    Close();
                    break;
                }
            }

        }

        // Closes the connection
        public void Close()
        {
            if (Connected)
            {
                Connected = false;
                Controller.LoggedIn = false;
                Controller.RaiseStateChangeEvent(new ClientStateChangedEvent(Connected));
                Stream.Close();
                Client.Close();
            }
        }
    }
}
