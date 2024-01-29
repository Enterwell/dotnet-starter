using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Newtonsoft.Json;

namespace Acme.Interface.WebAPI.Extensions;

/// <summary>
/// Health check extensions.
/// </summary>
public static class HealthCheckExtensions
{
    /// <summary>
    /// Maps the health checks with json response.
    /// </summary>
    /// <param name="endpoints">The endpoints.</param>
    /// <param name="path">The path.</param>
    public static void MapHealthChecksWithJsonResponse(this IEndpointRouteBuilder endpoints, PathString path)
    {
        var options = new HealthCheckOptions
        {
            ResponseWriter = async (httpContext, healthReport) =>
            {
                httpContext.Response.ContentType = "application/json";

                var result = JsonConvert.SerializeObject(new
                {
                    status = healthReport.Status.ToString(),
                    totalDurationInMs = Math.Round(healthReport.TotalDuration.TotalMilliseconds, 2),
                    entries = healthReport.Entries.Select(e => new
                    {
                        name = e.Key,
                        status = e.Value.Status.ToString(),
                        description = e.Value.Description,
                        durationInMs = Math.Round(e.Value.Duration.TotalMilliseconds, 2),
                        data = e.Value.Data,
                        tags = e.Value.Tags
                    }),
                });

                await httpContext.Response.WriteAsync(result);
            }
        };

        endpoints.MapHealthChecks(path, options);
    }
}
