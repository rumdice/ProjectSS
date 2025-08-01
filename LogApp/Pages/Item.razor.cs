using CoreDB.DBWebApp;
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

        private string itemName { get; set; } = "";

        private ItemEntity itemEntity { get; set; } = new ItemEntity();

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            AccountService.EnsureAuthenticated();
        }

        private async Task<ItemEntity> OnClick(string _itemName)
        {
            // ItemRepository를 통해 데이터 가져오기
            
            var item = await ItemRepository.GetItemSimpleEntityByName(_itemName);
            if (item != null)
            {
                itemEntity = item;
            }

            return new ItemEntity();

        }
    }
}
