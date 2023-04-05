using Acme.Application.Tests.ServiceRegistrars;
using Acme.Core.Management.Interfaces;
using Acme.Infrastructure.EF.PostgreSql.Tests.Mocks;
using Moq;

namespace Acme.Application.Tests.Services;

/// <summary>
/// Management service tests class.
/// </summary>
/// <seealso cref="ApplicationUnitTestBase{TMockedServices}"/>
/// <seealso cref="ManagementServiceTestsMocks"/>
/// <seealso cref="IDisposable"/>
public class ManagementServiceTests : IClassFixture<ApplicationUnitTestBase<ManagementServiceTestsMocks>>, IDisposable
{
    private readonly IManagementService managementService;

    private readonly DatabaseManagementServiceMock databaseManagementServiceMock;

    /// <summary>
    /// Initializes a new instance of the <see cref="ManagementServiceTests"/> class.
    /// </summary>
    /// <param name="testBase">Application unit test base.</param>
    public ManagementServiceTests(ApplicationUnitTestBase<ManagementServiceTestsMocks> testBase)
    {
        this.managementService = testBase.Resolve<IManagementService>();

        this.databaseManagementServiceMock = testBase.MockedServices.DatabaseManagementServiceMock;
    }

    /// <summary>
    /// Tests that the <see cref="IDatabaseManagementService.AssertMigrationsAsync"/> calls the service.
    /// </summary>
    [Fact]
    public async Task AssertMigrationsAsync_CallsService()
    {
        // Arrange.

        // Act.
        await this.managementService.AssertMigrationsAsync();

        // Assert.
        this.databaseManagementServiceMock.VerifyAssertMigrationsAsync(Times.Once());
    }

    /// <summary>
    /// Tests that the <see cref="IDatabaseManagementService.MigrateAsync"/> calls the service.
    /// </summary>
    [Theory]
    [InlineData(null)]
    [InlineData("InitialMigration")]
    public async Task MigrateAsync_CallsService(string? targetMigration)
    {
        // Arrange.

        // Act.
        await this.managementService.MigrateAsync(targetMigration);

        // Assert.
        this.databaseManagementServiceMock.VerifyMigrateAsync(Times.Once(), targetMigration);
    }

    /// <summary>
    /// Dispose pattern method called after every test to cleanup. Used to reset mock states.
    /// </summary>
    public void Dispose()
    {
        this.databaseManagementServiceMock.Reset();
    }
}