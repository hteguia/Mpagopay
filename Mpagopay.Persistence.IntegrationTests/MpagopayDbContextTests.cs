using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using Mpagopay.Application.Contrats;
using Mpagopay.Domain.Entities;
using Mpagopay.Domain.Entities.VirtualCard;
using Shouldly;

namespace Mpagopay.Persistence.IntegrationTests
{
    [TestFixture]
    public class MpagopayDbContextTests
    {
        private readonly MpagopayDbContext _mpagopayDbContext;
        private readonly Mock<ILoggedInUserService> _loggedInUserServiceMock;
        private readonly string _loggedInUserId;

        public MpagopayDbContextTests()
        {
            var dbContextOptions = new DbContextOptionsBuilder<MpagopayDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            _loggedInUserId = "1";
            _loggedInUserServiceMock = new Mock<ILoggedInUserService>();
            _loggedInUserServiceMock.Setup(m => m.UserId).Returns(_loggedInUserId);

            _mpagopayDbContext = new MpagopayDbContext(dbContextOptions, _loggedInUserServiceMock.Object);
        }

        [Test]
        public async Task Save_SetCreatedByProperty()
        {
            var card = new Card() { CardId = 1, Name = "KAMDJO" };

            _mpagopayDbContext.Cards.Add(card);
            await _mpagopayDbContext.SaveChangesAsync();

            card.CreateBy.ShouldBe(_loggedInUserId);
        }
    }
}
