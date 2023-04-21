
using AutoMapper;
using Blazored.LocalStorage;
using Mpagopay.App.Contrats;
using Mpagopay.App.Services.Base;
using Mpagopay.App.ViewModels;
using Newtonsoft.Json;

namespace Mpagopay.App.Services
{
    public class CardDataService : BaseDataService, ICardDataService
    {
        public readonly IMapper _mapper;
        public readonly IClient _httpClient;
        public CardDataService( IMapper mapper, ILocalStorageService localStorageService, IClient httpClient):base(httpClient, localStorageService)
        {
            _mapper = mapper;
            _httpClient = httpClient;

		}

        public Task<ApiResponse<long>> CreateCard(CardDetailViewModel cardDetailViewModel)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<long>> DeleteCard(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CardListViewModel>> GetAllCards()
        {
            try
            {
                //_httpClient.BaseAddress = new Uri("https://localhost:7275/");
                //var test = await _httpClient.GetStreamAsync($"api/card/all");
                this.AddBearerToken();
                var allCards = await _httpClient.GetAllCardsAsync();
                var mappedCards = _mapper.Map<ICollection<CardListViewModel>>(allCards);
                return mappedCards.ToList();
                //var tt = await System.Text.Json.JsonSerializer.DeserializeAsync<List<CardListViewModel>>
                //	(await _httpClient.GetStreamAsync($"api/card/all"), new System.Text.Json.JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                //return tt;
            }
			catch(Exception ex)
            {
                return null;
            }
            
		}

        public Task<CardDetailViewModel> GetCardById(long id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<long>> UpdateCard(CardDetailViewModel cardDetailViewModel)
        {
            throw new NotImplementedException();
        }
    }
}
