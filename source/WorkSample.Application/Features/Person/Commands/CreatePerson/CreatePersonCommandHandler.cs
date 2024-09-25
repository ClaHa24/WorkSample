using AutoMapper;
using MediatR;
using WorkSample.Application.Contracts.Logging;
using WorkSample.Application.Contracts.Persistence;
using WorkSample.Application.Exceptions;

namespace WorkSample.Application.Features.Person.Commands.CreatePerson;

/// <summary>
///     Handler to create a person.
/// </summary>
public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, int>
{
    private readonly IMapper _mapper;
    private readonly IPersonRepository _personRepository;
    private readonly IAppLogger<CreatePersonCommandHandler> _logger;

    /// <summary>
    ///     Initiates a new instance of <see cref="GetPersonRequestHandler"/>.
    /// </summary>
    /// <param name="mapper">The current instance of <see cref="IMapper"/>.</param>
    /// <param name="personRepository">The current instance of <see cref="IPersonRepository"/>.</param>
    /// <param name="logger">The current instance of <see cref="IAppLogger{T}"/>.</param>
    public CreatePersonCommandHandler(IMapper mapper, IPersonRepository personRepository, IAppLogger<CreatePersonCommandHandler> logger)
    {
        _mapper = mapper;
        _personRepository = personRepository;
        _logger = logger;
    }

    /// <summary>
    ///     Handles the request.
    /// </summary>
    /// <param name="request">The request to be handled as <see cref="GetPersonQuery"/>.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>The ID of the new person.</returns>
    /// <exception cref="BadRequestException">Thrown if the given command is invalid.</exception>
    public async Task<int> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreatePersonCommandValidator(_personRepository);
        var validationResult = await validator.ValidateAsync(request);

        if (validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation errors in create request for {0} - '{1},{2}'", nameof(Domain.Person), request.Name, request.Surname);
            throw new BadRequestException("Invalid person", validationResult);
        }

        var personToCreate = _mapper.Map<Domain.Person>(request);

        await _personRepository.CreateAsync(personToCreate);

        return personToCreate.Id;
    }
}
