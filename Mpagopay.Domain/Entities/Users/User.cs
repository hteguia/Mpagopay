using Mpagopay.Domain.Common;

namespace Mpagopay.Domain.Entities.Users
{
    public class User : Person
    {
        public long UserId { get; set; }         
        public decimal Balance { get; set; }
        public string PinCode { get; set; }
        public string IdentityId { get; set; }
    }
}
