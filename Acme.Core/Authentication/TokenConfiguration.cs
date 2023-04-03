using Acme.Core.Authentication.Interfaces;

namespace Acme.Core.Authentication;

/// <summary>
/// Token configuration class.
/// </summary>
/// <seealso cref="ITokenConfiguration"/>
public class TokenConfiguration : ITokenConfiguration
{
    /// <summary>
    /// JSON web token secret.
    /// </summary>
    public string JwtSecret { get; set; }

    /// <summary>
    /// Seconds after which the JSON web token expires.
    /// </summary>
    public int JwtExpirationSeconds { get; set; }
}