using AutoMapper;
using FluentAssertions;
using NSubstitute;
using WorkSample.Application.Contracts.Persistence;
using WorkSample.Application.Features.Person.Queries.GetPersonList;
using WorkSample.Application.MappingProfiles;

namespace WorkSample.Application.UnitTests.Features.Person.Queries;

public class GetPersonListRequestHandlerTests
{
    private readonly IMapper _mapper;

    public GetPersonListRequestHandlerTests()
    {
        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<PersonProfile>();
        });

        _mapper = mapperConfig.CreateMapper();
    }

    [Fact]
    public async Task Handle_PersonsFound()
    {
        // Arrange
        var command = new GetPersonListQuery();
        var repositoryMock = Substitute.For<IPersonRepository>();
        var persons = new List<Domain.Person>
        {
            new() { Id = 1,Surname = "Duck",Name = "Donald" },
            new() { Id = 2,Surname = "Mouse",Name = "Mickey" },
        };
        repositoryMock.GetAsync().Returns(persons);
        var handler = new GetPersonListRequestHandler(_mapper, repositoryMock);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().BeOfType<List<PersonListDto>>();
        result.Count.Should().Be(persons.Count);
    }
}
