using Acme.Core.Books.Interfaces;

namespace Acme.Core.Books;

/// <summary>
/// Book create class.
/// </summary>
/// <seealso cref="IBookCreate"/>
public class BookCreate : IBookCreate
{
    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the price.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Gets or sets the category.
    /// </summary>
    public string Category { get; set; }

    /// <summary>
    /// Gets or sets the author.
    /// </summary>
    public string Author { get; set; }
}