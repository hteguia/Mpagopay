using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Mpagopay.Application.Contrats.Infrastructure;
using Mpagopay.Application.Contrats.Persistence;
using Mpagopay.Application.Features.Cards.Commands.CreateCard;
using Mpagopay.Domain.Entities;

namespace Mpagopay.Application.Features.Recharges.Commands.CreateRecharge
{
    public class CreateRechargeCommandHandler : IRequestHandler<CreateRechargeCommand, Recharge>
    {
        private readonly IMapper _mapper;
        private readonly IRechargeRepository _rechargeRepository;
        private readonly IEmailService _emailService;
        public ILogger<CreateRechargeCommandHandler> _logger;

        public CreateRechargeCommandHandler(IMapper mapper, IRechargeRepository rechargeRepository, IEmailService emailService, ILogger<CreateRechargeCommandHandler> logger)
        {
            _mapper = mapper;
            _rechargeRepository = rechargeRepository;
            _emailService = emailService;
            _logger = logger;
        }

        public async Task<Recharge> Handle(CreateRechargeCommand request, CancellationToken cancellationToken)
        {
            var @recharge = _mapper.Map<Recharge>(request);
            recharge.Status = Domain.Tools.Status.PENDING;
            recharge.ReferenceId = Guid.NewGuid().ToString();
            var validator = new CreateRechargeCommandValidator(_rechargeRepository);
            var validationResut = await validator.ValidateAsync(request);
            if (validationResut.Errors.Count > 0)
            {
                throw new Exceptions.ValidationException(validationResut);
            }


            @recharge = await _rechargeRepository.AddAsync(@recharge);
            return @recharge;
        }
    }
}
