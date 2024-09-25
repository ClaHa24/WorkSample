using MediatR;

namespace WorkSample.Application.Features.Person.Queries.GetPerson;

/// <summary>
///     Query to get person with details.
/// </summary>
public class GetPersonQuery : IRequest<PersonDto>
{
    /// <summary>
    ///     ID of the person to get.
    /// </summary>
    public int Id { get; set; }
}
