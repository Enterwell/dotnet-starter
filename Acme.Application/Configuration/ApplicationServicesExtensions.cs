using Microsoft.Extensions.DependencyInjection;

namespace Acme.Application.Configuration;

/// <summary>
/// Application logic services installer extensions.
/// </summary>
public static class ApplicationServicesExtensions
{
    /// <summary>
    /// Adds application project services.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/> instance.</param>
    /// <returns><see cref="IServiceCollection"/> instance.</returns>
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        return services;
    }
}