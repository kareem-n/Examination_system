namespace Examination.Application.Interfaces.Consumers
{
    public interface IRabbitMQConsumer
    {
        Task ConsumeAsync(CancellationToken cancellationToken = default);
    }
}
