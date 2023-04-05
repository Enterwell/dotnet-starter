using Acme.Infrastructure.EF.PostgreSql;
using Acme.Tests.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Acme.Tests;

/// <summary>
/// Integration test base class.
/// Used as a test context shared with xUnit <see cref="IClassFixture{TFixture}"/>.
/// Must have a single parameterless constructor. 
/// </summary>
public class IntegrationTestBase : TestBase
{
    /// <summary>
    /// Application database context.
    /// </summary>
    public ApplicationDbContext Context => base.Resolve<ApplicationDbContext>();

    /// <summary>
    /// Database entity factory helper.
    /// </summary>
    public DatabaseEntityFactory EntityFactory => base.Resolve<DatabaseEntityFactory>();

    /// <summary>
    /// Adds custom test services to the DI container.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/> instance.</param>
    protected override void AddServices(IServiceCollection services)
    {
        services.AddTransient<DatabaseEntityFactory>();
    }

    /// <summary>
    /// Performs the action with entity detach.
    /// </summary>
    /// <param name="action">The action to invoke.</param>
    public void PerformActionWithEntityDetach(Action action)
    {
        try
        {
            action.Invoke();

            this.Context.SaveChanges();
        }
        catch (DbUpdateException)
        {
            foreach (var entry in this.Context.ChangeTracker.Entries().ToArray())
            {
                entry.State = EntityState.Detached;
            }
        }
    }
}