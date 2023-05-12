using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Mpagopay.Application.Features.Pricings.Commands.DeletePricing
{
    public class DeletePricingCommand : IRequest
    {
        public long PricingId { get; set; }
    }
}
