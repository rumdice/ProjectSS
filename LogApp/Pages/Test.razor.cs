using LogApp.Service;
using Microsoft.AspNetCore.Components;

namespace LogApp.Pages
{
    public partial class Test : ComponentBase, IAsyncDisposable
    {
        [Inject]
        private AccountService AccountService { get; set; }

        [Inject]
        private MessageService MessageService { get; set; }


        protected override async Task OnInitializedAsync()
        {
            AccountService.EnsureAuthenticated();
            await MessageService.Init();
        }

        async Task  ButtonClicked()
        {
            await MessageService.Send();   
        }

        public async ValueTask DisposeAsync()
        {
            // MessageService의 자원을 명시적으로 해제
            if (MessageService is IAsyncDisposable disposableService)
            {
                await disposableService.DisposeAsync();
            }
        }
    }
}
