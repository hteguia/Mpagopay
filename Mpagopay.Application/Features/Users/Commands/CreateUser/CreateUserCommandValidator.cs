using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Mpagopay.Application.Contrats.Persistence;
using Mpagopay.Application.Features.Pricings.Commands.CreatePricing;
using PhoneNumbers;

namespace Mpagopay.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly PhoneNumberUtil phoneUtil = PhoneNumberUtil.GetInstance();
        public CreateUserCommandValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;

            RuleFor(p => p.FirstName).NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull()
                .MaximumLength(200).WithMessage("{PropertyName} must not exceed 200 characters.");

            RuleFor(p => p.LastName).NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull()
                .MaximumLength(200).WithMessage("{PropertyName} must not exceed 200 characters.");

            RuleFor(p => p.Email).NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull()
                .EmailAddress().WithMessage("{PropertyName} is not a valid email address");

            RuleFor(p => p).NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull().Custom((x, context) =>
                {
                    var number = phoneUtil.Parse(x.PhoneNumber, x.CodeIso2);

                    if (!phoneUtil.IsValidNumberForRegion(number, x.CodeIso2))
                    {
                        context.AddFailure($"{x} is an invalid phone number");
                    }
                });
        }
    }
}
