using Acme.Application.Authentication;
using Acme.Application.Users;
using Acme.Core.Authentication.Interfaces;
using Acme.Core.Users.Interfaces;
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
        services
            .AddTransient<IAuthenticationService, AuthenticationService>()
            .AddTransient<IApplicationUsersService, ApplicationUsersService>();

        return services;
    }
}