using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Mpagopay.Application.Contrats.Persistence;

namespace Mpagopay.Application.Features.Cards.Commands.CreateCard
{
    public class CreateCardCommandValidator : AbstractValidator<CreateCardCommand>
    {
        private readonly ICardRepository _cardRepository;
        public CreateCardCommandValidator(ICardRepository cardRepository) 
        { 
            _cardRepository = cardRepository;

            RuleFor(p=>p.Name).NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull()
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");


            RuleFor(p => p.Number).NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull()
                .Length(19).WithMessage("{PropertyName} must contain 19 characters.")
                .MustAsync(CardNumberUnique).WithMessage("An card with the same number already exists");

            RuleFor(p => p.Cvv).NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull()
                .MaximumLength(3).WithMessage("{PropertyName} must contain 3 characters.");

            RuleFor(p => p.Expires).NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull()
                .MaximumLength(7).WithMessage("{PropertyName} must contain 7 characters.");

        }

        private async Task<bool> CardNumberUnique(string cardNumber, CancellationToken cancellationToken)
        {
            return !(await _cardRepository.IsCardNumberUnique(cardNumber));
        }

    }
}
