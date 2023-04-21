using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Mpagopay.Application.Features.Cards.Queries.GetCardDetail
{
    public class GetCardDetailQuery : IRequest<CardDetailVm>
    {
        public long Id { get; set; }
    }
}
