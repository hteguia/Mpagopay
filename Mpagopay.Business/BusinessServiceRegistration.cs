using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mpagopay.Application.Contrats.Infrastructure;
using Mpagopay.Application.Models.Mail;
using Mpagopay.Application.Models.Sms;
using Mpagopay.Application.Contrats.Business;
using Mpagopay.Business.Users;

namespace Mpagopay.Business
{
    public static class BusinessServiceRegistration
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IUserBusiness, UserBusiness>();

            return services;
        }
    }
}
