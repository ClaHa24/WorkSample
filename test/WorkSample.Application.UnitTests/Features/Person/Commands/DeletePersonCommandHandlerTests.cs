using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using WorkSample.Application.Contracts.Logging;
using WorkSample.Application.Contracts.Persistence;
using WorkSample.Application.Exceptions;
using WorkSample.Application.Features.Person.Commands.DeletePerson;

namespace WorkSample.Application.UnitTests.Features.Person.Commands;

public class DeletePersonCommandHandlerTests
{
    private readonly IAppLogger<DeletePersonCommandHandler> _logger;

    public DeletePersonCommandHandlerTests()
    {
        _logger = Substitute.For<IAppLogger<DeletePersonCommandHandler>>();
    }

    [Fact]
    public async Task Handle_PersonFound()
    {
        // Arrange
        var command = new DeletePersonCommand { Id = 1 };
        var repositoryMock = Substitute.For<IPersonRepository>();
        repositoryMock.GetByIdAsync(command.Id).Returns(new Domain.Person());
        var handler = new DeletePersonCommandHandler(repositoryMock, _logger);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        await repositoryMock.Received(1)
            .DeleteAsync(Arg.Any<Domain.Person>());
    }

    [Fact]
    public async Task Handle_PersonNotFound()
    {
        // Arrange
        var command = new DeletePersonCommand { Id = 1 };
        var repositoryMock = Substitute.For<IPersonRepository>();
        repositoryMock.GetByIdAsync(command.Id).ReturnsNull();
        var handler = new DeletePersonCommandHandler(repositoryMock, _logger);

        // Act
        var action = async () => await handler.Handle(command, CancellationToken.None);

        // Assert
        await action.Should().ThrowExactlyAsync<NotFoundException>();
        await repositoryMock.DidNotReceive()
            .DeleteAsync(Arg.Any<Domain.Person>());
    }
}
