using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Mpagopay.Application.Features.Cards.Commands.CreateCard
{
    public class CreateCardCommand : IRequest<long>
    {
        public string Name { get; set; } = string.Empty;
        public string Number { get; set; } = string.Empty;
        public string Cvv { get; set; } = string.Empty;
        public string Expires { get; set; } = string.Empty;
        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
