namespace Acme.Interface.WebAPI.Models.Authentication;

/// <summary>
/// Login request DTO.
/// </summary>
public class LoginRequestDto
{
    /// <summary>
    /// Gets or sets the user's email.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets the user's password.
    /// </summary>
    public string Password { get; set; }
}