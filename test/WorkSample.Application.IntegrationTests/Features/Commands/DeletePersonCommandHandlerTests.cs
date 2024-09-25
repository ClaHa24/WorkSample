using FluentAssertions;
using NSubstitute;
using WorkSample.Application.Contracts.Logging;
using WorkSample.Application.Features.Person.Commands.DeletePerson;
using WorkSample.Application.IntegrationTests.Repositories;

namespace WorkSample.Application.IntegrationTests.Features.Commands;

public class DeletePersonCommandHandlerTests
{
    private readonly IAppLogger<DeletePersonCommandHandler> _logger;
    private readonly PersonInMemoryRepository _personInMemoryRepository;

    public DeletePersonCommandHandlerTests()
    {
        _logger = Substitute.For<IAppLogger<DeletePersonCommandHandler>>();
        _personInMemoryRepository = new PersonInMemoryRepository();
    }

    [Fact]
    public async Task Handle_CreateNewEntry()
    {
        // Arrange
        var command = new DeletePersonCommand();
        var handler = new DeletePersonCommandHandler(_personInMemoryRepository.PersonRepository, _logger);
        command.Id = await _personInMemoryRepository.PersonRepository.CreateAsync(new Domain.Person { Name = "Daisy", Surname = "Duck" });

        _personInMemoryRepository.DetachAllEntities();

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        var createdEntry = await _personInMemoryRepository.PersonRepository.GetByIdAsync(command.Id);
        createdEntry.Should().BeNull();
    }
}
