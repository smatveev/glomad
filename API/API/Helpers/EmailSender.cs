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
                From = new MailAddress("bot@glomad.net"),
                Subject = "New user with plan #" + plan.PlanNumber.ToString(),
                Body = string.Format("<h3>New user</h3>" +
                    "<p>name: {0}</p>" +
                    "<p>with email: {1}</p>" +
                    "<p>selected plan: {2}</p>",
                    plan.Username, plan.Email, plan.PlanNumber),
                IsBodyHtml = true,
            };
            mailMessage.To.Add("matv33v@gmail.com, grishakyana@gmail.com");

            smtpClient.Send(mailMessage);
        }
    }
}
