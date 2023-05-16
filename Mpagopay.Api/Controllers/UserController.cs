using MediatR;
using Mpagopay.Application.Models.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mpagopay.Application.Contrats.Infrastructure;
using Mpagopay.Application.Features.Cards.Commands.CreateCard;
using Mpagopay.Application.Features.Cards.Queries.GetCardDetail;
using Mpagopay.Application.Features.Cards.Queries.GetCardsList;
using Mpagopay.Application.Features.Users.Commands.CreateUser;
using Mpagopay.Application.Features.Users.Queries.GetUserList;
using Mpagopay.Application.Models.VirtualCard;
using Mpagopay.Infrastructure.VirtualCard;
using Mpagopay.Application.Contrats.Identity;
using Mpagopay.Business.Users;
using Mpagopay.Identity;
using Mpagopay.Persistence;
using Microsoft.Extensions.Options;
using Mpagopay.Application.Models.Contexts;
using Mpagopay.Application.Contrats.Business;

namespace Mpagopay.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        protected readonly IUserBusiness _userBusinessProcess;

        public UserController(IMediator mediator, IUserBusiness userBusinessProcess)
        {
            _mediator = mediator;
            _userBusinessProcess = userBusinessProcess;
        }

        [HttpGet("all", Name = "GetAllUsers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<UserListVm>>> GetAllUsers()
        {
            var dtos = await _mediator.Send(new GetUserListQuery());
            return Ok(dtos);
        }

        [HttpPost("AddUser", Name = "AddUser")]
        public async Task<ActionResult<long>> AddUser([FromBody] CreateUserCommand createUserCommand)
        {
            bool result =  await _userBusinessProcess.Create(createUserCommand, "User@tegus05");
            return Ok(result);
        }
    }
}
