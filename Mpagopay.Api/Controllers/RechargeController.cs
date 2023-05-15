using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mpagopay.Application.Features.Recharges.Queries.GetRechargeList;
using Mpagopay.Application.Features.Users.Queries.GetUserList;

namespace Mpagopay.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RechargeController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RechargeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("all", Name = "GetAllRecharges")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<RechargeListVm>>> GetAllRecharges()
        {
            var dtos = await _mediator.Send(new GetRechargeListQuery());
            return Ok(dtos);
        }
    }
}
