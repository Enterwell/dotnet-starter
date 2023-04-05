using Acme.Core.Authentication.Interfaces;
using Moq;

namespace Acme.Application.Tests.Mocks;

/// <summary>
/// Authentication service mock class.
/// </summary>
/// <seealso cref="IAuthenticationService"/>
public class AuthenticationServiceMock : Mock<IAuthenticationService>
{
    /// <summary>
    /// Setups the <see cref="IAuthenticationService.AuthenticateAsync"/> service method.
    /// </summary>
    /// <param name="email">User's email.</param>
    /// <param name="result">Result to return on the method call.</param>
    /// <returns><see cref="AuthenticationServiceMock"/> instance.</returns>
    public AuthenticationServiceMock SetupAuthenticateAsync(string email, IAuthenticationResponse? result)
    {
        this.Setup(x => x.AuthenticateAsync(email, It.IsAny<string>())).ReturnsAsync(result);

        return this;
    }

    /// <summary>
    /// Verifies that the <see cref="IAuthenticationService.AuthenticateAsync"/> service method was called specified number of times for the
    /// user with the given email.
    /// </summary>
    /// <param name="email">Explicit user's email.</param>
    /// <param name="times">Number of times the method should have been called.</param>
    /// <returns><see cref="AuthenticationServiceMock"/> instance.</returns>
    public AuthenticationServiceMock VerifyAuthenticateAsync(string email, Times times)
    {
        this.Verify(x => x.AuthenticateAsync(email, It.IsAny<string>()), times);

        return this;
    }
}