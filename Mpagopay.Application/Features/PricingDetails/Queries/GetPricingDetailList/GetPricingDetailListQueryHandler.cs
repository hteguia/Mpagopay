using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Mpagopay.Application.Contrats.Persistence;
using Mpagopay.Application.Features.Pricings.Queries.GetPricingList;
using Mpagopay.Domain.Entities.Tarification;

namespace Mpagopay.Application.Features.PricingDetails.Queries.GetPricingDetailList
{
    public class GetPricingDetailListQueryHandler : IRequestHandler<GetPricingDetailListQuery, List<PricingDetailListVm>>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<PricingDetail> _pricingDetailRepository;
        public GetPricingDetailListQueryHandler(IMapper mapper, IAsyncRepository<PricingDetail> pricingDetailRepository)
        {
            _mapper = mapper;
            _pricingDetailRepository = pricingDetailRepository;
        }

        public async Task<List<PricingDetailListVm>> Handle(GetPricingDetailListQuery request, CancellationToken cancellationToken)
        {
            var allPricingDetail = (await _pricingDetailRepository.ListAllAsync()).ToList();
            return _mapper.Map<List<PricingDetailListVm>>(allPricingDetail);
        }
    }
}
