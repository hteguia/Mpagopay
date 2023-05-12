using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Mpagopay.Application.Features.Pricings.Queries.GetPricingDetail
{
    public class GetPricingDetailQuery : IRequest<PricingDetailVm>
    {
        public long Id { get; set; }
    }
}
