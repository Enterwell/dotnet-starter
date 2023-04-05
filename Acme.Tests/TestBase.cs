using Acme.Application.Configuration;
using Acme.Infrastructure.EF.PostgreSql;
using Acme.Infrastructure.EF.PostgreSql.Configuration;
using Acme.Interface.WebAPI.Configuration;
using Acme.Interface.WebAPI.Configuration.InstallerExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Acme.Tests;

/// <summary>
/// Test base class.
/// </summary>
public class TestBase : IDisposable
{
    private static readonly object Lock = new();

    /// <summary>
    /// Service collection used for overriding the DI container.
    /// </summary>
    protected IServiceCollection ServiceCollection { get; private set; }

    /// <summary>
    /// Gets or sets the service provider.
    /// </summary>
    private IServiceProvider ServiceProvider { get; set; }

    /// <summary>
    /// Test base class constructor.
    /// </summary>
    /// <param name="isUnitTest"><c>True</c> if the test is unit test. In that case, it doesn't reload the database.</param>
    public TestBase(bool isUnitTest = false)
    {
        this.InitTestEnvironment();

        if (!isUnitTest)
        {
            this.ReloadDatabase();
        }
    }

    /// <summary>
    /// Tries to resolve the service of a given type.
    /// </summary>
    /// <typeparam name="T">Generic type of the service requested.</typeparam>
    /// <returns>
    /// An instance of service <see cref="T"/>
    /// </returns>
    public T Resolve<T>()
    {
        var service = this.ServiceProvider.GetService<T>();

        // Check if resolved
        if (service == null)
        {
            throw new ArgumentNullException($"Could not resolve {typeof(T)} in {typeof(TestBase)}");
        }

        return service;
    }

    private void InitTestEnvironment()
    {
        // Build the IConfiguration instance with the test application settings
        var configuration = new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile("appsettings.Test.json", optional: false, reloadOnChange: true)
            .Build();

        // Construct the ServiceCollection instance
        this.ServiceCollection = new ServiceCollection();

        // Build services
        this.ServiceCollection
            .AddLogging()
            .AddCustomConfiguration(configuration)
            .AddAutoMapper(
                typeof(ApiMapperProfile).Assembly,
                typeof(ApplicationMapperProfile).Assembly,
                typeof(EfPostgreSqlMapperProfile).Assembly
            )
            .AddApiServices()
            .AddApplicationServices()
            .AddEfPostgreSqlInfrastructure(configuration);

        // Add custom services to the DI container
        this.AddServices(this.ServiceCollection);

        // Build the service provider
        this.ServiceProvider = this.ServiceCollection.BuildServiceProvider();
    }

    /// <summary>
    /// Reloads the database between test classes.
    /// </summary>
    private void ReloadDatabase()
    {
        lock (Lock)
        {
            // Get the database context from the service provider
            var context = this.Resolve<ApplicationDbContext>();

            // Reload database
            context.Database.EnsureDeleted();
            context.Database.Migrate();

            // Save changes
            context.SaveChanges();
        }
    }

    /// <summary>
    /// Overrideable method for adding custom services to the DI container.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/> collection of services.</param>
    protected virtual void AddServices(IServiceCollection services)
    {
        // Add services
    }

    /// <summary>
    /// Dispose pattern method.
    /// </summary>
    public void Dispose()
    {
    }
}