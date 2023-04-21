using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Mpagopay.Application.Contrats.Infrastructure;
using Mpagopay.Application.Models.Mail;
using Mpagopay.Application.Models.Sms;
using Mpagopay.Infrastructure.Mail;
using Newtonsoft.Json;

namespace Mpagopay.Infrastructure.Sms
{
    public class SmsService : ISmsService
    {
        private SmsSettings _smsSettings { get; }
        private readonly ILogger<SmsService> _logger;
        public SmsService(IOptions<SmsSettings> smsSettings, ILogger<SmsService> logger)
        {
            _smsSettings = smsSettings.Value;
            _logger = logger;
        }

        public async Task<bool> SendSms(SmsModel smsModel)
        {
            RequestData sendData = new()
            {
                recipient = (smsModel.Recipient.StartsWith("+") ? "" : "+") + smsModel.Recipient.Replace(" ", ""),
                text = smsModel.Message,
                sender = _smsSettings.SenderName
            };

            _logger.LogInformation("Sms sent");

            HttpClient client = new();
            string requestContent = JsonConvert.SerializeObject(sendData);
            var stringContent = new StringContent(requestContent, Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{_smsSettings.ApiUrl}?api_key={_smsSettings.ApiKey}", stringContent);
            var responseContent = await response.Content.ReadAsStringAsync();
            var dataResponse = JsonConvert.DeserializeObject<ResponseData>(responseContent);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                _logger.LogInformation("Sms sending failed");

                return false;
            }
        }
    }

    public class RequestData
    {
        public string sender { get; set; }
        public string recipient { get; set; }
        public string text { get; set; }
    }

    public class ResponseData
    {
        public string id { get; set; }
        public decimal cost { get; set; }
        public int parts { get; set; }
    }

    public class ResponseStatus
    {
        public string id { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public decimal cost { get; set; }
        public int parts { get; set; }
        public string status { get; set; }
    }
}
