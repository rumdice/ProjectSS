using LogApp.Component;
using LogApp.Service;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Radzen;

namespace LogApp.Pages
{
    public partial class Gallary : ComponentBase
    {
        [Inject] 
        private ImageService ImageService { get; set; }
        [Inject] 
        private AccountService AccountService { get; set; }
        [Inject]
        private DialogService DialogService { get; set; }


        private string SelectedFolder { get; set; } = string.Empty;

        public List<string> Folders { get; private set; } = new();

       


        protected override async Task OnInitializedAsync()
        {
            try
            {
                await base.OnInitializedAsync();


                AccountService.EnsureAuthenticated();

                //await ImageService.LoadImagesByS3();

                Folders = await ImageService.LoadFoldersAsync();
                
                StateHasChanged();
            }
            catch(Exception e)
            {
                var msg = e.Message;
                Console.WriteLine(msg);
            }
        }

        private async Task ShowImagePopup(string imageUrl)
        {
            await DialogService.OpenAsync<DialogContent>(
                "Image Viewer",
                new Dictionary<string, object> { { "ImageUrl", imageUrl } },
                new DialogOptions { Width = "768px", Height = "1024px" });
        }

       
        private async void SelectFolder(string folder)
        {
            SelectedFolder = folder;
            await ImageService.GetImagesByFolder(SelectedFolder);
        }
    }

}