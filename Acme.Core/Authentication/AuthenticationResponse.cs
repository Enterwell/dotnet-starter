using Acme.Core.Authentication.Interfaces;

namespace Acme.Core.Authentication;

/// <summary>
/// Authentication response class.
/// </summary>
/// <seealso cref="IAuthenticationResponse"/>
public class AuthenticationResponse : IAuthenticationResponse
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AuthenticationResponse"/> class.
    /// </summary>
    /// <param name="userId">User's identifier.</param>
    /// <param name="username">User's username.</param>
    /// <param name="token">User's token.</param>
    public AuthenticationResponse(string userId, string username, IToken token)
    {
        this.UserId = userId;
        this.Username = username;
        this.Token = token;
    }

    /// <summary>
    /// Gets the user's identifier.
    /// </summary>
    public string UserId { get; }

    /// <summary>
    /// Gets the user's username.
    /// </summary>
    public string Username { get; }

    /// <summary>
    /// Gets the user's token.
    /// </summary>
    public IToken Token { get; }
}