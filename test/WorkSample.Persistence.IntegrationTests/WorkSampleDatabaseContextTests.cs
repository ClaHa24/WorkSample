using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WorkSample.Domain;
using WorkSample.Persistence.DatabaseContext;

namespace WorkSample.Persistence.IntegrationTests;

public class WorkSampleDatabaseContextTests
{
    private WorkSampleDatabaseContext _workSampleDatabaseContext;

    public WorkSampleDatabaseContextTests()
    {
        var dbOptions = new DbContextOptionsBuilder<WorkSampleDatabaseContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

        _workSampleDatabaseContext = new WorkSampleDatabaseContext(dbOptions);
    }

    [Fact]
    public async void SaveChangesAsync_SetDateCreatedValue()
    {
        // Arrange
        var person = new Person
        {
            Id = 1,
            Name = "Donald",
            Surname = "Duck"
        };

        // Act
        await _workSampleDatabaseContext.Persons.AddAsync(person);
        await _workSampleDatabaseContext.SaveChangesAsync();

        // Assert
        person.DateCreated.Should().NotBeNull();
    }

    [Fact]
    public async void SaveChangesAsync_SetDateModifiedValue()
    {
        // Arrange
        var person = new Person
        {
            Id = 1,
            Name = "Donald",
            Surname = "Duck"
        };

        // Act
        await _workSampleDatabaseContext.Persons.AddAsync(person);
        await _workSampleDatabaseContext.SaveChangesAsync();

        // Assert
        person.DateModified.Should().NotBeNull();
    }
}