using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using MediatR;
using Mpagopay.Application.Contrats.Identity;
using Mpagopay.Application.Features.Users.Commands.CreateUser;
using Mpagopay.Application.Models.Authentication;

namespace Mpagopay.Application.Business.User
{
    public class User
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IMediator _mediator;

        public User(IAuthenticationService authenticationService, IMediator mediator)
        {
            _authenticationService = authenticationService;
            _mediator = mediator;
        }

        public async Task<bool> Create(CreateUserCommand createUserCommand, string password)
        {
            
            RegistrationRequest registrationRequest = new()
            {
                FirstName = createUserCommand.FirstName,
                LastName = createUserCommand.LastName,
                Email = createUserCommand.Email,
                UserName = createUserCommand.Email,
                Password = password
            };

            RegistrationResponse registrationResponse = await _authenticationService.RegisterAsync(registrationRequest);

            createUserCommand.IdentityId = registrationResponse.UserId;
            await _mediator.Send(createUserCommand);

            return true;
        }   
    }
}
