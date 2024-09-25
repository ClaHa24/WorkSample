using WorkSample.Domain.Common;

namespace WorkSample.Domain;

/// <summary>
///     Entity representing a person.
/// </summary>
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
