using Acme.Core.Authentication.Interfaces;

namespace Acme.Core.Settings;

/// <summary>
/// Application settings provider interface.
/// </summary>
public interface IApplicationSettingsProvider
{
    /// <summary>
    /// JSON web token configuration.
    /// </summary>
    ITokenConfiguration TokenConfiguration { get; }
}