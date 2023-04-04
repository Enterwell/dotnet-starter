using Acme.Application.Tests.Mocks;
using Acme.Tests.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Acme.Interface.WebAPI.Tests.ServiceRegistrars;

/// <summary>
/// Authentication web API service tests mocked dependencies.
/// </summary>
public class AuthenticationApiServiceTestsMocks : IAddServices
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AuthenticationApiServiceTestsMocks"/> class.
    /// </summary>
    public AuthenticationApiServiceTestsMocks()
    {
        this.AuthenticationServiceMock = new AuthenticationServiceMock();
    }

    /// <summary>
    /// Adds mocked services to the testing DI container.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/> instance.</param>
    public void AddServices(IServiceCollection services)
    {
        services.AddSingleton(this.AuthenticationServiceMock.Object);
    }

    /// <summary>
    /// Gets or sets the authentication service mock.
    /// </summary>
    public AuthenticationServiceMock AuthenticationServiceMock { get; set; }
}