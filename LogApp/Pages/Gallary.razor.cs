using LogApp.Service;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

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
                // 로그인 체크
                AccountService.EnsureAuthenticated();

                // 페이지가 초기화 될 때 이미지 로드
                await ImageService.LoadImagesByS3();
                StateHasChanged();
            }
            catch(Exception e)
            {
                var msg = e.Message;
                // 예외 처리 로직 추가 가능
            }
        }
    }
}