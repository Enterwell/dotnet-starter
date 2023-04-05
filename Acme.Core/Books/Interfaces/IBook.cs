namespace Acme.Core.Books.Interfaces;

/// <summary>
/// Book interface.
/// </summary>
public interface IBook
{
    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    string Id { get; set; }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    string Name { get; set; }

    /// <summary>
    /// Gets or sets the price.
    /// </summary>
    decimal Price { get; set; }

    /// <summary>
    /// Gets or sets the category.
    /// </summary>
    string Category { get; set; }

    /// <summary>
    /// Gets or sets the author.
    /// </summary>
    string Author { get; set; }
}