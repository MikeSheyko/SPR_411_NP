using System.Net;
using System.Net.Sockets;
using System.Text;

namespace _01_TCP_Client_HW_Task2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            Console.WriteLine("----Оберіть варіант----");
            Console.WriteLine("1 - Час");
            Console.WriteLine("2 - Дата");

            string? choice = Console.ReadLine();

            string request;

            if (choice == "1")
                request = "Time";
            else
                request = "Date";

            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPAddress ip = IPAddress.Parse("127.0.0.1");
            int port = 6000;

            IPEndPoint ipEnd = new IPEndPoint(ip, port);

            server.Connect(ipEnd);

            byte[] requestBytes = Encoding.UTF8.GetBytes(request);

            server.Send(requestBytes);

            byte[] buffer = new byte[1024];

            int len = server.Receive(buffer);

            string response = Encoding.UTF8.GetString(buffer, 0, len);

            Console.WriteLine($"Відповідь сервера: {response}");

            server.Close();
        }
    }
}
