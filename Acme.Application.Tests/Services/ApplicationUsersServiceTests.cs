using Acme.Application.Tests.ServiceRegistrars;
using Acme.Core.Users;
using Acme.Core.Users.Interfaces;
using Acme.Infrastructure.EF.PostgreSql.Tests.Mocks;
using Enterwell.Exceptions;
using FluentAssertions;
using Moq;

namespace Acme.Application.Tests.Services;

/// <summary>
/// Application users service tests class.
/// </summary>
/// <seealso cref="ApplicationUnitTestBase{TMockedServices}"/>
/// <seealso cref="ApplicationUsersServiceTestsMocks"/>
/// <seealso cref="IDisposable"/>
public class ApplicationUsersServiceTests : IClassFixture<ApplicationUnitTestBase<ApplicationUsersServiceTestsMocks>>, IDisposable
{
    private readonly IApplicationUsersService applicationUsersService;

    private readonly ApplicationUsersRepositoryMock applicationUsersRepositoryMock;

    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationUsersServiceTests"/> class.
    /// </summary>
    /// <param name="testBase">Application unit test base.</param>
    public ApplicationUsersServiceTests(ApplicationUnitTestBase<ApplicationUsersServiceTestsMocks> testBase)
    {
        this.applicationUsersService = testBase.Resolve<IApplicationUsersService>();

        this.applicationUsersRepositoryMock = testBase.MockedServices.ApplicationUsersRepositoryMock;
    }

    /// <summary>
    /// Tests that the <see cref="IApplicationUsersService.GetByIdAsync"/> calls the repository and returns correctly.
    /// </summary>
    [Fact]
    public async Task GetByIdAsync_CallsRepo_ReturnsCorrectly()
    {
        // Arrange.
        var testId = Guid.NewGuid().ToString();
        var userToReturn = new ApplicationUser { Id = testId };

        this.applicationUsersRepositoryMock.SetupGetByIdAsync(testId, userToReturn);

        // Act.
        var result = await this.applicationUsersService.GetByIdAsync(testId);

        // Assert.
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(userToReturn);

        this.applicationUsersRepositoryMock.VerifyGetByIdAsync(testId, Times.Once());
    }

    /// <summary>
    /// Tests that the <see cref="IApplicationUsersService.CreateAsync"/> throw an <see cref="ArgumentNullException"/> when
    /// the create model is <c>null</c>.
    /// </summary>
    [Fact]
    public async Task CreateAsync_WithNullCreateModel_ThrowsAnException()
    {
        // Arrange.
        IApplicationUserCreate? userCreate = null;

        // Act.
        Func<Task> act = () => this.applicationUsersService.CreateAsync(userCreate!);

        // Assert.
        await act.Should().ThrowExactlyAsync<ArgumentNullException>();
    }

    /// <summary>
    /// Tests that the <see cref="IApplicationUsersService.CreateAsync"/> throws an <see cref="EnterwellException"/> when
    /// user for the newly created identifier cannot be found.
    /// </summary>
    [Fact]
    public async Task CreateAsync_NoUserFoundById_ThrowsAnException()
    {
        // Arrange.
        var userCreate = new ApplicationUserCreate
        {
            Email = "test@user.com",
            Username = "user"
        };

        var createdUserId = Guid.NewGuid().ToString();

        this.applicationUsersRepositoryMock.SetupCreateAsync(createdUserId);

        // Act.
        Func<Task> act = () => this.applicationUsersService.CreateAsync(userCreate);

        // Assert.
        await act.Should().ThrowExactlyAsync<EnterwellException>();

        this.applicationUsersRepositoryMock
            .VerifyCreateAsync(userCreate, Times.Once())
            .VerifyGetByIdAsync(createdUserId, Times.Once());
    }

    /// <summary>
    /// Tests that the <see cref="IApplicationUsersService.CreateAsync"/> calls the repository and returns correctly.
    /// </summary>
    [Fact]
    public async Task CreateAsync_CallsRepo_ReturnsCorrectly()
    {
        // Arrange.
        var userCreate = new ApplicationUserCreate
        {
            Email = "test@user.com",
            Username = "user"
        };

        var userToReturn = new ApplicationUser
        {
            Id = Guid.NewGuid().ToString(),
            Email = "test@user.com",
            Username = "user"
        };

        this.applicationUsersRepositoryMock
            .SetupCreateAsync(userToReturn.Id)
            .SetupGetByIdAsync(userToReturn.Id, userToReturn);

        // Act.
        var result = await this.applicationUsersService.CreateAsync(userCreate);

        // Assert.
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(userToReturn);

        this.applicationUsersRepositoryMock
            .VerifyCreateAsync(userCreate, Times.Once())
            .VerifyGetByIdAsync(userToReturn.Id, Times.Once());
    }

    /// <summary>
    /// Dispose pattern method called after every test to cleanup. Used to reset mock states.
    /// </summary>
    public void Dispose()
    {
        this.applicationUsersRepositoryMock.Reset();
    }
}