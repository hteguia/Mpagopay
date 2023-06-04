using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;
using Mpagopay.Identity.Models;

namespace Mpagopay.Persistence.UnitTests.Mocks
{
    public class MockSignInManager
    {
        public static Mock<SignInManager<ApplicationUser>> GetSignInManager()
        {
            var store = new Mock<IUserStore<ApplicationUser>>();
            var h = new Mock<IHttpContextAccessor>();
            var hd = new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>();
            var mgr = new Mock<SignInManager<ApplicationUser>>(MockUserManager.GetUserManager().Object, h.Object, hd.Object, null, null, null, null);

            mgr.Setup(x => x.PasswordSignInAsync("john", "dp@5y!gW", It.IsAny<bool>(), It.IsAny<bool>())).ReturnsAsync(SignInResult.Success);
            mgr.Setup(x => x.PasswordSignInAsync("john", "wrongpassword", It.IsAny<bool>(), It.IsAny<bool>())).ReturnsAsync(SignInResult.Failed);

            return mgr;
        }
    }
}
