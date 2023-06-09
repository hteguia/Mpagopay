using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mpagopay.Application.Contrats.Persistence;
using Mpagopay.Domain.Entities.BankAccounts;
using Mpagopay.Domain.Entities.Users;

namespace Mpagopay.Persistence.Repositories
{
    public class RechargeRepository : BaseRepository<CreditBankAccount>, IRechargeRepository
    {
        public RechargeRepository(MpagopayDbContext dbContext) : base(dbContext)
        {
        }
    }
}
