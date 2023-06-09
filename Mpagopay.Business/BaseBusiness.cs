using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mpagopay.Application.Contrats.Business;
using Mpagopay.Identity;
using Mpagopay.Persistence;

namespace Mpagopay.Business
{
    public class BaseBusiness : IBusiness
    {
        protected readonly MpagopayIdentityDbContext _IdentityContext;
        protected readonly MpagopayDbContext _dbContext;

        public BaseBusiness(MpagopayIdentityDbContext IdentityContext, MpagopayDbContext dbContext)
        {
            _IdentityContext = IdentityContext;
            _dbContext = dbContext;
        }
    }
}
