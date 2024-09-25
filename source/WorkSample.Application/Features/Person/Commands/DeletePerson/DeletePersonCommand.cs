using MediatR;

namespace WorkSample.Application.Features.Person.Commands.DeletePerson;

/// <summary>
///     Command to delete a person.
/// </summary>
public class DeletePersonCommand : IRequest<Unit>
{
    /// <summary>
    ///     ID of person to be deleted.
    /// </summary>
    public int Id { get; set; }
}
