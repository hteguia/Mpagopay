using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Mpagopay.Identity.Models;

namespace Mpagopay.Identity
{
    public class MpagopayIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public MpagopayIdentityDbContext()
        {

        }

        public MpagopayIdentityDbContext(DbContextOptions<MpagopayIdentityDbContext>  options):base(options)
        {

        }
    }
}
