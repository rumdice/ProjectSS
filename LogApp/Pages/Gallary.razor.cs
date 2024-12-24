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

        private async Task ShowImagePopup(string imageUrl)
        {
            await DialogService.OpenAsync<DialogContent>(
                "Image Viewer",
                new Dictionary<string, object> { { "ImageUrl", imageUrl } },
                new DialogOptions { Width = "768px", Height = "1024px" });
        }
    }

    public class DialogContent : ComponentBase
    {
        [Parameter]
        public string ImageUrl { get; set; }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenElement(0, "div");
            builder.AddAttribute(1, "style", "text-align: center; padding: 20px;");
            builder.OpenElement(2, "img");
            builder.AddAttribute(3, "src", ImageUrl);
            builder.AddAttribute(4, "style", "max-width: 100%; max-height: 100%;");
            builder.CloseElement();
            builder.CloseElement();
        }
    }
}