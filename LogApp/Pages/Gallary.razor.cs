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

        private async Task OnFolderSelected(object value)
        {
            if (value is string selectedFolder && !string.IsNullOrWhiteSpace(selectedFolder))
            {
                SelectedFolder = selectedFolder;

                // 선택한 폴더의 이미지를 로드
                await ImageService.GetImagesByFolder(SelectedFolder);

                // 상태 변경에 따른 UI 업데이트 강제
                await InvokeAsync(StateHasChanged);
            }
        }

    }

}