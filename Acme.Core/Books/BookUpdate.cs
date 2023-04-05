using Acme.Core.Books.Interfaces;

namespace Acme.Core.Books;

/// <summary>
/// Book update class.
/// </summary>
/// <seealso cref="BookCreate"/>
/// <seealso cref="IBookUpdate"/>
public class BookUpdate : BookCreate, IBookUpdate
{
    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    public string Id { get; set; }
}