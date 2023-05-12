using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mpagopay.Application.Models.Mail.Brevo
{
    public class BrevoModel
    {
        public SenderModel Sender { get; set; }
        public List<SenderModel> To { get; set; }
        public string Subject { get; set; }
        public string HtmlContent { get; set; }
    }
}
