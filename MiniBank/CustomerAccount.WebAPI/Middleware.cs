using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;


namespace CustomerAccount.WebAPI;

// You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
public class Middleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<Middleware> _logger;
  
    public Middleware(RequestDelegate next, ILogger<Middleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {

        await _next(httpContext);
        var exception = httpContext.Features
       .Get<IExceptionHandlerPathFeature>().Error;

       _logger.LogError(exception,$"oh no! {exception.Message}");
        
    }

}

// Extension method used to add the middleware to the HTTP request pipeline.
public static class MiddlewareExtensions
{
    public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<Middleware>();
    }
}
