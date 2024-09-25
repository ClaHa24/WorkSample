using MediatR;
using Microsoft.AspNetCore.Mvc;
using WorkSample.Application.Features.Person.Commands.CreatePerson;
using WorkSample.Application.Features.Person.Commands.DeletePerson;
using WorkSample.Application.Features.Person.Commands.UpdatePerson;
using WorkSample.Application.Features.Person.Queries.GetPerson;
using WorkSample.Application.Features.Person.Queries.GetPersonList;

namespace WorkSample.Api.Controllers;

/// <summary>
///     Controller for persons.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class PersonsController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    ///     Initiates a new instance of <see cref="PersonsController"/>.
    /// </summary>
    /// <param name="mediator">The current instance of <see cref="IMediator"/>.</param>
    public PersonsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Gets all persons.
    /// </summary>
    /// <returns>The persons as <see cref="List{PersonListDto}"/>.</returns>
    [HttpGet]
    [ProducesResponseType<List<PersonListDto>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<PersonListDto>>> Get()
    {
        var persons = await _mediator.Send(new GetPersonListQuery());
        return Ok(persons);
    }

    /// <summary>
    ///     Gets a single person.
    /// </summary>
    /// <param name="id">ID of the person to get.</param>
    /// <returns>The person as <see cref="PersonDto"/>.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType<PersonDto>(StatusCodes.Status200OK)]
    public async Task<ActionResult<PersonDto>> Get(int id)
    {
        var person = await _mediator.Send(new GetPersonQuery { Id = id });
        return Ok(person);
    }


    /// <summary>
    ///     Creates a new person.
    /// </summary>
    /// <param name="person">The person to create.</param>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Post(CreatePersonCommand person)
    {
        var response = await _mediator.Send(person);
        return CreatedAtAction(nameof(Get), new { id = response });
    }

    /// <summary>
    ///     Updates a person.
    /// </summary>
    /// <param name="person">The person to update.</param>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Put(UpdatePersonCommand person)
    {
        await _mediator.Send(person);
        return NoContent();
    }

    /// <summary>
    ///     Deletes a person.
    /// </summary>
    /// <param name="id">ID of the person to be deleted.</param>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeletePersonCommand { Id = id };
        await _mediator.Send(command);
        return NoContent();
    }
}
