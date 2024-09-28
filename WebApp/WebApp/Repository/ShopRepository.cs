using CoreLibrary.Database;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Repository;


public class ShopRepository : BaseRepository
{
    public ShopRepository(
        IServiceProvider serviceProvider,
        IHttpContextAccessor httpContextAccessor,
        ILogger<ItemRepository> logger,
        DbWebAppContext webAppContext)
        : base (serviceProvider, httpContextAccessor, logger, webAppContext)
    {
    }

    // TODO: EF Core Query method

   
}