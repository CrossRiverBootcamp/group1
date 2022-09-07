
using Microsoft.Extensions.Configuration;
using NServiceBus;
using System.Data.SqlClient;

class Program
{
    static async Task Main()
    {
        var configuration = new ConfigurationBuilder().
            SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
            .AddJsonFile("appsettings.json", false).Build();

        Console.Title = "CustomerAccount";
        var endpointConfiguration = new EndpointConfiguration("CustomerAccount.NSB");
        endpointConfiguration.EnableInstallers();

        endpointConfiguration.EnableOutbox();
        var persistence = endpointConfiguration.UsePersistence<SqlPersistence>();
        persistence.ConnectionBuilder(
        connectionBuilder: () =>
        {
            return new SqlConnection(configuration.GetConnectionString("NSBconn"));
        });
        var dialect = persistence.SqlDialect<SqlDialect.MsSqlServer>();
        dialect.Schema("dbo");
        var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
        transport.ConnectionString(configuration.GetConnectionString("rabbitMQconn"));
        transport.UseConventionalRoutingTopology(QueueType.Quorum);
        var endpointInstance = await Endpoint.Start(endpointConfiguration);

        Console.WriteLine("Press Enter to exit.");
        Console.ReadLine();

        await endpointInstance.Stop();
    }
}