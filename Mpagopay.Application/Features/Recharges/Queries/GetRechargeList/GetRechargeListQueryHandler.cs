using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Mpagopay.Application.Contrats.Persistence;
using Mpagopay.Application.Features.Pricings.Queries.GetPricingList;
using Mpagopay.Domain.Entities;

namespace Mpagopay.Application.Features.Recharges.Queries.GetRechargeList
{
    public class GetRechargeListQueryHandler : IRequestHandler<GetRechargeListQuery, List<RechargeListVm>>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Recharge> _rechargeRepository;
        public GetRechargeListQueryHandler(IMapper mapper, IAsyncRepository<Recharge> rechargeRepository)
        {
            _mapper = mapper;
            _rechargeRepository = rechargeRepository;
        }

        public async Task<List<RechargeListVm>> Handle(GetRechargeListQuery request, CancellationToken cancellationToken)
        {
            var allRecharge = (await _rechargeRepository.ListAllAsync()).ToList();
            return _mapper.Map<List<RechargeListVm>>(allRecharge);
        }
    }
}
