using CoreDB.DBWebApp;
using CoreLibrary.Repository;
using CoreLibrary.Service;

namespace WebApp.Service;

public class ShopService : BaseService
{
    private readonly ItemRepository _itemRepository;
    private readonly ILogger<ShopService> _logger;

    public ShopService( 
        IServiceProvider serviceProvider,
        ILogger<ShopService> logger)
        : base (serviceProvider, logger)
    {
        _itemRepository = serviceProvider.GetRequiredService<ItemRepository>();
        _logger = logger;
    }

}