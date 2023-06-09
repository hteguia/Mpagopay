using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mpagopay.Domain.Entities.VirtualCard;

namespace Mpagopay.Application.Contrats.Persistence
{
    public interface ICardRepository : IAsyncRepository<Card>
    {
        Task<bool> IsCardNumberUnique(string name);
    }
}
