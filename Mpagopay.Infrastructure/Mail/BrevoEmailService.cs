using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Mpagopay.Application.Contrats.Infrastructure;
using Mpagopay.Application.Models.Mail;
using Mpagopay.Application.Models.Mail.Brevo;
using Newtonsoft.Json;

namespace Mpagopay.Infrastructure.Mail
{
    public class BrevoEmailService : IEmailService
    {
        private BrevoEmailSettings _emailSettings { get; }
        private ILogger<EmailService> _logger;
        public BrevoEmailService(IOptions<BrevoEmailSettings> mailSettings, ILogger<EmailService> logger)
        {
            _emailSettings = mailSettings.Value;
            _logger = logger;
        }

        public async Task<bool> SendEmail(Email email)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("api-key", _emailSettings.ApiKey);
            BrevoModel brevoModel = new BrevoModel
            {
                Sender = new SenderModel
                {
                    Email = _emailSettings.SenderEmail,
                    Name = _emailSettings.SenderName
                },
                To = new List<SenderModel>
                {
                    new SenderModel{Email = email.To, Name = _emailSettings.SenderName }
                },
                Subject = email.Subject,
                HtmlContent = email.Body
            };
            string requestContent = JsonConvert.SerializeObject(brevoModel);
            var stringContent = new StringContent(requestContent, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(_emailSettings.Url, stringContent);
            var responseContent = await response.Content.ReadAsStringAsync();
            var dataResponse = JsonConvert.DeserializeObject<object>(responseContent);
            if (response.IsSuccessStatusCode)
            {
                return  true;
            }
            return false;
        }
    }
}
