namespace Acme.Core.Management.Interfaces;

/// <summary>
/// Database seed service interface.
/// </summary>
public interface IDatabaseSeedService
{
    /// <summary>
    /// Seeds the database asynchronously.
    /// </summary>
    /// <returns>
    /// An asynchronous task that seeds the database.
    /// </returns>
    Task SeedDatabaseAsync();
}