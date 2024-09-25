using Microsoft.EntityFrameworkCore;
using WorkSample.Application.Contracts.Logging;
using WorkSample.Application.Contracts.Persistence;
using WorkSample.Domain;
using WorkSample.Persistence.DatabaseContext;

namespace WorkSample.Persistence.Repositories;

/// <summary>
///     Repository for entry type <see cref="Person"/>.
/// </summary>
public class PersonRepository : GenericRepository<Person>, IPersonRepository
{
    /// <summary>
    ///     Initiates a new instance of <see cref="PersonRepository"/>.
    /// </summary>
    /// <param name="context">The database context as <see cref="WorkSampleDatabaseContext"/>.</param>
    /// <param name="logger">The current instance of <see cref="IAppLogger{T}"/>.</param>
    public PersonRepository(WorkSampleDatabaseContext context, IAppLogger<GenericRepository<Person>> logger) : base(context, logger)
    {
    }

    /// <inheritdoc cref="IPersonRepository"/>
    public async Task<bool> IsPersonUniqueAsync(string name, string surname)
    {
        _logger.LogInformation("Checking if person '{0},{1}' is unique.", name, surname);

        bool isPersonThere = await _context.Persons.AnyAsync(person =>
            person.Name.Equals(name) &&
            person.Surname.Equals(surname));

        return !isPersonThere;
    }
}
