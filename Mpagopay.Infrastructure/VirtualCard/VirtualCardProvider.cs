using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mpagopay.Application.Contrats.Infrastructure;
using Mpagopay.Application.Models.VirtualCard;

namespace Mpagopay.Infrastructure.VirtualCard
{
    public class VirtualCardProvider : IVirtualCardProvider
    {
        public Task<CardModel> CreateCard(CardModel cardModel)
        {
            Random _random = new();
            int value = _random.Next(1000);

            cardModel.Number = GenerateCardNumber();
            cardModel.Expires = "12/2025";
            cardModel.Cvv = value.ToString("000");
            return Task.FromResult(cardModel);
        }

        private string GenerateCardNumber()
        {
            Random _random = new Random();

            int[] checkArray = new int[15];

            var cardNum = new int[16];

            for (int d = 14; d >= 0; d--)
            {
                cardNum[d] = _random.Next(0, 9);
                checkArray[d] = (cardNum[d] * (((d + 1) % 2) + 1)) % 9;
            }

            cardNum[15] = (checkArray.Sum() * 9) % 10;

            var sb = new StringBuilder();

            int count = 0;
            for (int d = 0; d < 16; d++)
            {
                sb.Append(cardNum[d].ToString());
                if ((++count % 4) == 0 && d < 15)
                {
                    sb.Append('-');
                }
            }
            return sb.ToString();
        }
    }
}
