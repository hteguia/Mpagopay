using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Mpagopay.Application.Features.Cards.Commands.UpdateCard
{
    public class UpdateCardCommand : IRequest
    {
        public long CardId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Number { get; set; } = string.Empty;
        public string Cvv { get; set; } = string.Empty;
        public DateTime Expires { get; set; }
    }
}
