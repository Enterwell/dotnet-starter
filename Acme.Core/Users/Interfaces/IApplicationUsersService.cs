namespace Acme.Core.Users.Interfaces;

/// <summary>
/// Application users service interface.
/// </summary>
public interface IApplicationUsersService
{
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
    /// A task returning newly created <see cref="IApplicationUser"/>.
    /// </returns>
    Task<IApplicationUser> CreateAsync(IApplicationUserCreate userCreate);
}