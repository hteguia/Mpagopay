
using Mpagopay.Api.IntegrationTests.Base;
using System.Text.Json;
using Mpagopay.Application.Features.Cards.Queries.GetCardsList;
using Xunit;

namespace Mpagopay.Api.IntegrationTests.Controllers
{
    
    public class CardControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;
        public CardControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task ReturnsSuccessResult()
        {
            var client = _factory.GetAnonymousClient();

            var response = await client.GetAsync("/api/card/all");

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<List<CardListVm>>(responseString);

            Assert.IsType<List<CardListVm>>(result);
            Assert.NotEmpty(result);
        }
    }
}
