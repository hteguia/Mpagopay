using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Mpagopay.Application.Contrats.Persistence;
using Mpagopay.Application.Models.Authentication;
using Mpagopay.Identity.Models;

namespace Mpagopay.Identity.Services
{
    public class RegistrationRequestValidator : AbstractValidator<RegistrationRequest>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public RegistrationRequestValidator(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;

            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .EmailAddress().WithMessage("A valid email is required")
                .MustAsync(EmailUnique).WithMessage("An user with with the same email already exists");

            RuleFor(p => p.UserName)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MustAsync(UserNameUnique).WithMessage("An user with the same username already exists");
        }

        private async Task<bool> EmailUnique(string email, CancellationToken cancellationToken)
        {
            return (await _userManager.FindByEmailAsync(email) == null);
        }

        private async Task<bool> UserNameUnique(string userName, CancellationToken cancellationToken)
        {
            return (await _userManager.FindByNameAsync(userName) == null);
        }
    }
}
