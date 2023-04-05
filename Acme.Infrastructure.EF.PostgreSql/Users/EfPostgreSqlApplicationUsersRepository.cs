using Acme.Core.CodeExtensions;
using Acme.Core.Users.Interfaces;
using AutoMapper;
using Enterwell.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Acme.Infrastructure.EF.PostgreSql.Users;

/// <summary>
/// Entity Framework PostgreSQL users repository class.
/// </summary>
/// <seealso cref="IApplicationUsersRepository"/>
public class EfPostgreSqlApplicationUsersRepository : IApplicationUsersRepository
{
    private readonly ApplicationDbContext context;
    private readonly UserManager<DbApplicationUser> userManager;
    private readonly IMapper mapper;
    private readonly ILogger<EfPostgreSqlApplicationUsersRepository> logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="EfPostgreSqlApplicationUsersRepository"/> class.
    /// </summary>
    /// <param name="context">Application DB context.</param>
    /// <param name="userManager">User manager.</param>
    /// <param name="mapper">The mapper.</param>
    /// <param name="logger">The logger.</param>
    /// <exception cref="ArgumentNullException">
    /// context
    /// or
    /// userManager
    /// or
    /// mapper
    /// or
    /// logger
    /// </exception>
    public EfPostgreSqlApplicationUsersRepository(ApplicationDbContext context, UserManager<DbApplicationUser> userManager, IMapper mapper, ILogger<EfPostgreSqlApplicationUsersRepository> logger)
    {
        this.context = context ?? throw new ArgumentNullException(nameof(context));
        this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Gets the user by its email and password asynchronously.
    /// </summary>
    /// <param name="email">User's email.</param>
    /// <param name="password">User's password.</param>
    /// <returns>
    /// A task returning <see cref="IApplicationUser"/> if found, <c>null</c> otherwise.
    /// </returns>
    public async Task<IApplicationUser?> GetByEmailAndPasswordAsync(string email, string password)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentNullException(nameof(email));
        }

        if (string.IsNullOrWhiteSpace(password))
        {
            throw new ArgumentNullException(nameof(password));
        }

        var user = await this.context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());

        // Check if found
        if (user == null) return null;

        var isValidPassword = await this.userManager.CheckPasswordAsync(user, password);

        return isValidPassword ? user.MapTo<IApplicationUser>(this.mapper) : null;
    }

    /// <summary>
    /// Gets the application user by its identifier asynchronously.
    /// </summary>
    /// <param name="id">Application user's identifier.</param>
    /// <returns>
    /// A task returning <see cref="IApplicationUser"/> if found, <c>null</c> otherwise.
    /// </returns>
    public async Task<IApplicationUser?> GetByIdAsync(string id)
    {
        var user = await this.context.Users.FindAsync(id);

        return user?.MapTo<IApplicationUser>(this.mapper);
    }

    /// <summary>
    /// Creates a new application user asynchronously.
    /// </summary>
    /// <param name="userCreate">The application user create.</param>
    /// <returns>
    /// A task returning an identifier of the newly created user.
    /// </returns>
    public async Task<string> CreateAsync(IApplicationUserCreate userCreate)
    {
        ArgumentNullException.ThrowIfNull(userCreate);

        var dbUser = new DbApplicationUser
        {
            UserName = userCreate.Username,
            Email = userCreate.Email
        };

        // Create user
        var result = await this.userManager.CreateAsync(dbUser, userCreate.Password);

        // Check if failed
        if (!result.Succeeded)
        {
            var errors = string.Join("; ", result.Errors.Select(r => r.Code));

            this.logger.LogError($"Failed to create the application user. {errors}");
            throw new EnterwellException("Failed to create the application user.");
        }

        this.logger.LogInformation($"User {dbUser.UserName} created successfully.");

        return dbUser.Id;
    }
}