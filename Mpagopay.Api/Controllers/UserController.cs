using MediatR;
using Microsoft.AspNetCore.Mvc;
using Mpagopay.Application.Features.Users.Commands.CreateUser;
using Mpagopay.Application.Features.Users.Queries.GetUserList;
using Mpagopay.Application.Contrats.Business;
using Microsoft.AspNetCore.Authorization;

namespace Mpagopay.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
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
