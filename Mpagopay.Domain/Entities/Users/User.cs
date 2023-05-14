using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mpagopay.Domain.Common;

namespace Mpagopay.Domain.Entities.Users
{
    public class User : BaseEntity
    {
        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }  
        public decimal Balance { get; set; }
        public string CodeIso2 { get; set; }
        public string PinCode { get; set; }
        public string IdentityId { get; set; }
    }
}
