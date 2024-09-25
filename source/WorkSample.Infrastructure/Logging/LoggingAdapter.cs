using Microsoft.Extensions.Logging;
using WorkSample.Application.Contracts.Logging;

namespace WorkSample.Infrastructure.Logging;

public class LoggingAdapter<T> : IAppLogger<T>
{
    private readonly ILogger<T> _logger;

    /// <summary>
    ///     Initiates a new instance of <see cref="LoggingAdapter{T}"/>.
    /// </summary>
    /// <param name="loggerFactory">The current instance of <see cref="ILoggerFactory"/>.</param>
    public LoggingAdapter(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<T>();
    }

    /// <inheritdoc cref="IAppLogger{T}"/>
    public void LogInformation(string message, params object[] args)
    {
        _logger.LogInformation(message, args);
    }

    /// <inheritdoc cref="IAppLogger{T}"/>
    public void LogWarning(string message, params object[] args)
    {
        _logger.LogWarning(message, args);
    }
}