using System.Text;
using Acme.Interface.WebAPI.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Acme.Interface.WebAPI.Configuration.InstallerExtensions;

/// <summary>
/// Web API authentication services installer extensions.
/// </summary>
public static class ApiAuthServicesExtensions
{
    /// <summary>
    /// Adds the custom authentication to the Web API project.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/> instance.</param>
    /// <param name="configuration"><see cref="IConfiguration"/> instance.</param>
    /// <returns><see cref="IServiceCollection"/> instance.</returns>
    public static IServiceCollection AddCustomAuth(this IServiceCollection services, IConfiguration configuration)
    {
        var applicationSettings = configuration.Get<ApplicationSettings>();
        var tokenConfiguration = applicationSettings.TokenConfiguration;

        // Check for Jwt secret
        if (string.IsNullOrWhiteSpace(tokenConfiguration.JwtSecret))
        {
            throw new ArgumentException("Jwt secret not configured.");
        }

        var key = Encoding.ASCII.GetBytes(tokenConfiguration.JwtSecret);

        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

        return services;
    }

    /// <summary>
    /// An <see cref="IApplicationBuilder"/> extension method that uses custom authentication.
    /// </summary>
    /// <param name="app"><see cref="IApplicationBuilder"/> instance.</param>
    /// <returns><see cref="IApplicationBuilder"/> instance.</returns>
    public static IApplicationBuilder UseCustomAuth(this IApplicationBuilder app)
    {
        return app
            .UseAuthentication()
            .UseAuthorization();
    }
}