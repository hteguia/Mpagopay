using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mpagopay.Application.Contrats.Infrastructure;
using Mpagopay.Application.Contrats.Persistence;
using Mpagopay.Application.Models.Mail;
using Mpagopay.Application.Models.Mail.Brevo;
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
            services.Configure<BrevoEmailSettings>(configuration.GetSection("BrevoEmailSettings"));

            services.AddTransient<ICsvExporter, CsvExporter>();
            services.AddTransient<ISmsService, SmsService>();
            services.AddTransient<IVirtualCardProvider, VirtualCardProvider>();
            services.AddTransient<IEmailService, BrevoEmailService>();
            //services.AddScoped<BrevoEmailService>();

            //services.AddTransient<EmailServiceResolver>(serviceProvider => serviceTypeName =>
            //{
            //    switch(serviceTypeName)
            //    {
            //        case EmailServiceType.DEFAULT:
            //            return serviceProvider.GetService<EmailService>();
            //        case EmailServiceType.BREVO:
            //            return serviceProvider.GetService<BrevoEmailService>();
            //        default:
            //            return null;
            //    }
            //});

            return services;
        }
    }
}
