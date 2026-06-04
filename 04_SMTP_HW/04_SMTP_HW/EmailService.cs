using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace _04_SMTP_HW
{
    public class EmailService
    {
        private readonly string host;
        private readonly int port;
        private readonly string email;
        private readonly string password;
        private readonly SmtpClient smtpClient;

        public EmailService(string email, string password)
        {
            this.email = email;
            this.password = password;

            host = "smtp.gmail.com";
            port = 587;

            smtpClient = new SmtpClient(host, port);
            smtpClient.Credentials = new NetworkCredential(email, password);
            smtpClient.EnableSsl = true;
        }

        public void SendMessage(string to, string subject, string filePath, string attachmentPath = "")
        {
            MailMessage message = new MailMessage();

            message.From = new MailAddress(email);

            message.To.Add(to);

            message.Subject = subject;

            string body = File.ReadAllText(filePath);
            message.Body = body;

            string extension = Path.GetExtension(filePath);

            if (extension.ToLower() == ".html")
            {
                message.IsBodyHtml = true;
            }

            if (!string.IsNullOrWhiteSpace(attachmentPath) && File.Exists(attachmentPath))
            {
                Attachment attachment = new Attachment(attachmentPath);
                message.Attachments.Add(attachment);
            }

            smtpClient.Send(message);
        }
    }
}
