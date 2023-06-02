using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Mpagopay.Application.Contrats.Persistence;
using Mpagopay.Application.Features.Cards.Commands.UpdateCard;
using Mpagopay.Domain.Entities.VirtualCard;

namespace Mpagopay.Application.Features.Cards.Commands.DeleteCard
{
    public class DeleteCardCommandHandler : IRequestHandler<DeleteCardCommand>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Card> _cardRepository;

        public DeleteCardCommandHandler(IMapper mapper, IAsyncRepository<Card> cardRepository)
        {
            _mapper = mapper;
            _cardRepository = cardRepository;
        }

        public async Task<Unit> Handle(DeleteCardCommand request, CancellationToken cancellationToken)
        {
            var cardToDelete = await _cardRepository.GetByIdAsync(request.CardId);


            await _cardRepository.DeleteAsync(cardToDelete);

            return Unit.Value;
        }
    }
}
