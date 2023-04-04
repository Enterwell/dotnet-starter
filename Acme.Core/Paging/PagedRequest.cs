using Acme.Core.Paging.Enums;
using Acme.Core.Paging.Interfaces;

namespace Acme.Core.Paging;

/// <summary>
/// Paged request class.
/// </summary>
/// <seealso cref="IPagedRequest"/>
public class PagedRequest : IPagedRequest
{
    /// <summary>
    /// Gets or sets the current page.
    /// </summary>
    public int Page { get; set; }

    /// <summary>
    /// Gets or sets the size of the page.
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// Gets or sets the column to sort.
    /// </summary>
    public string SortColumn { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the sort direction.
    /// </summary>
    public SortDirection SortDirection { get; set; }
}