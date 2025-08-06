using System.Text;
using System.Text.Json;
using EvalutaionService.cs.Contracts;
using EvalutaionService.cs.DTOs;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace EvalutaionService.cs.ConsumerSubmmited
{
    internal class ConsumeSubbmitedExam : IConsume
    {

        private readonly IConnection _connection;
        private readonly IChannel _channel;
        private readonly IMessagePublisher messagePublisher;
        private readonly ILogger<ConsumeSubbmitedExam> logger;

        public ConsumeSubbmitedExam(IMessagePublisher messagePublisher, ILogger<ConsumeSubbmitedExam> logger)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = factory.CreateConnectionAsync().GetAwaiter().GetResult();
            _channel = _connection.CreateChannelAsync().GetAwaiter().GetResult();
            _channel.QueueDeclareAsync(queue: "exam-submitted", durable: false, exclusive: false, autoDelete: false).GetAwaiter().GetResult();
            this.messagePublisher = messagePublisher;
            this.logger = logger;
        }

        public async Task ConsumeAsync()
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var examEvent = JsonSerializer.Deserialize<ExamMessageRecieved>(message);


                float score = 0.0f;

                foreach (var correct in examEvent!.CorrectAnswers)
                {
                    if (examEvent.StudentAnswer.ExamQuestionsAnswers.Any(a => a.AnswerId!.ToString().ToLower() == correct.ToLower()))
                    {
                        score += 1.0f;
                    }
                }

                this.logger.LogInformation($"{examEvent.ExamId} - score: {score}");
                await messagePublisher.PublishAsync(new { examEvent.ExamId, Score = score }, "evaluation_result");
            };

            await _channel.BasicConsumeAsync(queue: "exam-submitted", autoAck: true, consumer: consumer);

        }
    }
}
