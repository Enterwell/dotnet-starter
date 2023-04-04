using Acme.Core.Users;
using Acme.Core.Users.Interfaces;
using Acme.Tests;
using Acme.Tests.Helpers;
using Enterwell.Exceptions;
using FluentAssertions;

namespace Acme.Infrastructure.EF.PostgreSql.Tests.Repositories;

/// <summary>
/// Entity Framework PostgreSQL application users repository tests class.
/// </summary>
/// <seealso cref="IntegrationTestBase"/>
/// <seealso cref="IDisposable"/>
public class EfPostgreSqlApplicationUsersRepositoryTests : IClassFixture<IntegrationTestBase>, IDisposable
{
    private readonly IApplicationUsersRepository applicationUsersRepository;

    private readonly IntegrationTestBase testBase;
    private readonly ApplicationDbContext context;
    private readonly DatabaseEntityFactory entityFactory;

    /// <summary>
    /// Initializes a new instance of the <see cref="EfPostgreSqlApplicationUsersRepositoryTests"/> class.
    /// </summary>
    /// <param name="testBase">Integration test base.</param>
    public EfPostgreSqlApplicationUsersRepositoryTests(IntegrationTestBase testBase)
    {
        this.applicationUsersRepository = testBase.Resolve<IApplicationUsersRepository>();

        this.testBase = testBase;
        this.context = testBase.Context;
        this.entityFactory = testBase.EntityFactory;
    }

    /// <summary>
    /// Tests that the <see cref="IApplicationUsersRepository.GetByEmailAndPasswordAsync"/> throws an <see cref="ArgumentNullException"/>
    /// when called with <c>null</c> email.
    /// </summary>
    [Fact]
    public async Task GetByEmailAndPasswordAsync_NullEmail_ThrowsAnException()
    {
        // Arrange.
        string? email = null;
        const string password = "password123";

        // Act.
        Func<Task> act = () => this.applicationUsersRepository.GetByEmailAndPasswordAsync(email!, password);

        // Assert.
        await act.Should().ThrowExactlyAsync<ArgumentNullException>();
    }

    /// <summary>
    /// Tests that the <see cref="IApplicationUsersRepository.GetByEmailAndPasswordAsync"/> throws an <see cref="ArgumentNullException"/>
    /// when called with <c>null</c> password.
    /// </summary>
    [Fact]
    public async Task GetByEmailAndPasswordAsync_NullPassword_ThrowsAnException()
    {
        // Arrange.
        const string email = "test@email.com";
        string? password = null;
        
        // Act.
        Func<Task> act = () => this.applicationUsersRepository.GetByEmailAndPasswordAsync(email, password!);

        // Assert.
        await act.Should().ThrowExactlyAsync<ArgumentNullException>();
    }

    /// <summary>
    /// Tests that the <see cref="IApplicationUsersRepository.GetByEmailAndPasswordAsync"/> returns <c>null</c> when the user
    /// with the given email does not exist.
    /// </summary>
    [Fact]
    public async Task GetByEmailAndPasswordAsync_WithNonExistingUser_ReturnsNull()
    {
        // Arrange.
        const string email = "test@email.com";
        const string password = "password";

        // Act.
        var fetchedUser = await this.applicationUsersRepository.GetByEmailAndPasswordAsync(email, password);

        // Assert.
        fetchedUser.Should().BeNull();
    }

    /// <summary>
    /// Tests that the <see cref="IApplicationUsersRepository.GetByEmailAndPasswordAsync"/> returns <c>null</c> when the user
    /// with the given email exists but the given password is invalid.
    /// </summary>
    [Fact]
    public async Task GetByEmailAndPasswordAsync_InvalidPassword_ReturnsNull()
    {
        // Arrange.
        var existingUser = await this.entityFactory.CreateUserAsync("existing@user.com", "pa$$w0rd");

        // Act.
        var fetchedUser =
            await this.applicationUsersRepository.GetByEmailAndPasswordAsync(existingUser.Email!, "invalidPassword");

        // Assert.
        fetchedUser.Should().BeNull();
    }

    /// <summary>
    /// Tests that the <see cref="IApplicationUsersRepository.GetByEmailAndPasswordAsync"/> returns correct <see cref="IApplicationUser"/>
    /// model when the correct params are passed in.
    /// </summary>
    [Fact]
    public async Task GetByEmailAndPasswordAsync_WithValidParams_ReturnsCorrectly()
    {
        // Arrange.
        const string validPassword = "pa$$w0rd";

        var existingUser = await this.entityFactory.CreateUserAsync("existing@user.com", validPassword);

        // Act.
        var fetchedUser =
            await this.applicationUsersRepository.GetByEmailAndPasswordAsync(existingUser.Email!, validPassword);

        // Assert.
        fetchedUser.Should().NotBeNull();

        fetchedUser!.Id.Should().Be(existingUser.Id);
        fetchedUser.Email.Should().Be(existingUser.Email);
    }

    /// <summary>
    /// Tests that the <see cref="IApplicationUsersRepository.GetByIdAsync"/> returns <c>null</c> if the user with the
    /// given identifier does not exist.
    /// </summary>
    [Fact]
    public async Task GetByIdAsync_WithNonExistingUser_ReturnsNull()
    {
        // Arrange.
        var testId = Guid.NewGuid().ToString();

        // Act.
        var fetchedUser = await this.applicationUsersRepository.GetByIdAsync(testId);

        // Assert.
        fetchedUser.Should().BeNull();
    }

    /// <summary>
    /// Tests that the <see cref="IApplicationUsersRepository.GetByIdAsync"/> returns correct user when valid identifier is passed in.
    /// </summary>
    [Fact]
    public async Task GetByIdAsync_WithValidParams_ReturnsCorrectly()
    {
        // Arrange.
        var existingUser = await this.entityFactory.CreateUserAsync("existing@user.com", "pa$$w0rd");

        // Act.
        var fetchedUser = await this.applicationUsersRepository.GetByIdAsync(existingUser.Id);

        // Assert.
        fetchedUser.Should().NotBeNull();

        fetchedUser!.Id.Should().Be(existingUser.Id);
        fetchedUser!.Email.Should().Be(existingUser.Email);
    }

    /// <summary>
    /// Tests that the <see cref="IApplicationUsersRepository.CreateAsync"/> throws an <see cref="ArgumentNullException"/> when the create
    /// model is <c>null</c>.
    /// </summary>
    [Fact]
    public async Task CreateAsync_NullCreateModel_ThrowsAnException()
    {
        // Arrange.
        IApplicationUserCreate? userCreate = null;

        // Act.
        Func<Task> act = () => this.applicationUsersRepository.CreateAsync(userCreate!);

        // Assert.
        await act.Should().ThrowExactlyAsync<ArgumentNullException>();
    }

    /// <summary>
    /// Tests that the <see cref="IApplicationUsersRepository.CreateAsync"/> throws an <see cref="EnterwellException"/> when trying to create
    /// a user with invalid password.
    /// </summary>
    [Fact]
    public async Task CreateAsync_InvalidPassword_ThrowsAnException()
    {
        // Arrange.
        var userCreate = new ApplicationUserCreate
        {
            Email = "test@user.com",
            Username = "user",
            Password = "invalidPassword"
        };

        // Act.
        Func<Task> act = () => this.applicationUsersRepository.CreateAsync(userCreate);

        // Assert.
        await act.Should().ThrowExactlyAsync<EnterwellException>();
    }

    /// <summary>
    /// Tests that the <see cref="IApplicationUsersRepository.CreateAsync"/> correctly stores the user to the database
    /// when valid params are passed in and correctly returns its identifier.
    /// </summary>
    [Fact]
    public async Task CreateAsync_WithValidParams_ReturnsCorrectly()
    {
        // Arrange.
        var userCreate = new ApplicationUserCreate
        {
            Email = "test@user.com",
            Username = "user",
            Password = "pa$$w0rd"
        };

        // Act.
        var createdUserId = await this.applicationUsersRepository.CreateAsync(userCreate);

        var userFromDb = await this.context.Users.FindAsync(createdUserId);

        // Assert.
        createdUserId.Should().NotBeNull();

        userFromDb.Should().NotBeNull();
        userFromDb!.Email.Should().Be(userCreate.Email);
    }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
        this.testBase.PerformActionWithEntityDetach(() =>
        {
            this.context.Users.RemoveAll();
        });
    }
}