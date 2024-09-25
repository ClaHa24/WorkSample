using FluentValidation.Results;

namespace WorkSample.Application.Exceptions;

/// <summary>
///     Exception thrown if a request is bad.
/// </summary>
public class BadRequestException : Exception
{
    /// <summary>
    ///     Initiates a new instance of <see cref="BadRequestException"/>.
    /// </summary>
    /// <param name="message">Exception message.</param>
    public BadRequestException(string message) : base(message)
    {

    }

    /// <summary>
    ///     Initiates a new instance of <see cref="BadRequestException"/>.
    /// </summary>
    /// <param name="message">Exception message.</param>
    /// <param name="validationResult">Result of the validation as <see cref="ValidationResult"/>.</param>
    public BadRequestException(string message, ValidationResult validationResult) : base(message)
    {
        ValidationErrors = validationResult.ToDictionary();
    }

    /// <summary>
    ///     The validation errors.
    /// </summary>
    public IDictionary<string, string[]> ValidationErrors { get; set; } = new Dictionary<string, string[]>();
}