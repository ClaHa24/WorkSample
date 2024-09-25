namespace WorkSample.Application.Features.Person.Queries.GetPerson;

/// <summary>
///     Data transfer object for person details.
/// </summary>
public class PersonDto
{
    /// <summary>
    ///     ID of the person.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    ///     Name of the person.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    ///     Surname of the person.
    /// </summary>
    public string Surname { get; set; } = string.Empty;
}
