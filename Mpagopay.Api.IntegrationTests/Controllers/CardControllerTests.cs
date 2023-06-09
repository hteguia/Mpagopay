
using Mpagopay.Api.IntegrationTests.Base;
using System.Text.Json;
using Mpagopay.Application.Features.Cards.Queries.GetCardsList;
using Shouldly;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Mpagopay.Application.Features.Users.Commands.CreateUser;

namespace Mpagopay.Api.IntegrationTests.Controllers
{
    [TestFixture]
    public class CardControllerTests
    {
        private CustomWebApplicationFactory<Program> _factory;      

        [SetUp]
        public void Setup()
        {
            _factory = new CustomWebApplicationFactory<Program>();
        }

        [Test]
        public async Task ReturnsSuccessResult()
        {
            var client = _factory.GetAnonymousClient();

            var response = await client.GetAsync("/api/card/all");

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<List<CardListVm>>(responseString);

            result.ShouldBeOfType<List<CardListVm>>();
            result.Count.ShouldBe(1);
        }
    }
}
