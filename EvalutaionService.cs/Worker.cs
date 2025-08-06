using EvalutaionService.cs.Contracts;
namespace EvalutaionService.cs
{
    public class Worker : BackgroundService
    {

        //private readonly ILogger<Worker> _logger;
        private readonly IConsume consume;

        public Worker(IConsume consume)
        {
            //_logger = logger;
            this.consume = consume;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await consume.ConsumeAsync();

        }
    }



}
