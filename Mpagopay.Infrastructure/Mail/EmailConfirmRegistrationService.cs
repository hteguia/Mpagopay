using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Mpagopay.Application.Contrats.Infrastructure;
using Mpagopay.Application.Models.Mail;
using Mpagopay.Application.Tools;
using Newtonsoft.Json;

namespace Mpagopay.Infrastructure.Mail
{
    public class EmailConfirmRegistrationService : IEmailService
    {
        private EmailSettings _emailSettings { get; }
        private ILogger<EmailService> _logger;

        public EmailConfirmRegistrationService(IOptions<EmailSettings> mailSettings, ILogger<EmailService> logger)
        {
            _emailSettings = mailSettings.Value;
            _logger = logger;
        }

        public async Task<bool> SendEmail(Email email)
        {
            try
            {
                var emailModel = new
                {
                    From = new { Email = _emailSettings.SenderEmail, Name = _emailSettings.SenderName },
                    To = new[] { new { Email = email.To } },
                    template_uuid = _emailSettings.RegisterUuid,
                    template_variables = new { confirm_link = email.Body }
                };

                var client = new HttpClient();
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_emailSettings.Token}");
                string requestContent = JsonConvert.SerializeObject(emailModel);
                var stringContent = new StringContent(requestContent, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(_emailSettings.Url, stringContent);
                var responseContent = await response.Content.ReadAsStringAsync();
                var dataResponse = JsonConvert.DeserializeObject<object>(responseContent);

                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Email sent successfully");
                    return true;
                }

                _logger.LogInformation("Email sending failed");

                return false;
            }
            catch(Exception ex)
            {
                return false;


            }
            
        }
    }
}
