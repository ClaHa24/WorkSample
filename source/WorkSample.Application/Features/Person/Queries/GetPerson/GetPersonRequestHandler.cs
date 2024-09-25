using AutoMapper;
using MediatR;
using WorkSample.Application.Contracts.Logging;
using WorkSample.Application.Contracts.Persistence;
using WorkSample.Application.Exceptions;

namespace WorkSample.Application.Features.Person.Queries.GetPerson;

/// <summary>
///     Handler to get person details.
/// </summary>
public class GetPersonRequestHandler : IRequestHandler<GetPersonQuery, PersonDto>
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;
    private readonly IAppLogger<GetPersonRequestHandler> _logger;

    /// <summary>
    ///     Initiates a new instance of <see cref="GetPersonRequestHandler"/>.
    /// </summary>
    /// <param name="mapper">The current instance of <see cref="IMapper"/>.</param>
    /// <param name="personRepository">The current instance of <see cref="IPersonRepository"/>.</param>
    /// <param name="logger">The current instance of <see cref="IAppLogger{T}"/>.</param>
    public GetPersonRequestHandler(IMapper mapper, IPersonRepository personRepository, IAppLogger<GetPersonRequestHandler> logger)
    {
        _personRepository = personRepository;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    ///     Handles the request.
    /// </summary>
    /// <param name="request">The request to be handled as <see cref="GetPersonQuery"/>.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>A list of persons as <see cref="List{PersonDetailsDto}"/>.</returns>
    public async Task<PersonDto> Handle(GetPersonQuery request, CancellationToken cancellationToken)
    {
        var person = await _personRepository.GetByIdAsync(request.Id);
        if (person == null)
        {
            _logger.LogWarning("Person with id {0} not found", request.Id);
            throw new NotFoundException(nameof(Person), request.Id);
        }

        return _mapper.Map<PersonDto>(person);
    }
}
