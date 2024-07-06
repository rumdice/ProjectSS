using CoreLibrary.Database;

using WebApp.Service;

namespace WebApp.Repository;


public class BaseRepository : BaseService
{
    protected readonly DbWebAppContext _webDbContext;

    private readonly ILogger<BaseRepository> _logger;

    public BaseRepository( 
        IServiceProvider serviceProvider,
        IHttpContextAccessor httpContextAccessor,
        ILogger<BaseRepository> logger,
        DbWebAppContext webDbContext)
        : base (serviceProvider, httpContextAccessor, logger)
    {
        _webDbContext = webDbContext;
        _logger = logger;
    }

    // TODO: 모든 repository에 공통적인 요소?


}
