using System.Net;
using System.Net.Sockets;
using System.Text;

namespace _01_TCP_HW_Task2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPAddress ip = IPAddress.Parse("127.0.0.1");
            int port = 6000;

            IPEndPoint ipEnd = new IPEndPoint(ip, port);

            server.Bind(ipEnd);
            server.Listen(5);

            Console.WriteLine("Waiting client...");

            while (true)
            {
                Socket client = server.Accept();

                Console.WriteLine($"Client {client.RemoteEndPoint} connected");

                byte[] buffer = new byte[1024];

                int len = client.Receive(buffer);

                string request = Encoding.UTF8.GetString(buffer, 0, len);

                string response;

                if (request == "Time")
                {
                    response = DateTime.Now.ToLongTimeString();
                }
                else if (request == "Date")
                {
                    response = DateTime.Now.ToShortDateString();
                }
                else
                {
                    response = "Помилка";
                }

                byte[] responseBytes = Encoding.UTF8.GetBytes(response);

                client.Send(responseBytes);

                Console.WriteLine($"Send: {response}");

                client.Close();
            }
        }
    }
}
