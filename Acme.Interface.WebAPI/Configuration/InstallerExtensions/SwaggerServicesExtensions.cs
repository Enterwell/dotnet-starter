using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Acme.Interface.WebAPI.Configuration.InstallerExtensions;

/// <summary>
/// Web API Swagger services installer extensions.
/// </summary>
public static class SwaggerServicesExtensions
{
    /// <summary>
    /// Name of the application swagger document.
    /// </summary>
    private const string ApplicationName = "ACME - Web API";

    /// <summary>
    /// URL of the Swagger JSON.
    /// </summary>
    private const string SwaggerJsonUrl = "/docs/v1/swagger.json";

    /// <summary>
    /// Swagger JSON URL template.
    /// </summary>
    private const string SwaggerJsonUrlTemplate = "/docs/{documentName}/swagger.json";

    /// <summary>
    /// Adds the Swagger API documentation to the Web API project.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/> instance.</param>
    /// <returns><see cref="IServiceCollection"/> instance.</returns>
    public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(config =>
        {
            config.SwaggerDoc("v1", new OpenApiInfo { Title = ApplicationName, Version = "v1" });
            config.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Scheme = "bearer"
            });

            config.OperationFilter<AuthenticationRequirementOperationFilter>();

            // Set the Web API documentation file path for the Swagger JSON
            var xmlFile = $"{typeof(ApiServicesExtensions).Assembly.GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

            config.IncludeXmlComments(xmlPath);
        });

        return services;
    }

    /// <summary>
    /// Adds the Swagger API documentation user interface to the Web API project.
    /// </summary>
    /// <param name="app"><see cref="IApplicationBuilder"/> instance.</param>
    /// <param name="isProduction">(Optional) <c>True</c> if the application is running in the production environment.</param>
    /// <returns><see cref="IApplicationBuilder"/> instance.</returns>
    public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app, bool isProduction = false)
    {
        if (isProduction) return app;

        app
            .UseSwagger(options =>
            {
                options.RouteTemplate = SwaggerJsonUrlTemplate;
            })
            .UseSwaggerUI(config =>
            {
                config.SwaggerEndpoint(SwaggerJsonUrl, ApplicationName);
            });

        return app;
    }
}

/// <summary>
/// Authentication requirement operation filter.
/// </summary>
public class AuthenticationRequirementOperationFilter : IOperationFilter
{
    /// <summary>
    /// Applies the authentication requirement operation filter.
    /// </summary>
    /// <param name="operation">Open API operation to act on.</param>
    /// <param name="context">Operation filter content.</param>
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Security ??= new List<OpenApiSecurityRequirement>();

        var scheme = new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "bearer"
            }
        };

        operation.Security.Add(new OpenApiSecurityRequirement
        {
            [scheme] = new List<string>()
        });
    }
}