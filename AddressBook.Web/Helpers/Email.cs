using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace AddressBook.Helpers
{
    public static class Email
    {
        public static void SendMail(string toAddress, string subject, string body)
        {
            SendMail(Config.GetFromEmail(), toAddress, subject, body);
        }

        public static void SendMail(string fromAddress, string toAddress, string subject, string body)
        {
            using (MailMessage mail = BuildMessageWith(fromAddress, toAddress.Replace(',', ';'), subject, body))
            {
                SendMail(mail);
            }
        }

        public static void SendMail(MailMessage mail)
        {
            int smtpServerPort = 0;
            int.TryParse(Config.GetSMTPServerPort(), out smtpServerPort);
            if (smtpServerPort == 0)
                smtpServerPort = 25;
            try
            {
                var smtp = new SmtpClient();
                smtp.Host = Config.GetSMTPServer();
                smtp.Port = smtpServerPort;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(
                    Config.GetSMTPServerUserName(),
                    Config.GetSMTPServerPassword()
                );
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Send mail error: {ex.Message}");
            }
        }

        // Build a mail message.
        private static MailMessage BuildMessageWith(string fromAddress, string toAddress, string subject, string body)
        {
            var message = new MailMessage
            {
                Sender = new MailAddress(fromAddress),
                From = new MailAddress(fromAddress),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };
            string[] tos = toAddress.Split(';');

            foreach (string to in tos)
            {
                message.To.Add(new MailAddress(to));
            }
            return message;
        }

        // Read the text in a template file and return it as a string.
        private static string ReadFileFrom(string templateName)
        {
            string filePath = System.Web.HttpContext.Current.Server.MapPath("~/Mailer/" + templateName);
            string body = File.ReadAllText(filePath);

            return body;
        }

        // Get the template body, cache it and return the text.
        private static string GetMailBodyOfTemplate(string templateName)
        {
            string cacheKey = string.Concat("mailTemplate:", templateName);
            string body;
            body = (string)System.Web.HttpContext.Current.Cache[cacheKey];
            if (string.IsNullOrEmpty(body))
            {
                // Read template file text.
                body = ReadFileFrom(templateName);

                if (!string.IsNullOrEmpty(body))
                {
                    System.Web.HttpContext.Current.Cache.Insert(cacheKey, body, null, DateTime.Now.AddHours(1), System.Web.Caching.Cache.NoSlidingExpiration);
                }
            }
            return body;
        }

        // Replace the tokens in template body with corresponding values.
        public static string PrepareMailBodyWith(string templateName, Dictionary<string, string> pairs)
        {
            string body = GetMailBodyOfTemplate(templateName);

            pairs.ForEach(i =>
            {
                body = body.Replace("{{" + i.Key + "}}", i.Value);
            });
            return body;
        }

        public static string FormatWith(this string target, params object[] args) => string.Format(Constants.CurrentCulture, target, args);
    }
}

public static class Constants
{
    // Culture info.
    public static CultureInfo CurrentCulture => CultureInfo.CurrentCulture;
}