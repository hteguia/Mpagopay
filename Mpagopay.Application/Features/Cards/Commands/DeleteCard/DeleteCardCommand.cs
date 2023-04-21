using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Mpagopay.Application.Features.Cards.Commands.DeleteCard
{
    public class DeleteCardCommand : IRequest
    {
        public long CardId { get; set; }
    }
}
