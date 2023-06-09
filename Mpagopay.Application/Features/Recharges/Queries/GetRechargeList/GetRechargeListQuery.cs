using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Mpagopay.Application.Features.Pricings.Queries.GetPricingList;

namespace Mpagopay.Application.Features.Recharges.Queries.GetRechargeList
{
    public class GetRechargeListQuery : IRequest<List<RechargeListVm>>
    {
    }
}
