using AutoMapper;
using FluentAssertions;
using NSubstitute;
using WorkSample.Application.Contracts.Logging;
using WorkSample.Application.Features.Person.Commands.CreatePerson;
using WorkSample.Application.IntegrationTests.Repositories;
using WorkSample.Application.MappingProfiles;

namespace WorkSample.Application.IntegrationTests.Features.Commands;

public class CreatePersonCommandHandlerTests
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<CreatePersonCommandHandler> _logger;
    private readonly PersonInMemoryRepository _personInMemoryRepository;

    public CreatePersonCommandHandlerTests()
    {
        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<PersonProfile>();
        });

        _mapper = mapperConfig.CreateMapper();
        _logger = Substitute.For<IAppLogger<CreatePersonCommandHandler>>();
        _personInMemoryRepository = new PersonInMemoryRepository();
    }

    [Fact]
    public async Task Handle_CreateNewEntry()
    {
        // Arrange
        var command = new CreatePersonCommand()
        {
            Name = "Donald",
            Surname = "Duck"
        };
        var handler = new CreatePersonCommandHandler(_mapper, _personInMemoryRepository.PersonRepository, _logger);

        // Act
        var id = await handler.Handle(command, CancellationToken.None);

        // Assert
        var createdEntry = await _personInMemoryRepository.PersonRepository.GetByIdAsync(id);
        createdEntry.Should().NotBeNull();
        createdEntry.Name.Should().Be(command.Name);
        createdEntry.Surname.Should().Be(command.Surname);
    }
}
