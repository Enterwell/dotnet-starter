using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using System.Text.Json.Serialization;

namespace Acme.Interface.WebAPI.Configuration.InstallerExtensions;

/// <summary>
/// Web API controller services installer extensions.
/// </summary>
public static class ApiControllersServicesExtensions
{
    /// <summary>
    /// Acme development CORS policy.
    /// </summary>
    private const string AcmeDevCorsPolicy = "AcmeDevCorsPolicy";

    /// <summary>
    /// Adds custom CORS configuration to the Web API project.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/> instance.</param>
    /// <returns><see cref="IServiceCollection"/> instance.</returns>
    public static IServiceCollection AddCustomCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(AcmeDevCorsPolicy, builder =>
            {
                builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });

        // Add production policy later.

        return services;
    }

    /// <summary>
    /// An <see cref="IApplicationBuilder"/> extension method that uses the custom CORS.
    /// </summary>
    /// <param name="app"><see cref="IApplicationBuilder"/> instance.</param>
    /// <returns><see cref="IApplicationBuilder"/> instance.</returns>
    public static IApplicationBuilder UseCustomCors(this IApplicationBuilder app)
    {
        return app.UseCors(AcmeDevCorsPolicy);
    }

    /// <summary>
    /// Adds controllers' configuration to the Web API project.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/> instance.</param>
    /// <returns><see cref="IServiceCollection"/> instance.</returns>
    public static IServiceCollection AddCustomControllers(this IServiceCollection services)
    {
        services
            .AddControllers(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();

                config.Filters.Add(new AuthorizeFilter(policy));
            }).AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

        services.AddRouting(options => options.LowercaseUrls = true);

        return services;
    }

    /// <summary>
    /// An <see cref="IApplicationBuilder"/> extension method that uses custom controllers.
    /// </summary>
    /// <param name="app"><see cref="IApplicationBuilder"/> instance.</param>
    /// <returns>An <see cref="IApplicationBuilder"/> instance.</returns>
    public static IApplicationBuilder UseCustomControllers(this IApplicationBuilder app)
    {
        app.UseEndpoints(endpoints => endpoints.MapControllers());

        return app;
    }

    /// <summary>
    /// An <see cref="IApplicationBuilder"/> extension method that uses custom routing.
    /// </summary>
    /// <param name="app"><see cref="IApplicationBuilder"/> instance.</param>
    /// <param name="isProduction"><c>True</c> if the application is running in the production environment.</param>
    /// <returns><see cref="IApplicationBuilder"/> instance.</returns>
    public static IApplicationBuilder UseCustomRouting(this IApplicationBuilder app, bool isProduction)
    {
        var builder = app.UseRouting();

        // Use HTTPS redirection on production
        if (isProduction)
        {
            builder.UseHttpsRedirection();
        }

        return builder;
    }
}