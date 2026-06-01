using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    public enum Command
    {
        quote = 1,
        exit
    }

    public class TcpServer
    {
        private static Random random = new Random();

        private Dictionary<Command, string> commands;
        private List<Quote> quotes;

        public TcpServer()
        {
            commands = new Dictionary<Command, string>
            {
                { Command.quote, "quote" },
                { Command.exit, "exit" }
            };

            quotes = new List<Quote>
            {
                new Quote("I have a dream.", "Martin Luther King Jr."),
                new Quote("The only thing we have to fear is fear itself.", "Franklin D. Roosevelt"),
                new Quote("Imagination is more important than knowledge.", "Albert Einstein"),
                new Quote("Be the change that you wish to see in the world.", "Mahatma Gandhi"),
                new Quote("In the middle of difficulty lies opportunity.", "Albert Einstein"),
                new Quote("That's one small step for man, one giant leap for mankind.", "Neil Armstrong"),
                new Quote("Stay hungry, stay foolish.", "Steve Jobs"),
                new Quote("Knowledge is power.", "Francis Bacon"),
                new Quote("It always seems impossible until it's done.", "Nelson Mandela"),
                new Quote("Do not wait to strike till the iron is hot; but make it hot by striking.", "William Butler Yeats")
            };
        }

        public async Task StartAsync(int port)
        {
            TcpListener listener = new TcpListener(IPAddress.Parse("127.0.0.1"), port);
            listener.Start();
            Console.WriteLine($"Server started on 127.0.0.1:{port}");

            while (true)
            {
                TcpClient client = await listener.AcceptTcpClientAsync();
                _ = Task.Run(() => HandleClientAsync(client));
            }
        }

        public async Task HandleClientAsync(TcpClient client)
        {
            NetworkStream stream = client.GetStream();

            string clientInfo = client.Client.RemoteEndPoint?.ToString() ?? "Unknown";

            DateTime connectTime = DateTime.Now;

            List<Quote> sentQuotes = new List<Quote>();

            Console.WriteLine($"Client connected: {clientInfo}");
            Console.WriteLine($"Connection time: {connectTime}");

            try
            {
                byte[] buffer = new byte[1024];

                while (true)
                {
                    int len = await stream.ReadAsync(buffer);

                    if (len == 0)
                        break;

                    string request = Encoding.UTF8.GetString(buffer, 0, len).Trim();

                    Console.WriteLine($"Request from {clientInfo}: {request}");

                    Command command = GetCommand(request);

                    string response = string.Empty;

                    switch (command)
                    {
                        case Command.quote:
                            Quote quote = quotes[random.Next(quotes.Count)];
                            response = quote.ToString();
                            sentQuotes.Add(quote);
                            break;

                        case Command.exit:
                            await Send(stream, "bye");
                            return;

                        default:
                            response = "Unknown command";
                            break;
                    }
                    await Send(stream, response);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                Console.WriteLine();
                Console.WriteLine($"Client disconnected: {clientInfo}");
                Console.WriteLine($"Time: {DateTime.Now}");

                Console.WriteLine("Quotes sent:");

                foreach (var q in sentQuotes)
                    Console.WriteLine(q);

                Console.WriteLine("--------------------------------");

                stream.Dispose();
                client.Close();
            }
        }

        private async Task Send(NetworkStream stream, string message)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(message);
            await stream.WriteAsync(bytes);
        }

        private Command GetCommand(string value)
        {
            foreach (var item in commands)
            {
                if (item.Value == value)
                    return item.Key;
            }

            return Command.quote;
        }
    }
}
