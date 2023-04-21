using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Mpagopay.Application.Contrats.Persistence;
using Mpagopay.Application.Exceptions;
using Mpagopay.Application.Features.Cards.Commands.CreateCard;
using Mpagopay.Domain.Entities;

namespace Mpagopay.Application.Features.Cards.Commands.UpdateCard
{
    internal class UpdateCardCommandHandler : IRequestHandler<UpdateCardCommand>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Card> _cardRepository;

        public UpdateCardCommandHandler(IMapper mapper, IAsyncRepository<Card> cardRepository)
        {
            _mapper = mapper;
            _cardRepository = cardRepository;
        }

        public async Task<Unit> Handle(UpdateCardCommand request, CancellationToken cancellationToken)
        {
            var cardToUpdate = await _cardRepository.GetByIdAsync(request.CardId);
            if(cardToUpdate == null)
            {
                throw new NotFoundException(nameof(Card), request.CardId);
            }

            _mapper.Map(request, cardToUpdate, typeof(UpdateCardCommand), typeof(Card));

            await _cardRepository.UpdateAsync(cardToUpdate);

            return Unit.Value;
        }
    }
}
