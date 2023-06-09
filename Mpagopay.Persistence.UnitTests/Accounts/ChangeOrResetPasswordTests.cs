using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Moq;
using Mpagopay.Application.Contrats;
using Mpagopay.Application.Contrats.Identity;
using Mpagopay.Application.Models.Authentication;
using Mpagopay.Application.Models.Mail;
using Mpagopay.Identity.Models;
using Mpagopay.Identity.Services;
using Mpagopay.Persistence.UnitTests.Mocks;
using NUnit.Framework.Interfaces;
using Shouldly;

namespace Mpagopay.Persistence.UnitTests.Accounts
{
    public class ChangeOrResetPasswordTests
    {
        private Mock<UserManager<ApplicationUser>> _mockUserManager;
        private Mock<SignInManager<ApplicationUser>> _mockSignInManager;
        private Mock<IOptions<JwtSettings>> _mockJwtSettings;
        private Mock<ILoggedInUserService> _loggedInUserServiceMock;
        private Mock<JwtSecurityTokenHandler> JwtSecurityTokenHandler;
        private IAuthenticationService authenticationService;


        [SetUp]
        public void Setup()
        {
            _mockUserManager = MockUserManager.GetUserManager();
            _mockSignInManager = MockSignInManager.GetSignInManager();
            _loggedInUserServiceMock = new Mock<ILoggedInUserService>();
            _mockJwtSettings = new Mock<IOptions<JwtSettings>>();
            JwtSecurityTokenHandler = new Mock<JwtSecurityTokenHandler>();

            var userManager = _mockUserManager.Object;
            var signInManager = _mockSignInManager.Object;
            authenticationService = new AuthenticationService(userManager,
                                                              signInManager,
                                                              _mockJwtSettings.Object,
                                                              _loggedInUserServiceMock.Object);
        }

        [Test]
        public async Task ResetPasswordRequest_WithExistEmail_ThenReturnToken()
        {
            var (userId, token) = await authenticationService.ResetPassword("john@gmail.com");

            userId.ShouldNotBeNull();
            token.ShouldNotBeNull();
        }

        [Test]
        public async Task ResetPasswordRequest_WithNoExistEmail_ThenReturnException()
        {
            async Task ResetPasswordRequest() => await authenticationService.ResetPassword("noexist@gmail.com");

            await Should.ThrowAsync<Exception>(() => ResetPasswordRequest());
            _mockUserManager.Verify(x => x.FindByEmailAsync(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public async Task ChangePassword_WithValidInput_ThenChangePasswordInRepo()
        {
            _loggedInUserServiceMock.Setup(x => x.UserId).Returns("7edbf98d-4562-45c1-aa9b-b29c1052e8b0");
            var result = await authenticationService.ChangePassword(new ChangePasswordRequest
            {
                CurrentPassword = "dp@5y!gW",
                NewPassword = "monnouveaumotdepasse"
            });

            result.ShouldBeTrue();
            _mockUserManager.Verify(x => x.FindByIdAsync(It.IsAny<string>()), Times.Once);
            _mockUserManager.Verify(x => x.ChangePasswordAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public async Task ChangePassword_WithInValid_ThenReturnException()
        {

        }

        [Test]
        public async Task ResetPassword_WithValidInput_ThenResetPasswordInRepo()
        {

        }

        [Test]
        public async Task ResetPassword_WithInValid_ThenReturnException()
        {

        }

        [Test]
        public async Task GenerateEmailConfirmation_WithUserExist_ThenReturnException()
        {

        }

        [Test]
        public async Task GenerateEmailConfirmation_WithUserNoExist_ThenReturnException()
        {

        }

        [Test]
        public async Task ConfirmEmail_WithValidInput_ThenReturnSuccess()
        {

        }

        [Test]
        public async Task ConfirmEmail_WithInValidInput_ThenReturnException()
        {

        }
    }
}
