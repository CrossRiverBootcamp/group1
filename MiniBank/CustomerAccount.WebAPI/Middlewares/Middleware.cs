using System.Net;
using ExtendedExceptions;



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
            if(httpContext.Response.StatusCode > 400 && httpContext.Response.StatusCode < 500)
            {
                throw new KeyNotFoundException("Not Fount");
            }
        }
        catch (Exception error)
        {
            var response = httpContext.Response;
            response.ContentType = "application/json";
            _logger.LogError(error, error.Message);

            switch (error)
            {
                //case CreateUserException ex:
                //    //CreateCustomerAccount failed
                //    await response.WriteAsync("Ooops... \n Create customerAccount failed!");
                //    response.StatusCode = (int)HttpStatusCode.ExpectationFailed;
                //    //need to return false
                //    break;
                case DBContextException ex:
                    //Other DBContext Exceptions
                    await response.WriteAsync("DBContext issue:" + ex.Message);
                    response.StatusCode = (int)HttpStatusCode.ExpectationFailed;
                    break;
                case ArgumentNullException ex:
                    await response.WriteAsync("the argument " + ex.Message + "is null!");
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case UnauthorizedAccessException ex:
                    await response.WriteAsync("" + ex.Message);
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case KeyNotFoundException ex:
                    await response.WriteAsync("key not found!");
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                default:
                    // unhandled error
                    await response.WriteAsync("unknown problem:(");
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
