using System;
using System.Net.Sockets;
using System.Text;

namespace SocketClient
{
    class Program
    {
        static TcpClient client = new TcpClient();

        static void Main(string[] args)
        {
            Console.WriteLine("Client Started");
            client.Connect("127.0.0.1", 8888);
            Console.WriteLine("Client Socket Program - Server Connected ...");
        }

        static void NewMsg()
        {
            NetworkStream stream = client.GetStream();
            
        }
    }
}
