using Acme.Infrastructure.EF.PostgreSql.Tests.Mocks;
using Acme.Tests.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Acme.Application.Tests.ServiceRegistrars;

/// <summary>
/// Books service tests mocked dependencies.
/// </summary>
public class BooksServiceTestsMocks : IAddServices
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BooksServiceTestsMocks"/> class.
    /// </summary>
    public BooksServiceTestsMocks()
    {
        this.BooksRepositoryMock = new BooksRepositoryMock();
    }

    /// <summary>
    /// Adds mocked services to the testing DI container.
    /// </summary>
    /// <param name="services"><seealso cref="IServiceCollection"/> instance.</param>
    public void AddServices(IServiceCollection services)
    {
        services.AddSingleton(this.BooksRepositoryMock.Object);
    }

    /// <summary>
    /// Gets or sets the books repository mock.
    /// </summary>
    public BooksRepositoryMock BooksRepositoryMock { get; }
}