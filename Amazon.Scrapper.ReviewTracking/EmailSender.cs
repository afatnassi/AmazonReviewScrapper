using MimeKit;
using MailKit;
using MailKit.Net.Smtp;
using System;

namespace Amazon.Scrapper.ReviewTracking
{
	public class EmailSender : IEmailSender
	{
        private readonly string hostAddress = "fakeaddress";
        private readonly int hostPort = 0;
        private readonly string senderEmail = "fake@address.com";
        private readonly string senderPassword = "";
        private readonly string recipientEmail = "fake@address.com";

        public async void Send(string emailBody, string emailSubject)
		{
            try
            {
                var msg = new MimeMessage();
                msg.From.Add(new MailboxAddress("Web Scraper", "scrapey@localhost.com"));
                msg.To.Add(new MailboxAddress("Me", recipientEmail));

                msg.Subject = emailSubject;
                msg.Body = new TextPart("plain") { Text = emailBody };

                using (var client = new SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    client.Connect(hostAddress, hostPort, MailKit.Security.SecureSocketOptions.StartTls);
                    client.Authenticate(senderEmail, senderPassword);

                    await client.SendAsync(msg);
                    client.Disconnect(true);
                }
            }
            catch (Exception)
            {
                //the configs need to be set correctly for it to work
            }
        }
	}
}
