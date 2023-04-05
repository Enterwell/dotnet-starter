using Acme.Core.Users.Interfaces;

namespace Acme.Core.Users;

/// <summary>
/// Application user create class.
/// </summary>
/// <seealso cref="IApplicationUserCreate"/>
public class ApplicationUserCreate : IApplicationUserCreate
{
    /// <summary>
    /// Gets or sets the user's username.
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    /// Gets or sets the user's email.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets the user's password.
    /// </summary>
    public string Password { get; set; }
}