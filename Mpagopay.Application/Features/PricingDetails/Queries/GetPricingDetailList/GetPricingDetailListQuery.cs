using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Mpagopay.Application.Features.PricingDetails.Queries.GetPricingDetailList
{
    public class GetPricingDetailListQuery : IRequest<List<PricingDetailListVm>>
    {
    }
}
