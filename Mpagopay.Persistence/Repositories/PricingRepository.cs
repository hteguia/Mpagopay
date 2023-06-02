using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mpagopay.Application.Contrats.Persistence;
using Mpagopay.Domain.Entities.Tarification;

namespace Mpagopay.Persistence.Repositories
{
    public class PricingRepository : BaseRepository<Pricing>, IPricingRepository
    {
        public PricingRepository(MpagopayDbContext dbContext) : base(dbContext)
        {
        }

        public Task<bool> IsPricingNameUnique(string pricingName)
        {
            throw new NotImplementedException();
        }
    }
}
