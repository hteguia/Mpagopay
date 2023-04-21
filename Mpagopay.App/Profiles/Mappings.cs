using AutoMapper;
using Mpagopay.App.Services.Base;
using Mpagopay.App.ViewModels;

namespace Mpagopay.App.Profiles
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<CardListVm, CardListViewModel>().ReverseMap();
        }
        
    }
}
