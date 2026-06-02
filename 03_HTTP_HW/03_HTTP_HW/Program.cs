using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace _03_HTTP_HW
{
    internal class Program
    {
        static readonly HttpClient client = new HttpClient()
        {
            BaseAddress = new Uri("https://jsonplaceholder.typicode.com/")
        };

        static async Task Main(string[] args)
        {
            //Завантаження файлу з URL
            Console.Write("Введіть URL фото: ");
            string? url = Console.ReadLine();

            Console.Write("Введіть шлях для збереження (C:\\): ");
            string? path = Console.ReadLine();

            Console.Write("Введіть назву файлу (наприклад name.jpg): ");
            string? fileName = Console.ReadLine();

            await DownloadImage(url, path, fileName);

            Console.WriteLine("Завантаження завершено!");

            static async Task DownloadImage(string url, string path, string fileName)
            {
                byte[] imageBytes = await client.GetByteArrayAsync(url);

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                string fullPath = Path.Combine(path, fileName);

                await File.WriteAllBytesAsync(fullPath, imageBytes);
            }

            //Menu api
            while (true)
            {
                Console.WriteLine("------------MENU------------");
                Console.WriteLine("[1] - Posts");
                Console.WriteLine("[2] - Comments");
                Console.WriteLine("[3] - Albums");
                Console.WriteLine("[4] - Photos");
                Console.WriteLine("[5] - Todos");
                Console.WriteLine("[6] - Users");
                Console.WriteLine("[0] - Exit");

                Console.Write("Choose option: ");
                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        var posts = await GetData<Post>("posts");
                        foreach (var p in posts)
                            Console.WriteLine($"{p.id}: {p.title}");
                        break;

                    case "2":
                        var comments = await GetData<Comment>("comments");
                        foreach (var c in comments)
                            Console.WriteLine($"{c.id}: {c.email} - {c.body}");
                        break;

                    case "3":
                        var albums = await GetData<Album>("albums");
                        foreach (var a in albums)
                            Console.WriteLine($"{a.id}: {a.title}");
                        break;

                    case "4":
                        var photos = await GetData<Photo>("photos");
                        foreach (var ph in photos)
                            Console.WriteLine($"{ph.id}: {ph.title} | {ph.url}");
                        break;

                    case "5":
                        var todos = await GetData<Todo>("todos");
                        foreach (var t in todos)
                            Console.WriteLine($"{t.id}: {t.title} [{t.completed}]");
                        break;

                    case "6":
                        var users = await GetData<User>("users");
                        foreach (var u in users)
                            Console.WriteLine($"{u.id}: {u.name} ({u.email})");
                        break;

                    case "0":
                        return;
                }
            }
        }

        static async Task<T[]> GetData<T>(string endpoint)
        {
            var response = await client.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<T[]>(json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? Array.Empty<T>();
        }
    }
}