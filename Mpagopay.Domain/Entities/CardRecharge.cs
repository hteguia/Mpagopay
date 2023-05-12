using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mpagopay.Domain.Common;

namespace Mpagopay.Domain.Entities
{
    public class CardRecharge : BaseEntity
    {
        public long CardRechargeId { get; set; }
        public Card Card { get; set; }
        public decimal Amount { get; set; }
        public decimal Fee { get; set; }
    }
}
