using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mpagopay.Domain.Entities
{
    public class PricingDetail
    {
        public long PricingDetailId { get; set; }
        public Pricing Pricing { get; set; }
        public decimal LowerAmount { get; set; }
        public decimal UpperAmount { get; set; }
        public decimal Fee { get; set; }
    }
}
