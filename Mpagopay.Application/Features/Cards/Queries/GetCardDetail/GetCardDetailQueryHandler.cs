using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Mpagopay.Application.Contrats.Persistence;
using Mpagopay.Domain.Entities;

namespace Mpagopay.Application.Features.Cards.Queries.GetCardDetail
{
    public class GetCardDetailQueryHandler : IRequestHandler<GetCardDetailQuery, CardDetailVm>
    {
        private readonly IAsyncRepository<Card> _cardRepository;
        private IMapper _mapper;

        public GetCardDetailQueryHandler(IAsyncRepository<Card> cardRepository, IMapper mapper)
        {
            _cardRepository = cardRepository;
            _mapper = mapper;
        }

        public async Task<CardDetailVm> Handle(GetCardDetailQuery request, CancellationToken cancellationToken)
        {
            var @card = await _cardRepository.GetByIdAsync(request.Id);
            var cardDetail = _mapper.Map<CardDetailVm>(@card);

            return cardDetail;
        }
    }
}
