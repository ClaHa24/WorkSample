using AutoMapper;
using FluentAssertions;
using NSubstitute;
using WorkSample.Application.Contracts.Logging;
using WorkSample.Application.Features.Person.Commands.UpdatePerson;
using WorkSample.Application.IntegrationTests.Repositories;
using WorkSample.Application.MappingProfiles;

namespace WorkSample.Application.IntegrationTests.Features.Commands;
public class UpdatePersonCommandHandlerTests
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<UpdatePersonCommandHandler> _logger;
    private readonly PersonInMemoryRepository _personInMemoryRepository;

    public UpdatePersonCommandHandlerTests()
    {
        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<PersonProfile>();
        });

        _mapper = mapperConfig.CreateMapper();
        _logger = Substitute.For<IAppLogger<UpdatePersonCommandHandler>>();
        _personInMemoryRepository = new PersonInMemoryRepository();
    }

    [Fact]
    public async Task Handle_CreateNewEntry()
    {
        // Arrange
        var command = new UpdatePersonCommand()
        {
            Name = "Donald",
            Surname = "Duck"
        };
        var handler = new UpdatePersonCommandHandler(_mapper, _personInMemoryRepository.PersonRepository, _logger);
        var personToUpdate = new Domain.Person
        {
            Name = "Mickey",
            Surname = "Mouse"
        };
        command.Id = await _personInMemoryRepository.PersonRepository.CreateAsync(personToUpdate);

        _personInMemoryRepository.DetachAllEntities();

        // Act
        var id = await handler.Handle(command, CancellationToken.None);

        // Assert
        var createdEntry = await _personInMemoryRepository.PersonRepository.GetByIdAsync(command.Id);
        createdEntry.Should().NotBeNull();
        createdEntry?.Name.Should().Be(command.Name);
        createdEntry?.Surname.Should().Be(command.Surname);
    }
}
