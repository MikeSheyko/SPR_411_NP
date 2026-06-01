using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    public enum Command
    {
        quote = 1,
        exit
    }

    internal class Program
    {
        static Dictionary<Command, string> commands = new Dictionary<Command, string>();

        static async Task Main(string[] args)
        {
            commands.Add(Command.quote, "quote");

            commands.Add(Command.exit, "exit");

            try
            {
                TcpClient client = new TcpClient();
                await client.ConnectAsync(IPAddress.Parse("127.0.0.1"), 5000);
                NetworkStream stream = client.GetStream();

                while (true)
                {
                    Console.WriteLine();
                    Console.WriteLine("[1] - Get quote");
                    Console.WriteLine("[0] - Exit");

                    int input;
                    bool result = int.TryParse(Console.ReadLine(), out input);

                    if (!result)
                        continue;

                    if (input == 0)
                    {
                        byte[] exitData = Encoding.UTF8.GetBytes("exit");
                        await stream.WriteAsync(exitData);
                        break;
                    }

                    Command command = (Command)input;

                    byte[] data = Encoding.UTF8.GetBytes(commands[command]);
                    await stream.WriteAsync(data);

                    byte[] buffer = new byte[1024];

                    int len = await stream.ReadAsync(buffer);

                    string response = Encoding.UTF8.GetString(buffer, 0, len);

                    Console.WriteLine();
                    Console.WriteLine($"Quote: {response}");
                }

                stream.Dispose();
                client.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
