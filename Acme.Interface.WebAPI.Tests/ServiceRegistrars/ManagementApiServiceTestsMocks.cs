using Acme.Application.Tests.Mocks;
using Acme.Tests.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Acme.Interface.WebAPI.Tests.ServiceRegistrars;

/// <summary>
/// Management web API service tests mocked dependencies.
/// </summary>
public class ManagementApiServiceTestsMocks : IAddServices
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ManagementApiServiceTestsMocks"/> class.
    /// </summary>
    public ManagementApiServiceTestsMocks()
    {
        this.ManagementServiceMock = new ManagementServiceMock();
    }

    /// <summary>
    /// Adds mocked services to the testing DI container.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/> instance.</param>
    public void AddServices(IServiceCollection services)
    {
        services.AddSingleton(this.ManagementServiceMock.Object);
    }

    /// <summary>
    /// Gets or sets the management service mock.
    /// </summary>
    public ManagementServiceMock ManagementServiceMock { get; set; }
}