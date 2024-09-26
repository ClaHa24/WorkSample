using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace WorkSample.Application;

/// <summary>
///     Contains service registration for application layer.
/// </summary>
[ExcludeFromCodeCoverage]
public static class ApplicationServiceRegistration
{
    /// <summary>
    ///     Adds the services needed for the application layer.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/>.</param>
    /// <returns>The <see cref="IServiceCollection"/> after the registration.</returns>
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        return services;
    }
}