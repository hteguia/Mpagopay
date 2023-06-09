using System.Text;
using Microsoft.Extensions.Options;
using Mpagopay.Application.Contrats.Infrastructure;
using Mpagopay.Application.Models.Mail;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Mpagopay.Application.Tools;

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
            var emailModel = new
            {
                From = new { Email = _emailSettings.SenderEmail, Name = _emailSettings.SenderName },
                To = new[] {new { Email = email.To } },
                email.Subject,
                Text = email.Body,
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
    }
}
