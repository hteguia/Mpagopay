using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mpagopay.Application.Models.Sms;
using Mpagopay.Application.Models.VirtualCard;

namespace Mpagopay.Application.Contrats.Infrastructure
{
    public interface IVirtualCardProvider
    {
        Task<CardModel> CreateCard(CardModel cardModel);
    }
}
