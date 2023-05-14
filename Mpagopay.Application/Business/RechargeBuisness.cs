using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Mpagopay.Application.Contrats.Infrastructure;
using Mpagopay.Application.Features.Cards.Commands.CreateCard;
using Mpagopay.Application.Features.Recharges.Commands.CreateRecharge;

namespace Mpagopay.Application.Buisness
{
    public class RechargeBuisness
    {
        private readonly IMediator _mediator;
        private readonly ICollectionService _collectionService;
        public RechargeBuisness(IMediator mediator, ICollectionService collectionService)
        {
            _mediator = mediator;
            _collectionService = collectionService;
        }

        public async Task AddRecharge(CreateRechargeCommand createRechargeCommand)
        {
            var recharge = await _mediator.Send(createRechargeCommand);
            await _collectionService.Collection(recharge.AccountNumber, recharge.Amount, recharge.PServiceProvider);
        }
    }
}
