using Acme.Application.Tests.Mocks;
using Acme.Core.Authentication;
using Acme.Interface.WebAPI.Models.Authentication;
using Acme.Interface.WebAPI.Services.Authentication;
using Acme.Interface.WebAPI.Tests.ServiceRegistrars;
using Enterwell.Exceptions;
using FluentAssertions;
using Moq;

namespace Acme.Interface.WebAPI.Tests.Services;

/// <summary>
/// Authentication web API service tests class.
/// </summary>
/// <seealso cref="ApiUnitTestBase{TMockedServices}"/>
/// <seealso cref="AuthenticationApiServiceTestsMocks"/>
/// <seealso cref="IDisposable"/>
public class AuthenticationApiServiceTests : IClassFixture<ApiUnitTestBase<AuthenticationApiServiceTestsMocks>>, IDisposable
{
    private readonly IAuthenticationApiService authenticationApiService;

    private readonly AuthenticationServiceMock authenticationServiceMock;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthenticationApiServiceTests"/> class.
    /// </summary>
    /// <param name="testBase">Api unit test base.</param>
    public AuthenticationApiServiceTests(ApiUnitTestBase<AuthenticationApiServiceTestsMocks> testBase)
    {
        this.authenticationApiService = testBase.Resolve<IAuthenticationApiService>();

        this.authenticationServiceMock = testBase.MockedServices.AuthenticationServiceMock;
    }

    /// <summary>
    /// Tests that the <see cref="IAuthenticationApiService.AuthenticateAsync"/> throws an <see cref="EnterwellValidationException"/>
    /// when <see cref="LoginRequestDto"/> with <c>null</c> email is passed in.
    /// </summary>
    [Fact]
    public async Task AuthenticateAsync_WithNullEmail_ThrowsAnException()
    {
        // Arrange.
        var loginRequestDto = new LoginRequestDto
        {
            Email = null!,
            Password = "Pa$$w0rd"
        };

        // Act.
        Func<Task> act = () => this.authenticationApiService.AuthenticateAsync(loginRequestDto);

        // Assert.
        await act.Should().ThrowExactlyAsync<EnterwellValidationException>();
    }

    /// <summary>
    /// Tests that the <see cref="IAuthenticationApiService.AuthenticateAsync"/> throws an <see cref="EnterwellValidationException"/>
    /// when <see cref="LoginRequestDto"/> with <c>null</c> password is passed in.
    /// </summary>
    [Fact]
    public async Task AuthenticateAsync_WithNullPassword_ThrowsAnException()
    {
        // Arrange.
        var loginRequestDto = new LoginRequestDto
        {
            Email = "test@user.com",
            Password = null!
        };

        // Act.
        Func<Task> act = () => this.authenticationApiService.AuthenticateAsync(loginRequestDto);

        // Assert.
        await act.Should().ThrowExactlyAsync<EnterwellValidationException>();
    }

    /// <summary>
    /// Tests that the <see cref="IAuthenticationApiService.AuthenticateAsync"/> throws an <see cref="EnterwellValidationException"/>
    /// when request DTO is filled in correctly but the authentication service cannot authenticate the user and returns <c>null</c>.
    /// </summary>
    [Fact]
    public async Task AuthenticateAsync_WithNullAuthResponse_ThrowsAnException()
    {
        // Arrange.
        var loginRequestDto = new LoginRequestDto
        {
            Email = "test@user.com",
            Password = "pa$$w0rd"
        };

        // Act.
        Func<Task> act = () => this.authenticationApiService.AuthenticateAsync(loginRequestDto);

        // Assert.
        await act.Should().ThrowExactlyAsync<EnterwellValidationException>();

        this.authenticationServiceMock.VerifyAuthenticateAsync(loginRequestDto.Email, Times.Once());
    }

    /// <summary>
    /// Tests that the <see cref="IAuthenticationApiService.AuthenticateAsync"/> returns correctly when the authentication
    /// service returns authentication response.
    /// </summary>
    [Fact]
    public async Task AuthenticateAsync_WithValidParamsAndServiceResponse_ReturnsCorrectly()
    {
        // Arrange.
        var loginRequestDto = new LoginRequestDto
        {
            Email = "test@user.com",
            Password = "pa$$w0rd"
        };

        var tokenToReturn = new Token("accessToken", DateTime.UtcNow, DateTime.UtcNow);
        var responseToReturn = new AuthenticationResponse("userId", "username", tokenToReturn);

        this.authenticationServiceMock.SetupAuthenticateAsync(loginRequestDto.Email, responseToReturn);

        var expectedResult = new AuthenticationResponseDto
        {
            Token = new TokenDto
            {
                AccessToken = tokenToReturn.AccessToken,
                CreatedDateTimeUtc = tokenToReturn.CreatedDateTimeUtc,
                ExpirationDateTimeUtc = tokenToReturn.ExpirationDateTimeUtc
            },
            Username = responseToReturn.Username,
            UserId = responseToReturn.UserId
        };

        // Act.
        var result = await this.authenticationApiService.AuthenticateAsync(loginRequestDto);

        // Assert.
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expectedResult);

        this.authenticationServiceMock.VerifyAuthenticateAsync(loginRequestDto.Email, Times.Once());
    }

    /// <summary>
    /// Dispose pattern method called after every test to cleanup. Used to reset mock states.
    /// </summary>
    public void Dispose()
    {
        this.authenticationServiceMock.Reset();
    }
}