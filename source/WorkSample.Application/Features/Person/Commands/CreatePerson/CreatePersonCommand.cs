using MediatR;

namespace WorkSample.Application.Features.Person.Commands.CreatePerson;

/// <summary>
///     Command to create a person.
/// </summary>
public class CreatePersonCommand : IRequest<int>
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
