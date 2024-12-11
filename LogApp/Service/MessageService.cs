using RabbitMQ.Stream.Client.Reliable;
using RabbitMQ.Stream.Client;
using System.Text;
using CoreLibrary.Service;
using CoreLibrary;


namespace LogApp.Service
{
    public class MessageService : BaseService, IAsyncDisposable
    {
        private readonly BaseLogger<MessageService> _logger;

        private StreamSystem _streamSystem;

        private Producer _producer;

        private bool _initialized;

        public MessageService(
            IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
            _logger = serviceProvider.GetRequiredService<BaseLogger<MessageService>>();
        }

        public async Task Init()
        {
            if (_initialized) return;
            _initialized = true;

            _streamSystem = await StreamSystem.Create(new StreamSystemConfig());

            await _streamSystem.CreateStream(new StreamSpec("hello-stream")
            {
                MaxLengthBytes = 5_000_000_000
            });

            _producer = await Producer.Create(new ProducerConfig(_streamSystem, "hello-stream"));
        }
        public async Task Send()
        {
            // Send Message to Rabbit MQ
            // 버튼을 누르면 메시지를 계속 보낸다.
            
            await _producer.Send(new Message(Encoding.UTF8.GetBytes($"Hello, World")));
        }

        protected override async ValueTask DisposeAsyncCore()
        {
            if (_producer != null)
            {
                await _producer.Close();
            }

            if (_streamSystem != null)
            {
                await _streamSystem.Close();
            }

            await base.DisposeAsync(); // BaseService의 DisposeAsync 호출
        }
    }
}
