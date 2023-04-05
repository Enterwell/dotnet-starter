namespace Acme.Core.Management.Interfaces;

/// <summary>
/// Database management service interface.
/// </summary>
public interface IDatabaseManagementService
{
    /// <summary>
    /// Asserts that migrations are up-to-date asynchronously.
    /// </summary>
    /// <param name="cancellationToken">(Optional) A token that allows processing to be cancelled.</param>
    /// <returns>
    /// An asynchronous task that asserts migrations.
    /// </returns>
    Task AssertMigrationsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Migrates the database asynchronously.
    /// </summary>
    /// <param name="targetMigration">(Optional) The target migration.</param>
    /// <param name="cancellationToken">(Optional) A token that allows processing to be cancelled.</param>
    /// <returns>
    /// An asynchronous task that migrates the database.
    /// </returns>
    Task MigrateAsync(string? targetMigration = null, CancellationToken cancellationToken = default);
}