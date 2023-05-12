using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Mpagopay.Application.Contrats.Infrastructure;
using Mpagopay.Application.Contrats.Persistence;
using Mpagopay.Application.Features.Cards.Commands.CreateCard;
using Mpagopay.Application.Models.Mail;
using Mpagopay.Domain.Entities;

namespace Mpagopay.Application.Features.Pricings.Commands.CreatePricing
{
    public class CreatePricingCommandHandler : IRequestHandler<CreatePricingCommand, long>
    {
        private readonly IMapper _mapper;
        private readonly IPricingRepository _pricingRepository;
        public ILogger<CreatePricingCommandHandler> _logger;

        public CreatePricingCommandHandler(IMapper mapper, IPricingRepository pricingRepository, ILogger<CreatePricingCommandHandler> logger)
        {
            _mapper = mapper;
            _pricingRepository = pricingRepository;
            _logger = logger;
        }

        public async Task<long> Handle(CreatePricingCommand request, CancellationToken cancellationToken)
        {
            var @pricing = _mapper.Map<Pricing>(request);

            var validator = new CreatePricingCommandValidator(_pricingRepository);
            var validationResut = await validator.ValidateAsync(request);
            if (validationResut.Errors.Count > 0)
            {
                throw new Exceptions.ValidationException(validationResut);
            }

            pricing = await _pricingRepository.AddAsync(pricing);
          
            return pricing.PricingId;
        }
    }
}
