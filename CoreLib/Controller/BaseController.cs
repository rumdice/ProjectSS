

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CoreLibrary.Controller;

public class BaseController : ControllerBase
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<BaseController> _logger;


    public BaseController(
        IServiceProvider serviceProvider,
        IHttpContextAccessor httpContextAccessor,
        ILogger<BaseController> logger)
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

    protected ILogger<BaseController> GetLogger()
    {
        return this._logger;
    }
}
