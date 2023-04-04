namespace Acme.Core.Paging.Interfaces;

/// <summary>
/// Paged result interface.
/// </summary>
/// <typeparam name="T">Type of the paged data.</typeparam>
public interface IPagedResult<T>
{
    /// <summary>
    /// Gets the paginated data of the given type.
    /// </summary>
    /// <param name="page">The current page.</param>
    /// <param name="pageSize">Size of the page.</param>
    /// <returns><see cref="IEnumerable{T}"/> containing the paged data.</returns>
    Task<IEnumerable<T>> GetPageAsync(int page, int pageSize);

    /// <summary>
    /// Gets the total count of the data.
    /// </summary>
    /// <returns>Total count of the data.</returns>
    Task<int> TotalCountAsync();
}