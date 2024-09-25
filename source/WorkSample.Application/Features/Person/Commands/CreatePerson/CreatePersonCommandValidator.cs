using FluentValidation;
using WorkSample.Application.Contracts.Persistence;

namespace WorkSample.Application.Features.Person.Commands.CreatePerson;

/// <summary>
///     Validator for <see cref="CreatePersonCommand"/>.
/// </summary>
public class CreatePersonCommandValidator : AbstractValidator<CreatePersonCommand>
{
    private readonly IPersonRepository _personRepository;

    /// <summary>
    ///     Initiates a new instance of <see cref="CreatePersonCommandValidator"/>.
    /// </summary>
    /// <param name="personRepository">The current instance of <see cref="IPersonRepository"/>.</param>
    public CreatePersonCommandValidator(IPersonRepository personRepository)
    {
        RuleFor(p => p.Name)
           .NotEmpty().WithMessage("{PropertyName} is required")
           .NotNull()
           .MaximumLength(100).WithMessage("{PropertyName} must be fewer than 100 characters");

        RuleFor(p => p.Surname)
           .NotEmpty().WithMessage("{PropertyName} is required")
           .NotNull()
           .MaximumLength(100).WithMessage("{PropertyName} must be fewer than 100 characters");

        RuleFor(q => q)
            .MustAsync(CheckIfPersonIsUniqueAsync)
            .WithMessage("Person already exists");

        _personRepository = personRepository;
    }

    /// <summary>
    ///     Checks if a person is unique.
    /// </summary>
    /// <param name="command">The command to check as <see cref="CreatePersonCommand"/>.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>True if the person is unique, false otherwise.</returns>
    private async Task<bool> CheckIfPersonIsUniqueAsync(CreatePersonCommand command, CancellationToken cancellationToken)
    {
        return await _personRepository.IsPersonUniqueAsync(command.Name, command.Surname);
    }
}