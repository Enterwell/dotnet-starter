using Microsoft.Extensions.DependencyInjection;

namespace Acme.Tests.Interfaces;

/// <summary>
/// Add services interface.
/// </summary>
public interface IAddServices
{
    /// <summary>
    /// Adds mocked services to the testing DI container.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/> collection of services.</param>
    void AddServices(IServiceCollection services);
}