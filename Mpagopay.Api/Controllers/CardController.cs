using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Mpagopay.Api.Tools;
using Mpagopay.Application.Contrats.Infrastructure;
using Mpagopay.Application.Features.Cards.Commands.CreateCard;
using Mpagopay.Application.Features.Cards.Commands.DeleteCard;
using Mpagopay.Application.Features.Cards.Commands.UpdateCard;
using Mpagopay.Application.Features.Cards.Queries.GetCardDetail;
using Mpagopay.Application.Features.Cards.Queries.GetCardsExport;
using Mpagopay.Application.Features.Cards.Queries.GetCardsList;
using Mpagopay.Application.Models.Mail;
using Mpagopay.Application.Models.VirtualCard;
using Mpagopay.Application.Tools;
using Mpagopay.Domain.Entities;

namespace Mpagopay.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IVirtualCardProvider _virtualCardProvider;
        private readonly EmailServiceResolver _emailService;

        public CardController(IMediator mediator, EmailServiceResolver emailServiceResolver, IVirtualCardProvider virtualCardProvider)
        {
            _mediator = mediator;
            _virtualCardProvider = virtualCardProvider;
            _emailService = emailServiceResolver;
        }

        //[Authorize]
        [HttpGet("all",Name = "GetAllCards")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<CardListVm>>> GetAllCards()
        {
            var dtos = await _mediator.Send(new GetCardListQuery());
            return Ok(dtos);
        }

        [HttpGet("{id}", Name = "GetCardById")]
        public async Task<ActionResult<List<CardDetailVm>>> GetCardById(long id)
        {
            var getCardDetailQuery = new GetCardDetailQuery() { Id = id };
            return Ok(await _mediator.Send(getCardDetailQuery));
        }

        [HttpPost("AddCard", Name = "AddCard")]
        public async Task<ActionResult<long>> Create([FromBody] CreateCardCommand createCardCommand)
        {
            CardModel cardModel = new CardModel
            {
                Name = createCardCommand.Name
            };
            cardModel = await _virtualCardProvider.CreateCard(cardModel);

            createCardCommand.Number = cardModel.Number;
            createCardCommand.Cvv = cardModel.Cvv;
            createCardCommand.Expires = cardModel.Expires;
            var id = await _mediator.Send(createCardCommand);

            //Sending email notification to admin address
            var email = new Email
            {
                To = "tkh.tegus@gmail.com",
                Body = "Votre carte a été créee avec success",
                Subject = "Nouvelle Card Virtuelle"
            };

            _emailService(EmailType.CONFIRM_REGISTRATION).SendEmail(email, EmailType.CONFIRM_REGISTRATION);

            return Ok(id);
        }

        [HttpPut(Name = "UpdateCard")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Update([FromBody] UpdateCardCommand updateCardCommand)
        {
            await _mediator.Send(updateCardCommand);
            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteCard")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(long id)
        {
            var deleteCardCommand = new DeleteCardCommand() { CardId = id };
            await _mediator.Send(deleteCardCommand);
            return NoContent();
        }

        [HttpGet("export", Name = "ExportCards")]
        [FileResultContentTye("text/csv")]
        public async Task<ActionResult> ExportCards()
        {
            var fileDto = await _mediator.Send(new GetCardsExportQuery());
            

            return File(fileDto.Data, fileDto.ContentType, fileDto.CardExportFileName);
        }
    }
}
