namespace Acme.Core.Authentication.Interfaces;

/// <summary>
/// Authentication response interface.
/// </summary>
public interface IAuthenticationResponse
{
    /// <summary>
    /// Gets the user's identifier.
    /// </summary>
    string UserId { get; }

    /// <summary>
    /// Gets the user's username.
    /// </summary>
    string Username { get; }

    /// <summary>
    /// Gets the user's token.
    /// </summary>
    IToken Token { get; }
}