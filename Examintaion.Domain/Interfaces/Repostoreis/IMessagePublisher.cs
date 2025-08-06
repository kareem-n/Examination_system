namespace Examination.Domain.Interfaces.Repostoreis
{
    public interface IMessagePublisher
    {
        Task PublishAsync<T>(T message, string topicName, CancellationToken cancellation = default) where T : class;
    }
}
