using FluentValidation;
using WorkSample.Application.Contracts.Persistence;

namespace WorkSample.Application.Features.Person.Commands.UpdatePerson;

/// <summary>
///     Validator for <see cref="UpdatePersonCommand"/>.
/// </summary>
public class UpdatePersonCommandValidator : AbstractValidator<UpdatePersonCommand>
{
    private readonly IPersonRepository _personRepository;

    /// <summary>
    ///     Initiates a new instance of <see cref="UpdatePersonCommandValidator"/>.
    /// </summary>
    /// <param name="personRepository">The current instance of <see cref="IPersonRepository"/>.</param>
    public UpdatePersonCommandValidator(IPersonRepository personRepository)
    {
        RuleFor(p => p.Name)
           .NotEmpty().WithMessage("{PropertyName} is required")
           .NotNull()
           .MaximumLength(100).WithMessage("{PropertyName} must be fewer than 100 characters");

        RuleFor(p => p.Surname)
           .NotEmpty().WithMessage("{PropertyName} is required")
           .NotNull()
           .MaximumLength(100).WithMessage("{PropertyName} must be fewer than 100 characters");

        RuleFor(p => p.Id)
            .NotNull()
            .MustAsync(CheckIfPersonExistsAsync)
            .WithMessage("{PropertyName} must be present");

        _personRepository = personRepository;
    }

    /// <summary>
    ///     Checks if the person exists.
    /// </summary>
    /// <param name="id">ID of the person to check.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>True if the person exists, false otherwise.</returns>
    private async Task<bool> CheckIfPersonExistsAsync(int id, CancellationToken cancellationToken)
    {
        var person = await _personRepository.GetByIdAsync(id);
        return person != null;
    }
}
