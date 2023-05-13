using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Mpagopay.Application.Contrats;

namespace Mpagopay.Api.Services
{
    public class LoggedInUserService : ILoggedInUserService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public LoggedInUserService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public string UserId
        {
            get
            {
                return  _contextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            }
        }
    }
}
