namespace EvalutaionService.cs.Contracts
{
    public interface IMessagePublisher
    {
        Task PublishAsync<T>(T message, string topicName, CancellationToken cancellation = default) where T : class;
    }
}
