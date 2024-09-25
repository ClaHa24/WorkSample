using AutoMapper;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using WorkSample.Application.Contracts.Logging;
using WorkSample.Application.Contracts.Persistence;
using WorkSample.Application.Exceptions;
using WorkSample.Application.Features.Person.Queries.GetPerson;
using WorkSample.Application.MappingProfiles;

namespace WorkSample.Application.UnitTests.Features.Person.Queries;

public class GetPersonRequestHandlerTests
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<GetPersonRequestHandler> _logger;

    public GetPersonRequestHandlerTests()
    {
        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<PersonProfile>();
        });

        _mapper = mapperConfig.CreateMapper();
        _logger = Substitute.For<IAppLogger<GetPersonRequestHandler>>();
    }

    [Fact]
    public async Task Handle_PersonFound()
    {
        // Arrange
        var command = new GetPersonQuery { Id = 1 };
        var repositoryMock = Substitute.For<IPersonRepository>();
        var person = new Domain.Person
        {
            Id = command.Id,
            Surname = "Duck",
            Name = "Donald"
        };
        repositoryMock.GetByIdAsync(command.Id).Returns(person);
        var handler = new GetPersonRequestHandler(_mapper, repositoryMock, _logger);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().BeOfType<PersonDto>();
        result.Name.Should().BeEquivalentTo(person.Name);
        result.Surname.Should().BeEquivalentTo(person.Surname);
        result.Id.Should().Be(command.Id);
    }

    [Fact]
    public async Task Handle_PersonNotFound()
    {
        // Arrange
        var command = new GetPersonQuery { Id = 1 };
        var repositoryMock = Substitute.For<IPersonRepository>();
        repositoryMock.GetByIdAsync(command.Id).ReturnsNull();
        var handler = new GetPersonRequestHandler(_mapper, repositoryMock, _logger);

        // Act
        var action = async () => await handler.Handle(command, CancellationToken.None);

        // Assert
        await action.Should().ThrowExactlyAsync<NotFoundException>();
    }
}
