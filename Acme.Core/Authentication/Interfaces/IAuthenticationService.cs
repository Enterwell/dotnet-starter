namespace Acme.Core.Authentication.Interfaces;

/// <summary>
/// Authentication service interface.
/// </summary>
public interface IAuthenticationService
{
    /// <summary>
    /// Authenticates the user asynchronously.
    /// </summary>
    /// <param name="email">User's email address.</param>
    /// <param name="password">User's password.</param>
    /// <returns>
    /// An <see cref="IAuthenticationResponse"/> instance or <c>null</c> if authentication failed.
    /// </returns>
    Task<IAuthenticationResponse?> AuthenticateAsync(string email, string password);
}