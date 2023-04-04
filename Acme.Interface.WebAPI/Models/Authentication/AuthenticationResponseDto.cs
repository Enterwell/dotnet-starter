namespace Acme.Interface.WebAPI.Models.Authentication;

/// <summary>
/// Authentication response DTO.
/// </summary>
public class AuthenticationResponseDto
{
    /// <summary>
    /// Gets or sets the user's identifier.
    /// </summary>
    public string UserId { get; set; }

    /// <summary>
    /// Gets or sets the user's username.
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    /// Gets or sets the user's token.
    /// </summary>
    public TokenDto Token { get; set; }
}