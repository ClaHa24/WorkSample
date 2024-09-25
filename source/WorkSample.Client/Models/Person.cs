namespace WorkSample.Client.Models;

/// <summary>
///     Model representing a person.
/// </summary>
public class Person
{
    /// <summary>
    ///     ID of the entity. Needs to be unique.
    /// </summary>
    public int? Id { get; set; }

    /// <summary>
    ///     Name of the person.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    ///     Surname of the person.
    /// </summary>
    public string Surname { get; set; } = string.Empty;
}

