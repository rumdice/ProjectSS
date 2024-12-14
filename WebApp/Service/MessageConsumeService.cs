using RabbitMQ.Stream.Client;
using RabbitMQ.Stream.Client.Reliable;
using Microsoft.Extensions.Logging;
using CoreLibrary.Service;
using CoreLibrary;
using System.Text;
using System.Buffers;
using System.Net;


namespace WebApp.Service
{
    public class MessageConsumeService : BaseService, IAsyncDisposable
    {
        private readonly StreamSystem _streamSystem;
        private Consumer _consumer;
        private readonly BaseLogger<MessageConsumeService> _logger;

        public MessageConsumeService(
            IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
            _logger = serviceProvider.GetRequiredService<BaseLogger<MessageConsumeService>>();
            var config = new StreamSystemConfig
            {
                // 필요한 설정 추가
                Endpoints = new List<EndPoint> { new IPEndPoint(IPAddress.Loopback, 5552) },
            };
            _streamSystem = StreamSystem.Create(config).GetAwaiter().GetResult(); // 

            // 비동기 함수 호출
            Init().GetAwaiter().GetResult();

            // 동기 함수 호출
            //Init();
            _logger.LogInformation($"Init()!~~");

        }

        //public async Task Init()
        //{
        //    // Consumer 설정
        //    _consumer = Consumer.Create(new ConsumerConfig(_streamSystem, "hello-stream")
        //    {
        //        MessageHandler = async (messageContext, _, _, message) =>
        //        {
        //            string receivedMessage = Encoding.UTF8.GetString(message.Data.Contents.ToArray());
        //            _logger.LogInformation($"Message received: {receivedMessage}");

        //            // 메시지 처리 로직
        //            await ProcessMessage(receivedMessage);
        //        }
        //    }).GetAwaiter().GetResult();
        //}

        public async Task Init()
        {
            await _streamSystem.CreateStream(new StreamSpec("hello-stream")
            {
                MaxLengthBytes = 5_000_000_000
            });

            _consumer = await Consumer.Create(new ConsumerConfig(_streamSystem, "hello-stream")
            {
                MessageHandler = async (messageContext, _, _, message) =>
                {
                    string receivedMessage = Encoding.UTF8.GetString(message.Data.Contents.ToArray());
                    _logger.LogInformation($"Message received: {receivedMessage}");

                    await ProcessMessage(receivedMessage);
                }
            });

            _logger.LogInformation($"Init()!");

        }

        private Task ProcessMessage(string message)
        {
            // 예: 데이터 저장, 알림 전송 등
            _logger.LogInformation($"Processing message: {message}");
            return Task.CompletedTask;
        }

        public async ValueTask DisposeAsync()
        {
            if (_consumer != null)
            {
                await _consumer.Close();
            }

            if (_streamSystem != null)
            {
                await _streamSystem.Close();
            }
        }

    }
}


