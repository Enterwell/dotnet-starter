using Acme.Tests;
using Acme.Tests.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Acme.Application.Tests;

/// <summary>
/// Application unit test base class.
/// Used as a test context shared with xUnit <see cref="IClassFixture{TFixture}"/>
/// Must have a single parameterless constructor.
/// </summary>
/// <seealso cref="UnitTestBase"/>
public class ApplicationUnitTestBase<TMockedServices> : UnitTestBase where TMockedServices : IAddServices, new()
{
    /// <summary>
    /// Mocked services for the specific test class.
    /// </summary>
    public TMockedServices MockedServices;

    /// <summary>
    /// Overridable method for adding custom services to the DI container.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/> collection of services.</param>
    protected override void AddServices(IServiceCollection services)
    {
        base.AddServices(services);

        // Add mock dependencies
        this.MockedServices = new TMockedServices();
        this.MockedServices.AddServices(services);
    }
}