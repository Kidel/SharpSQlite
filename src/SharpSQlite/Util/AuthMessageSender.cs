using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using MailKit.Security;

namespace SharpSQlite.Util
{
    public class AuthMessageSender : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string message, string name = "")
        {
            // Plug in your email service here to send an email.
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress(Config.name, Config.email));
            emailMessage.To.Add(new MailboxAddress(name, email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart("plain") { Text = message };

            await Task.Run(() =>
            {
                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.live.com", 587, false);

                    // Note: since we don't have an OAuth2 token, disable
                    // the XOAUTH2 authentication mechanism.
                    client.AuthenticationMechanisms.Remove("XOAUTH2");

                    // Note: only needed if the SMTP server requires authentication
                    client.Authenticate(Config.email, Config.password);

                    client.Send(emailMessage);
                    client.Disconnect(true);
                }
            });
        }
    }
}
