namespace Acme.Infrastructure.EF.PostgreSql.Books;

/// <summary>
/// PostgreSQL database book model.
/// </summary>
public class DbBook
{
    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    public string Id { get; set; }

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