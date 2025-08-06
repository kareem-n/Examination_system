using Examination.Application.Interfaces.Consumers;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Examination.Application.Workers
{
    public class ConsumeWorker : BackgroundService
    {
        private readonly IRabbitMQConsumer _rabbitMQConsumer;
        private readonly ILogger<ConsumeWorker> logger;

        public ConsumeWorker(IRabbitMQConsumer rabbitMQConsumer, ILogger<ConsumeWorker> logger)
        {
            _rabbitMQConsumer = rabbitMQConsumer;
            this.logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _rabbitMQConsumer.ConsumeAsync(stoppingToken);
        }
    }

}
