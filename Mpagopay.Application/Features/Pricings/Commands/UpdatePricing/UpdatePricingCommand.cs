using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Mpagopay.Application.Features.Pricings.Commands.UpdatePricing
{
    public class UpdatePricingCommand : IRequest
    {
        public long PricingId { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
