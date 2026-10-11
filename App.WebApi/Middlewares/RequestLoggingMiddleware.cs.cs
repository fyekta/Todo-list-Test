namespace WebApi.Middlewares;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public RequestLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }
   
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Request failed: {ex.Message}");
            throw;
        }
        finally
        {
            Console.WriteLine(
                $"IP: {context.Connection.RemoteIpAddress} | " +
                $"API: {context.Request.Scheme}://{context.Request.Host}{context.Request.Path} | " +
                $"Endpoint: {context.Request.Path} | " +
                $"Method: {context.Request.Method} | " +
                $"Status Code: {context.Response.StatusCode}");
        }
    }

}

  
