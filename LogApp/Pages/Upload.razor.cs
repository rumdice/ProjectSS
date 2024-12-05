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

            // TODO: 호출이 안되는 이유? - 알았다.
            //await ImageService.UploadFileAsync();
            // 업로드가 완료되면, 업로드된 파일의 URL을 가져오고 싶다면
            //if (args.Files != null && args.Files.Count > 0)
            //{
            //    var uploadedFile = args.Files[0]; // 업로드된 첫 번째 파일
            //    var fileUrl = await ImageService.UploadFileAsync(uploadedFile);
            //    Console.WriteLine($"File uploaded successfully. URL: {fileUrl}");
            //}
        }

    }
}
