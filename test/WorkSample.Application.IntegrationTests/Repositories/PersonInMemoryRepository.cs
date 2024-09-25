using Microsoft.EntityFrameworkCore;
using NSubstitute;
using WorkSample.Application.Contracts.Logging;
using WorkSample.Domain;
using WorkSample.Persistence.DatabaseContext;
using WorkSample.Persistence.Repositories;

namespace WorkSample.Application.IntegrationTests.Repositories;

public class PersonInMemoryRepository
{
    private readonly WorkSampleDatabaseContext _workSampleDatabaseContext;

    public PersonRepository PersonRepository { get; }

    public PersonInMemoryRepository()
    {
        var dbOptions = new DbContextOptionsBuilder<WorkSampleDatabaseContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

        _workSampleDatabaseContext = new WorkSampleDatabaseContext(dbOptions);

        var logMock = Substitute.For<IAppLogger<GenericRepository<Person>>>();

        PersonRepository = new PersonRepository(_workSampleDatabaseContext, logMock);
    }

    public void DetachAllEntities()
    {
        var undetachedEntriesCopy = _workSampleDatabaseContext.ChangeTracker.Entries()
            .Where(e => e.State != EntityState.Detached)
            .ToList();

        foreach (var entry in undetachedEntriesCopy)
            entry.State = EntityState.Detached;
    }
}
