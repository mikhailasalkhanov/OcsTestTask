using System.Net;
using System.Text.Json;
using Ordering.Api.Dto;

namespace Ordering.Api.Middleware;

public class UnhandledExceptionMiddleware
{

    private readonly RequestDelegate _next;
    private readonly ILogger<UnhandledExceptionMiddleware> _logger;
    private readonly IHostEnvironment _env;

    public UnhandledExceptionMiddleware(RequestDelegate next,
        ILogger<UnhandledExceptionMiddleware> logger, IHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "{EMessage}", e.Message);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            if (_env.IsDevelopment())
            {
                throw;
            }
            
            var response = new ErrorDto("Internal server error");
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var json = JsonSerializer.Serialize(response, options);
            await context.Response.WriteAsync(json);
        }
    }
}