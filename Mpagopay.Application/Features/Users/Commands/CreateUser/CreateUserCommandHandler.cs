using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Mpagopay.Application.Contrats.Persistence;
using Mpagopay.Application.Features.Pricings.Commands.CreatePricing;
using Mpagopay.Domain.Entities.Users;

namespace Mpagopay.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, long>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        public ILogger<CreateUserCommandHandler> _logger;

        public CreateUserCommandHandler(IMapper mapper, IUserRepository userRepository, ILogger<CreateUserCommandHandler> logger)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<long> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var @user = _mapper.Map<User>(request);

            var validator = new CreateUserCommandValidator(_userRepository);
            var validationResut = await validator.ValidateAsync(request);
            if (validationResut.Errors.Count > 0)
            {
                throw new Exceptions.ValidationException(validationResut);
            }

            var existingEmail = await _userRepository.FindByEmail(user.Email);
            if (existingEmail != null)
            {
                throw new Exceptions.ResourceAlreadyExistException(user.Email);
            }

            //@user.PinCode = BCrypt.Net.BCrypt.HashPassword(@user.PinCode);
            user = await _userRepository.AddAsync(user);

            return user.UserId;
        }
    }
}
