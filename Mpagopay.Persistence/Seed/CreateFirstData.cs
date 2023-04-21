using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mpagopay.Domain.Entities;

namespace Mpagopay.Persistence.Seed
{
    public static class CreateFirstData
    {
        public static async Task SeedAsync(MpagopayDbContext mpagopayDbContext)
        {
            var card = new Card
            {
                Name = "KAMDJO TEGUIA HERVE",
                Number = "2645-7576-7755-2759",
                Cvv = "754",
                Expires = "25/2025",
            };
            var _card = await mpagopayDbContext.Cards.Where(p=>p.Number == card.Number).FirstOrDefaultAsync();
            if (_card == null)
            {

                await mpagopayDbContext.AddAsync(card);
                await mpagopayDbContext.SaveChangesAsync();
            }
        }
    }
}
