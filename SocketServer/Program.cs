using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;


namespace SocketServer
{
    class Program
    {
        private const int BUFSIZE = 32;

        static void Main(string[] args)
        {
            if (args.Length > 1)
                throw new ArgumentException("");

            int servPort = (args.Length == 1) ? Int32.Parse(args[0]) : 7;

            TcpListener listener = null;

            try
            {
                listener = new TcpListener(IPAddress.Any, 8888);
                listener.Start();
            }
            catch(SocketException se)
            {
                Console.WriteLine(se.Message);
                Environment.Exit(se.ErrorCode);
            }

            byte[] rcvBuffer = new byte[BUFSIZE];
            int bytesRcvd;

            while (true)
            {
                TcpClient client = null;
                NetworkStream ns = null;

                try
                {
                    client = listener.AcceptTcpClient();
                    ns = client.GetStream();
                    Console.Write("Handling client -");

                    int totalBytesEchoed = 0;
                    while ((bytesRcvd = ns.Read(rcvBuffer, 0, rcvBuffer.Length)) > 0)
                    {
                        ns.Write(rcvBuffer, 0, bytesRcvd);
                        totalBytesEchoed += bytesRcvd;
                    }
                    Console.WriteLine("echoed {0} bytes.", totalBytesEchoed);

                    ns.Close();
                    client.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    ns.Close();
                }
            }
        }
    }
}
