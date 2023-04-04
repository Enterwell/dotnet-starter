using Acme.Core.Management.Interfaces;
using Moq;

namespace Acme.Application.Tests.Mocks;

/// <summary>
/// Management service mock class.
/// </summary>
/// <seealso cref="IManagementService"/>
public class ManagementServiceMock : Mock<IManagementService>
{
    /// <summary>
    /// Setups the <see cref="IManagementService.AssertMigrationsAsync"/> service method to throw on execution.
    /// </summary>
    /// <param name="exceptionToThrow">Explicit exception to throw.</param>
    /// <returns><see cref="ManagementServiceMock"/> instance.</returns>
    public ManagementServiceMock SetupAssertMigrationsAsyncToThrow(Exception exceptionToThrow)
    {
        this.Setup(x => x.AssertMigrationsAsync(It.IsAny<CancellationToken>()))
            .ThrowsAsync(exceptionToThrow);

        return this;
    }

    /// <summary>
    /// Verifies that the <see cref="IManagementService.AssertMigrationsAsync"/> service method was called specified number of times.
    /// </summary>
    /// <param name="times">Number of times the method should have been called.</param>
    /// <returns><see cref="ManagementServiceMock"/> instance.</returns>
    public ManagementServiceMock VerifyAssertMigrationsAsync(Times times)
    {
        this.Verify(x => x.AssertMigrationsAsync(It.IsAny<CancellationToken>()), times);

        return this;
    }

    /// <summary>
    /// Setups the <see cref="IManagementService.MigrateAsync"/> service method to throw on execution.
    /// </summary>
    /// <param name="exceptionToThrow">Explicit exception to throw.</param>
    /// <returns><see cref="ManagementServiceMock"/> instance.</returns>
    public ManagementServiceMock SetupMigrateAsyncToThrow(Exception exceptionToThrow)
    {
        this.Setup(x => x.MigrateAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(exceptionToThrow);

        return this;
    }

    /// <summary>
    /// Verifies that the <see cref="IManagementService.MigrateAsync"/> service method was called specified number of times.
    /// </summary>
    /// <param name="times">Number of times the method should have been called.</param>
    /// <param name="targetMigration">(Optional) Explicit target migration.</param>
    /// <returns><see cref="ManagementServiceMock"/> instance.</returns>
    public ManagementServiceMock VerifyMigrateAsync(Times times, string? targetMigration = null)
    {
        this.Verify(x => x.MigrateAsync(targetMigration, It.IsAny<CancellationToken>()), times);

        return this;
    }
}