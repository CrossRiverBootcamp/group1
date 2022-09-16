using CustomerAccount.Messeges;
using NServiceBus;
using NServiceBus.Logging;
using System.Net;

namespace Scheduler.NSB
{
    public class Program
    {

        static async Task RunLoop(IEndpointInstance endpointInstance)
        {
            while (true)
            {
                //after workins- change to 24 hours
                await endpointInstance.ScheduleEvery(
                        timeSpan: TimeSpan.FromSeconds(20),
                        task: pipelineContext =>
                        {
                            return pipelineContext.Send(new DeleteExpiredRows()
                            {
                                Date = DateTime.UtcNow
                            });
                        });
            }

        }
        static async Task Main()
        {
            var endpointConfiguration = new EndpointConfiguration("JeneralScheduler");
            //endpointConfiguration.SendOnly();
            endpointConfiguration.EnableInstallers();

            var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
            transport.UseConventionalRoutingTopology(QueueType.Quorum);
            transport.ConnectionString("host=localhost");
            var routing = transport.Routing();
            routing.RouteToEndpoint(typeof(DeleteExpiredRows), "CustomerAccount.WebAPI");

            var endpointInstance = await Endpoint.Start(endpointConfiguration);
            var defaultFactory = LogManager.Use<DefaultFactory>();
            defaultFactory.Level(LogLevel.Info);

            await RunLoop(endpointInstance);

            await endpointInstance.Stop();
        }
    }
}
