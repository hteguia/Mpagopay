using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mpagopay.Application.Models.Authentication;

namespace Mpagopay.Application.Contrats.Identity
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
        Task<RegistrationResponse> RegisterAsync(RegistrationRequest request);
        Task<bool> ChangePassword(ChangePasswordRequest request);
        Task<bool> ResetPassword(ResetPasswordRequest request);
        Task<(string userId, string token)> ResetPassword(string email);
        Task<string> GenerateEmailConfirmationTokenAsync(string userId);
        Task<object> ConfirmEmailAsync(string userId, string token);
    }
}
