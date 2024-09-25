using MediatR;

namespace WorkSample.Application.Features.Person.Commands.UpdatePerson;

/// <summary>
///     Command to update a person.
/// </summary>
public class UpdatePersonCommand : IRequest<Unit>
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
