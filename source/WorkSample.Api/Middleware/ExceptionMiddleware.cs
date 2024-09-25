using System.Net;
using System.Text.Json;
using WorkSample.Api.Models;
using WorkSample.Application.Exceptions;

namespace WorkSample.Api.Middleware;

/// <summary>
///     Middleware used to handle exceptions.
/// </summary>
public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    /// <summary>
    ///     Initiates a new instance of <see cref="ExceptionMiddleware"/>.
    /// </summary>
    /// <param name="next">The next <see cref="RequestDelegate"/>.</param>
    /// <param name="logger">The current instance of <see cref="ILogger{T}"/>.</param>
    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    /// <summary>
    ///     Invokes the middleware.
    /// </summary>
    /// <param name="httpContext">The current <see cref="HttpContext"/>.</param>
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    /// <summary>
    ///     Handles the exception.
    /// </summary>
    /// <param name="httpContext">The current <see cref="HttpContext"/>.</param>
    /// <param name="ex">The exception to handle.</param>
    private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
    {
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
        CustomProblemDetails problem = new();

        switch (ex)
        {
            case BadRequestException badRequestException:
                statusCode = HttpStatusCode.BadRequest;
                problem = new CustomProblemDetails
                {
                    Title = badRequestException.Message,
                    Status = (int)statusCode,
                    Detail = badRequestException.InnerException?.Message,
                    Type = nameof(BadRequestException),
                    Errors = badRequestException.ValidationErrors
                };
                break;
            case NotFoundException NotFound:
                statusCode = HttpStatusCode.NotFound;
                problem = new CustomProblemDetails
                {
                    Title = NotFound.Message,
                    Status = (int)statusCode,
                    Type = nameof(NotFoundException),
                    Detail = NotFound.InnerException?.Message,
                };
                break;
            default:
                problem = new CustomProblemDetails
                {
                    Title = ex.Message,
                    Status = (int)statusCode,
                    Type = nameof(HttpStatusCode.InternalServerError),
                    Detail = ex.StackTrace,
                };
                break;
        }

        httpContext.Response.StatusCode = (int)statusCode;
        var logMessage = JsonSerializer.Serialize(problem);
        _logger.LogError(logMessage);
        await httpContext.Response.WriteAsJsonAsync(problem);
    }
}