using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mpagopay.Application.Contrats.Persistence;
using Mpagopay.Domain.Entities.Users;

namespace Mpagopay.Persistence.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(MpagopayDbContext dbContext) : base(dbContext)
        {
        }
    }
}
