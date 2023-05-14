using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mpagopay.Application.Contrats.Persistence;
using Mpagopay.Domain.Entities;
using Mpagopay.Domain.Entities.Users;

namespace Mpagopay.Persistence.Repositories
{
    public class RechargeRepository : BaseRepository<Recharge>, IRechargeRepository
    {
        public RechargeRepository(MpagopayDbContext dbContext) : base(dbContext)
        {
        }
    }
}
