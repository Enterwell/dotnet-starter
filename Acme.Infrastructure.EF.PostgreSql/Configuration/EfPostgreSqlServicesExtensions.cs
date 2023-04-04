using Acme.Core.Books.Interfaces;
using Acme.Core.Management.Interfaces;
using Acme.Core.Users.Interfaces;
using Acme.Infrastructure.EF.PostgreSql.Books;
using Acme.Infrastructure.EF.PostgreSql.Management;
using Acme.Infrastructure.EF.PostgreSql.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Acme.Infrastructure.EF.PostgreSql.Configuration;

/// <summary>
/// Entity Framework PostgreSQL services installer extensions.
/// </summary>
public static class EfPostgreSqlServicesExtensions
{
    /// <summary>
    /// Adds Entity Framework PostgreSQL infrastructure.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/> instance.</param>
    /// <param name="configuration"><see cref="IConfiguration"/> instance.</param>
    /// <param name="isContextTransient">Boolean representing should DB context be configured as transient service.</param>
    /// <returns><see cref="IServiceCollection"/> instance.</returns>
    public static IServiceCollection AddEfPostgreSqlInfrastructure(this IServiceCollection services, IConfiguration configuration, bool isContextTransient = false)
    {
        // Register services
        RegisterServices(services);

        // Register repositories
        RegisterRepositories(services);

        // Register PostgreSQL DB context and .NET Core Identity
        RegisterContextAndIdentity(services, configuration, isContextTransient);

        return services;
    }

    /// <summary>
    /// Registers Entity Framework PostgreSQL services.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/> instance.</param>
    private static void RegisterServices(IServiceCollection services)
    {
        services
            .AddTransient<IDatabaseManagementService, DatabaseManagementService>()
            .AddTransient<IDatabaseSeedService, DatabaseSeedService>();
    }

    /// <summary>
    /// Registers Entity Framework PostgreSQL repositories.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/> instance.</param>
    private static void RegisterRepositories(IServiceCollection services)
    {
        services
            .AddTransient<IApplicationUsersRepository, EfPostgreSqlApplicationUsersRepository>()
            .AddTransient<IBooksRepository, EfPostgreSqlBooksRepository>();
    }

    /// <summary>
    /// Registers PostgreSQL DB context and Identity.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/> instance.</param>
    /// <param name="configuration"><see cref="IConfiguration"/> instance.</param>
    /// <param name="isContextTransient">Boolean representing should DB context be configured as transient service.</param>
    private static void RegisterContextAndIdentity(IServiceCollection services, IConfiguration configuration, bool isContextTransient)
    {
        var configurationSection = configuration.GetSection("PostgreSQL");

        var connKey = "ConnectionString";
        var connString = configurationSection[connKey] ?? throw new ArgumentNullException(connKey);

        var serviceLifetime = isContextTransient ? ServiceLifetime.Transient : ServiceLifetime.Scoped;
        services
            .AddDbContext<ApplicationDbContext>(options =>
                    options
                        .UseNpgsql(connString, o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)),
                serviceLifetime, serviceLifetime)
            .AddDatabaseDeveloperPageExceptionFilter();

        // Add .NET Core Identity
        services.AddIdentityCore<DbApplicationUser>(options =>
            {
                options.SignIn.RequireConfirmedEmail = false;
                options.User.RequireUniqueEmail = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>();
    }
}