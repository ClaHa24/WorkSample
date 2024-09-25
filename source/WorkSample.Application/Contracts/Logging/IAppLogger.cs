namespace WorkSample.Application.Contracts.Logging;

/// <summary>
///     Contains methods for logging.
/// </summary>
/// <typeparam name="T">Type of the class to log for.</typeparam>
public interface IAppLogger<T>
{
    /// <summary>
    ///     Logs an information.
    /// </summary>
    /// <param name="message">The message to log.</param>
    /// <param name="args">The arguments to use in string.Format().</param>
    void LogInformation(string message, params object[] args);

    /// <summary>
    ///     Logs a warning.
    /// </summary>
    /// <param name="message">The message to log.</param>
    /// <param name="args">The arguments to use in string.Format().</param>
    void LogWarning(string message, params object[] args);
}
