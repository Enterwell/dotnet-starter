namespace Acme.Interface.WebAPI.Models.Authentication;

/// <summary>
/// Token DTO.
/// </summary>
public class TokenDto
{
    /// <summary>
    /// Gets or sets the access token.
    /// </summary>
    public string AccessToken { get; set; }

    /// <summary>
    /// Gets or sets the token creation UTC DateTime.
    /// </summary>
    public DateTime CreatedDateTimeUtc { get; set; }

    /// <summary>
    /// Gets or sets the token expiration UTC DateTime.
    /// </summary>
    public DateTime ExpirationDateTimeUtc { get; set; }
}