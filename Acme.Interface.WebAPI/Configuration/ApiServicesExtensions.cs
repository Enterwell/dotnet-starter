using Acme.Core.Settings.Interfaces;
using Acme.Interface.WebAPI.Services.Authentication;
using Acme.Interface.WebAPI.Services.Books;
using Acme.Interface.WebAPI.Services.Management;
using Acme.Interface.WebAPI.Settings;

namespace Acme.Interface.WebAPI.Configuration;

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

        // Register API services
        services
            .AddTransient<IManagementApiService, ManagementApiService>()
            .AddTransient<IAuthenticationApiService, AuthenticationApiService>()
            .AddTransient<IBooksApiService, BooksApiService>();

        return services;
    }
}