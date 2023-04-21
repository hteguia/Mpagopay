using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mpagopay.Application.Contrats.Persistence;
using Mpagopay.Domain.Entities;

namespace Mpagopay.Persistence.Repositories
{
    public class CardRechargeRepository : BaseRepository<CardRecharge>, ICardRechargeRepository
    {
        public CardRechargeRepository(MpagopayDbContext dbContext) : base(dbContext)
        {
        }
    }
}
