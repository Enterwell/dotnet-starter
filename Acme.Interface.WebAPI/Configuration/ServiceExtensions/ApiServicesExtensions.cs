using Acme.Core.Settings;
using Acme.Interface.WebAPI.Settings;

namespace Acme.Interface.WebAPI.Configuration.ServiceExtensions;

/// <summary>
/// Web API project services installer extensions.
/// </summary>
public static class ApiServicesExtensions
{
    /// <summary>
    /// Adds Web API project services.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/> instance.</param>
    /// <returns><see cref="IServiceCollection"/> instance.</returns>
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        // Register providers
        services
            .AddTransient<IApplicationSettingsProvider, ApplicationSettingsProvider>();

        return services;
    }
}