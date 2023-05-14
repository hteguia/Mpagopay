using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Mpagopay.Application.Contrats.Business;
using Mpagopay.Application.Contrats.Identity;
using Mpagopay.Application.Features.Users.Commands.CreateUser;
using Mpagopay.Application.Models.Authentication;
using Mpagopay.Identity;
using Mpagopay.Persistence;

namespace Mpagopay.Business.Users
{
    public class UserBusiness : BaseBusiness, IUserBusiness
    {
        protected IAuthenticationService _authenticationService;
        private readonly IMediator _mediator;

        public UserBusiness(MpagopayIdentityDbContext IdentityContext, MpagopayDbContext dbContext, IAuthenticationService authenticationService, IMediator mediator) : base(IdentityContext, dbContext)
        {
            _authenticationService = authenticationService;
            _mediator = mediator;
        }

        public async Task<bool> Create(CreateUserCommand createUserCommand, string password)
        {
            using var identityTransaction = _IdentityContext.Database.BeginTransaction();
            using var dbTransaction = _dbContext.Database.BeginTransaction();
            try
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

                dbTransaction.Commit();
                identityTransaction.Commit();
                return true;
            }
            catch(Exception ex)
            {
                dbTransaction.Rollback();
                identityTransaction.Rollback();
                return false;
            }
            
        }
    }
}
