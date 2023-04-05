using Acme.Application.Tests.Mocks;
using Acme.Interface.WebAPI.Services.Management;
using Acme.Interface.WebAPI.Tests.ServiceRegistrars;
using FluentAssertions;
using Moq;

namespace Acme.Interface.WebAPI.Tests.Services;

/// <summary>
/// Management web API service tests class.
/// </summary>
/// <seealso cref="ApiUnitTestBase{TMockedServices}"/>
/// <seealso cref="ManagementApiServiceTestsMocks"/>
/// <seealso cref="IDisposable"/>
public class ManagementApiServiceTests : IClassFixture<ApiUnitTestBase<ManagementApiServiceTestsMocks>>, IDisposable
{
    private readonly IManagementApiService managementApiService;

    private readonly ManagementServiceMock managementServiceMock;

    /// <summary>
    /// Initializes a new instance of the <see cref="ManagementApiServiceTests"/> class.
    /// </summary>
    /// <param name="testBase">Api unit test base.</param>
    public ManagementApiServiceTests(ApiUnitTestBase<ManagementApiServiceTestsMocks> testBase)
    {
        this.managementApiService = testBase.Resolve<IManagementApiService>();

        this.managementServiceMock = testBase.MockedServices.ManagementServiceMock;
    }

    /// <summary>
    /// Tests that the <see cref="IManagementApiService.AssertMigrationsAsync"/> returns <c>true</c> when assertion delegation completes
    /// without an error.
    /// </summary>
    [Fact]
    public async Task AssertMigrationsAsync_AssertionCompletes_ReturnsTrue()
    {
        // Arrange.

        // Act.
        var result = await this.managementApiService.AssertMigrationsAsync();

        // Assert.
        result.Should().BeTrue();

        this.managementServiceMock.VerifyAssertMigrationsAsync(Times.Once());
    }

    /// <summary>
    /// Tests that the <see cref="IManagementApiService.AssertMigrationsAsync"/> returns <c>false</c> when assertion delegation throws.
    /// </summary>
    [Fact]
    public async Task AssertMigrationsAsync_AssertionThrows_ReturnsFalse()
    {
        // Arrange.

        this.managementServiceMock.SetupAssertMigrationsAsyncToThrow(new Exception());

        // Act.
        var result = await this.managementApiService.AssertMigrationsAsync();

        // Assert.
        result.Should().BeFalse();

        this.managementServiceMock.VerifyAssertMigrationsAsync(Times.Once());
    }

    /// <summary>
    /// Tests that the <see cref="IManagementApiService.MigrateAsync"/> returns <c>true</c> when assertion delegation completes
    /// without an error.
    /// </summary>
    [Fact]
    public async Task MigrateAsync_MigrationCompletes_ReturnsTrue()
    {
        // Arrange.

        // Act.
        var result = await this.managementApiService.MigrateAsync();

        // Assert.
        result.Should().BeTrue();

        this.managementServiceMock.VerifyMigrateAsync(Times.Once());
    }

    /// <summary>
    /// Tests that the <see cref="IManagementApiService.MigrateAsync"/> returns <c>false</c> when assertion delegation throws.
    /// </summary>
    [Fact]
    public async Task MigrateAsync_MigrationThrows_ReturnsFalse()
    {
        // Arrange.

        this.managementServiceMock.SetupMigrateAsyncToThrow(new Exception());

        // Act.
        var result = await this.managementApiService.MigrateAsync();

        // Assert.
        result.Should().BeFalse();

        this.managementServiceMock.VerifyMigrateAsync(Times.Once());
    }

    /// <summary>
    /// Dispose pattern method called after every test to cleanup. Used to reset mock states.
    /// </summary>
    public void Dispose()
    {
        this.managementServiceMock.Reset();
    }
}