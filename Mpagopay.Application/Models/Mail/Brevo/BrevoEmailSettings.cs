using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mpagopay.Application.Models.Mail.Brevo
{
    public class BrevoEmailSettings
    {
        public string Url { get; set; }
        public string ApiKey { get; set; }
        public string SenderEmail { get; set; }
        public string SenderName { get; set; }
    }
}
