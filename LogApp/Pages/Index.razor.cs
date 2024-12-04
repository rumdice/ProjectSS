using LogApp.Service;
using Microsoft.AspNetCore.Components;

namespace LogApp.Pages
{
    public partial class Index : ComponentBase
    {
        [Inject]
        private AccountService AccountService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            AccountService.EnsureAuthenticated();
        }

    }
}
