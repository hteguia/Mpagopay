using Mpagopay.Application.Models.Sms;

namespace Mpagopay.Application.Contrats.Infrastructure
{
    public interface ISmsService
    {
        Task<bool> SendSms(SmsModel smsModel);
    }
}
