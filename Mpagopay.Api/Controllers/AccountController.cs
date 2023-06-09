using Microsoft.AspNetCore.Mvc;
using Mpagopay.Application.Contrats.Identity;
using Mpagopay.Application.Models.Authentication;
using Mpagopay.Application.Models.Mail;
using Mpagopay.Application.Tools;

namespace Mpagopay.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly EmailServiceResolver _emailService;

        public AccountController(IAuthenticationService authenticationService, EmailServiceResolver emailServiceResolver)
        {
            _authenticationService = authenticationService;
            _emailService = emailServiceResolver;
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request)
        {

            return Ok(await _authenticationService.AuthenticateAsync(request));
        }

        [HttpPost("register")]
        public async Task<ActionResult<RegistrationResponse>> RegistereAsync(RegistrationRequest request)
        {
            var result = await _authenticationService.RegisterAsync(request);

            var token = await _authenticationService.GenerateEmailConfirmationTokenAsync(result.UserId);
            var url = Url.Action("ConfirmEmail", "Account", new { userId = result.UserId, token = result.UserId }, Request.Scheme);
            var emailModel = new Email
            {
                To = request.Email,
                Body = url,
            };

            await _emailService(EmailType.CONFIRM_REGISTRATION).SendEmail(emailModel);
            return Ok(result);
        }

        [HttpGet("confirmEmail")]
        public async Task<ActionResult<RegistrationResponse>> ConfirmEmailAsync(string userId, string token)
        {
            return Ok(await _authenticationService.ConfirmEmailAsync(userId, token));
        }

        [HttpGet("resetPassword")]
        public async Task<ActionResult<RegistrationResponse>> ResetPasswordAsync(string email)
        {
            (string userId, string token) = await _authenticationService.ResetPassword(email);
            var resetLink = Url.Action("ResetPassword", "Account", new { userId, token }, Request.Scheme);

            var emailModel = new Email
            {
                To = email,
                Body = resetLink,
            };

            await _emailService(EmailType.RESET_PASSWORD).SendEmail(emailModel);
            return Ok(new { userId, token });
        }

        [HttpPost("resetPassword")]
        public async Task<ActionResult<RegistrationResponse>> ResetPasswordAsync(ResetPasswordRequest request)
        {
            return Ok(await _authenticationService.ResetPassword(request));
        }

        [HttpPost("changePassword")]
        public async Task<ActionResult<RegistrationResponse>> ChangePasswordAsync(ChangePasswordRequest request)
        {
            return Ok(await _authenticationService.ChangePassword(request));
        }
    }
}
