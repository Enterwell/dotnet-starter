using Acme.Core.Paging.Interfaces;

namespace Acme.Core.Books.Interfaces;

/// <summary>
/// Books repository interface.
/// </summary>
public interface IBooksRepository
{
    /// <summary>
    /// Gets all books paged.
    /// </summary>
    /// <param name="pagedRequest">Paged request.</param>
    /// <returns>
    /// A task returning a <see cref="IPagedResult{T}"/> containing paged books.
    /// </returns>
    IPagedResult<IBook> GetAllPaged(IPagedRequest pagedRequest);

    /// <summary>
    /// Gets the book by its identifier asynchronously.
    /// </summary>
    /// <param name="id">The book identifier.</param>
    /// <returns>
    /// A task returning a book if found or <c>null</c>.
    /// </returns>
    Task<IBook?> GetByIdAsync(string id);

    /// <summary>
    /// Creates the book asynchronously.
    /// </summary>
    /// <param name="bookCreate">The book create.</param>
    /// <returns>
    /// A task returning newly created <see cref="IBook"/>.
    /// </returns>
    Task<IBook> CreateAsync(IBookCreate bookCreate);

    /// <summary>
    /// Updates the book asynchronously.
    /// </summary>
    /// <param name="bookUpdate">The book update.</param>
    /// <returns>
    /// A task updating the book.
    /// </returns>
    Task UpdateAsync(IBookUpdate bookUpdate);

    /// <summary>
    /// Deletes the book asynchronously.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns>
    /// A task deleting the book.
    /// </returns>
    Task DeleteAsync(string id);
}