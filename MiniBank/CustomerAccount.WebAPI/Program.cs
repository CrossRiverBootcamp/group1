using Microsoft.EntityFrameworkCore;
using CustomerAccount.DAL.EF;
using CustomerAccount.BL.Interfaces;
using CustomerAccount.BL;
using CustomerAccount.DAL;
using Microsoft.AspNetCore.Diagnostics;
using CustomerAccount.WebAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContextFactory<CustomerAccountDBContext>(item =>
    item.UseSqlServer(builder.Configuration.GetConnectionString("myconn"))); 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// add DI services
builder.Services.AddScoped<IAccountBL, AccountBL>();
builder.Services.AddScoped<ILoginBL, LoginBL>();

builder.Services.AddScoped<IStorage, Storage>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.ConfigureCustomExceptionMiddleware();

app.MapControllers();

app.Run();
