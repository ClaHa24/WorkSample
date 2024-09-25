using AutoMapper;
using MediatR;
using WorkSample.Application.Contracts.Logging;
using WorkSample.Application.Contracts.Persistence;
using WorkSample.Application.Exceptions;

namespace WorkSample.Application.Features.Person.Commands.UpdatePerson;

/// <summary>
///     Handler to update a person.
/// </summary>
public class UpdatePersonCommandHandler : IRequestHandler<UpdatePersonCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly IPersonRepository _personRepository;
    private readonly IAppLogger<UpdatePersonCommandHandler> _logger;

    /// <summary>
    ///     Initiates a new instance of <see cref="UpdatePersonCommandHandler"/>.
    /// </summary>
    /// <param name="mapper">The current instance of <see cref="IMapper"/>.</param>
    /// <param name="personRepository">The current instance of <see cref="IPersonRepository"/>.</param>
    /// <param name="logger">The current instance of <see cref="IAppLogger{T}"/>.</param>
    public UpdatePersonCommandHandler(IMapper mapper, IPersonRepository personRepository, IAppLogger<UpdatePersonCommandHandler> logger)
    {
        _mapper = mapper;
        _personRepository = personRepository;
        _logger = logger;
    }

    /// <summary>
    ///     Handles the request.
    /// </summary>
    /// <param name="request">The request to be handled as <see cref="UpdatePersonCommand"/>.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <exception cref="BadRequestException">Thrown if the given command is invalid.</exception>
    public async Task<Unit> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdatePersonCommandValidator(_personRepository);
        var validationResult = await validator.ValidateAsync(request);

        if (validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation errors in update request for {0} - {1}", nameof(Domain.Person), request.Id);
            throw new BadRequestException("Invalid person", validationResult);
        }

        var personToUpdate = _mapper.Map<Domain.Person>(request);

        await _personRepository.UpdateAsync(personToUpdate);

        return Unit.Value;
    }
}
