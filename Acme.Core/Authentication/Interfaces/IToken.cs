namespace Acme.Core.Authentication.Interfaces;

/// <summary>
/// Token interface.
/// </summary>
public interface IToken
{
    /// <summary>
    /// Gets the access token.
    /// </summary>
    string AccessToken { get; }

    /// <summary>
    /// Gets the token creation UTC DateTime.
    /// </summary>
    DateTime CreatedDateTimeUtc { get; }

    /// <summary>
    /// Gets the token expiration UTC DateTime.
    /// </summary>
    DateTime ExpirationDateTimeUtc { get; }
}