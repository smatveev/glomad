using API.Models;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace API.Helpers
{
    public static class EmailSender
    {
        public static void Send(SelectPlan plan)
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
                From = new MailAddress("matv33v@gmail.com"),
                Subject = "New user: " + plan.Email,
                Body = string.Format("<h3>New user</h3>" +
                    "<p>Name: {0}</p>" +
                    "<p>Email: {1}</p>" +
                    "<p>Plan: {2}</p>" +
                    "<p>Details: {3}</p>",
                    plan.Username, plan.Email, plan.Name, plan.Details),
                IsBodyHtml = true,
            };
            mailMessage.To.Add("matv33v@gmail.com, grishakyana@gmail.com");

            smtpClient.Send(mailMessage);
        }

        public static void SendFeedback(CreateFeedback createFeedback)
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
                From = new MailAddress("matv33v@gmail.com"),
                Subject = "Feedback: " + createFeedback.Email,
                Body = string.Format("<h3>New feedback</h3>" +
                    "<p>User name: {0}</p>" +
                    "<p>Email: {1}</p>" +
                    "<p>Selected Country: {2}</p>" +
                    "<p>Is notify: {3}</p>",
                    createFeedback.Username, createFeedback.Email, createFeedback.CountryId, createFeedback.IsNotify),
                IsBodyHtml = true,
            };
            mailMessage.To.Add("matv33v@gmail.com, grishakyana@gmail.com");

            smtpClient.Send(mailMessage);
        }
    }
}
