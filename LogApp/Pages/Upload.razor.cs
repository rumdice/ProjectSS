using LogApp.Service;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Radzen;

namespace LogApp.Pages
{
    public partial class Upload : ComponentBase
    {
        private int progress { get; set; } = 0;
        private List<IBrowserFile> files { get; set; } = new();

        [Inject]
        private AccountService AccountService { get; set; }

        [Inject]
        private NavigationManager Navigation { get; set; }

        protected override async Task OnInitializedAsync()
        {
            AccountService.EnsureAuthenticated();
        }

        private void OnProgress(UploadProgressArgs args)
        {
            progress = args.Progress;
        }

        private void OnUploadComplete(UploadCompleteEventArgs args)
        {
            Console.WriteLine("Upload Complete!");
            Navigation.NavigateTo("/gallary");
        }

    }
}
