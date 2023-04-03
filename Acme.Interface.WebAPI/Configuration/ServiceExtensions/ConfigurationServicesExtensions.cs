using Acme.Interface.WebAPI.Settings;

namespace Acme.Interface.WebAPI.Configuration.ServiceExtensions;

/// <summary>
/// Web API configuration installer extensions.
/// </summary>
public static class ConfigurationServicesExtensions
{
    /// <summary>
    /// Adds custom configuration to the Web API project.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/> instance.</param>
    /// <param name="configuration"><see cref="IConfiguration"/> instance.</param>
    /// <returns><see cref="IServiceCollection"/> instance.</returns>
    public static IServiceCollection AddCustomConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ApplicationSettings>(configuration);

        return services;
    }
}