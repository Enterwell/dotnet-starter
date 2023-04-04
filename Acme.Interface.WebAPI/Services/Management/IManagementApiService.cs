namespace Acme.Interface.WebAPI.Services.Management;

/// <summary>
/// Application management Web API service interface.
/// </summary>
public interface IManagementApiService
{
    /// <summary>
    /// Asserts that migrations are up-to-date asynchronously.
    /// </summary>
    /// <param name="cancellationToken">(Optional) A token that allows processing to be cancelled.</param>
    /// <returns>
    /// <c>True</c> if the migrations are up-to-date, <c>false</c> otherwise.
    /// </returns>
    Task<bool> AssertMigrationsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Migrates the database asynchronously.
    /// </summary>
    /// <param name="targetMigration">(Optional) The target migration.</param>
    /// <returns>
    /// <c>True</c> if the database was migrated successfully, <c>false</c> otherwise.
    /// </returns>
    Task<bool> MigrateAsync(string? targetMigration = null);
}