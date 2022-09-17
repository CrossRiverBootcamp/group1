/*
 using Messages.Commands;
using NServiceBus;
using NServiceBus.Logging;

public class Program
{
    static ILog log = LogManager.GetLogger<Program>();
    static async Task RunLoop(IEndpointInstance endpointInstance)
    {
        while (true)
        {
            await endpointInstance.ScheduleEvery(
         timeSpan: TimeSpan.FromHours(2),
         task: pipelineContext =>
         {
             var message = new DeleteExpiredCodes();
             return pipelineContext.Send(message);
         });
        }

    }

    static async Task Main()
    {
        Console.Title = "Scheduling";

        var endpointConfiguration = new EndpointConfiguration("Scheduling");
        endpointConfiguration.SendOnly();
        var rabbitMQConnection = @"host=localhost";

        var transport =  endpointConfiguration.UseTransport<RabbitMQTransport>();
        transport.ConnectionString(rabbitMQConnection);
        transport.UseConventionalRoutingTopology(QueueType.Quorum);

        var routing = transport.Routing();
        routing.RouteToEndpoint(typeof(DeleteExpiredCodes), "CustomerAccount.WebAPI");


        var conventions = endpointConfiguration.Conventions();
        conventions.DefiningCommandsAs(type => type.Namespace == "Messages.Commands");

        var endpointInstance = await Endpoint.Start(endpointConfiguration);

         await RunLoop(endpointInstance);

        await endpointInstance.Stop();
    }
}
 
 */

using CustomerAccount.Messeges;
using NServiceBus;
using NServiceBus.Logging;
using System.Net;

namespace Scheduler.NSB
{
    public class Program
    {
        static ILog log = LogManager.GetLogger<Program>();
        static async Task RunLoop(IEndpointInstance endpointInstance)
        {
           // while (true)
            {
                //after workins- change to 24 hours
                await endpointInstance.ScheduleEvery(
                        timeSpan: TimeSpan.FromSeconds(30),
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
            Console.Title = "JeneralScheduler";
            var endpointConfiguration = new EndpointConfiguration("JeneralScheduler");
           
            endpointConfiguration.EnableInstallers();
            //endpointConfiguration.SendOnly();
            var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
            transport.UseConventionalRoutingTopology(QueueType.Quorum);
            transport.ConnectionString("host=localhost");
            var routing = transport.Routing();
            routing.RouteToEndpoint(typeof(DeleteExpiredRows), "CustomerAccount");

            var endpointInstance = await Endpoint.Start(endpointConfiguration);
            //var defaultFactory = LogManager.Use<DefaultFactory>();
            //defaultFactory.Level(LogLevel.Info);

            // await RunLoop(endpointInstance);
            await endpointInstance.ScheduleEvery(
                       timeSpan: TimeSpan.FromSeconds(30),
                       task: pipelineContext =>
                       {
                           return pipelineContext.Send(new DeleteExpiredRows()
                           {
                               Date = DateTime.UtcNow
                           });
                       });
            await endpointInstance.Stop();
        }
    }
}
