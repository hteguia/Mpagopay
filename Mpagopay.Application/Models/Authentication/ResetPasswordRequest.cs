using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mpagopay.Application.Models.Authentication
{
    public class ResetPasswordRequest
    {
        public string UserId { get; set; }  
        public string Token { get; set; }
        public string Password { get; set; }
    }
}
