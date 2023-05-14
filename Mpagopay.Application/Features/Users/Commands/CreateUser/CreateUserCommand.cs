using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Mpagopay.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<long>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string CodeIso2 { get; set; }
        public string Email { get; set; }
        public string PinCode { get; set; }
        public string IdentityId { get; set; }
    }
}
