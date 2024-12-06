using Microsoft.Extensions.Logging;

// Service : 비즈니스 로직을 처리하는 단계. 간단히 자료를 가져오는 것 부터 복잡한 쿼리 연계까지

namespace CoreLibrary.Service;


public class BaseService
{
    private readonly IServiceProvider _serviceProvider;
    //private readonly ILogger<BaseService> _logger;

    public BaseService(
        IServiceProvider serviceProvider
        //ILogger<BaseService> logger
    )
    {
        this._serviceProvider = serviceProvider;
        //this._logger = logger;
    }

    protected IServiceProvider GetServiceProvider()
    {
        return this._serviceProvider;
    }

    //protected ILogger<BaseService> GetLogger()
    //{
    //    return this._logger;
    //}


}