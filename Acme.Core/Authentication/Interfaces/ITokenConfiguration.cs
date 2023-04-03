namespace Acme.Core.Authentication.Interfaces;

/// <summary>
/// Token configuration interface.
/// </summary>
public interface ITokenConfiguration
{
    /// <summary>
    /// JSON web token secret.
    /// </summary>
    string JwtSecret { get; }

    /// <summary>
    /// Seconds after which the JSON web token expires.
    /// </summary>
    int JwtExpirationSeconds { get; }
}