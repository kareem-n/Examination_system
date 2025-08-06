using System.Text;
using System.Text.Json;
using EvalutaionService.cs.Contracts;
using RabbitMQ.Client;

namespace EvalutaionService.cs.PublishMesaages
{
    internal class PublishMessage : IMessagePublisher
    {
        private readonly IConnection _connection;
        private readonly IChannel _channel;


        public PublishMessage()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = factory.CreateConnectionAsync().GetAwaiter().GetResult();
            _channel = _connection.CreateChannelAsync().GetAwaiter().GetResult();

        }
        public async Task PublishAsync<T>(T message, string topicName, CancellationToken cancellation = default) where T : class
        {
            await _channel.QueueDeclareAsync(queue: topicName, durable: false, exclusive: false, autoDelete: false);

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

            await _channel.BasicPublishAsync(exchange: "", routingKey: topicName, body: body);

        }
    }
}
