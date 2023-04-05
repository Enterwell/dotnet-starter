using Acme.Core.Paging.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Acme.Infrastructure.EF.PostgreSql.Paging;

/// <summary>
/// Paged result class.
/// </summary>
/// <seealso cref="IPagedResult{T}"/>
public class PagedResult<TInterface> : IPagedResult<TInterface>
{
    private readonly IQueryable<TInterface> query;

    /// <summary>
    /// Initializes a new instance of the <see cref="PagedResult{TInterface}"/> class.
    /// </summary>
    /// <param name="query">Query to get the paged data from.</param>
    /// <exception cref="ArgumentNullException">
    /// query
    /// </exception>
    public PagedResult(IQueryable<TInterface> query)
    {
        this.query = query ?? throw new ArgumentNullException(nameof(query));
    }

    /// <summary>
    /// Gets the paginated data from the query.
    /// </summary>
    /// <param name="page">The page.</param>
    /// <param name="pageSize">The size of the page.</param>
    /// <returns>
    /// A <see cref="IEnumerable{T}"/> containing the paged data.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// page
    /// or
    /// pageSize
    /// </exception>
    public async Task<IEnumerable<TInterface>> GetPageAsync(int page, int pageSize)
    {
        if (page < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(page));
        }

        if (pageSize <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(pageSize));
        }

        return await this.query
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    /// <summary>
    /// Gets the total count of the data.
    /// </summary>
    /// <returns>
    /// Total count of the data.
    /// </returns>
    public async Task<int> TotalCountAsync()
    {
        return await this.query.CountAsync();
    }
}