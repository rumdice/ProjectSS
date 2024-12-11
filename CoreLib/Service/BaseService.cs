using Microsoft.Extensions.Logging;

// Service : 비즈니스 로직을 처리하는 단계. 간단히 자료를 가져오는 것 부터 복잡한 쿼리 연계까지

namespace CoreLibrary.Service;


public class BaseService : IDisposable, IAsyncDisposable
{
    private readonly IServiceProvider _serviceProvider;
    private bool _disposed;

    public BaseService(
        IServiceProvider serviceProvider
    )
    {
        this._serviceProvider = serviceProvider;
    }

    protected virtual void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this); // Finalizer 호출 방지
    }

    protected virtual async ValueTask DisposeAsync()
    {
        await DisposeAsyncCore();
        Dispose(false);
        GC.SuppressFinalize(this);
    }

    protected virtual async ValueTask DisposeAsyncCore()
    {
        // Override this method in derived classes for async resource disposal
        await Task.CompletedTask;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed) return;

        if (disposing)
        {
            // IDisposable 리소스 정리
            if (_serviceProvider is IDisposable disposableProvider)
            {
                disposableProvider.Dispose();
            }
        }

        // 관리되지 않는 리소스 정리 (필요한 경우)
        _disposed = true;
    }

    protected IServiceProvider GetServiceProvider()
    {
        return this._serviceProvider;
    }
}