namespace Acme.Interface.WebAPI.Models.Books;

/// <summary>
/// Book create DTO.
/// </summary>
public class BookCreateDto
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