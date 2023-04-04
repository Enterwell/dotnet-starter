namespace Acme.Core.Users.Interfaces;

/// <summary>
/// Application user interface.
/// </summary>
public interface IApplicationUser
{
    /// <summary>
    /// Gets or sets the user's identifier.
    /// </summary>
    string Id { get; set; }

    /// <summary>
    /// Gets or sets the user's username.
    /// </summary>
    string Username { get; set; }

    /// <summary>
    /// Gets or sets the user's email.
    /// </summary>
    string Email { get; set; }
}