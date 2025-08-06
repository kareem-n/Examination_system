using System.Text;
using System.Text.Json;
using Examination.Domain.Interfaces.Repostoreis;
using RabbitMQ.Client;

namespace Examintaion.Infrastructure.RabbitMQMessageHandlers
{
    public class RabbitMqPublisher : IMessagePublisher
    {
        private readonly IConnection _connection;
        private readonly IChannel _channel;

        public RabbitMqPublisher()
        {
            // Initialize RabbitMQ connection and channel here
            var factory = new ConnectionFactory()
            {
                HostName = "localhost", // Replace with your RabbitMQ host
                //Port = 5672, // Default RabbitMQ port
                //UserName = "guest", // Default username
                //Password = "guest" // Default password
            };

            _connection = factory.CreateConnectionAsync().GetAwaiter().GetResult();
            _channel = _connection.CreateChannelAsync().GetAwaiter().GetResult();
        }
        public async Task PublishAsync<T>(T message, string topicName, CancellationToken cancellationToken = default) where T : class
        {
            await _channel.QueueDeclareAsync(
                queue: topicName,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null
                );


            var json = JsonSerializer.Serialize(message);


            var body = Encoding.UTF8.GetBytes(json);

            await _channel.BasicPublishAsync(exchange: string.Empty, routingKey: topicName, body: body);

            //return Task.CompletedTask;
        }
    }
}
