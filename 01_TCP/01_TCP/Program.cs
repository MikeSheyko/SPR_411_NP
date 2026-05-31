using System.Net;
using System.Net.Sockets;
using System.Text;

namespace _01_TCP
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPAddress ip = IPAddress.Parse("127.0.0.1");
            int port = 7000;

            IPEndPoint ipEnd = new IPEndPoint(ip, port);

            server.Bind(ipEnd);
            server.Listen(5);

            Console.WriteLine("Waiting client...");

            Socket client = server.Accept();

            Console.WriteLine($"Client {client.RemoteEndPoint} connected");

            byte[] buffer = new byte[1024];

            int len = client.Receive(buffer);

            string clientMessage = Encoding.UTF8.GetString(buffer, 0, len);

            Console.WriteLine($"О {DateTime.Now:HH:mm} від {client.RemoteEndPoint} отримано рядок: {clientMessage}");

            string message = "Привіт, клієнт!";
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);

            client.Send(messageBytes);

            client.Close();
            server.Close();
        }
    }
}
