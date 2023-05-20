using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Mpagopay.Application.Contrats.Infrastructure;
using Mpagopay.Application.Contrats.Persistence;
using Mpagopay.Application.Exceptions;
using Mpagopay.Application.Models.Mail;
using Mpagopay.Application.Tools;
using Mpagopay.Domain.Entities;

namespace Mpagopay.Application.Features.Cards.Commands.CreateCard
{
    public class CreateCardCommandHandler : IRequestHandler<CreateCardCommand, long>
    {
        private readonly IMapper _mapper;
        private readonly ICardRepository _cardRepository;
        
        public ILogger<CreateCardCommandHandler> _logger;

        public CreateCardCommandHandler(IMapper mapper,ICardRepository cardRepository,  ILogger<CreateCardCommandHandler> logger)
        {
            _mapper = mapper;
            _cardRepository = cardRepository;
            _logger = logger;
        }

        public async Task<long> Handle(CreateCardCommand request, CancellationToken cancellationToken)
        {
           var @card = _mapper.Map<Card>(request);
            var validator = new CreateCardCommandValidator(_cardRepository);
            var validationResut = await validator.ValidateAsync(request);
            if(validationResut.Errors.Count > 0)
            {
                throw new Exceptions.ValidationException(validationResut);
            }


            card = await _cardRepository.AddAsync(card);

            

            return card.CardId;
        }

    }
}
