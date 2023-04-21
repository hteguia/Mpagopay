using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mpagopay.Application.Contrats.Persistence;
using Mpagopay.Domain.Entities;

namespace Mpagopay.Persistence.Repositories
{
    public class PricingDetailRepository : BaseRepository<PricingDetail>, IPricingDetailRepository
    {
        public PricingDetailRepository(MpagopayDbContext dbContext) : base(dbContext)
        {
        }
    }
}
