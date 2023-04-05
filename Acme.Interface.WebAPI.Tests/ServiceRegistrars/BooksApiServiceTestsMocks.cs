using Acme.Application.Tests.Mocks;
using Acme.Tests.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Acme.Interface.WebAPI.Tests.ServiceRegistrars;

/// <summary>
/// Books web API service tests mocked dependencies.
/// </summary>
public class BooksApiServiceTestsMocks : IAddServices
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BooksApiServiceTestsMocks"/> class.
    /// </summary>
    public BooksApiServiceTestsMocks()
    {
        this.BooksServiceMock = new BooksServiceMock();
    }

    /// <summary>
    /// Adds mocked services to the testing DI container.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/> instance.</param>
    public void AddServices(IServiceCollection services)
    {
        services.AddSingleton(this.BooksServiceMock.Object);
    }

    /// <summary>
    /// Gets or sets the books service mock.
    /// </summary>
    public BooksServiceMock BooksServiceMock { get; set; }
}