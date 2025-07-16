using System.Net.Mail;
using Template.Application.Interfaces.Email;

namespace Template.Application.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly SmtpClient smtpClient;

        public EmailService(SmtpClient smtpClient)
        {
            this.smtpClient = smtpClient;
        }
        public Task<bool> SendEmailAsync(string to, string subject, string body)
        {
            throw new Exception();


        }
    }
}
