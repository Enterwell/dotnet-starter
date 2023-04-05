using Acme.Core.Authentication.Interfaces;
using Acme.Core.Settings.Interfaces;
using Microsoft.Extensions.Options;

namespace Acme.Interface.WebAPI.Settings;

/// <summary>
/// Application settings provider class.
/// </summary>
/// <seealso cref="IApplicationSettingsProvider"/>
public class ApplicationSettingsProvider : IApplicationSettingsProvider
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationSettingsProvider"/> class.
    /// </summary>
    /// <param name="settings">Application settings.</param>
    public ApplicationSettingsProvider(IOptions<ApplicationSettings> settings)
    {
        ArgumentNullException.ThrowIfNull(settings);

        this.TokenConfiguration = settings.Value.TokenConfiguration;
    }

    /// <summary>
    /// JSON web token configuration.
    /// </summary>
    public ITokenConfiguration TokenConfiguration { get; }
}