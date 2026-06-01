using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TcpServer server = new TcpServer();
            server.StartAsync(5000).Wait();
        }
    }
}

