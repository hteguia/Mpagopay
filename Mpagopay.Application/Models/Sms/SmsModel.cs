using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mpagopay.Application.Models.Sms
{
    public class SmsModel
    {
        public string Recipient { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}
