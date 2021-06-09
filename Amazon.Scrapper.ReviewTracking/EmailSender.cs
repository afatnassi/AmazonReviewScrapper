using MimeKit;
using MailKit;
using MailKit.Net.Smtp;


namespace Amazon.Scrapper.ReviewTracking
{
	public class EmailSender : IEmailSender
	{
        private readonly string hostAddress = "";
        private readonly int hostPort = 0;
        private readonly string senderEmail = "";
        private readonly string senderPassword = "";
        private readonly string recipientEmail = "";

        public async void Send(string emailBody, string emailSubject)
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
	}
}
