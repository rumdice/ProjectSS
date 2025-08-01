using Microsoft.AspNetCore.Components;
using Radzen;
using LogApp.Service;
using CoreDB.DBLogApp;


namespace LogApp.Pages
{
    public partial class Table : ComponentBase
    {

        PagerPosition pagerPosition = PagerPosition.Bottom;

        IEnumerable<AccountEntity> accountList;

        [Inject]
        private AccountService AccountService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            AccountService.EnsureAuthenticated();

            accountList = await AccountService.GetInfoAll();
        }
    }
}
