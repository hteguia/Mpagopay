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
using Mpagopay.Domain.Entities;

namespace Mpagopay.Application.Features.Cards.Commands.CreateCard
{
    public class CreateCardCommandHandler : IRequestHandler<CreateCardCommand, long>
    {
        private readonly IMapper _mapper;
        private readonly ICardRepository _cardRepository;
        private readonly IEmailService _emailService;
        public ILogger<CreateCardCommandHandler> _logger;

        public CreateCardCommandHandler(IMapper mapper,ICardRepository cardRepository, IEmailService emailService, ILogger<CreateCardCommandHandler> logger)
        {
            _mapper = mapper;
            _cardRepository = cardRepository;
            _emailService = emailService;
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

            //Sending email notification to admin address
            var email = new Email
            {
                To = "teguia@teguia.me",
                Body = "message",
                Subject = "message"
            };

            try
            {
                _emailService.SendEmail(email);
            }
            catch(Exception ex)
            {
                //this shouldn't stop the API from doing else so this can be logged
                _logger.LogError($"Mailing about card {card.CardId} failed due to an error with the email service: {ex.Message}");

            }

            return card.CardId;
        }

    }
}
