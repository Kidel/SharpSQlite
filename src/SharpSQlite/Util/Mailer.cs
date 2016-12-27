using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using MailKit.Security;

namespace SharpSQlite.Util
{
    public class Mailer
    {
        public async Task SendEmailAsync(string email, string subject, string message, string name = "")
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Gaetano Bonofiglio", "developer@SharpSQlite.azurewebsites.net"));
            emailMessage.To.Add(new MailboxAddress(name, email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart("plain") { Text = message };

            using (var client = new SmtpClient())
            {
                client.LocalDomain = "SharpSQlite.azurewebsites.net";
                await client.ConnectAsync("smtp.relay.uri", 25, SecureSocketOptions.None).ConfigureAwait(false);
                await client.SendAsync(emailMessage).ConfigureAwait(false);
                await client.DisconnectAsync(true).ConfigureAwait(false);
            }
        }
    }
}
