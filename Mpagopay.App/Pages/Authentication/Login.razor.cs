using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Mpagopay.App.Contrats;
using Mpagopay.App.Services;
using Mpagopay.App.ViewModels;

namespace Mpagopay.App.Pages.Authentication
{
    public partial class Login
    {
        public LoginViewModel ViewModel { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public string Message { get; set; }        

        [CascadingParameter]
        private Task<AuthenticationState> AuthenticationStateTask { get; set; }
      
        protected override async Task OnInitializedAsync()
        {
            ViewModel= new LoginViewModel();           
        }

        public async Task HandleValidSubmit()
        {
            if(await AuthenticationService.Authenticate(ViewModel.Email, ViewModel.Password))
            {
                NavigationManager.NavigateTo("home");
            }

            Message = "Username/password combination unknow";
        }

        protected void NavigateToRegister()
        {
            NavigationManager.NavigateTo("register");
        }
    }
}
