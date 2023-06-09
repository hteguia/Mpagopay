using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Mpagopay.Application.Contrats.Persistence;
using Mpagopay.Application.Exceptions;
using Mpagopay.Application.Features.Users.Commands.CreateUser;
using Mpagopay.Application.Features.Users.Commands.UpdateUser;
using Mpagopay.Application.Profiles;
using Mpagopay.Application.UnitTests.Mocks;
using Mpagopay.Domain.Entities.Users;
using NuGet.Frameworks;
using Shouldly;

namespace Mpagopay.Application.UnitTests.Features.Users
{
    public class UpdateUserTests
    {
        private IMapper _mapper;
        private Mock<IUserRepository> _mockUserRepository;
        private Mock<ILogger<UpdateUserCommandHandler>> _logger;

        [SetUp]
        public void Setup()
        {
            _mockUserRepository = RepositoryMocks.GetUserRepository();
            _logger = new Mock<ILogger<UpdateUserCommandHandler>>();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        [Test]
        public async Task UpdateUser_InvokeGetAndUpdate_ThenUpdateUsersRepo()
        {
            var handler = new UpdateUserCommandHandler(_mapper, _mockUserRepository.Object, _logger.Object);
            UpdateUserCommand updateUserCommand = new()
            {
                Id = 1,
                FirstName = "UpColt",
                LastName = "UpBlankenship",
                PhoneNumber = "58146949",
                CodeIso2 = "MU",
                PinCode = "4321"
            };


            await handler.Handle(updateUserCommand, CancellationToken.None);

            _mockUserRepository.Verify(x => x.GetByIdAsync(It.IsAny<long>()), Times.Once);
            _mockUserRepository.Verify(x => x.UpdateAsync(It.IsAny<User>()), Times.Once);

            var allcards = await _mockUserRepository.Object.ListAllAsync();
            var user = allcards.FirstOrDefault(p => p.UserId == 1);
            Assert.Multiple(() =>
            {
                Assert.That(user.FirstName, Is.EqualTo("UpColt"));
                Assert.That(user.LastName, Is.EqualTo("UpBlankenship"));
                //Assert.That(BCrypt.Net.BCrypt.Verify("1234", user.PinCode), Is.True);
                Assert.That(user.PhoneNumber, Is.EqualTo("58146949"));
                Assert.That(user.CodeIso2, Is.EqualTo("MU"));
            });
        }

        [Test]
        public async Task UpdateUser_OnInvalidInput_ThenReturnValidationException()
        {
            var handler = new UpdateUserCommandHandler(_mapper, _mockUserRepository.Object, _logger.Object);
            UpdateUserCommand updateUserCommand = new()
            {
                Id = 1,
                FirstName = "",
                LastName = "",
                PhoneNumber = "58146949",
                CodeIso2 = "CM",
                PinCode = "4321"
            };

            async Task updateUser() => await handler.Handle(updateUserCommand, CancellationToken.None);

            await Should.ThrowAsync<ValidationException>(() => updateUser());
            var allcards = await _mockUserRepository.Object.ListAllAsync();
            var user = allcards.FirstOrDefault(p => p.UserId == 1);
            Assert.Multiple(() =>
            {
                Assert.That(user.FirstName, Is.Not.EqualTo(updateUserCommand.FirstName));
                Assert.That(user.LastName, Is.Not.EqualTo(updateUserCommand.LastName));
                Assert.That(user.PhoneNumber, Is.Not.EqualTo(updateUserCommand.PhoneNumber));
                Assert.That(user.CodeIso2, Is.Not.EqualTo(updateUserCommand.CodeIso2));
            });
        }
    }
}
