namespace Acme.Interface.WebAPI.Models.Authentication;

/// <summary>
/// Login request DTO.
/// </summary>
public class LoginRequestDto
{
    /// <summary>
    /// Gets or sets the user's email.
    /// </summary>
    /// <example>admin@acme.com</example>
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets the user's password.
    /// </summary>
    /// <example>pa$$w0rd</example>
    public string Password { get; set; }
}