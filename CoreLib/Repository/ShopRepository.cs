using CoreDB.DBWebApp;
using Microsoft.Extensions.Logging;

namespace CoreLibrary.Repository;

public class ShopRepository : BaseRepository
{
    public ShopRepository(
        IServiceProvider serviceProvider,
        ILogger<ItemRepository> logger)
        : base (serviceProvider, logger)
    {
    }

    // TODO: EF Core Query method

   
}