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

    /// <summary>
    /// Initializes a new instance of the <see cref="PagedRequestDto"/> class.
    /// </summary>
    public PagedRequestDto()
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="PagedRequestDto"/> class.
    /// </summary>
    /// <param name="page">The page.</param>
    /// <param name="pageSize">Size of the page.</param>
    /// <param name="sortColumn">The sort column.</param>
    /// <param name="sortDirection">The sort direction.</param>
    public PagedRequestDto(int page, int pageSize, string sortColumn, SortDirection sortDirection)
    {
        Page = page;
        PageSize = pageSize;
        SortColumn = sortColumn;
        SortDirection = sortDirection;
    }
}