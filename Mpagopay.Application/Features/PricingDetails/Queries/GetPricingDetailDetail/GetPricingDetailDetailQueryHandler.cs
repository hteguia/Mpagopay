using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Mpagopay.Application.Contrats.Persistence;
using Mpagopay.Application.Features.Pricings.Queries.GetPricingDetail;
using Mpagopay.Domain.Entities;

namespace Mpagopay.Application.Features.PricingDetails.Queries.GetPricingDetailDetail
{
    internal class GetPricingDetailDetailQueryHandler : IRequestHandler<GetPricingDetailDetailQuery, PricingDetailDetailVm>
    {
        private readonly IAsyncRepository<PricingDetail> _pricingDetailRepository;
        private IMapper _mapper;

        public GetPricingDetailDetailQueryHandler(IAsyncRepository<PricingDetail> pricingDetailRepository, IMapper mapper)
        {
            _pricingDetailRepository = pricingDetailRepository;
            _mapper = mapper;
        }

        public async Task<PricingDetailDetailVm> Handle(GetPricingDetailDetailQuery request, CancellationToken cancellationToken)
        {
            var @pricingDetail = await _pricingDetailRepository.GetByIdAsync(request.Id);
            var pricingDetailDetail = _mapper.Map<PricingDetailDetailVm>(@pricingDetail);
            return pricingDetailDetail;
        }
    }
}
