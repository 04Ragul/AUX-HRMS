using HRMS.Shared.Utilities.Requests.Mail;

namespace HRMS.Application.Interfaces.Services
{
    public interface IMailService
    {
        Task SendAsync(MailRequest request);
    }
}
