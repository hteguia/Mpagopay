using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mpagopay.Application.Models.Mail;
using Mpagopay.Application.Tools;

namespace Mpagopay.Application.Contrats.Infrastructure
{
    public interface IEmailService
    {
        Task<bool> SendEmail(Email email, EmailType emailType);
    }
}
