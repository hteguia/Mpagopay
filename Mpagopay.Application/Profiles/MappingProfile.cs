using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Mpagopay.Application.Features.Cards.Commands.CreateCard;
using Mpagopay.Application.Features.Cards.Commands.DeleteCard;
using Mpagopay.Application.Features.Cards.Commands.UpdateCard;
using Mpagopay.Application.Features.Cards.Queries.GetCardDetail;
using Mpagopay.Application.Features.Cards.Queries.GetCardsList;
using Mpagopay.Domain.Entities;

namespace Mpagopay.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        { 
            CreateMap<Card, CardListVm>().ReverseMap();
            CreateMap<Card, CardDetailVm>().ReverseMap();

            CreateMap<Card, CreateCardCommand>().ReverseMap();
            CreateMap<Card, UpdateCardCommand>().ReverseMap();
            CreateMap<Card, DeleteCardCommand>().ReverseMap();
        }
    }
}
