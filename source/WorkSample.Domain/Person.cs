using System.Diagnostics.CodeAnalysis;
using WorkSample.Domain.Common;

namespace WorkSample.Domain;

/// <summary>
///     Entity representing a person.
/// </summary>
[ExcludeFromCodeCoverage]
public class Person : BaseEntity
{
    /// <summary>
    ///     Name of the person.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    ///     Surname of the person.
    /// </summary>
    public string Surname { get; set; } = string.Empty;
}
