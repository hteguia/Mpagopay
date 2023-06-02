using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Mpagopay.Application.Contrats.Persistence;
using Mpagopay.Application.Features.Cards.Commands.DeleteCard;
using Mpagopay.Domain.Entities.Tarification;

namespace Mpagopay.Application.Features.Pricings.Commands.DeletePricing
{
    public class DeletePricingCommandHandler : IRequestHandler<DeletePricingCommand>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Pricing> _pricingRepository;

        public DeletePricingCommandHandler(IMapper mapper, IAsyncRepository<Pricing> pricingRepository)
        {
            _mapper = mapper;
            _pricingRepository = pricingRepository;
        }

        public async Task<Unit> Handle(DeletePricingCommand request, CancellationToken cancellationToken)
        {
            var cardToDelete = await _pricingRepository.GetByIdAsync(request.PricingId);


            await _pricingRepository.DeleteAsync(cardToDelete);

            return Unit.Value;
        }
    }
}
