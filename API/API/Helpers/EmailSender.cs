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
                Subject = "Feedback from " + createFeedback.Email,
                Body = string.Format("<h3>New feedback</h3>" +
                    "<p>User name: {0}</p>" +
                    "<p>Email: {1}</p>" +
                    "<p>Selected Country: {2}</p>" +
                    "<p>Is notify: {3}</p>" +
                    "<p>Text: {3}</p>",
                    createFeedback.Username, createFeedback.Email, 
                    createFeedback.CountryId, createFeedback.IsNotify, createFeedback.Text),
                IsBodyHtml = true,
            };
            mailMessage.To.Add("matv33v@gmail.com, grishakyana@gmail.com");

            smtpClient.Send(mailMessage);
        }

        public static void SendImprovePage(ImprovePage improve)
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
                Subject = "Glomad.net: Improve Page fom: " + improve.Email,
                Body = string.Format("<h3>Improve page details</h3>" +
                    "<p>User name: {0}</p>" +
                    "<p>Email: {1}</p>" +
                    "<p>Link: {2}</p>" +
                    "<p>Message: {3}</p>",
                    improve.Name, improve.Email, improve.Link, improve.Message),
                IsBodyHtml = true,
            };
            mailMessage.To.Add("matv33v@gmail.com, grishakyana@gmail.com");

            smtpClient.Send(mailMessage);
        }

        public static void SendVisaReport(ReportVisa report)
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
                Subject = "Glomad.net: Incorrect visa report",
                Body = string.Format("<h3>Incorrect visa info</h3>" +
                    "<p>Link: {0}</p>" +
                    "<p>Text: {1}</p>",
                    report.Url, report.Text),
                IsBodyHtml = true,
            };
            mailMessage.To.Add("matv33v@gmail.com, grishakyana@gmail.com");

            smtpClient.Send(mailMessage);
        }

        public static void SendReview(ReviewCreate review)
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
                Subject = $"Review for visa {review.VisaId} and embassy {review.EmbassyId}",
                Body = string.Format($"<h3>New Review</h3> " +
                    $"for visa: <b>{review.VisaId}<b> " +
                    $"and embassy: <b>{review.EmbassyId}<b>" +
                    $"text: {review.Text}" +
                    $"proc: {review.Pros}" +
                    $"cons: {review.Cons}"),
                IsBodyHtml = true,
            };
            mailMessage.To.Add("matv33v@gmail.com, grishakyana@gmail.com");

            smtpClient.Send(mailMessage);
        }
    }
}
