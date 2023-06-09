using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mpagopay.Domain.Common;
using Mpagopay.Domain.Entities.Users;

namespace Mpagopay.Domain.Entities.BankAccounts
{
    public class BankAccount : BaseEntity
    {
        public long BankAccountId { get; set; }
        public decimal Balance { get; set; }
        public string PinCode { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }
    }
}
