using TestCode.Entities;

namespace TestCode.Services.Interfaces
{
    public interface IMailService
    {
        Task SendMail(MailContent mailContent);
    }
}
