using Acme.Core.Management.Interfaces;

namespace Acme.Interface.WebAPI.Services.Management;

/// <summary>
/// Application management Web API service class.
/// </summary>
/// <seealso cref="IManagementApiService"/>
public class ManagementApiService : IManagementApiService
{
    private readonly IManagementService managementService;
    private readonly ILogger<ManagementApiService> logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="ManagementApiService"/> class.
    /// </summary>
    /// <param name="managementService">Application management service.</param>
    /// <param name="logger">The logger.</param>
    /// <exception cref="ArgumentNullException">
    /// managementService
    /// or
    /// logger
    /// </exception>
    public ManagementApiService(IManagementService managementService, ILogger<ManagementApiService> logger)
    {
        this.managementService = managementService ?? throw new ArgumentNullException(nameof(managementService));
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Asserts that migrations are up-to-date asynchronously.
    /// </summary>
    /// <param name="cancellationToken">(Optional) A token that allows processing to be cancelled.</param>
    /// <returns>
    /// <c>True</c> if the migrations are up-to-date, <c>false</c> otherwise.
    /// </returns>
    public async Task<bool> AssertMigrationsAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            await this.managementService.AssertMigrationsAsync(cancellationToken);

            return true;
        }
        catch (Exception ex)
        {
            this.logger.LogWarning(ex, "An error occurred while asserting migrations.");

            return false;
        }
    }

    /// <summary>
    /// Migrates the database asynchronously.
    /// </summary>
    /// <param name="targetMigration">(Optional) The target migration.</param>
    /// <returns>
    /// <c>True</c> if the database was migrated successfully, <c>false</c> otherwise.
    /// </returns>
    public async Task<bool> MigrateAsync(string? targetMigration = null)
    {
        try
        {
            await this.managementService.MigrateAsync(targetMigration);

            return true;
        }
        catch (Exception ex)
        {
            this.logger.LogWarning(ex, "An error occurred while migrating the database.");

            return false;
        }
    }
}