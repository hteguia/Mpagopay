using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Mpagopay.Application.Contrats.Persistence;
using Mpagopay.Application.Exceptions;
using Mpagopay.Application.Features.Cards.Commands.UpdateCard;
using Mpagopay.Domain.Entities.Tarification;

namespace Mpagopay.Application.Features.Pricings.Commands.UpdatePricing
{
    public class UpdatePricingCommandHandler : IRequestHandler<UpdatePricingCommand>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Pricing> _pricingRepository;

        public UpdatePricingCommandHandler(IMapper mapper, IAsyncRepository<Pricing> pricingRepository)
        {
            _mapper = mapper;
            _pricingRepository = pricingRepository;
        }

        public async Task<Unit> Handle(UpdatePricingCommand request, CancellationToken cancellationToken)
        {
            var pricingToUpdate = await _pricingRepository.GetByIdAsync(request.PricingId);
            if (pricingToUpdate == null)
            {
                throw new NotFoundException(nameof(Pricing), request.PricingId);
            }

            _mapper.Map(request, pricingToUpdate, typeof(UpdatePricingCommand), typeof(Pricing));

            await _pricingRepository.UpdateAsync(pricingToUpdate);

            return Unit.Value;
        }
    }
}
