using CoreLibrary;
using CoreLibrary.Service;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Stream.Client;


namespace WebApp.Service
{
    public class MessageBackgroundService : BackgroundService
    {
        private readonly MessageConsumeService _consumeService;
        private readonly BaseLogger<MessageBackgroundService> _logger;
        private readonly BaseService _baseService;

        public MessageBackgroundService(
            IServiceProvider serviceProvider)
        {
            _consumeService = serviceProvider.GetRequiredService<MessageConsumeService>();
            _baseService = serviceProvider.GetRequiredService<BaseService>();

            _logger = serviceProvider.GetRequiredService<BaseLogger<MessageBackgroundService>>();
            _logger.LogInformation("MessageBackgroundService()");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("RabbitMQ Consumer Service is starting.");
            while (!stoppingToken.IsCancellationRequested)
            {
                // RabbitMQ는 메시지를 비동기로 처리하므로 특별히 루프 처리 없이 대기합니다.
                await Task.Delay(10, stoppingToken);
            }

            _logger.LogInformation("RabbitMQ Consumer Service is stopping.");
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("RabbitMQ Consumer Service is cleaning up.");
            await _consumeService.DisposeAsync();
            await base.StopAsync(cancellationToken);
        }

        public override void Dispose()
        {
            _baseService.Dispose(); // BaseService 리소스 정리
            base.Dispose();
        }
    }
}



