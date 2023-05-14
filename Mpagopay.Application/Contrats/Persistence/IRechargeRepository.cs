using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mpagopay.Domain.Entities;
using Mpagopay.Domain.Entities.Users;

namespace Mpagopay.Application.Contrats.Persistence
{
    public interface IRechargeRepository : IAsyncRepository<Recharge>
    {
    }
}
