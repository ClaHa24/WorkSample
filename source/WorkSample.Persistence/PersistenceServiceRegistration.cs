using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using WorkSample.Application.Contracts.Persistence;
using WorkSample.Persistence.DatabaseContext;
using WorkSample.Persistence.Repositories;

namespace WorkSample.Persistence;

/// <summary>
///     Contains service registration for persistence layer.
/// </summary>
[ExcludeFromCodeCoverage]
public static class PersistenceServiceRegistration
{
    /// <summary>
    ///     Adds the services needed for the persistence layer.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/>.</param>
    /// <param name="configuration">The current configuration as <see cref="IConfiguration"/>.</param>
    /// <returns>The <see cref="IServiceCollection"/> after the registration.</returns>
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<WorkSampleDatabaseContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("HrDatabaseConnectionString"));
        });

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IPersonRepository, PersonRepository>();

        return services;
    }
}
