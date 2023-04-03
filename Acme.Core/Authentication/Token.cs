using Acme.Core.Authentication.Interfaces;

namespace Acme.Core.Authentication;

/// <summary>
/// Token class.
/// </summary>
/// <seealso cref="IToken"/>
public class Token : IToken
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Token"/> class.
    /// </summary>
    /// <param name="accessToken">Access token.</param>
    /// <param name="createdDateTimeUtc">Token creation DateTime in UTC format.</param>
    /// <param name="expirationDateTimeUtc">Token expiration DateTime in UTC format.</param>
    public Token(string accessToken, DateTime createdDateTimeUtc, DateTime expirationDateTimeUtc)
    {
        this.AccessToken = accessToken;
        this.CreatedDateTimeUtc = createdDateTimeUtc;
        this.ExpirationDateTimeUtc = expirationDateTimeUtc;
    }

    /// <summary>
    /// Gets the access token.
    /// </summary>
    public string AccessToken { get; }

    /// <summary>
    /// Gets the token creation UTC DateTime.
    /// </summary>
    public DateTime CreatedDateTimeUtc { get; }

    /// <summary>
    /// Gets the token expiration UTC DateTime.
    /// </summary>
    public DateTime ExpirationDateTimeUtc { get; }
}