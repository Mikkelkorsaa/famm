namespace conferencePlannerApi.Services.Interfaces
{
    public interface IEmailService
    {
        /// <summary>
        /// Sends an email asynchronously
        /// </summary>
        /// <param name="to">The recipient's email address</param>
        /// <param name="subject">The subject line of the email</param>
        /// <param name="body">The content/body of the email</param>
        Task SendEmailAsync(string to, string subject, string body);

        /// <summary>
        /// Sends an email using a predefined template
        /// </summary>
        /// <param name="to">The recipient's email address</param>
        /// <param name="templateName">The name of the template to use</param>
        /// <param name="model">The data model to populate the template with</param>
        void SendEmailWithTemplate(string to, string templateName, object model);

        /// <summary>
        /// Sends an email with an attachment asynchronously
        /// </summary>
        /// <param name="to">The recipient's email address</param>
        /// <param name="subject">The subject line of the email</param>
        /// <param name="body">The content/body of the email</param>
        /// <param name="attachment">The file content as a byte array</param>
        /// <param name="fileName">The name of the attached file</param>
        Task SendEmailWithAttachmentAsync(string to, string subject, string body, byte[] attachment, string fileName);
    }
}