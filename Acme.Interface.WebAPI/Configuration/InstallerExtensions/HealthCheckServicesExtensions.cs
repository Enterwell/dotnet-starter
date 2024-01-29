using HealthChecks.ApplicationStatus.DependencyInjection;

namespace Acme.Interface.WebAPI.Configuration.InstallerExtensions;

/// <summary>
/// Health check services extensions.
/// </summary>
public static class HealthCheckServicesExtensions
{
    /// <summary>
    /// Adds the custom health checks.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentNullException"></exception>
    public static IServiceCollection AddCustomHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        string postgreSqlConnString = configuration.GetValue<string>("PostgreSQL:ConnectionString") ?? throw new ArgumentNullException();

        services
            .AddHealthChecks()
            .AddApplicationStatus("WebAPI", tags: ["api"])
            .AddNpgSql(postgreSqlConnString, name: "PostgreSQL", tags: ["database"]);

        return services;
    }
}
