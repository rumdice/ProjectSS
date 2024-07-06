using CoreLibrary.Database;
using Microsoft.AspNetCore.JsonPatch.Internal;
using WebApp.Repository;

// Service : 비즈니스 로직을 처리하는 단계. 간단히 자료를 가져오는 것 부터 복잡한 쿼리 연계까지

namespace WebApp.Service;


public class BaseService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<BaseService> _logger;

    public BaseService(
        IServiceProvider serviceProvider,
        IHttpContextAccessor httpContextAccessor,
        ILogger<BaseService> logger
    )
    {
        this._serviceProvider = serviceProvider;
        this._httpContextAccessor = httpContextAccessor;
        this._logger = logger;
    }

    protected IServiceProvider GetServiceProvider()
    {
        return this._serviceProvider;
    }

    protected IHttpContextAccessor GetHttpContextAccessor()
    {
        return this._httpContextAccessor;
    }

    protected ILogger<BaseService> GetLogger()
    {
        return this._logger;
    }


}