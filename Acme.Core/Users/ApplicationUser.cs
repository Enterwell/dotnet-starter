using Acme.Core.Users.Interfaces;

namespace Acme.Core.Users;

/// <summary>
/// Application user class.
/// </summary>
/// <seealso cref="IApplicationUser"/>
public class ApplicationUser : IApplicationUser
{
    /// <summary>
    /// Gets or sets the user's identifier.
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// Gets or sets the user's username.
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    /// Gets or sets the user's email.
    /// </summary>
    public string Email { get; set; }
}