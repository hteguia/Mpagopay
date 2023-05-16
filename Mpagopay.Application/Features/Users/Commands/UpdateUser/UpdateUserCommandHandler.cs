using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Mpagopay.Application.Contrats.Persistence;

namespace Mpagopay.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
    {
        private IMapper mapper;
        private IUserRepository object1;
        private ILogger<UpdateUserCommandHandler> object2;

        public UpdateUserCommandHandler(IMapper mapper, IUserRepository object1, ILogger<UpdateUserCommandHandler> object2)
        {
            this.mapper = mapper;
            this.object1 = object1;
            this.object2 = object2;
        }

        public Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
