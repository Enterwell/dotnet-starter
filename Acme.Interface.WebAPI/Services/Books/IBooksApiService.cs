using Acme.Interface.WebAPI.Models.Books;
using Acme.Interface.WebAPI.Models.Paging;

namespace Acme.Interface.WebAPI.Services.Books;

/// <summary>
/// Books Web API service interface.
/// </summary>
public interface IBooksApiService
{
    /// <summary>
    /// Gets all books paged asynchronously.
    /// </summary>
    /// <param name="pagingDto">The paging data.</param>
    /// <returns>
    /// A task returning a <see cref="PagedResponseDto{T}"/> containing paged books.
    /// </returns>
    Task<PagedResponseDto<BookDto>> GetAllAsync(PagedRequestDto pagingDto);

    /// <summary>
    /// Gets the book by its identifier asynchronously.
    /// </summary>
    /// <param name="id">The book identifier.</param>
    /// <returns>
    /// A task returning a <see cref="BookDto"/> if found.
    /// </returns>
    Task<BookDto> GetByIdAsync(string id);

    /// <summary>
    /// Creates the book asynchronously.
    /// </summary>
    /// <param name="createDto">The book create.</param>
    /// <returns>
    /// A task returning newly created <see cref="BookDto"/>.
    /// </returns>
    Task<BookDto> CreateAsync(BookCreateDto createDto);

    /// <summary>
    /// Updates the book asynchronously.
    /// </summary>
    /// <param name="updateDto">The book update.</param>
    /// <returns>
    /// A task updating the book.
    /// </returns>
    Task UpdateAsync(BookUpdateDto updateDto);

    /// <summary>
    /// Deletes the book asynchronously.
    /// </summary>
    /// <param name="id">The book identifier.</param>
    /// <returns>
    /// A task deleting the book.
    /// </returns>
    Task DeleteAsync(string id);
}