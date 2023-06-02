using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Mpagopay.Application.Contrats.Persistence;
using Mpagopay.Application.Features.Cards.Queries.GetCardsList;
using Mpagopay.Domain.Entities.Tarification;

namespace Mpagopay.Application.Features.Pricings.Queries.GetPricingList
{
    public class GetPricingListQueryHandler : IRequestHandler<GetPricingListQuery, List<PricingListVm>>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Pricing> _pricingRepository;
        public GetPricingListQueryHandler(IMapper mapper, IAsyncRepository<Pricing> pricingRepository)
        {
            _mapper = mapper;
            _pricingRepository = pricingRepository;
        }



        public async Task<List<PricingListVm>> Handle(GetPricingListQuery request, CancellationToken cancellationToken)
        {
            var allCard = (await _pricingRepository.ListAllAsync()).OrderBy(p => p.CreateDate).ToList();
            return _mapper.Map<List<PricingListVm>>(allCard);
        }
    }
}
