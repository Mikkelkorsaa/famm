using System.Threading.Tasks;

namespace conferencePlannerApi.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string body);
        Task SendEmailWithTemplateAsync(string to, string templateName, object model);
        Task SendEmailWithAttachmentAsync(string to, string subject, string body, byte[] attachment, string fileName);
    }
}