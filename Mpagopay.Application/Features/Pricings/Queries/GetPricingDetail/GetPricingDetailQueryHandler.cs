using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Mpagopay.Application.Contrats.Persistence;
using Mpagopay.Application.Features.Cards.Queries.GetCardDetail;
using Mpagopay.Domain.Entities.Tarification;

namespace Mpagopay.Application.Features.Pricings.Queries.GetPricingDetail
{
    public class GetPricingDetailQueryHandler : IRequestHandler<GetPricingDetailQuery, PricingDetailVm>
    {
        private readonly IAsyncRepository<Pricing> _pricingRepository;
        private IMapper _mapper;

        public GetPricingDetailQueryHandler(IAsyncRepository<Pricing> pricingRepository, IMapper mapper)
        {
            _pricingRepository = pricingRepository;
            _mapper = mapper;
        }

        public async Task<PricingDetailVm> Handle(GetPricingDetailQuery request, CancellationToken cancellationToken)
        {
            var @card = await _pricingRepository.GetByIdAsync(request.Id);
            var cardDetail = _mapper.Map<PricingDetailVm>(@card);
            return cardDetail;

        }     


    }
}
