using CustomerAccount.Messeges;
using NServiceBus;
using NServiceBus.Logging;
using System.Net;

namespace Scheduler.NSB
{
    public class Program
    {

        static async Task Main()
        {
            var endpointConfiguration = new EndpointConfiguration("JeneralScheduler");
            //endpointConfiguration.SendOnly();
            endpointConfiguration.EnableInstallers();

            var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
            transport.UseConventionalRoutingTopology(QueueType.Quorum);
            transport.ConnectionString("host=localhost");

            var routing = transport.Routing();
            routing.RouteToEndpoint(typeof(DeleteExpiredRows), "CustomerAccount");

            var endpointInstance = await Endpoint.Start(endpointConfiguration);
            var defaultFactory = LogManager.Use<DefaultFactory>();
            defaultFactory.Level(LogLevel.Info);

            await endpointInstance.ScheduleEvery(
                  timeSpan: TimeSpan.FromMinutes(5),
                  task: pipelineContext =>
                  {
                      return pipelineContext.Send(new DeleteExpiredRows()
                      {
                          Date = DateTime.UtcNow
                      });
                  });

            Console.ReadKey();
            await endpointInstance.Stop();
        }
    }
}
