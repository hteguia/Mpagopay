using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mpagopay.Application.Features.Cards.Queries.GetCardsList
{
    public class CardListVm
    {
        public long CardId { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public string Cvv { get; set; }
        public string Expires { get; set; }
    }
}
