using EvalutaionService.cs.ConsumerSubmmited;
using EvalutaionService.cs.Contracts;
using EvalutaionService.cs.PublishMesaages;

namespace EvalutaionService.cs
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddSingleton<IConsume, ConsumeSubbmitedExam>();
                services.AddSingleton<IMessagePublisher, PublishMessage>();
                services.AddHostedService<Worker>();
            })
            .Build()
            .Run();


            //var builder = Host.CreateApplicationBuilder(args);
            //builder.Services.AddHostedService<Worker>();

            //var host = builder.Build();
            //host.Run();
        }
    }
}