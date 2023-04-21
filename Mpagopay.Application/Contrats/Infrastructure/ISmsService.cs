using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mpagopay.Application.Models.Mail;
using Mpagopay.Application.Models.Sms;

namespace Mpagopay.Application.Contrats.Infrastructure
{
    public interface ISmsService
    {
        Task<bool> SendSms(SmsModel smsModel);
    }
}
