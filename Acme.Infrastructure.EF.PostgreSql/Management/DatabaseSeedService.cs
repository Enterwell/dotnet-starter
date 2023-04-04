using Acme.Core.Management.Interfaces;
using Acme.Core.Users;
using Acme.Core.Users.Interfaces;
using Microsoft.Extensions.Logging;

namespace Acme.Infrastructure.EF.PostgreSql.Management;

/// <summary>
/// Database seed service class.
/// </summary>
/// <seealso cref="IDatabaseSeedService"/>
public class DatabaseSeedService : IDatabaseSeedService
{
    private const string AdminEmail = "admin@acme.com";

    private readonly ApplicationDbContext context;
    private readonly IApplicationUsersService applicationUsersService;
    private readonly ILogger<DatabaseSeedService> logger;

    /// <summary>
    /// Initialize a new instance of the <see cref="DatabaseSeedService"/> class.
    /// </summary>
    /// <param name="context">Application DB context.</param>
    /// <param name="applicationUsersService">Application users service.</param>
    /// <param name="logger">The logger.</param>
    /// <exception cref="ArgumentNullException">
    /// context
    /// or
    /// logger
    /// </exception>
    public DatabaseSeedService(ApplicationDbContext context, IApplicationUsersService applicationUsersService, ILogger<DatabaseSeedService> logger)
    {
        this.context = context ?? throw new ArgumentNullException(nameof(context));
        this.applicationUsersService = applicationUsersService ?? throw new ArgumentNullException(nameof(applicationUsersService));
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Seeds the database asynchronously.
    /// </summary>
    /// <returns>
    /// An asynchronous task that seeds the database.
    /// </returns>
    public async Task SeedDatabaseAsync()
    {
        try
        {
            // Seed the database.
            await this.SeedAdminUserAsync();
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    /// <summary>
    /// Seeds the admin user asynchronously.
    /// </summary>
    /// <returns>
    /// An asynchronous task that seeds the admin user.
    /// </returns>
    private async Task SeedAdminUserAsync()
    {
        try
        {
            // Return if it exists
            if (this.context.Users.Any(u => u.Email == AdminEmail))
            {
                return;
            }

            var adminUserCreate = new ApplicationUserCreate
            {
                Email = AdminEmail,
                Username = "admin",
                Password = "pa$$w0rd"
            };

            await this.applicationUsersService.CreateAsync(adminUserCreate);
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "An error occurred while seeding admin user.");
            throw;
        }
    }
}