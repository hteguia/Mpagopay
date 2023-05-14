using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mpagopay.Application.Contrats.Infrastructure;

namespace Mpagopay.Application.Tools
{
    public delegate IEmailService EmailServiceResolver(EmailServiceType serviceType);
    public enum EmailServiceType
    {
        DEFAULT,
        BREVO
    }
}
