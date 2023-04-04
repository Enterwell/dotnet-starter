using Acme.Core.Paging.Enums;

namespace Acme.Core.Paging.Interfaces;

/// <summary>
/// Paged request interface.
/// </summary>
public interface IPagedRequest
{
    /// <summary>
    /// Gets or sets the current page.
    /// </summary>
    int Page { get; set; }

    /// <summary>
    /// Gets or sets the size of the page.
    /// </summary>
    int PageSize { get; set; }

    /// <summary>
    /// Gets or sets the column to sort.
    /// </summary>
    string SortColumn { get; set; }

    /// <summary>
    /// Gets or sets the sort direction.
    /// </summary>
    SortDirection SortDirection { get; set; }
}