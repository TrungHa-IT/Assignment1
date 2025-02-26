namespace ASS1.Services
{
    using MailKit.Net.Smtp;
    using MailKit.Security;
    using MimeKit;
    using Microsoft.Extensions.Configuration;
    using System.Threading.Tasks;

    public class EmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var emailSettings = _configuration.GetSection("EmailSettings");

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("AssignmentPRN222", emailSettings["SenderEmail"]));
            message.To.Add(new MailboxAddress("", toEmail));
            message.Subject = subject;

            message.Body = new TextPart("html") { Text = body };

            using var client = new SmtpClient();
            await client.ConnectAsync(emailSettings["SmtpServer"], int.Parse(emailSettings["Port"]), SecureSocketOptions.StartTls);

            await client.AuthenticateAsync(emailSettings["SenderEmail"], emailSettings["SenderPassword"]);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }

}
