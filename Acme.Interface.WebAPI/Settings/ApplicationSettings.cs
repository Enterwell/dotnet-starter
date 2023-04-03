using Acme.Core.Authentication;

namespace Acme.Interface.WebAPI.Settings;

/// <summary>
/// Application settings.
/// </summary>
public class ApplicationSettings
{
    /// <summary>
    /// JSON web token configuration.
    /// </summary>
    public TokenConfiguration TokenConfiguration { get; set; }
}