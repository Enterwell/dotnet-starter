namespace Acme.Interface.WebAPI.Models.Paging;

/// <summary>
/// Paged response DTO.
/// </summary>
public class PagedResponseDto<T>
{
    /// <summary>
    /// Gets or sets the paging data response DTO.
    /// </summary>
    public PagingDataResponseDto PagingData { get; set; }

    /// <summary>
    /// Gets or sets the paged items.
    /// </summary>
    public IEnumerable<T> Items { get; set; }
}