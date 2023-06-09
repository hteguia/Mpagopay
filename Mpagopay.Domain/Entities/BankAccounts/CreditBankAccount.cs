using Mpagopay.Domain.Tools;

namespace Mpagopay.Domain.Entities.BankAccounts
{
    public class CreditBankAccount
    {
        public long CreditBankAccountId { get; set; }
        public decimal Amount { get; set; }
        public decimal Fee { get; set; }
        public PaymentServiceProvider PServiceProvider { get; set; }
        public Status Status { get; set; }
        public string ReferenceId { get; set; }

        public long BanAccountId { get; set; }
        public BankAccount BankAccount { get; set;}
    }
}
