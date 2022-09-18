using Transaction.BL;
using NServiceBus;
using Microsoft.Data.SqlClient;
using Transaction.Messeges;
using Microsoft.OpenApi.Models;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using CustomerAccount.BL.Options;

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

//add options
builder.Services.Configure<EmailOptions>(builder.Configuration.GetSection(
        nameof(EmailOptions)));

var key = Encoding.UTF8.GetBytes(builder.Configuration.GetSection("key").Value);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{

    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
    };
});
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "miniBank", Version = "v1" });
    // To Enable authorization using Swagger (JWT)
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}
                    }
                });
});

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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
