using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Mpagopay.Application.Features.Pricings.Commands.CreatePricing
{
    public class CreatePricingCommand : IRequest<long>
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
