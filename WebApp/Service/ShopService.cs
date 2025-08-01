using CoreDB.DBWebApp;
using CoreLibrary;
using CoreLibrary.Repository;
using CoreLibrary.Service;

namespace WebApp.Service;

public class ShopService : BaseService
{
    private readonly ItemRepository _itemRepository;
    private readonly BaseLogger<ShopService> _logger;

    public ShopService( 
        IServiceProvider serviceProvider)
        : base (serviceProvider)
    {
        _itemRepository = serviceProvider.GetRequiredService<ItemRepository>();
        _logger = serviceProvider.GetRequiredService<BaseLogger<ShopService>>();
    }

}