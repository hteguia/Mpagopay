using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Mpagopay.Application.Contrats.Infrastructure;
using Mpagopay.Application.Contrats.Persistence;
using Mpagopay.Application.Exceptions;
using Mpagopay.Application.Features.Cards.Commands.CreateCard;
using Mpagopay.Application.Features.Users.Commands.CreateUser;
using Mpagopay.Application.Profiles;
using Mpagopay.Application.UnitTests.Mocks;
using Mpagopay.Domain.Entities;
using Mpagopay.Domain.Entities.Users;
using Shouldly;

namespace Mpagopay.Application.UnitTests.Features.Users.Commands
{
    public class CreateUserTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<ILogger<CreateUserCommandHandler>> _logger;

        public CreateUserTests()
        {
            _mockUserRepository = RepositoryMocks.GetUserRepository();
            _logger = new Mock<ILogger<CreateUserCommandHandler>>();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        [Test]
        public async Task CreateUser_InvokeFindByEmailAndAddUser_ThenAddedToUsersRepo()
        {
            var handler = new CreateUserCommandHandler(_mapper, _mockUserRepository.Object, _logger.Object);
            CreateUserCommand createUserCommand = new()
            {
                FirstName = "John",
                LastName = "Smith",
                PhoneNumber = "679799607",
                Email = "johnsmith@gmail.com",
                CodeIso2 = "CM",
                PinCode = "1234"
            };

            await handler.Handle(createUserCommand, CancellationToken.None);

            _mockUserRepository.Verify(x => x.FindByEmail(It.IsAny<string>()), Times.Once);
            _mockUserRepository.Verify(x => x.AddAsync(It.IsAny<User>()), Times.Once);

            var allUsers = await _mockUserRepository.Object.ListAllAsync();
            allUsers.Count.ShouldBe(6);
        }

        [Test]
        public async Task CreateUser_OnInvalidInput_ThenReturnValidationException()
        {
            var handler = new CreateUserCommandHandler(_mapper, _mockUserRepository.Object, _logger.Object);
            CreateUserCommand createUserCommand = new()
            {
                FirstName = "",
                LastName = "",
                PhoneNumber = "",
                Email = "johnsmithgmail.com",
                CodeIso2 = "MU",
                PinCode = "1234"
            };

            async Task createUser() => await handler.Handle(createUserCommand, CancellationToken.None);

            await Should.ThrowAsync<ValidationException>(() => createUser());

            var allcards = await _mockUserRepository.Object.ListAllAsync();
            allcards.Count.ShouldBe(5);
        }

        [Test]
        public async Task CreateUser_WithEmailAlreadyExist_ThenReturnResourceAlreadyExistException()
        {
            var handler = new CreateUserCommandHandler(_mapper, _mockUserRepository.Object, _logger.Object);

            CreateUserCommand createUserCommand = new()
            {
                FirstName = "John",
                LastName = "Smith",
                PhoneNumber = "679799607",
                Email = "cotl@gmail.com",
                CodeIso2 = "CM",
                PinCode = "1234"
            };

            async Task createUser() => await handler.Handle(createUserCommand, CancellationToken.None);

            await Should.ThrowAsync<ResourceAlreadyExistException>(() => createUser());

            var allcards = await _mockUserRepository.Object.ListAllAsync();
            allcards.Count.ShouldBe(5);
        }
    }
}
