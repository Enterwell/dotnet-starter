using Acme.Core.Users.Interfaces;
using Enterwell.Exceptions;

namespace Acme.Application.Users;

/// <summary>
/// Application users service class.
/// </summary>
/// <seealso cref="IApplicationUsersService"/>
public class ApplicationUsersService : IApplicationUsersService
{
    private readonly IApplicationUsersRepository applicationUsersRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationUsersService"/> class.
    /// </summary>
    /// <param name="applicationUsersRepository">Application user repository.</param>
    /// <exception cref="ArgumentNullException">
    /// applicationUserRepository
    /// </exception>
    public ApplicationUsersService(IApplicationUsersRepository applicationUsersRepository)
    {
        this.applicationUsersRepository = applicationUsersRepository ?? throw new ArgumentNullException(nameof(applicationUsersRepository));
    }

    /// <summary>
    /// Gets the application user by its identifier asynchronously.
    /// </summary>
    /// <param name="id">Application user's identifier.</param>
    /// <returns>
    /// A task returning <see cref="IApplicationUser"/> if found, <c>null</c> otherwise.
    /// </returns>
    public Task<IApplicationUser?> GetByIdAsync(string id) =>
        this.applicationUsersRepository.GetByIdAsync(id);

    /// <summary>
    /// Creates a new application user asynchronously.
    /// </summary>
    /// <param name="userCreate">The application user create.</param>
    /// <returns>
    /// A task returning newly created <see cref="IApplicationUser"/>.
    /// </returns>
    public async Task<IApplicationUser> CreateAsync(IApplicationUserCreate userCreate)
    {
        ArgumentNullException.ThrowIfNull(userCreate);

        var createdUserId = await this.applicationUsersRepository.CreateAsync(userCreate);

        var createdUser = await this.GetByIdAsync(createdUserId);

        // Check if found
        if (createdUser == null)
        {
            throw new EnterwellException(
                $"Could not find application user with the newly created identifier. {createdUserId}");
        }

        return createdUser;
    }
}