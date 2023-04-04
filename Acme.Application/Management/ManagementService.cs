using Acme.Core.Management.Interfaces;

namespace Acme.Application.Management;

/// <summary>
/// Application management service class.
/// </summary>
/// <seealso cref="IManagementService"/>
public class ManagementService : IManagementService
{
    private readonly IDatabaseManagementService databaseManagementService;

    /// <summary>
    /// Initializes a new instance of the <see cref="ManagementService"/> class.
    /// </summary>
    /// <param name="databaseManagementService">Database management service.</param>
    /// <exception cref="ArgumentNullException">
    /// databaseManagementService
    /// </exception>
    public ManagementService(IDatabaseManagementService databaseManagementService)
    {
        this.databaseManagementService = databaseManagementService ?? throw new ArgumentNullException(nameof(databaseManagementService));
    }

    /// <summary>
    /// Asserts that migrations are up-to-date asynchronously.
    /// </summary>
    /// <param name="cancellationToken">(Optional) A token that allows processing to be cancelled.</param>
    /// <returns>
    /// An asynchronous task that asserts migrations.
    /// </returns>
    public Task AssertMigrationsAsync(CancellationToken cancellationToken = default) =>
        this.databaseManagementService.AssertMigrationsAsync(cancellationToken);

    /// <summary>
    /// Migrates the database asynchronously.
    /// </summary>
    /// <param name="targetMigration">(Optional) The target migration.</param>
    /// <param name="cancellationToken">(Optional) A token that allows processing to be cancelled.</param>
    /// <returns>
    /// An asynchronous task that migrates the database.
    /// </returns>
    public Task MigrateAsync(string? targetMigration = null, CancellationToken cancellationToken = default) =>
        this.databaseManagementService.MigrateAsync(targetMigration, cancellationToken);
}