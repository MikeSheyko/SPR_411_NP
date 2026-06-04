namespace _04_SMTP_HW
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter sender email: ");
            string? senderEmail = Console.ReadLine();

            Console.Write("Enter app password: ");
            string? password = Console.ReadLine();

            Console.Write("Enter recipient email: ");
            string? recipient = Console.ReadLine();

            Console.Write("Enter subject: ");
            string? subject = Console.ReadLine();

            Console.Write("Enter path to txt/html file: ");
            string? filePath = Console.ReadLine();

            if (!File.Exists(filePath))
            {
                Console.WriteLine("File not found");
                return;
            }

            string attachmentPath = "";

            Console.Write("Add attachment? (y/n): ");
            string? answer = Console.ReadLine();

            if (answer.ToLower() == "y")
            {
                Console.Write("Enter attachment file path: ");
                attachmentPath = Console.ReadLine();

                if (!File.Exists(attachmentPath))
                {
                    Console.WriteLine("Attachment file not found");
                    return;
                }
            }

            try
            {
                EmailService emailService = new EmailService(senderEmail, password);

                emailService.SendMessage(recipient, subject, filePath, attachmentPath);

                Console.WriteLine("Email sent successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }
    }
}
