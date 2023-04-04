namespace Acme.Core.Books.Interfaces;

/// <summary>
/// Book update interface.
/// </summary>
/// <seealso cref="IBookCreate"/>
public interface IBookUpdate : IBookCreate
{
    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    string Id { get; set; }
}