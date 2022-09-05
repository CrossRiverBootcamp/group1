using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CustomerAccount.WebAPI.Middlewares;
using ExtendedExceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;


namespace CustomerAccount.WebAPI.Middlewares;

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
        try
        {
            await _next(httpContext);
            if (httpContext.Response.StatusCode > 400 && httpContext.Response.StatusCode < 500)
            {
                throw new KeyNotFoundException("Not Found");
            }​
        }
        catch (Exception error)
        {
            var response = httpContext.Response;
            response.ContentType = "application/json";
            _logger.Log(LogLevel.Error, error.Message);
            switch (error)
            {
                case CreateUserException ex:
                    //CreateCustomerAccount failed
                    await response.WriteAsync("Ooops... \n Create customerAccount failed!");
                    response.StatusCode = (int)HttpStatusCode.ExpectationFailed;
                    //need to return false
                    break;
                case DBContextException ex:
                    //Other DBContext Exceptions
                    await response.WriteAsync("Ooops... \n the argument {e.Message} is null!");
                    response.StatusCode = (int)HttpStatusCode.ExpectationFailed;
                    break;
                case ArgumentNullException ex:
                    await response.WriteAsync("Ooops... \n the argument {e.Message} is null!");
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case KeyNotFoundException ex:
                    await response.WriteAsync("Ooops... \n page not found!");
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                default:
                    // unhandled error
                    await response.WriteAsync("Ooops... \n unknown problem:(");
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }
        }
    }

}
public static class MiddlewareExtensions
{
    public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<Middleware>();
    }
}
