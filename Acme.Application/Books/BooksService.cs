using Acme.Core.Books.Interfaces;
using Acme.Core.Paging.Interfaces;

namespace Acme.Application.Books;

/// <summary>
/// Books service class.
/// </summary>
/// <seealso cref="IBooksService"/>
public class BooksService : IBooksService
{
    private readonly IBooksRepository booksRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="BooksService"/> class.
    /// </summary>
    /// <param name="booksRepository">The books repository.</param>
    /// <exception cref="ArgumentNullException">
    /// booksRepository
    /// </exception>
    public BooksService(IBooksRepository booksRepository)
    {
        this.booksRepository = booksRepository ?? throw new ArgumentNullException(nameof(booksRepository));
    }

    /// <summary>
    /// Gets all books paged.
    /// </summary>
    /// <param name="pagedRequest">Paged request.</param>
    /// <returns>
    /// A task returning a <see cref="IPagedResult{T}"/> containing paged books.
    /// </returns>
    public IPagedResult<IBook> GetAllPaged(IPagedRequest pagedRequest) =>
        this.booksRepository.GetAllPaged(pagedRequest);

    /// <summary>
    /// Gets the book by its identifier asynchronously.
    /// </summary>
    /// <param name="id">The book identifier.</param>
    /// <returns>
    /// A task returning a book if found or <c>null</c>.
    /// </returns>
    public Task<IBook?> GetByIdAsync(string id) =>
        this.booksRepository.GetByIdAsync(id);

    /// <summary>
    /// Creates the book asynchronously.
    /// </summary>
    /// <param name="bookCreate">The book create.</param>
    /// <returns>
    /// A task returning newly created <see cref="IBook"/>.
    /// </returns>
    public Task<IBook> CreateAsync(IBookCreate bookCreate) =>
        this.booksRepository.CreateAsync(bookCreate);

    /// <summary>
    /// Updates the book asynchronously.
    /// </summary>
    /// <param name="bookUpdate">The book update.</param>
    /// <returns>
    /// A task updating the book.
    /// </returns>
    public Task UpdateAsync(IBookUpdate bookUpdate) =>
        this.booksRepository.UpdateAsync(bookUpdate);

    /// <summary>
    /// Deletes the book asynchronously.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns>
    /// A task deleting the book.
    /// </returns>
    public Task DeleteAsync(string id) =>
        this.booksRepository.DeleteAsync(id);
}