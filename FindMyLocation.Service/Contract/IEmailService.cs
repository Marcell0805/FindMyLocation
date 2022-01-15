using FindMyLocation.Domain.Settings;
using System.Threading.Tasks;

namespace FindMyLocation.Service.Contract
{
    public interface IEmailService
    {
        Task SendEmailAsync(MailRequest mailRequest);

    }
}
