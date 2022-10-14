using MailKit.Security;
using MimeKit;
using TestCode.Entities;
using TestCode.Services.Interfaces;

namespace TestCode.Services
{
    public class MailService : IMailService
    {
        private readonly IConfiguration _configuration;
        private readonly MailSettings _mailSettings;
        private readonly ILogger _logger;

        public MailService(IConfiguration configuration, ILogger<MailService> logger)
        {
            _configuration = configuration;
            _mailSettings = configuration.GetSection("MailSettings").Get<MailSettings>();
            _logger = logger;
        }

        public async Task SendMail(MailContent mailContent)
        {
            var email = new MimeMessage();
            email.Sender = new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Mail);
            email.From.Add(new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Mail));
            email.To.Add(MailboxAddress.Parse(mailContent.To));
            email.Subject = mailContent.Subject;


            var builder = new BodyBuilder();
            builder.HtmlBody = mailContent.Body;
            email.Body = builder.ToMessageBody();

            using var smtp = new MailKit.Net.Smtp.SmtpClient();

            try
            {
                smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
                await smtp.SendAsync(email);
                _logger.LogInformation($"Send mail to {mailContent.To} success");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Send mail to {mailContent.To} fail");
                _logger.LogError(ex.Message);
            }

            smtp.Disconnect(true);
        }
    }
}
