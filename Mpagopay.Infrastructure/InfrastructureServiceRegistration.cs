using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mpagopay.Application.Contrats.Infrastructure;
using Mpagopay.Application.Models.Mail;
using Mpagopay.Application.Models.Sms;
using Mpagopay.Application.Tools;
using Mpagopay.Infrastructure.FileExport;
using Mpagopay.Infrastructure.Mail;
using Mpagopay.Infrastructure.Sms;
using Mpagopay.Infrastructure.VirtualCard;

namespace Mpagopay.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            services.Configure<SmsSettings>(configuration.GetSection("SmsSettings"));

            services.AddTransient<ICsvExporter, CsvExporter>();
            services.AddTransient<ISmsService, SmsService>();
            services.AddTransient<IVirtualCardProvider, VirtualCardProvider>();
            services.AddTransient<EmailService>();
            services.AddTransient<EmailConfirmRegistrationService>();

            services.AddTransient<EmailServiceResolver>(serviceProvider => token =>
            {
                return token switch
                {
                    EmailType.DEFAULT => serviceProvider.GetService<EmailService>(),
                    EmailType.CONFIRM_REGISTRATION => serviceProvider.GetService<EmailConfirmRegistrationService>(),
                    _ => throw new InvalidOperationException()
                };
            });

            return services;
        }
    }
}
