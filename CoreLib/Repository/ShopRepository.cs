using CoreDB.DBWebApp;
using Microsoft.Extensions.Logging;

namespace CoreLibrary.Repository;

public class ShopRepository : BaseRepository
{
    public ShopRepository(IServiceProvider serviceProvider)
        : base (serviceProvider)
    {
    }

    // TODO: EF Core Query method

   
}