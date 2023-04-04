namespace Acme.Tests;

/// <summary>
/// Unit test base class.
/// Used as a test context shared with xUnit <see cref="IClassFixture{TFixture}"/>.
/// Must have a single parameterless constructor.
/// </summary>
public class UnitTestBase : TestBase
{
    /// <summary>
    /// Initializes a new instance of the <seealso cref="UnitTestBase"/>
    /// </summary>
    public UnitTestBase() : base(true)
    {
    }
}