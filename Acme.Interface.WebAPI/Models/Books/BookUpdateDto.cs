namespace Acme.Interface.WebAPI.Models.Books;

/// <summary>
/// Book update DTO.
/// </summary>
/// <seealso cref="BookCreateDto"/>
public class BookUpdateDto : BookCreateDto
{
    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    public string Id { get; set; }
}