using Acme.Application.Tests.ServiceRegistrars;
using Acme.Core.Authentication.Interfaces;
using Acme.Core.Users;
using Acme.Infrastructure.EF.PostgreSql.Tests.Mocks;
using FluentAssertions;
using Moq;

namespace Acme.Application.Tests.Services;

/// <summary>
/// Authentication service tests class.
/// </summary>
/// <seealso cref="ApplicationUnitTestBase{TMockedServices}"/>
/// <seealso cref="AuthenticationServiceTestsMocks"/>
/// <seealso cref="IDisposable"/>
public class AuthenticationServiceTests : IClassFixture<ApplicationUnitTestBase<AuthenticationServiceTestsMocks>>, IDisposable
{
    private readonly IAuthenticationService authenticationService;

    private readonly ApplicationUsersRepositoryMock applicationUsersRepositoryMock;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthenticationServiceTests"/> class.
    /// </summary>
    /// <param name="testBase">Application unit test base.</param>
    public AuthenticationServiceTests(ApplicationUnitTestBase<AuthenticationServiceTestsMocks> testBase)
    {
        this.authenticationService = testBase.Resolve<IAuthenticationService>();

        this.applicationUsersRepositoryMock = testBase.MockedServices.ApplicationUsersRepositoryMock;
    }

    /// <summary>
    /// Tests that the <see cref="IAuthenticationService.AuthenticateAsync"/> returns <c>null</c> when the user
    /// does not exist.
    /// </summary>
    [Fact]
    public async Task AuthenticateAsync_WithNoExistingUser_ReturnsNull()
    {
        // Arrange.
        const string email = "test@user.com";
        const string password = "pa$$w0rd";

        // Act.
        var authenticationResponse = await this.authenticationService.AuthenticateAsync(email, password);

        // Assert.
        authenticationResponse.Should().BeNull();

        this.applicationUsersRepositoryMock.VerifyGetByEmailAndPasswordAsync(email, Times.Once());
    }

    /// <summary>
    /// Tests that the <see cref="IAuthenticationService.AuthenticateAsync"/> returns correctly when user exists
    /// and valid params are passed in.
    /// </summary>
    [Fact]
    public async Task AuthenticateAsync_WithExistingUser_ReturnsCorrectly()
    {
        // Arrange.
        const string email = "test@user.com";
        const string password = "pa$$word";

        var userToReturn = new ApplicationUser
        {
            Id = Guid.NewGuid().ToString(),
            Email = email,
            Username = "user"
        };

        this.applicationUsersRepositoryMock.SetupGetByEmailAndPasswordAsync(email, userToReturn);

        // Act.
        var authenticationResponse = await this.authenticationService.AuthenticateAsync(email, password);

        // Assert.
        authenticationResponse.Should().NotBeNull();

        authenticationResponse!.UserId.Should().Be(userToReturn.Id);
        authenticationResponse.Username.Should().Be(userToReturn.Username);
        authenticationResponse.Token.CreatedDateTimeUtc.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromMinutes(1));

        this.applicationUsersRepositoryMock.VerifyGetByEmailAndPasswordAsync(email, Times.Once());
    }

    /// <summary>
    /// Dispose pattern method called after every test to cleanup. Used to reset mock states.
    /// </summary>
    public void Dispose()
    {
        this.applicationUsersRepositoryMock.Reset();
    }
}