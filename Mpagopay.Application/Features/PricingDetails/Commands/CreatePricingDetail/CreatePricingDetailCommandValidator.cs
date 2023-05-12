using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Mpagopay.Application.Contrats.Persistence;

namespace Mpagopay.Application.Features.PricingDetails.Commands.CreatePricingDetail
{
    public class CreatePricingDetailCommandValidator : AbstractValidator<CreatePricingDetailCommand>
    {
        private IPricingDetailRepository _pricingDetailRepository;

        public CreatePricingDetailCommandValidator(IPricingDetailRepository pricingDetailRepository)
        {
            _pricingDetailRepository = pricingDetailRepository;
        }
    }
}
