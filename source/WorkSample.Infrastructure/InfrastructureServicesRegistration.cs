using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using WorkSample.Application.Contracts.Logging;
using WorkSample.Infrastructure.Logging;

namespace WorkSample.Infrastructure;

/// <summary>
///     Contains service registration for infrastructure layer.
/// </summary>
[ExcludeFromCodeCoverage]
public static class InfrastructureServicesRegistration
{
    /// <summary>
    ///     Adds the services needed for the infrastructure layer.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/>.</param>
    /// <returns>The <see cref="IServiceCollection"/> after the registration.</returns>
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IAppLogger<>), typeof(LoggingAdapter<>));
        return services;
    }
}