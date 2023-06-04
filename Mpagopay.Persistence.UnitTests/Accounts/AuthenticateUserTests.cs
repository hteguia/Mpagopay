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
using Mpagopay.Identity.Models;
using Mpagopay.Identity.Services;
using Mpagopay.Persistence.UnitTests.Mocks;
using Shouldly;

namespace Mpagopay.Persistence.UnitTests.Accounts
{
    public class AuthenticateUserTests
    {
        private Mock<UserManager<ApplicationUser>> _mockUserManager;
        private Mock<SignInManager<ApplicationUser>> _mockSignInManager;
        private Mock<IOptions<JwtSettings>> _mockJwtSettings;
        private Mock<ILoggedInUserService> _loggedInUserServiceMock;
        private Mock<JwtSecurityTokenHandler> JwtSecurityTokenHandler;


        [SetUp]
        public void Setup()
        {
            _mockUserManager = MockUserManager.GetUserManager();
            _mockSignInManager = MockSignInManager.GetSignInManager();
            _loggedInUserServiceMock = new Mock<ILoggedInUserService>();
            _mockJwtSettings = new Mock<IOptions<JwtSettings>>();
            JwtSecurityTokenHandler = new Mock<JwtSecurityTokenHandler>();
        }

        [Test]
        public async Task AuthenticateUser_WithValidInput_ThenReturnToken()
        {
            var userManager = _mockUserManager.Object;
            var signInManager = _mockSignInManager.Object;
            string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJ0a2gudGVndXMiLCJqdGkiOiI4NTVlMmZiNC0yNjNhLTRmMDQtOTRjZC0zMjBlMzI3ZmVlMTYiLCJlbWFpbCI6InRraC50ZWd1c0BnbWFpbC5jb20iLCJ1aWQiOiJlMzBkMTEwYi02NWIzLTQ2ZjMtYTExNS04OWFmYjhkYzNkYTkiLCJleHAiOjE2ODU4MjQzNjQsImlzcyI6Ik1wYWdvcGF5SWRlbnRpdHkiLCJhdWQiOiJNcGFnb3BheUlkZW50aXR5In0.eFN7KK-zv8sfgS5TkRKXUaHve5_1lbvLasCsOOyNhqk";
            JwtSecurityTokenHandler.Setup(x => x.WriteToken(It.IsAny<JwtSecurityToken>())).Returns(token);
            _mockJwtSettings.Setup(x => x.Value).Returns(new JwtSettings { Key = "Key", Audience = "Audience", Issuer = "Issuer", DurationInMinutes = 60 });

            var configurationSectionMock = new Mock<IConfigurationSection>();
            var configurationMock = new Mock<IConfiguration>();

            configurationSectionMock
               .Setup(x => x.Value)
               .Returns("http://someservice:81");

            configurationMock
               .Setup(x => x.GetSection("JwtSettings:Key"))
               .Returns(configurationSectionMock.Object);

            IAuthenticationService authenticationService = new AuthenticationService(userManager,
                                                                                     signInManager,
                                                                                     _mockJwtSettings.Object,
                                                                                     _loggedInUserServiceMock.Object);

           var result = await authenticationService.AuthenticateAsync(new AuthenticationRequest
            {
                Email = "john@gmail.com",
                Password = "dp@5y!gW"
           });

            _mockUserManager.Verify(x => x.FindByEmailAsync(It.IsAny<string>()), Times.Once);
            _mockSignInManager.Verify(x => x.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()), Times.Once);

            result.ShouldNotBeNull();
            result.ShouldNotBeOfType<AuthenticationResponse>();
            result.Token.ShouldBe(token);
        }

        [Test]
        public async Task AuthenticateUser_WithNoExistsEmail_ThenReturnException()
        {
            var userManager = _mockUserManager.Object;
            var signInManager = _mockSignInManager.Object;

            IAuthenticationService authenticationService = new AuthenticationService(userManager,
                                                                                     signInManager,
                                                                                     _mockJwtSettings.Object,
                                                                                     _loggedInUserServiceMock.Object);
            

            async Task authenticateUser() => await authenticationService.AuthenticateAsync(new AuthenticationRequest
            {
                Email = "noexist@gmail.com",
                Password = "dp@5y!gW"
            });

            _mockUserManager.Verify(x => x.FindByEmailAsync(It.IsAny<string>()), Times.Once);
            await Should.ThrowAsync<Exception>(() => authenticateUser());
        }

        [Test]
        public async Task AuthenticateUser_WithWrongPassword_ThenReturnException()
        {
            var userManager = _mockUserManager.Object;
            var signInManager = _mockSignInManager.Object;

            IAuthenticationService authenticationService = new AuthenticationService(userManager,
                                                                                     signInManager,
                                                                                     _mockJwtSettings.Object,
                                                                                     _loggedInUserServiceMock.Object);


            async Task authenticateUser() => await authenticationService.AuthenticateAsync(new AuthenticationRequest
            {
                Email = "john@gmail.com",
                Password = "wrongpassword"
            });

            _mockSignInManager.Verify(x => x.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()), Times.Once);
            await Should.ThrowAsync<Exception>(() => authenticateUser());
        }
    }
}
