using AutoMapper;
using WorkSample.Application.Features.Person.Commands.CreatePerson;
using WorkSample.Application.Features.Person.Commands.DeletePerson;
using WorkSample.Application.Features.Person.Commands.UpdatePerson;
using WorkSample.Application.Features.Person.Queries.GetPerson;
using WorkSample.Application.Features.Person.Queries.GetPersonList;
using WorkSample.Domain;

namespace WorkSample.Application.MappingProfiles;

/// <summary>
///     Holds the mapping profile for person.
/// </summary>
public class PersonProfile : Profile
{
    /// <summary>
    ///     Initiates a new instance of <see cref="PersonProfile"/>.
    /// </summary>
    public PersonProfile()
    {
        CreateMap<PersonListDto, Person>().ReverseMap();
        CreateMap<Person, PersonDto>();
        CreateMap<CreatePersonCommand, Person>();
        CreateMap<UpdatePersonCommand, Person>();
        CreateMap<DeletePersonCommand, Person>();
    }
}
