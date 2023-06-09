using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mpagopay.Application.Features.Recharges.Queries.GetRechargeList
{
    public class RechargeListVm
    {
        public string AccountNumber { get; set; }
        public decimal Amount { get; set; }
        public decimal Fee { get; set; }
    }
}
