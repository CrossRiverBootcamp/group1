using Microsoft.EntityFrameworkCore;
using CustomerAccount.DAL.EF;
using CustomerAccount.BL.Interfaces;
using CustomerAccount.BL;
using CustomerAccount.DAL;
using Microsoft.AspNetCore.Diagnostics;
using CustomerAccount.WebAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);

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

app.ConfigureCustomExceptionMiddleware();

app.MapControllers();

app.Run();
