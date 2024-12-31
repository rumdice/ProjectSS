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
        private ImageService ImageService { get; set; }

        [Inject]
        private NavigationManager Navigation { get; set; }

        private string SelectedFolder { get; set; } = string.Empty;

        public List<string> Folders { get; private set; } = new();


        protected override async Task OnInitializedAsync()
        {
            AccountService.EnsureAuthenticated();

            Folders = await ImageService.LoadFoldersAsync();
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

        private async Task OnFolderSelected(object value)
        {
            if (value is string selectedFolder && !string.IsNullOrWhiteSpace(selectedFolder))
            {
                SelectedFolder = selectedFolder;

                // 선택한 폴더의 이미지를 로드
                //await ImageService.GetImagesByFolder(SelectedFolder);

                // 상태 변경에 따른 UI 업데이트 강제
                await InvokeAsync(StateHasChanged);
            }
        }


        private string GetUploadUrl()
        {
            var folder = SelectedFolder ?? "default-folder";
            return $"upload/multiple?folderName={folder}";
        }


    }
}
