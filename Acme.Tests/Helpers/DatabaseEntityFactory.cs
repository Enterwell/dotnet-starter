using Acme.Infrastructure.EF.PostgreSql;
using Acme.Infrastructure.EF.PostgreSql.Books;
using Acme.Infrastructure.EF.PostgreSql.Users;
using Enterwell.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace Acme.Tests.Helpers;

/// <summary>
/// Database entity factory helper.
/// </summary>
public class DatabaseEntityFactory
{
    private readonly ApplicationDbContext context;
    private readonly UserManager<DbApplicationUser> userManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="DatabaseEntityFactory"/> class.
    /// </summary>
    /// <param name="context">Application DB context.</param>
    /// <param name="userManager">User manager.</param>
    /// <exception cref="ArgumentNullException">
    /// context
    /// or
    /// userManager
    /// </exception>
    public DatabaseEntityFactory(ApplicationDbContext context, UserManager<DbApplicationUser> userManager)
    {
        this.context = context ?? throw new ArgumentNullException(nameof(context));
        this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
    }

    /// <summary>
    /// Creates the application user asynchronously.
    /// </summary>
    /// <param name="email">User's email.</param>
    /// <param name="password">User's password.</param>
    /// <param name="fillProperties">Fill properties action.</param>
    /// <returns>
    /// An asynchronous task that creates and returns an instance of <see cref="DbApplicationUser"/>.
    /// </returns>
    public async Task<DbApplicationUser> CreateUserAsync(string email, string password, Action<DbApplicationUser>? fillProperties = null)
    {
        var dbUser = new DbApplicationUser
        {
            Email = email,
            UserName = email
        };

        // Fill application user properties
        fillProperties?.Invoke(dbUser);

        var result = await this.userManager.CreateAsync(dbUser, password);

        if (!result.Succeeded)
        {
            throw new EnterwellException(result.Errors.First().Description);
        }

        return dbUser;
    }

    /// <summary>
    /// Creates the book asynchronously.
    /// </summary>
    /// <param name="name">Name of the book to create.</param>
    /// <param name="fillProperties">Fill properties action.</param>
    /// <returns>
    /// An asynchronous task that creates and returns an instance of <see cref="DbBook"/>.
    /// </returns>
    public async Task<DbBook> CreateBookAsync(string name, Action<DbBook>? fillProperties = null)
    {
        var dbBook = new DbBook
        {
            Name = name,
            Category = "Test Category",
            Author = "Test Author"
        };

        // Fill book properties
        fillProperties?.Invoke(dbBook);

        this.context.Books.Add(dbBook);
        await this.context.SaveChangesAsync();

        return dbBook;
    }
}