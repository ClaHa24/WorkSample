using MediatR;

namespace WorkSample.Application.Features.Person.Queries.GetPersonList;

/// <summary>
///     Query to get list of persons.
/// </summary>
public class GetPersonListQuery : IRequest<List<PersonListDto>>
{
}
