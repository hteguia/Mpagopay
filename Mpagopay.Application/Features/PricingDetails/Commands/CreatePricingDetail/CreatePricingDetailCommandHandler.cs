using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Mpagopay.Application.Contrats.Persistence;
using Mpagopay.Domain.Entities.Tarification;

namespace Mpagopay.Application.Features.PricingDetails.Commands.CreatePricingDetail
{
    public  class CreatePricingDetailCommandHandler : IRequestHandler<CreatePricingDetailCommand, long>
    {
        private readonly IMapper _mapper;
        private readonly IPricingDetailRepository _pricingDetailRepository;
        public ILogger<CreatePricingDetailCommandHandler> _logger;

        public CreatePricingDetailCommandHandler(IMapper mapper, IPricingDetailRepository pricingDetailRepository, ILogger<CreatePricingDetailCommandHandler> logger)
        {
            _mapper = mapper;
            _pricingDetailRepository = pricingDetailRepository;
            _logger = logger;
        }

        public async Task<long> Handle(CreatePricingDetailCommand request, CancellationToken cancellationToken)
        {
            var @pricing = _mapper.Map<PricingDetail>(request);

            var validator = new CreatePricingDetailCommandValidator(_pricingDetailRepository);
            var validationResut = await validator.ValidateAsync(request);
            if (validationResut.Errors.Count > 0)
            {
                throw new Exceptions.ValidationException(validationResut);
            }

            pricing = await _pricingDetailRepository.AddAsync(pricing);

            return 10;
        }
    }
}
