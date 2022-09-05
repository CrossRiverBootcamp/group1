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

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());




//Extention method to add DBContext 
builder.Services.AddServices(builder.Configuration.GetConnectionString("myconn"));
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
