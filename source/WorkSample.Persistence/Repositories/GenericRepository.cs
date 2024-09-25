using Microsoft.EntityFrameworkCore;
using WorkSample.Application.Contracts.Logging;
using WorkSample.Application.Contracts.Persistence;
using WorkSample.Domain.Common;
using WorkSample.Persistence.DatabaseContext;

namespace WorkSample.Persistence.Repositories;

/// <summary>
///     Generic repository containing common methods.
/// </summary>
/// <typeparam name="T">Type of the repository.</typeparam>
public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    protected readonly WorkSampleDatabaseContext _context;
    protected readonly IAppLogger<GenericRepository<T>> _logger;

    /// <summary>
    ///     Initiates a new instance of <see cref="GenericRepository{T}"/>.
    /// </summary>
    /// <param name="context">The database context as <see cref="WorkSampleDatabaseContext"/>.</param>
    /// <param name="logger">The current instance of <see cref="IAppLogger{T}"/>.</param>
    public GenericRepository(WorkSampleDatabaseContext context, IAppLogger<GenericRepository<T>> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc cref="GenericRepository{T}"/>
    public async Task<int> CreateAsync(T entity)
    {
        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Added new entity {0}", entity.Id);

        return entity.Id;
    }

    /// <inheritdoc cref="GenericRepository{T}"/>
    public async Task DeleteAsync(T entity)
    {
        _context.Remove(entity);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Deleted entity {0}", entity.Id);
    }

    /// <inheritdoc cref="GenericRepository{T}"/>
    public async Task<List<T>> GetAsync()
    {
        return await _context.Set<T>().AsNoTracking().ToListAsync();
    }

    /// <inheritdoc cref="GenericRepository{T}"/>
    public async Task<T?> GetByIdAsync(int id)
    {
        _logger.LogInformation("Getting entity {0}", id);

        return await _context.Set<T>()
            .AsNoTracking()
            .FirstOrDefaultAsync(q => q.Id == id);
    }

    /// <inheritdoc cref="GenericRepository{T}"/>
    public async Task UpdateAsync(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        _logger.LogInformation("Updated entity {0}", entity.Id);
    }
}