using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Mpagopay.App.Auth;
using Mpagopay.App.Contrats;
using Mpagopay.App.Services.Base;


namespace Mpagopay.App.Services
{
    public class AuthenticationService : BaseDataService, IAuthenticationService
    {
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        public AuthenticationService(IClient client, ILocalStorageService localStorage, AuthenticationStateProvider authenticationStateProvider)
            :base(client, localStorage)
        {
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<bool> Authenticate(string email, string password)
        {
            try
            {
                AuthenticationRequest authenticationRequest = new AuthenticationRequest() { Email = email, Password = password };   
                var authencitationResponse = await _client.AuthenticateAsync(authenticationRequest);

                if(authencitationResponse.Token != string.Empty)
                {
                    await _localStorage.SetItemAsync("token", authencitationResponse.Token);
                    ((CustomAuthenticationStateProvider)_authenticationStateProvider).SetUserAuthenticated(email);
                    _client.HttpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", authencitationResponse.Token);
                    return true;
                }

                return false;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("token");
            ((CustomAuthenticationStateProvider)_authenticationStateProvider).SetUserLoggedOut();
            _client.HttpClient.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<bool> Register(string firstName, string lastName, string userName, string email, string password)
        {
            RegistrationRequest registrationRequest = new RegistrationRequest()
            {
                FirstName = firstName,
                LastName = lastName,
                UserName = userName,
                Email = email,
                Password = password
            };
            var response = await _client.RegisterAsync(registrationRequest);
            if (!string.IsNullOrWhiteSpace(response.UserId))
            {
                return true;
            }

            return false;
        }
    }
}
