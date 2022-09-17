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
                case ArgumentNullException ex:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    await response.WriteAsync("" + ex.Message);
                    break;
                case UnauthorizedAccessException ex:
                    response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    await response.WriteAsync("Ooops your email or password isn't currect.. " + ex.Message);
                    break;
                case KeyNotFoundException ex:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    await response.WriteAsync("key not found!");                   
                    break;
                case EmailInUseException ex:
                    response.StatusCode = (int)HttpStatusCode.NotAcceptable;
                    await response.WriteAsync("Email address in use");
                    break;
                case VerificationCodeExpiredException ex:
                    response.StatusCode = (int)HttpStatusCode.Forbidden;
                    await response.WriteAsync("Verification code expired..:(");
                    break;
                case TooManyRetriesException ex:
                    response.StatusCode = (int)HttpStatusCode.TooManyRequests;
                    await response.WriteAsync("too many retries requests..:(");
                    break;
                case DBContextException ex:
                    //Other DBContext Exceptions
                    response.StatusCode = (int)HttpStatusCode.ExpectationFailed;
                    await response.WriteAsync("DBContext issue:" + ex.Message);
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
