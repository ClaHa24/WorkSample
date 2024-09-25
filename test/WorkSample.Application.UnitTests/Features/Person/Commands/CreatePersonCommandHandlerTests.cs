using AutoMapper;
using FluentAssertions;
using NSubstitute;
using WorkSample.Application.Contracts.Logging;
using WorkSample.Application.Contracts.Persistence;
using WorkSample.Application.Exceptions;
using WorkSample.Application.Features.Person.Commands.CreatePerson;
using WorkSample.Application.MappingProfiles;

namespace WorkSample.Application.UnitTests.Features.Person.Commands;

public class CreatePersonCommandHandlerTests
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<CreatePersonCommandHandler> _logger;

    public CreatePersonCommandHandlerTests()
    {
        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<PersonProfile>();
        });

        _mapper = mapperConfig.CreateMapper();
        _logger = Substitute.For<IAppLogger<CreatePersonCommandHandler>>();
    }

    [Fact]
    public async Task Handle_ValidPerson()
    {
        // Arrange
        var command = new CreatePersonCommand()
        {
            Name = "Donald",
            Surname = "Duck"
        };
        var repositoryMock = Substitute.For<IPersonRepository>();
        repositoryMock.CreateAsync(Arg.Any<Domain.Person>()).Returns(1);
        repositoryMock.IsPersonUniqueAsync(command.Name, command.Surname).Returns(true);
        var handler = new CreatePersonCommandHandler(_mapper, repositoryMock, _logger);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        await repositoryMock.Received(1)
            .CreateAsync(Arg.Is<Domain.Person>(person => person.Name.Equals(command.Name) && person.Surname.Equals(command.Surname)));
    }

    [Fact]
    public async Task Handle_InvalidPerson_NoSurname()
    {
        // Arrange
        var command = new CreatePersonCommand()
        {
            Name = "Donald",
            Surname = string.Empty
        };
        var repositoryMock = Substitute.For<IPersonRepository>();
        repositoryMock.CreateAsync(Arg.Any<Domain.Person>()).Returns(1);
        repositoryMock.IsPersonUniqueAsync(command.Name, command.Surname).Returns(true);
        var handler = new CreatePersonCommandHandler(_mapper, repositoryMock, _logger);

        // Act
        var action = async () => await handler.Handle(command, CancellationToken.None);

        // Assert
        await action.Should().ThrowExactlyAsync<BadRequestException>();
        await repositoryMock.DidNotReceive()
            .CreateAsync(Arg.Any<Domain.Person>());
    }

    [Fact]
    public async Task Handle_InvalidPerson_NoName()
    {
        // Arrange
        var command = new CreatePersonCommand()
        {
            Name = string.Empty,
            Surname = "Duck"
        };
        var repositoryMock = Substitute.For<IPersonRepository>();
        repositoryMock.CreateAsync(Arg.Any<Domain.Person>()).Returns(1);
        repositoryMock.IsPersonUniqueAsync(command.Name, command.Surname).Returns(true);
        var handler = new CreatePersonCommandHandler(_mapper, repositoryMock, _logger);

        // Act
        var action = async () => await handler.Handle(command, CancellationToken.None);

        // Assert
        await action.Should().ThrowExactlyAsync<BadRequestException>();
        await repositoryMock.DidNotReceive()
            .CreateAsync(Arg.Any<Domain.Person>());
    }

    [Fact]
    public async Task Handle_InvalidPerson_NotUnique()
    {
        // Arrange
        var command = new CreatePersonCommand()
        {
            Name = "Donald",
            Surname = "Duck"
        };
        var repositoryMock = Substitute.For<IPersonRepository>();
        repositoryMock.CreateAsync(Arg.Any<Domain.Person>()).Returns(1);
        repositoryMock.IsPersonUniqueAsync(command.Name, command.Surname).Returns(false);
        var handler = new CreatePersonCommandHandler(_mapper, repositoryMock, _logger);

        // Act
        var action = async () => await handler.Handle(command, CancellationToken.None);

        // Assert
        await action.Should().ThrowExactlyAsync<BadRequestException>();
        await repositoryMock.DidNotReceive()
            .CreateAsync(Arg.Any<Domain.Person>());
    }
}
