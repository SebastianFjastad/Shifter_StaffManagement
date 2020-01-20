using System;
using System.Net;
using System.Net.Mail;

namespace Framework.Email
{
    public static class EmailUtils
    {
        public static SmtpClient CreateSmtpClient()
        {
            var client = new SmtpClient(SharedConfig.SMTPHost);

            client.DeliveryMethod = SmtpDeliveryMethod.Network;

            if (SharedConfig.SMTPEmailUsername.IsNotNullOrEmpty())
            {
                client.Credentials = new NetworkCredential(SharedConfig.SMTPEmailUsername, SharedConfig.SMTPEmailPassword);
            }

            //client.EnableSsl = SharedConfig.SMTPEnableSsl;

            return client;
        }

        /// <summary>
        /// Creates a mail message from the provided params
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="fromEmailAddress"></param>
        /// <param name="toEmailAddress"></param>
        /// <returns></returns>
        public static MailMessage CreateMailMessage(string subject, string body, string fromEmailAddress, string toEmailAddress)
        {
            var msg = new MailMessage()
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };

            msg.From = new MailAddress(fromEmailAddress);

            var toAddress = new MailAddress(toEmailAddress);

            msg.To.Add(toAddress);

            return msg;
        }
    }
}
