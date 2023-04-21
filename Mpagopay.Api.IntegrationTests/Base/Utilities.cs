using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mpagopay.Persistence;

namespace Mpagopay.Api.IntegrationTests.Base
{
    public class Utilities
    {
        public static void InitializeDbForTests(MpagopayDbContext context)
        {
            context.Cards.Add(new Domain.Entities.Card { CardId = 1, Name = "HERVE" });
        }
    }
}
