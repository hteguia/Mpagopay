using Mpagopay.Application.Contrats.Infrastructure;

namespace Mpagopay.Application.Tools
{
    public delegate IEmailService EmailServiceResolver(EmailType emailType);

    public enum EmailType
    {
        DEFAULT = 0,
        CONFIRM_REGISTRATION = 1,
    }
}
