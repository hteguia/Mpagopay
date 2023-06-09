using Mpagopay.Domain.Common;

namespace Mpagopay.Domain.Entities.Users
{
    public class User : Person
    {
        public long UserId { get; set; }         
        
        public string IdentityId { get; set; }
    }
}
