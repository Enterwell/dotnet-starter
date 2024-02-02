using Acme.Core.Paging.Enums;

namespace Acme.Interface.WebAPI.Models.Paging;

/// <summary>
/// Paged request DTO.
/// </summary>
public class PagedRequestDto
{
    /// <summary>
    /// Gets or sets the current page.
    /// </summary>
    public int Page { get; set; } = 0;

    /// <summary>
    /// Gets or sets the size of the page.
    /// </summary>
    public int PageSize { get; set; } = 20;

    /// <summary>
    /// Gets or sets the column to sort.
    /// </summary>
    public string SortColumn { get; set; } = "Id";

    /// <summary>
    /// Gets or sets the sort direction.
    /// </summary>
    public SortDirection SortDirection { get; set; } = SortDirection.Ascending;
}