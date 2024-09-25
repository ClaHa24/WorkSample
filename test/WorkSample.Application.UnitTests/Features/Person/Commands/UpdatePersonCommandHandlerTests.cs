using AutoMapper;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using WorkSample.Application.Contracts.Logging;
using WorkSample.Application.Contracts.Persistence;
using WorkSample.Application.Exceptions;
using WorkSample.Application.Features.Person.Commands.UpdatePerson;
using WorkSample.Application.MappingProfiles;

namespace WorkSample.Application.UnitTests.Features.Person.Commands;

public class UpdatePersonCommandHandlerTests
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<UpdatePersonCommandHandler> _logger;

    public UpdatePersonCommandHandlerTests()
    {
        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<PersonProfile>();
        });

        _mapper = mapperConfig.CreateMapper();
        _logger = Substitute.For<IAppLogger<UpdatePersonCommandHandler>>();
    }

    [Fact]
    public async Task Handle_ValidPerson()
    {
        // Arrange
        var command = new UpdatePersonCommand()
        {
            Id = 1,
            Name = "Donald",
            Surname = "Duck"
        };
        var repositoryMock = Substitute.For<IPersonRepository>();
        repositoryMock.GetByIdAsync(command.Id).Returns(new Domain.Person());
        var handler = new UpdatePersonCommandHandler(_mapper, repositoryMock, _logger);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        await repositoryMock.Received(1)
            .UpdateAsync(Arg.Is<Domain.Person>(person =>
                person.Name.Equals(command.Name) &&
                person.Surname.Equals(command.Surname) &&
                person.Id == command.Id));
    }

    [Fact]
    public async Task Handle_InvalidPerson_NoSurname()
    {
        // Arrange
        var command = new UpdatePersonCommand()
        {
            Id = 1,
            Name = "Donald",
            Surname = string.Empty
        };
        var repositoryMock = Substitute.For<IPersonRepository>();
        repositoryMock.GetByIdAsync(command.Id).Returns(new Domain.Person());
        var handler = new UpdatePersonCommandHandler(_mapper, repositoryMock, _logger);

        // Act
        var action = async () => await handler.Handle(command, CancellationToken.None);

        // Assert
        await action.Should().ThrowExactlyAsync<BadRequestException>();
        await repositoryMock.DidNotReceive()
            .UpdateAsync(Arg.Any<Domain.Person>());
    }

    [Fact]
    public async Task Handle_InvalidPerson_NoName()
    {
        // Arrange
        var command = new UpdatePersonCommand()
        {
            Id = 1,
            Name = string.Empty,
            Surname = "Duck"
        };
        var repositoryMock = Substitute.For<IPersonRepository>();
        repositoryMock.GetByIdAsync(command.Id).Returns(new Domain.Person());
        var handler = new UpdatePersonCommandHandler(_mapper, repositoryMock, _logger);

        // Act
        var action = async () => await handler.Handle(command, CancellationToken.None);

        // Assert
        await action.Should().ThrowExactlyAsync<BadRequestException>();
        await repositoryMock.DidNotReceive()
            .UpdateAsync(Arg.Any<Domain.Person>());
    }

    [Fact]
    public async Task Handle_InvalidPerson_WrongId()
    {
        // Arrange
        var command = new UpdatePersonCommand()
        {
            Id = 1,
            Name = "Donald",
            Surname = "Duck"
        };
        var repositoryMock = Substitute.For<IPersonRepository>();
        repositoryMock.GetByIdAsync(command.Id).ReturnsNull();
        var handler = new UpdatePersonCommandHandler(_mapper, repositoryMock, _logger);

        // Act
        var action = async () => await handler.Handle(command, CancellationToken.None);

        // Assert
        await action.Should().ThrowExactlyAsync<BadRequestException>();
        await repositoryMock.DidNotReceive()
            .UpdateAsync(Arg.Any<Domain.Person>());
    }
}
