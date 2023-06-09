using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mpagopay.Application.Contrats.Persistence;
using Mpagopay.Persistence.Repositories;

namespace Mpagopay.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MpagopayDbContext>(options => 
                options.UseSqlServer(configuration.GetConnectionString("MpagopayConnectionString")));

            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));

            services.AddScoped<ICardRepository, CardRpository>();
            services.AddScoped<IPricingRepository, PricingRepository>();
            services.AddScoped<IPricingDetailRepository, PricingDetailRepository>();
            services.AddScoped<ICardRechargeRepository, CardRechargeRepository>();
            services.AddScoped<IRechargeRepository, RechargeRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
