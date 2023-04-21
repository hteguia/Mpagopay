using Mpagopay.App.Services.Base;
using Mpagopay.App.ViewModels;

namespace Mpagopay.App.Contrats
{
    public interface ICardDataService
    {
        Task<List<CardListViewModel>> GetAllCards();
        Task<CardDetailViewModel> GetCardById(long id);
        Task<ApiResponse<long>> CreateCard(CardDetailViewModel cardDetailViewModel);
        Task<ApiResponse<long>> UpdateCard(CardDetailViewModel cardDetailViewModel);
        Task<ApiResponse<long>> DeleteCard(long id);
    }
}
