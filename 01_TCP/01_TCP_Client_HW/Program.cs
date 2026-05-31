using System.Net;
using System.Net.Sockets;
using System.Text;

namespace _01_TCP_Client_HW
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

            server.Connect(ipEnd);

            string message = "Привіт, сервер!";

            byte[] messageBytes = Encoding.UTF8.GetBytes(message);

            server.Send(messageBytes);

            byte[] buffer = new byte[1024];

            int len = server.Receive(buffer);

            string serverMessage = Encoding.UTF8.GetString(buffer, 0, len);

            Console.WriteLine($"О {DateTime.Now:HH:mm} від {server.RemoteEndPoint} отримано рядок: {serverMessage}");

            server.Close();
        }
    }
}
