using System.Text;
using System.Text.Json;
using Examination.Application.DTOs.Exam;
using Examination.Application.Interfaces.Consumers;
using Examination.Domain.Enums;
using Examination.Domain.Interfaces.Services;
using Examintaion.Infrastructure.Repostories;
using Examintaion.Infrastructure.SingalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Examination.Application.MessagesConsumers
{
    public class RabbitMQMessageConsumer : IRabbitMQConsumer
    {
        private readonly ILogger<RabbitMQMessageConsumer> logger;
        private readonly IServiceScopeFactory serviceScopeFactory;

        public RabbitMQMessageConsumer(ILogger<RabbitMQMessageConsumer> logger, IServiceScopeFactory serviceScopeFactory)
        {
            this.logger = logger;
            this.serviceScopeFactory = serviceScopeFactory;
        }


        public async Task ConsumeAsync(CancellationToken cancellationToken = default)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost"
            };
            var connection = await factory.CreateConnectionAsync();
            var channel = await connection.CreateChannelAsync();
            await channel.QueueDeclareAsync(queue: "evaluation_result", durable: false, exclusive: false, autoDelete: false);

            logger.LogError("tetssssssssssssssss");
            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var examEvent = JsonSerializer.Deserialize<ExamResule>(message);
                //
                //
                var scope = serviceScopeFactory.CreateScope();
                var examRepo = scope.ServiceProvider.GetRequiredService<ExamRepo>();
                var notificationService = scope.ServiceProvider.GetRequiredService<INotificationService>();

                var exam = await examRepo.GetByIdAsync(Guid.Parse(examEvent!.ExamId), [x => x.Subject]);
                if (exam != null)
                {
                    exam.Score = examEvent.Score;
                    exam.Status = ExamStudentState.Completed;
                    var result = await examRepo.UpdateAsync(exam);
                    if (result == null)
                    {
                        this.logger.LogError($"Failed to update exam with ID: {examEvent.ExamId}");
                    }
                    else
                    {

                        this.logger.LogInformation($"Exam with ID: {examEvent.ExamId} updated successfully with score: {examEvent.Score}");
                        var connections = UserConnectionManager.GetConnections(exam.StudentId);
                        foreach (var item in connections)
                        {
                            await notificationService.SendNotificationAsync(exam.StudentId, $"Your exam: {exam.Subject.Title} has been evaluated. Your score is: {exam.Score}");
                        }

                    }
                }
                else
                {
                    this.logger.LogWarning($"Exam with ID: {examEvent.ExamId} not found.");
                };


            };
            await channel.BasicConsumeAsync(queue: "evaluation_result", autoAck: true, consumer: consumer);
        }
    }
}
