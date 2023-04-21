using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Mpagopay.Application.Contrats.Persistence;
using Mpagopay.Domain.Entities;

namespace Mpagopay.Application.Features.Cards.Queries.GetCardsList
{
    public class GetCardListQueryHandler : IRequestHandler<GetCardListQuery, List<CardListVm>>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Card> _cardRepository;
        public GetCardListQueryHandler(IMapper mapper, IAsyncRepository<Card> cardRepository)
        {
            _mapper = mapper;
            _cardRepository = cardRepository;
        }



        public async Task<List<CardListVm>> Handle(GetCardListQuery request, CancellationToken cancellationToken)
        {
            var allCard = (await _cardRepository.ListAllAsync()).OrderBy(p => p.CreateDate).ToList();
            return _mapper.Map<List<CardListVm>>(allCard);
        }
    }
}
