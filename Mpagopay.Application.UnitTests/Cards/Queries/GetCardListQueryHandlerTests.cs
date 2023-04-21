using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Mpagopay.Application.Contrats.Persistence;
using Mpagopay.Domain.Entities;
using NUnit.Framework;
using Moq;
using Mpagopay.Application.UnitTests.Mocks;
using Mpagopay.Application.Profiles;
using Mpagopay.Application.Features.Cards.Queries.GetCardDetail;
using Mpagopay.Application.Features.Cards.Queries.GetCardsList;
using Shouldly;

namespace Mpagopay.Application.UnitTests.Cards.Queries
{
    [TestFixture]
    public class GetCardListQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<ICardRepository> _mockCardRepository;

        public GetCardListQueryHandlerTests()
        {
            _mockCardRepository = RepositoryMocks.GetCardRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        [Test]
        public async Task GetCardsListTest()
        {
            var handler = new GetCardListQueryHandler(_mapper, _mockCardRepository.Object);

            var result = await handler.Handle(new GetCardListQuery(), CancellationToken.None);

            result.ShouldBeOfType<List<CardListVm>>();

            result.Count.ShouldBe(4);
        }
    }
}
