using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Moq;
using Mpagopay.Application.Contrats;
using Mpagopay.Application.Contrats.Identity;
using Mpagopay.Application.Exceptions;
using Mpagopay.Application.Models.Authentication;
using Mpagopay.Identity.Models;
using Mpagopay.Identity.Services;
using Mpagopay.Persistence.UnitTests.Mocks;
using Shouldly;

namespace Mpagopay.Persistence.UnitTests.Accounts
{
    public class RegisterUserTests
    {
        private  Mock<UserManager<ApplicationUser>> _mockUserManager;
        private  Mock<SignInManager<ApplicationUser>> _mockSignInManager;
        private  Mock<IOptions<JwtSettings>> _mockJwtSettings;
        private  Mock<ILoggedInUserService> _loggedInUserServiceMock;

       
        [SetUp]
        public void Setup()
        {
            _mockUserManager = MockUserManager.GetUserManager();
            _mockSignInManager = MockSignInManager.GetSignInManager();
            _loggedInUserServiceMock = new Mock<ILoggedInUserService>();
            _mockJwtSettings = new Mock<IOptions<JwtSettings>>();
        }

        [Test]
        public async Task RegisterUser_WithValidInput_ThenAddedToUsersRepo()
        {
            var userManager = _mockUserManager.Object;
            var signInManager = _mockSignInManager.Object;
            IAuthenticationService authenticationService = new AuthenticationService(userManager,
                                                                                     signInManager, 
                                                                                     _mockJwtSettings.Object, 
                                                                                     _loggedInUserServiceMock.Object);

            await authenticationService.RegisterAsync(new RegistrationRequest
            {
                Email = "luckamto@bv.com",
                UserName = "luckamto",
                EmailConfirmed = false
            });

            var users = userManager.Users.ToList();
            _mockUserManager.Verify(x => x.FindByEmailAsync(It.IsAny<string>()), Times.Once);
            _mockUserManager.Verify(x => x.FindByNameAsync(It.IsAny<string>()), Times.Once);
            users.Count.ShouldBe(6);
        }

        [Test]
        public async Task RegisterUser_WithInValidInput_ThenReturnValidationException()
        {
            var userManager = _mockUserManager.Object;
            var signInManager = _mockSignInManager.Object;
            IAuthenticationService authenticationService = new AuthenticationService(userManager,
                                                                                     signInManager,
                                                                                     _mockJwtSettings.Object,
                                                                                     _loggedInUserServiceMock.Object);

            async Task registerUser() => await authenticationService.RegisterAsync(new RegistrationRequest
            {
                Email = "johngmail.com",
                UserName = "",
                EmailConfirmed = false
            });

            await Should.ThrowAsync<ValidationException>(() => registerUser());
        }

        [Test]
        public async Task RegisterUser_WithAlreadyExistEmail_ThenReturnException()
        {
            var userManager = _mockUserManager.Object;
            var signInManager = _mockSignInManager.Object;
            IAuthenticationService authenticationService = new AuthenticationService(userManager,
                                                                                     signInManager,
                                                                                     _mockJwtSettings.Object,
                                                                                     _loggedInUserServiceMock.Object);

            async Task registerUser() => await authenticationService.RegisterAsync(new RegistrationRequest
            {
                Email = "john@gmail.com",
                UserName = "john",
                EmailConfirmed = false
            });

            await Should.ThrowAsync<ValidationException>(() => registerUser());
        }
    }
}
