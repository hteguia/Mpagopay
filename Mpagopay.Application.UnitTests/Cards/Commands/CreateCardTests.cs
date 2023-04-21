using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Mpagopay.Application.Contrats.Infrastructure;
using Mpagopay.Application.Contrats.Persistence;
using Mpagopay.Application.Exceptions;
using Mpagopay.Application.Features.Cards.Commands.CreateCard;
using Mpagopay.Application.Profiles;
using Mpagopay.Application.UnitTests.Mocks;
using Mpagopay.Domain.Entities;
using Shouldly;

namespace Mpagopay.Application.UnitTests.Cards.Commands
{
    public class CreateCardTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<ICardRepository> _mockCardRepository;
        private readonly Mock<IEmailService> _mockEmailService;
        private readonly Mock<ILogger<CreateCardCommandHandler>> _logger;

        public CreateCardTests()
        {
            _mockCardRepository = RepositoryMocks.GetCardRepository();
            _mockEmailService = EmailServiceMock.GetEmailService();
            _logger = new Mock<ILogger<CreateCardCommandHandler>>();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        [Test]
        public async Task Handle_ValidateCard_AddedToCardsRepo()
        {
            var handler = new CreateCardCommandHandler(_mapper, _mockCardRepository.Object, _mockEmailService.Object, _logger.Object);

            Card card = new()
            {
                Name = "John Smith",
                Number = "5334 6343 5214 6222",
                Cvv = "234",
                Expires = "12/2025"
            };

            await handler.Handle(new CreateCardCommand() { Name = card.Name, Number = card.Number, Cvv = card.Cvv, Expires = card.Expires }, CancellationToken.None);

            var allcards = await _mockCardRepository.Object.ListAllAsync();
            allcards.Count.ShouldBe(6);
        }

        [Test]
        public async Task Handle_AddedWithExistingNumber()
        {
            var handler = new CreateCardCommandHandler(_mapper, _mockCardRepository.Object, _mockEmailService.Object, _logger.Object);

            Card card = new()
            {
                Name = "John Smith",
                Number = "5549 8287 7683 5854",
                Cvv = "234",
                Expires = "12/2025"
            };

            Func<Task> createCard = async () => await handler.Handle(new CreateCardCommand() { Name = card.Name, Number = card.Number, Cvv = card.Cvv, Expires = card.Expires }, CancellationToken.None);
      
            var exception = await Should.ThrowAsync<ValidationException>(() => createCard());
           
            var allcards = await _mockCardRepository.Object.ListAllAsync();
            allcards.Count.ShouldBe(5);
        }
    }
}
