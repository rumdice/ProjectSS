using CoreLibrary.Database;
using Microsoft.AspNetCore.JsonPatch.Internal;
using WebApp.Repository;

namespace WebApp.Service;

public class ShopService : BaseService
{
    private readonly ItemRepository _itemRepository;
    private readonly ILogger<ShopService> _logger;

    public ShopService( 
        IServiceProvider serviceProvider,
        IHttpContextAccessor httpContextAccessor,
        ILogger<ShopService> logger)
        : base (serviceProvider, httpContextAccessor, logger)
    {
        _itemRepository = serviceProvider.GetService<ItemRepository>();
        _logger = logger;
    }

}