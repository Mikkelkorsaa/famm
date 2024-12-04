using conferencePlannerCore.Models;
using conferencePlannerApi.Services.Interfaces;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace conferencePlannerApi.Services.Implementations
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfiguration _config;

        public EmailService(IOptions<EmailConfiguration> config)
        {
            _config = config.Value ?? throw new ArgumentNullException(nameof(config));
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(_config.FromName, _config.FromEmail));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = body };

            using var client = new SmtpClient();
            try
            {
        await client.ConnectAsync(_config.SmtpServer, _config.SmtpPort, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(_config.SmtpUsername, _config.SmtpPassword);
                await client.SendAsync(email);
                await client.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Failed to send email to {to}", ex);
            }
        }

        public async Task SendEmailWithTemplateAsync(string to, string templateName, object model)
        {
            throw new NotImplementedException("Template-based emails not yet implemented");
        }

        public async Task SendEmailWithAttachmentAsync(string to, string subject, string body, byte[] attachment, string fileName)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(_config.FromName, _config.FromEmail));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;

            var builder = new BodyBuilder { HtmlBody = body };
            
            if (attachment != null && fileName != null)
            {
                builder.Attachments.Add(fileName, attachment);
            }

            email.Body = builder.ToMessageBody();

            using var client = new SmtpClient();
            try
            {
                await client.ConnectAsync(_config.SmtpServer, _config.SmtpPort, _config.UseSSL);
                await client.AuthenticateAsync(_config.SmtpUsername, _config.SmtpPassword);
                await client.SendAsync(email);
                await client.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Failed to send email with attachment to {to}", ex);
            }
        }
    }
}