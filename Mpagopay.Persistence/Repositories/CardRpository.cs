using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mpagopay.Application.Contrats.Persistence;
using Mpagopay.Domain.Entities;

namespace Mpagopay.Persistence.Repositories
{
    public class CardRpository : BaseRepository<Card>, ICardRepository
    {
        public CardRpository(MpagopayDbContext dbContext) : base(dbContext)
        {
        }

        public Task<bool> IsCardNumberUnique(string name)
        {
            var mathes = _dbContext.Cards.Any(p=>p.Name.Equals(name));
            return Task.FromResult(mathes);
        }
    }
}
