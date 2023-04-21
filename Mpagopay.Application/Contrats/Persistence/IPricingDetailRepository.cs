using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mpagopay.Domain.Entities;

namespace Mpagopay.Application.Contrats.Persistence
{
    public interface IPricingDetailRepository : IAsyncRepository<PricingDetail>
    {
    }
}
