using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Mpagopay.Application.Contrats.Persistence;

namespace Mpagopay.Application.Features.Recharges.Commands.CreateRecharge
{
    public class CreateRechargeCommandValidator : AbstractValidator<CreateRechargeCommand>
    {
        private readonly IRechargeRepository _rechargeRepository;
        public CreateRechargeCommandValidator(IRechargeRepository rechargeRepository)
        {
            _rechargeRepository = rechargeRepository;
        }
    }
}
