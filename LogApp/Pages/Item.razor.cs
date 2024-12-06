using CoreLibrary.Repository;
using LogApp.Service;
using Microsoft.AspNetCore.Components;

namespace LogApp.Pages
{
    public partial class Item : ComponentBase
    {
        [Inject]
        private ItemRepository ItemRepository { get; set; }

        [Inject]
        private AccountService AccountService { get; set; }

        private string? itemName { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            AccountService.EnsureAuthenticated();
        }

        private async Task GetItem()
        {
            // ItemRepository를 통해 데이터 가져오기
            var item = await ItemRepository.GetSimpleItemInfoByItemTId(1002);
            itemName = item?.name ?? "No item found";
        }
    }
}
