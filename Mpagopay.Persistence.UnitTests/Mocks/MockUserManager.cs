using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Moq;
using Mpagopay.Identity.Models;

namespace Mpagopay.Persistence.UnitTests.Mocks
{
    public class MockUserManager
    {
        public static Mock<UserManager<ApplicationUser>> GetUserManager() 
        {
            ApplicationUser applicationUserOne = new() 
            { 
                Id = "d0045786-628a-4e7a-ba37-a3144071dac4", 
                Email = "cotl@gmail.com", 
                UserName = "cotl", 
                PasswordHash = "AQAAAAEAACcQAAAAEBAe9K82p1cp7CDfojjQoXIOKy/sBOXZY29JwMPInAt1/gSJXvUgk7Yx7W7TyUSIZA==",
                EmailConfirmed = true 
            };
            ApplicationUser applicationUserTwo = new()
            {
                Id = "7edbf98d-4562-45c1-aa9b-b29c1052e8b0",
                Email = "john@gmail.com",
                UserName = "john",
                PasswordHash = "AQAAAAEAACcQAAAAEBAe9K82p1cp7CDfojjQoXIOKy/sBOXZY29JwMPInAt1/gSJXvUgk7Yx7W7TyUSIZA==",
                EmailConfirmed = true
            };
            ApplicationUser applicationUserThree = new() 
            { 
                Id = "999f7b03-5c3c-48d4-84b8-1d397c6143a3", 
                Email = "timon@gmail.com", 
                UserName = "timon",
                PasswordHash = "AQAAAAEAACcQAAAAEBAe9K82p1cp7CDfojjQoXIOKy/sBOXZY29JwMPInAt1/gSJXvUgk7Yx7W7TyUSIZA==",
                EmailConfirmed = true 
            };
            ApplicationUser applicationUserFour = new() { 
                Id = "0634dae9-166f-460f-85d9-d1cd7345ec1b", 
                Email = "steven@gmail.com", 
                UserName = "steven",
                PasswordHash = "AQAAAAEAACcQAAAAEBAe9K82p1cp7CDfojjQoXIOKy/sBOXZY29JwMPInAt1/gSJXvUgk7Yx7W7TyUSIZA==",
                EmailConfirmed = true 
            };
            ApplicationUser applicationUserFive = new() { 
                Id = "d3971174-1ace-48a5-8d43-3537e40077a2", 
                Email = "omar@gmail.com", 
                UserName = "omar",
                PasswordHash = "AQAAAAEAACcQAAAAEBAe9K82p1cp7CDfojjQoXIOKy/sBOXZY29JwMPInAt1/gSJXvUgk7Yx7W7TyUSIZA==",
                EmailConfirmed = true ,
            };
            
            List<ApplicationUser> _users = new()
            {
                applicationUserOne, applicationUserTwo, applicationUserThree, applicationUserFour, applicationUserFive
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
            mgr.Setup(x => x.FindByEmailAsync("john@gmail.com")).ReturnsAsync(applicationUserTwo);
            mgr.Setup(x => x.FindByEmailAsync("noexist@gmail.com")).ReturnsAsync(null as ApplicationUser);
            mgr.Setup(x => x.GeneratePasswordResetTokenAsync(It.IsAny<ApplicationUser>())).ReturnsAsync("token");
            mgr.Setup(x => x.FindByIdAsync("7edbf98d-4562-45c1-aa9b-b29c1052e8b0")).ReturnsAsync(applicationUserTwo);
            mgr.Setup(x => x.ChangePasswordAsync(applicationUserTwo, "dp@5y!gW", It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
            mgr.Setup(x => x.Users).Returns(_users.AsQueryable());
            

            return mgr;
        }
    }
}
