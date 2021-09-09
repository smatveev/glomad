using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace API.Helpers
{
    public static class EmailSender
    {
        public static void Send()
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            var config = builder.Build();

            var smtpClient = new SmtpClient(config["Smtp:Host"])
            {
                Port = int.Parse(config["Smtp:Port"]),
                Credentials = new NetworkCredential(config["Smtp:Username"], config["Smtp:Password"]),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress("bot@glomad.net"),
                Subject = "New user",
                Body = "<h1>User with email: " + "test@mymail.com" + "</h1><h1>selected plan: 25$</h1>",
                IsBodyHtml = true,
            };
            mailMessage.To.Add("matv33v@gmail.com, grishakyana@gmail.com");

            smtpClient.Send(mailMessage);
        }
    }
}
