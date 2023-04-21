using Microsoft.AspNetCore.Components;
using Mpagopay.App.ViewModels.Account;

namespace Mpagopay.App.Pages.Authentication
{
    public partial class Register
    {
        public RegisterViewModel ViewModel { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public string Message { get; set; }

        public Register()
        {

        }

        protected override void OnInitialized()
        {
            ViewModel = new RegisterViewModel();
        }

        public async Task HandleValidSubmit()
        {
            var result = await AuthenticationService.Register(ViewModel.FirstName, ViewModel.LastName, ViewModel.UserName, ViewModel.Email, ViewModel.Password);
            if (result)
            {
                NavigationManager.NavigateTo("home");
            }

            Message = "Something wrong. Please try again";
        }

        protected void NavigateToLogin()
        {
            NavigationManager.NavigateTo("login");
        }
    }
}
