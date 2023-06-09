using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Mpagopay.Domain.Entities.VirtualCard;
using Mpagopay.Identity;
using Mpagopay.Identity.Models;
using Mpagopay.Persistence;

namespace Mpagopay.Api.IntegrationTests.Base
{
    public class Utilities
    {
        public static void InitializeDbForTests(MpagopayDbContext context)
        {
            context.Cards.Add(new Card
            {
                Name = "John Smith",
                Number = "5334 6343 5214 6222",
                Cvv = "234",
                Expires = "12/2025"
            });
            context.SaveChanges();
        }

        public static async Task InitializeDbIdentityForTests(UserManager<ApplicationUser> userManager)
        {
            await userManager.CreateAsync(new ApplicationUser { UserName = "hteguia", Email = "teguia@teguia.me", EmailConfirmed = true }, "");
           
        }
    }
}
