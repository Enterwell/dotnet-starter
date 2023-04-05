using AutoMapper;

namespace Acme.Tests.AutoMapperProfile;

/// <summary>
/// AutoMapper profile tests class.
/// </summary>
public class AutoMapperProfileTests
{
    /// <summary>
    /// Tests to see if any AutoMapper destination does not have all of its properties initialized.
    /// </summary>
    [Fact]
    public void AutoMapper_ValidatesMappingMethods_ShouldBeValid()
    {
        new MapperConfiguration(config =>
        {
            var profiles = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => a.FullName != null && a.FullName.StartsWith("Acme"))
                .Select(a => a.GetTypes().Where(t => typeof(Profile).IsAssignableFrom(t)))
                .SelectMany(s => s)
                .Distinct();

            foreach (var profile in profiles)
            {
                config.AddProfile(Activator.CreateInstance(profile) as Profile);
            }

        }).AssertConfigurationIsValid();
    }
}