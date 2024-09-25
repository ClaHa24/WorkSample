using AutoMapper;
using MediatR;
using WorkSample.Application.Contracts.Persistence;

namespace WorkSample.Application.Features.Person.Queries.GetPersonList;

/// <summary>
///     Handler to get list of persons.
/// </summary>
public class GetPersonListRequestHandler : IRequestHandler<GetPersonListQuery, List<PersonListDto>>
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    /// <summary>
    ///     Initiates a new instance of <see cref="GetPersonListRequestHandler"/>.
    /// </summary>
    /// <param name="mapper">The current instance of <see cref="IMapper"/>.</param>
    /// <param name="personRepository">The current instance of <see cref="IPersonRepository"/>.</param>
    public GetPersonListRequestHandler(IMapper mapper, IPersonRepository personRepository)
    {
        _personRepository = personRepository;
        _mapper = mapper;
    }

    /// <summary>
    ///     Handles the request.
    /// </summary>
    /// <param name="request">The request to be handled as <see cref="GetPersonListQuery"/>.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>A list of persons as <see cref="List{PersonListDto}"/>.</returns>
    public async Task<List<PersonListDto>> Handle(GetPersonListQuery request, CancellationToken cancellationToken)
    {
        var persons = await _personRepository.GetAsync();
        return _mapper.Map<List<PersonListDto>>(persons);
    }
}
