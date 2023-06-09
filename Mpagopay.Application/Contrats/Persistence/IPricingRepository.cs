using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mpagopay.Domain.Entities.Tarification;

namespace Mpagopay.Application.Contrats.Persistence
{
    public interface IPricingRepository : IAsyncRepository<Pricing>
    {
        Task<bool> IsPricingNameUnique(string pricingName);
    }
}
