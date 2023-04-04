namespace Acme.Core.Users.Interfaces;

/// <summary>
/// Application user create interface.
/// </summary>
public interface IApplicationUserCreate
{
    /// <summary>
    /// Gets or sets the user's username.
    /// </summary>
    string Username { get; set; }

    /// <summary>
    /// Gets or sets the user's email.
    /// </summary>
    string Email { get; set; }

    /// <summary>
    /// Gets or sets the user's password.
    /// </summary>
    string Password { get; set; }
}