using Mpagopay.Domain.Tools;

namespace Mpagopay.Domain.Entities
{
    public class Recharge
    {
        public long RechargeId { get; set; }
        public string AccountNumber { get; set; }
        public decimal Amount { get; set; }
        public decimal Fee { get; set; }
        public PaymentServiceProvider PServiceProvider { get; set; }
        public Status Status { get; set; }
        public string ReferenceId { get; set; }
    }
}
