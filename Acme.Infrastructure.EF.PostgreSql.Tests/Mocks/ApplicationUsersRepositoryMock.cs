using Acme.Core.Users;
using Acme.Core.Users.Interfaces;
using Moq;

namespace Acme.Infrastructure.EF.PostgreSql.Tests.Mocks;

/// <summary>
/// Application users repository mock class.
/// </summary>
/// <seealso cref="IApplicationUsersRepository"/>
public class ApplicationUsersRepositoryMock : Mock<IApplicationUsersRepository>
{
    /// <summary>
    /// Setups the <see cref="IApplicationUsersRepository.GetByEmailAndPasswordAsync"/> repository method.
    /// </summary>
    /// <param name="email">User's email.</param>
    /// <param name="result">Result to return on the method call.</param>
    /// <returns><see cref="ApplicationUsersRepositoryMock"/> instance.</returns>
    public ApplicationUsersRepositoryMock SetupGetByEmailAndPasswordAsync(string email, IApplicationUser? result)
    {
        this.Setup(x => x.GetByEmailAndPasswordAsync(email, It.IsAny<string>())).ReturnsAsync(result);

        return this;
    }

    /// <summary>
    /// Verifies that the <see cref="IApplicationUsersRepository.GetByEmailAndPasswordAsync"/> repository method was called specified number of times for the given email.
    /// </summary>
    /// <param name="email">User's email.</param>
    /// <param name="times">Number of times the method should have been called.</param>
    /// <returns><see cref="ApplicationUsersRepositoryMock"/> instance.</returns>
    public ApplicationUsersRepositoryMock VerifyGetByEmailAndPasswordAsync(string email, Times times)
    {
        this.Verify(x => x.GetByEmailAndPasswordAsync(email, It.IsAny<string>()), times);

        return this;
    }

    /// <summary>
    /// Setups the <see cref="IApplicationUsersRepository.GetByIdAsync"/> repository method.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="result">Result to return on the method call.</param>
    /// <returns><see cref="ApplicationUsersRepositoryMock"/> instance.</returns>
    public ApplicationUsersRepositoryMock SetupGetByIdAsync(string id, IApplicationUser? result)
    {
        this.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(result);

        return this;
    }

    /// <summary>
    /// Verifies that the <see cref="IApplicationUsersRepository.GetByIdAsync"/> repository method was called specified number of times for the given identifier.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="times">Number of times the method should have been called.</param>
    /// <returns><see cref="ApplicationUsersRepositoryMock"/> instance.</returns>
    public ApplicationUsersRepositoryMock VerifyGetByIdAsync(string id, Times times)
    {
        this.Verify(x => x.GetByIdAsync(id), times);

        return this;
    }

    /// <summary>
    /// Setups the <see cref="IApplicationUsersRepository.CreateAsync"/> repository method.
    /// </summary>
    /// <param name="result">Result to return on the method call.</param>
    /// <returns><see cref="ApplicationUsersRepositoryMock"/> instance.</returns>
    public ApplicationUsersRepositoryMock SetupCreateAsync(string result)
    {
        this.Setup(x => x.CreateAsync(It.IsAny<IApplicationUserCreate>())).ReturnsAsync(result);

        return this;
    }

    /// <summary>
    /// Verifies that the <see cref="IApplicationUsersRepository.CreateAsync"/> repository method was called specified number of times with the given model.
    /// </summary>
    /// <param name="createModel">Explicit create model.</param>
    /// <param name="times">Number of times the method should have been called.</param>
    /// <returns><see cref="ApplicationUsersRepositoryMock"/> instance.</returns>
    public ApplicationUsersRepositoryMock VerifyCreateAsync(ApplicationUserCreate createModel, Times times)
    {
        this.Verify(x => x.CreateAsync(createModel), times);

        return this;
    }
}