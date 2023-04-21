using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Mpagopay.Application.Features.Cards.Queries.GetCardsList;

namespace Mpagopay.Application.Features.Pricings.Queries.GetPricingList
{
    public class GetPricingListQuery : IRequest<List<PricingListVm>>
    {
    }
}
