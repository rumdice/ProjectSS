using CoreDB.DBWebApp;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CoreLibrary.Repository;

public class ShopRepository : BaseRepository
{
    public ShopRepository(
        IServiceProvider serviceProvider,
        IHttpContextAccessor httpContextAccessor,
        ILogger<ItemRepository> logger)
        : base (serviceProvider, httpContextAccessor, logger)
    {
    }

    // TODO: EF Core Query method

   
}