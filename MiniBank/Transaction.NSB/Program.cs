using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using NServiceBus;



class Program
{
    static async Task Main()
    {
        var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
            .AddJsonFile("appsettings.json", false).Build();

        Console.Title = "Transaction";
        var endpointConfiguration = new EndpointConfiguration("Transaction.NSB");
        endpointConfiguration.EnableInstallers();

        endpointConfiguration.EnableOutbox();
        var persistence = endpointConfiguration.UsePersistence<SqlPersistence>();
        persistence.ConnectionBuilder(
        connectionBuilder: () =>
        {
            return new SqlConnection(configuration.GetConnectionString("nsbconn"));
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