using WorkSample.Domain.Common;

namespace WorkSample.Application.Contracts.Persistence;

/// <summary>
///     Interface containing common methods for all repositories.
/// </summary>
/// <typeparam name="T">Type of the repository.</typeparam>
public interface IGenericRepository<T> where T : BaseEntity
{
    /// <summary>
    ///     Gets all entries.
    /// </summary>
    /// <returns>All entries as <see cref="List{T}"/>.</returns>
    Task<List<T>> GetAsync();

    /// <summary>
    ///     Gets an entry by ID.
    /// </summary>
    /// <param name="id">The ID of the entry.</param>
    /// <returns>The entry.</returns>
    Task<T?> GetByIdAsync(int id);

    /// <summary>
    ///     Creates a new entry.
    /// </summary>
    /// <param name="entity">The entry to create.</param>
    /// <returns>The ID of the new entity.</returns>
    Task<int> CreateAsync(T entity);

    /// <summary>
    ///     Updates the given entry.
    /// </summary>
    /// <param name="entity">The entry to update.</param>
    Task UpdateAsync(T entity);

    /// <summary>
    ///     Deletes the given entry.
    /// </summary>
    /// <param name="entity">The entry to delete.</param>
    Task DeleteAsync(T entity);
}