using Acme.Infrastructure.EF.PostgreSql.Tests.Mocks;
using Acme.Tests.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Acme.Application.Tests.ServiceRegistrars;

/// <summary>
/// Management service tests mocked dependencies.
/// </summary>
/// <seealso cref="IAddServices"/>
public class ManagementServiceTestsMocks : IAddServices
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ManagementServiceTestsMocks"/> class.
    /// </summary>
    public ManagementServiceTestsMocks()
    {
        this.DatabaseManagementServiceMock = new DatabaseManagementServiceMock();
    }

    /// <summary>
    /// Adds mocked services to the testing DI container.
    /// </summary>
    /// <param name="services"><seealso cref="IServiceCollection"/> instance.</param>
    public void AddServices(IServiceCollection services)
    {
        services.AddSingleton(this.DatabaseManagementServiceMock.Object);
    }

    /// <summary>
    /// Gets or sets the database management service mock.
    /// </summary>
    public DatabaseManagementServiceMock DatabaseManagementServiceMock { get; set; }
}