using AutoMapper;
using Mpagopay.Application.Features.Cards.Commands.CreateCard;
using Mpagopay.Application.Features.Cards.Commands.DeleteCard;
using Mpagopay.Application.Features.Cards.Commands.UpdateCard;
using Mpagopay.Application.Features.Cards.Queries.GetCardDetail;
using Mpagopay.Application.Features.Cards.Queries.GetCardsList;
using Mpagopay.Application.Features.Users.Commands.CreateUser;
using Mpagopay.Application.Features.Users.Queries.GetUserList;
using Mpagopay.Domain.Entities.Users;
using Mpagopay.Domain.Entities.VirtualCard;
using PhoneNumbers;

namespace Mpagopay.Application.Profiles
{
    public class MappingProfile : Profile
    {
        private readonly PhoneNumberUtil phoneUtil = PhoneNumberUtil.GetInstance();

        public MappingProfile() 
        { 
            CreateMap<Card, CardListVm>().ReverseMap();
            CreateMap<Card, CardDetailVm>().ReverseMap();

            CreateMap<Card, CreateCardCommand>().ReverseMap();
            CreateMap<Card, UpdateCardCommand>().ReverseMap();
            CreateMap<Card, DeleteCardCommand>().ReverseMap();

            CreateMap<User, CreateUserCommand>().ReverseMap();

            CreateMap<User, UserListVm>()
                .ForMember(to => to.PhoneNumber, map => map.MapFrom(from => $"{phoneUtil.Parse(from.PhoneNumber, from.CodeIso2).CountryCode} {phoneUtil.FormatInOriginalFormat(phoneUtil.Parse(from.PhoneNumber, from.CodeIso2), from.CodeIso2)}"));
        }
    }
}
