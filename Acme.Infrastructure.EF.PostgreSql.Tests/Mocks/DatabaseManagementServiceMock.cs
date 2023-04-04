using Acme.Core.Management.Interfaces;
using Moq;

namespace Acme.Infrastructure.EF.PostgreSql.Tests.Mocks;

/// <summary>
/// Database management service mock class.
/// </summary>
/// <seealso cref="IDatabaseManagementService"/>
public class DatabaseManagementServiceMock : Mock<IDatabaseManagementService>
{
    /// <summary>
    /// Verifies that the <see cref="IDatabaseManagementService.AssertMigrationsAsync"/> service method was called specified number of times.
    /// </summary>
    /// <param name="times">Number of times the method should have been called.</param>
    /// <returns><see cref="DatabaseManagementServiceMock"/> instance.</returns>
    public DatabaseManagementServiceMock VerifyAssertMigrationsAsync(Times times)
    {
        this.Verify(x => x.AssertMigrationsAsync(It.IsAny<CancellationToken>()), times);

        return this;
    }

    /// <summary>
    /// Verifies that the <see cref="IDatabaseManagementService.MigrateAsync"/> service method was called specified number of times.
    /// </summary>
    /// <param name="times">Number of times the method should have been called.</param>
    /// <param name="targetMigration">(Optional) Explicit target migration.</param>
    /// <returns><see cref="DatabaseManagementServiceMock"/> instance.</returns>
    public DatabaseManagementServiceMock VerifyMigrateAsync(Times times, string? targetMigration = null)
    {
        this.Verify(x => x.MigrateAsync(targetMigration, It.IsAny<CancellationToken>()), times);

        return this;
    }
}