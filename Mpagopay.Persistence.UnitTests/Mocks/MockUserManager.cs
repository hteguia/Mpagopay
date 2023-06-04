using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Moq;
using Mpagopay.Application.Models.Mail;
using Mpagopay.Identity.Models;

namespace Mpagopay.Persistence.UnitTests.Mocks
{
    public class MockUserManager
    {
        public static Mock<UserManager<ApplicationUser>> GetUserManager() 
        {
            List<ApplicationUser> _users = new()
            {
                new ApplicationUser{ Id = Guid.NewGuid().ToString(), Email = "cotl@gmail.com", UserName = "cotl", EmailConfirmed = true },
                new ApplicationUser{ Id = Guid.NewGuid().ToString(), Email = "john@gmail.com", UserName = "john", EmailConfirmed = true },
                new ApplicationUser{ Id = Guid.NewGuid().ToString(), Email = "timon@gmail.com", UserName = "timon", EmailConfirmed = true },
                new ApplicationUser{ Id = Guid.NewGuid().ToString(), Email = "steven@gmail.com", UserName = "steven", EmailConfirmed = true },
                new ApplicationUser{ Id = Guid.NewGuid().ToString(), Email = "omar@gmail.com", UserName = "omar", EmailConfirmed = true }
            };

            var store = new Mock<IUserStore<ApplicationUser>>();
            var mgr = new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);
            mgr.Object.UserValidators.Add(new UserValidator<ApplicationUser>());
            mgr.Object.PasswordValidators.Add(new PasswordValidator<ApplicationUser>());
                
            mgr.Setup(x => x.DeleteAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(IdentityResult.Success);
            mgr.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success).Callback<ApplicationUser, string>((x, y) => _users.Add(x));
            mgr.Setup(x => x.UpdateAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(IdentityResult.Success);
            mgr.Setup(x => x.GetClaimsAsync(It.IsAny<ApplicationUser>())).Returns(Task.FromResult(new List<Claim>() as IList<Claim>));
            mgr.Setup(x => x.GetRolesAsync(It.IsAny<ApplicationUser>())).Returns(Task.FromResult(new List<string>() as IList<string>));
            mgr.Setup(x => x.FindByEmailAsync("john@gmail.com")).ReturnsAsync(new ApplicationUser { Email = "john@gmail.com" });
            mgr.Setup(x => x.FindByEmailAsync("noexist@gmail.com")).ReturnsAsync(null as ApplicationUser);
            mgr.Setup(x => x.Users).Returns(_users.AsQueryable());
            

            return mgr;
        }
    }
}
