using Microsoft.EntityFrameworkCore;
using WorkSample.Domain;
using WorkSample.Domain.Common;

namespace WorkSample.Persistence.DatabaseContext;

/// <summary>
///     Database context for the application.
/// </summary>
public class WorkSampleDatabaseContext : DbContext
{
    /// <summary>
    ///     Initiates a new instance of <see cref="WorkSampleDatabaseContext"/>. 
    /// </summary>
    /// <param name="options">Options regarding the database as <see cref="DbContextOptions{T}"/>.</param>
    public WorkSampleDatabaseContext(DbContextOptions<WorkSampleDatabaseContext> options) : base(options) { }

    /// <summary>
    ///     Holds the <see cref="Person"/> database entries.
    /// </summary>
    public DbSet<Person> Persons { get; set; }

    /// <summary>
    ///     Model is being created.
    /// </summary>
    /// <param name="modelBuilder">The current instance of <see cref="ModelBuilder"/>.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WorkSampleDatabaseContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    /// <summary>
    ///     Saves the current changes.
    /// </summary>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>The number of state entries written to the database.</returns>
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in base.ChangeTracker.Entries<BaseEntity>()
            .Where(q => q.State == EntityState.Added || q.State == EntityState.Modified))
        {
            entry.Entity.DateModified = DateTime.Now;
            if (entry.State == EntityState.Added)
            {
                entry.Entity.DateCreated = DateTime.Now;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
