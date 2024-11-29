using CoreLibrary.Database;
using CoreLibrary.Repository;
using CoreLibrary.Service;
using Microsoft.AspNetCore.JsonPatch.Internal;

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
        _itemRepository = serviceProvider.GetRequiredService<ItemRepository>();
        _logger = logger;
    }

}