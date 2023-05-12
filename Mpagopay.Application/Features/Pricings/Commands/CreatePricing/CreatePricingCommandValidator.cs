using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Mpagopay.Application.Contrats.Persistence;
using Mpagopay.Application.Features.Cards.Commands.CreateCard;

namespace Mpagopay.Application.Features.Pricings.Commands.CreatePricing
{
    public class CreatePricingCommandValidator : AbstractValidator<CreatePricingCommand>
    {
        //declare a repository to access the database and check for duplicate records a pricing
        private readonly IPricingRepository _pricingRepository;

        //create a validator for the CreateCardCommand
        public CreatePricingCommandValidator(IPricingRepository pricingRepository)
        {
            _pricingRepository = pricingRepository;
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            //add control to check if the pricing name is unique
            RuleFor(p => p.Name)
                .MustAsync(PricingNameUnique).WithMessage("A pricing with the same name already exists.");
        }

        //create a method to check for duplicate pricing
        private async Task<bool> PricingNameUnique(string pricingName, CancellationToken cancellationToken)
        {
            return !(await _pricingRepository.IsPricingNameUnique(pricingName));
        }
    }
}
