using Acme.Infrastructure.EF.PostgreSql.Tests.Mocks;
using Acme.Tests.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Acme.Application.Tests.ServiceRegistrars;

/// <summary>
/// Authentication service tests mocked dependencies.
/// </summary>
/// <seealso cref="IAddServices"/>
public class AuthenticationServiceTestsMocks : IAddServices
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationUsersRepositoryMock"/> class.
    /// </summary>
    public AuthenticationServiceTestsMocks()
    {
        this.ApplicationUsersRepositoryMock = new ApplicationUsersRepositoryMock();
    }

    /// <summary>
    /// Adds mocked services to the testing DI container.
    /// </summary>
    /// <param name="services"><seealso cref="IServiceCollection"/> instance.</param>
    public void AddServices(IServiceCollection services)
    {
        services.AddSingleton(this.ApplicationUsersRepositoryMock.Object);
    }

    /// <summary>
    /// Gets or sets the application users repository mock.
    /// </summary>
    public ApplicationUsersRepositoryMock ApplicationUsersRepositoryMock { get; set; }
}