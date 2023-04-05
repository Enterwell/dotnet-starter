using Acme.Core.Management.Interfaces;
using Enterwell.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Acme.Infrastructure.EF.PostgreSql.Management;

/// <summary>
/// Database management service class.
/// </summary>
/// <seealso cref="IDatabaseManagementService"/>
public class DatabaseManagementService : IDatabaseManagementService
{
    private readonly ApplicationDbContext context;
    private readonly IDatabaseSeedService databaseSeedService;
    private readonly ILogger<DatabaseManagementService> logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="DatabaseManagementService"/> class.
    /// </summary>
    /// <param name="context">Application DB context.</param>
    /// <param name="databaseSeedService">Database seed service.</param>
    /// <param name="logger">The logger.</param>
    /// <exception cref="ArgumentNullException">
    /// context
    /// or
    /// databaseSeedService
    /// or
    /// logger
    /// </exception>
    public DatabaseManagementService(ApplicationDbContext context, IDatabaseSeedService databaseSeedService, ILogger<DatabaseManagementService> logger)
    {
        this.context = context ?? throw new ArgumentNullException(nameof(context));
        this.databaseSeedService = databaseSeedService ?? throw new ArgumentNullException(nameof(databaseSeedService));
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Asserts that migrations are up-to-date asynchronously.
    /// </summary>
    /// <param name="cancellationToken">(Optional) A token that allows processing to be cancelled.</param>
    /// <returns>
    /// An asynchronous task that asserts migrations.
    /// </returns>
    public async Task AssertMigrationsAsync(CancellationToken cancellationToken = default)
    {
        var pendingMigrations = await this.context.Database.GetPendingMigrationsAsync(cancellationToken);
        var pendingMigrationsList = pendingMigrations.ToList();

        if (pendingMigrationsList.Any())
        {
            var pendingMigrationsListNames = string.Join(", ", pendingMigrationsList);

            throw new EnterwellException(
                $"Following migrations are waiting to be applied: {pendingMigrationsListNames}");
        }
    }

    /// <summary>
    /// Migrates the database asynchronously.
    /// </summary>
    /// <param name="targetMigration">(Optional) The target migration.</param>
    /// <param name="cancellationToken">(Optional) A token that allows processing to be cancelled.</param>
    /// <returns>
    /// An asynchronous task that migrates the database.
    /// </returns>
    public async Task MigrateAsync(string? targetMigration = null, CancellationToken cancellationToken = default)
    {
        string? finalMigration = targetMigration;

        // Migrate to the latest migration if the explicit migration is empty
        if (string.IsNullOrWhiteSpace(finalMigration))
        {
            await this.context.Database.MigrateAsync(cancellationToken);
            finalMigration = "Latest";
        }
        else if (finalMigration == "InitialMigration")
        {
            await this.context.Database.EnsureDeletedAsync(cancellationToken);
            await this.MigrateToTargetMigrationAsync(finalMigration);
            finalMigration = "InitialMigration";
        }
        else
        {
            await this.MigrateToTargetMigrationAsync(finalMigration);
        }

        await this.databaseSeedService.SeedDatabaseAsync();

        this.logger.LogInformation($"Database successfully migrated to {finalMigration}");
    }

    /// <summary>
    /// Migrates the database to target migration asynchronously.
    /// </summary>
    /// <param name="targetMigration">The target migration.</param>
    /// <returns>
    /// An asynchronous task that migrates the database.
    /// </returns>
    private async Task MigrateToTargetMigrationAsync(string targetMigration)
    {
        var isValidMigration = this.context.Database
            .GetMigrations()
            .Any(x => x[x.IndexOf('_')..].Contains(targetMigration));

        // Check if migration valid
        if (!isValidMigration)
        {
            throw new InvalidOperationException($"{targetMigration} is not a valid migration name!");
        }

        var migrator = this.context.GetInfrastructure().GetService<IMigrator>();

        // Check if resolved
        if (migrator == null)
        {
            throw new EnterwellException($"Migrator service could not be resolved");
        }

        await migrator.MigrateAsync(targetMigration);
    }
}