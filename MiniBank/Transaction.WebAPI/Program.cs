using Transaction.BL;
using NServiceBus;
using Microsoft.Data.SqlClient;
using Transaction.Messeges;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseNServiceBus(context =>
  {
      var endpointConfiguration = new EndpointConfiguration("Transaction");
      //permissions to administer resources
      endpointConfiguration.EnableInstallers();

      endpointConfiguration.EnableOutbox();

      var persistence = endpointConfiguration.UsePersistence<SqlPersistence>();
      persistence.ConnectionBuilder(
      connectionBuilder: () =>
      {
          return new SqlConnection(builder.Configuration.GetConnectionString("NSBconn"));
      });
      var dialect = persistence.SqlDialect<SqlDialect.MsSqlServer>();
      dialect.Schema("dbo");

      var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
      transport.ConnectionString(builder.Configuration.GetConnectionString("rabbitMQconn"));
      transport.UseConventionalRoutingTopology(QueueType.Quorum);

      var routing = transport.Routing();
          routing.RouteToEndpoint(
      messageType: typeof(MakeTransfer),
      destination: "CustomerAccount");
      return endpointConfiguration;
  });

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


//call extention methods
builder.Services.AddDBContextService(builder.Configuration.GetConnectionString("myconn"));
builder.Services.AddDIServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(options => {
    options.AllowAnyOrigin();
    options.AllowAnyMethod();
    options.AllowAnyHeader();
});
app.UseAuthorization();

app.MapControllers();

app.Run();
