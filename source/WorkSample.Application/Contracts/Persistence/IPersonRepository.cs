using WorkSample.Domain;

namespace WorkSample.Application.Contracts.Persistence;

/// <summary>
///     Contains methods for entity <see cref="Person">
/// </summary>
public interface IPersonRepository : IGenericRepository<Person>
{
    /// <summary>
    ///     Checks if the person is unique.
    /// </summary>
    /// <param name="name">Name of the person.</param>
    /// <param name="surname">Surname of the person.</param>
    /// <returns>True if the person is unique, false otherwise.</returns>
    Task<bool> IsPersonUniqueAsync(string name, string surname);
}
