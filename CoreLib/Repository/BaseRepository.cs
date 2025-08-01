using CoreDB.DBWebApp;
using CoreDB.DBLogApp;
using CoreLibrary.Service;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;


namespace CoreLibrary.Repository;


public class BaseRepository : BaseService
{
    protected readonly DbWebAppContext _webDbContext;
    protected readonly DbLogAppContext _logDbContext;

   
    public BaseRepository( 
        IServiceProvider serviceProvider
        )
        : base (serviceProvider)
    {
        _webDbContext = serviceProvider.GetRequiredService<DbWebAppContext>();
        _logDbContext = serviceProvider.GetRequiredService<DbLogAppContext>();
    }

    // TODO: 모든 repository에 공통적인 요소?
    // 인터페이스만 제시 하는 방식으로 개선?


}
