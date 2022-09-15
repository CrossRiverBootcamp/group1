using CustomerAccount.Messeges;
using NServiceBus;
using NServiceBus.Logging;

namespace Scheduler.NSB
{
    public class Program
    {

        static async Task RunLoop(IEndpointInstance endpointInstance)
        {
            while (true)
            {
                await endpointInstance.ScheduleEvery(
                        timeSpan: TimeSpan.FromHours(24),
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
            var endpointConfiguration = new EndpointConfiguration("scheduler");
            endpointConfiguration.SendOnly();
            endpointConfiguration.EnableInstallers();

            var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
            transport.UseConventionalRoutingTopology(QueueType.Quorum);
            transport.ConnectionString("host=localhost");
            var routing = transport.Routing();
            routing.RouteToEndpoint(typeof(DeleteExpiredRows), "CustomerAccount.WebAPI");

            var endpointInstance = await Endpoint.Start(endpointConfiguration)
            .ConfigureAwait(false);
            var defaultFactory = LogManager.Use<DefaultFactory>();
            defaultFactory.Level(LogLevel.Info);

            await RunLoop(endpointInstance);

            await endpointInstance.Stop();
        }
    }
}