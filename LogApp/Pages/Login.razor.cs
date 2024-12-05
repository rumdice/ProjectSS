using LogApp.Service;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace LogApp.Pages
{
    public partial class Login : ComponentBase
    {
        private string username { get; set; } = "admin";
        private string password { get; set; } = "admin";
        private bool rememberMe { get; set; } = true;

        [Inject]
        private NavigationManager Navigation { get; set; }

        [Inject]
        private AccountService AccountService { get; set; }

        private async Task OnLogin(LoginArgs args, string name)
        {
            Console.WriteLine($"{name} -> Username: {args.Username}, password: {args.Password}, remember me: {args.RememberMe}");

            var result = await AccountService.CheckAccountPassword(args.Username, args.Password);
            if (result)
            {
                AccountService.SetState<bool>("IsAuthenticated", true);
                Navigation.NavigateTo("/gallary");
            }
            else
            {
                AccountService.SetState<bool>("IsAuthenticated", false);
                Navigation.NavigateTo("/login");
            }
        }


        private void OnRegister(string name)
        {
            Console.WriteLine($"{name} -> Register");
        }

        private void OnResetPassword(string value, string name)
        {
            Console.WriteLine($"{name} -> ResetPassword for user: {value}");
        }
    }
}
