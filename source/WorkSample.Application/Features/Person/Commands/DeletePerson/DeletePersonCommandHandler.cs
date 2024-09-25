using MediatR;
using WorkSample.Application.Contracts.Logging;
using WorkSample.Application.Contracts.Persistence;
using WorkSample.Application.Exceptions;

namespace WorkSample.Application.Features.Person.Commands.DeletePerson;

/// <summary>
///     Handler to delete a person.
/// </summary>
public class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand, Unit>
{
    private readonly IPersonRepository _personRepository;
    private readonly IAppLogger<DeletePersonCommandHandler> _logger;

    /// <summary>
    ///     Initiates a new instance of <see cref="DeletePersonCommandHandler"/>.
    /// </summary>
    /// <param name="personRepository">The current instance of <see cref="IPersonRepository"/>.</param>
    /// <param name="logger">The current instance of <see cref="IAppLogger{T}"/>.</param>
    public DeletePersonCommandHandler(IPersonRepository personRepository, IAppLogger<DeletePersonCommandHandler> logger)
    {
        _personRepository = personRepository;
        _logger = logger;
    }

    /// <summary>
    ///     Handles the request.
    /// </summary>
    /// <param name="request">The request to be handled as <see cref="DeletePersonCommand"/>.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <exception cref="NotFoundException">Thrown if the person is not found.</exception>
    public async Task<Unit> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
    {
        var personToDelete = await _personRepository.GetByIdAsync(request.Id);

        if (personToDelete == null)
        {
            _logger.LogWarning("Person with id {0} not found", request.Id);
            throw new NotFoundException(nameof(Domain.Person), request.Id);
        }

        await _personRepository.DeleteAsync(personToDelete);

        return Unit.Value;
    }
}