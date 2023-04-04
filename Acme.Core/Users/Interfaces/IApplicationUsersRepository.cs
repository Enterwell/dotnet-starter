namespace Acme.Core.Users.Interfaces;

/// <summary>
/// Application users repository interface.
/// </summary>
public interface IApplicationUsersRepository
{
    /// <summary>
    /// Gets the user by its email and password asynchronously.
    /// </summary>
    /// <param name="email">User's email.</param>
    /// <param name="password">User's password.</param>
    /// <returns>
    /// A task returning <see cref="IApplicationUser"/> if found, <c>null</c> otherwise.
    /// </returns>
    Task<IApplicationUser?> GetByEmailAndPasswordAsync(string email, string password);

    /// <summary>
    /// Gets the application user by its identifier asynchronously.
    /// </summary>
    /// <param name="id">Application user's identifier.</param>
    /// <returns>
    /// A task returning <see cref="IApplicationUser"/> if found, <c>null</c> otherwise.
    /// </returns>
    Task<IApplicationUser?> GetByIdAsync(string id);

    /// <summary>
    /// Creates a new application user asynchronously.
    /// </summary>
    /// <param name="userCreate">The application user create.</param>
    /// <returns>
    /// A task returning an identifier of the newly created user.
    /// </returns>
    Task<string> CreateAsync(IApplicationUserCreate userCreate);
}