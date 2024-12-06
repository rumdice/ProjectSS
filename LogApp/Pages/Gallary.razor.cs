using LogApp.Service;
using Microsoft.AspNetCore.Components;

namespace LogApp.Pages
{
    public partial class Gallary : ComponentBase
    {
        [Inject] 
        private ImageService ImageService { get; set; }
        [Inject] 
        private AccountService AccountService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                await base.OnInitializedAsync();


                AccountService.EnsureAuthenticated();

                await ImageService.LoadImagesByS3();
                StateHasChanged();
            }
            catch(Exception e)
            {
                var msg = e.Message;
                Console.WriteLine(msg);
            }
        }
    }
}