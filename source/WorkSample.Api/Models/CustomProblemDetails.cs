using Microsoft.AspNetCore.Mvc;

namespace WorkSample.Api.Models;

/// <summary>
///     Custom problem details used in case of exceptions.
/// </summary>
public class CustomProblemDetails : ProblemDetails
{
    /// <summary>
    ///     Errors that occured.
    /// </summary>
    public IDictionary<string, string[]> Errors { get; set; } = new Dictionary<string, string[]>();
}