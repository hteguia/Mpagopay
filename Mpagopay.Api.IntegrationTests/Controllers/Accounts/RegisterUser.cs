using Mpagopay.Api.IntegrationTests.Base;
using System.Text.Json;
using Mpagopay.Application.Features.Cards.Queries.GetCardsList;
using Shouldly;
using System.Text;
using Mpagopay.Application.Models.Authentication;

namespace Mpagopay.Api.IntegrationTests.Controllers.Accounts
{
    public class RegisterUser
    {
        private CustomWebApplicationFactory<Program> _factory;

        [SetUp]
        public void Setup()
        {
            _factory = new CustomWebApplicationFactory<Program>();
        }

        [Test]
        public async Task Register_ReturnsSuccessResult()
        {
            var client = _factory.GetAnonymousClient();

            string requestContent = Newtonsoft.Json.JsonConvert.SerializeObject(new RegistrationRequest
            {
                Email = "teguia@teguia.me",
                UserName = "hteguia"
            });

            var stringContent = new StringContent(requestContent, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/account/register", stringContent);

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            //var result = JsonSerializer.Deserialize<List<CardListVm>>(responseString);

            //result.ShouldBeOfType<List<CardListVm>>();
            //result.Count.ShouldBe(1);
        }
    }
}
