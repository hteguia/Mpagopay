using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Mpagopay.Application.Contrats.Infrastructure;
using Mpagopay.Application.Models.Mail;
using SendGrid;
using SendGrid.Helpers.Mail;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Mpagopay.Application.Models.Sms;
using Mpagopay.Infrastructure.Sms;

namespace Mpagopay.Infrastructure.Mail
{
    public class EmailService : IEmailService
    {
        private EmailSettings _emailSettings { get; }
        private ILogger<EmailService> _logger;
        public EmailService(IOptions<EmailSettings> mailSettings, ILogger<EmailService> logger)
        {
            _emailSettings= mailSettings.Value;
            _logger = logger;
        }
        public async Task<bool> SendEmail(Email email)
        {
            var client = new SendGridClient(_emailSettings.ApiKey);

            var subject = email.Subject;
            var to = new EmailAddress(email.To);
            var emailBody = email.Body;

            var from = new EmailAddress
            {
                Email = _emailSettings.FromAddress,
                Name = _emailSettings.FromName
            };

            _logger.LogInformation("Email sent");

            var sendGridMessage = MailHelper.CreateSingleEmail(from, to, subject, emailBody, emailBody);
            var response = await client.SendEmailAsync(sendGridMessage);
            if(response.StatusCode == System.Net.HttpStatusCode.Accepted || response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }

            _logger.LogInformation("Email sending failed");

            return false;
            
        }
    }
}
