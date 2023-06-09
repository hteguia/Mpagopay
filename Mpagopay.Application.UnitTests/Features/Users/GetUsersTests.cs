using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using Mpagopay.Application.Contrats.Persistence;
using Mpagopay.Application.Features.Cards.Queries.GetCardsList;
using Mpagopay.Application.Features.Users.Queries.GetUserList;
using Mpagopay.Application.Profiles;
using Mpagopay.Application.UnitTests.Mocks;
using Shouldly;

namespace Mpagopay.Application.UnitTests.Features.Users
{
    [TestFixture]
    public class GetUsersTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUserRepository> _mockUserRepository;

        public GetUsersTests()
        {
            _mockUserRepository = RepositoryMocks.GetUserRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        [Test]
        public async Task GetUsersListTest()
        {
            var handler = new GetUserListQueryHandler(_mapper, _mockUserRepository.Object);

            var result = await handler.Handle(new GetUserListQuery(), CancellationToken.None);

            result.ShouldBeOfType<List<UserListVm>>();

            result.Count.ShouldBe(5);
        }
    }
}
