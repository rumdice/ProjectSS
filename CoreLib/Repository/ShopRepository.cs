using CoreLibrary.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CoreLibrary.Repository;

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