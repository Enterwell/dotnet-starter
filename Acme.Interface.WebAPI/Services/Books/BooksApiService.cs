using Acme.Core.Books.Interfaces;
using Acme.Core.CodeExtensions;
using Acme.Core.Paging.Interfaces;
using Acme.Interface.WebAPI.Models.Books;
using Acme.Interface.WebAPI.Models.Paging;
using AutoMapper;
using Enterwell.Exceptions;

namespace Acme.Interface.WebAPI.Services.Books;

/// <summary>
/// Books Web API service class.
/// </summary>
/// <seealso cref="IBooksApiService"/>
public class BooksApiService : IBooksApiService
{
    private readonly IBooksService booksService;
    private readonly IMapper mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="BooksApiService"/> class.
    /// </summary>
    /// <param name="booksService">Books service.</param>
    /// <param name="mapper">The mapper.</param>
    /// <exception cref="ArgumentNullException">
    /// booksService
    /// or
    /// mapper
    /// </exception>
    public BooksApiService(IBooksService booksService, IMapper mapper)
    {
        this.booksService = booksService ?? throw new ArgumentNullException(nameof(booksService));
        this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    /// <summary>
    /// Gets all books paged asynchronously.
    /// </summary>
    /// <param name="pagingDto">The paging data.</param>
    /// <returns>
    /// A task returning a <see cref="PagedResponseDto{T}"/> containing paged books.
    /// </returns>
    public async Task<PagedResponseDto<BookDto>> GetAllAsync(PagedRequestDto pagingDto)
    {
        var pagedRequest = pagingDto.MapTo<IPagedRequest>(this.mapper);
        var pagedBooks = this.booksService.GetAllPaged(pagedRequest);

        var totalItems = await pagedBooks.TotalCountAsync();
        var pagedItems = await pagedBooks.GetPageAsync(pagingDto.Page, pagingDto.PageSize);

        return new PagedResponseDto<BookDto>
        {
            PagingData = new PagingDataResponseDto
            {
                Page = pagingDto.Page,
                PageSize = pagingDto.PageSize,
                SortColumn = pagingDto.SortColumn,
                SortDirection = pagingDto.SortDirection,
                TotalItems = totalItems
            },
            Items = pagedItems.MapTo<IEnumerable<BookDto>>(this.mapper)
        };
    }

    /// <summary>
    /// Gets the book by its identifier asynchronously.
    /// </summary>
    /// <param name="id">The book identifier.</param>
    /// <returns>
    /// A task returning a <see cref="BookDto"/> if found.
    /// </returns>
    public async Task<BookDto> GetByIdAsync(string id)
    {
        var book = await this.booksService.GetByIdAsync(id);

        // Check if found
        if (book == null)
        {
            throw new EnterwellEntityNotFoundException($"No book was found for identifier {id}.");
        }

        return book.MapTo<BookDto>(this.mapper);
    }

    /// <summary>
    /// Creates the book asynchronously.
    /// </summary>
    /// <param name="createDto">The book create.</param>
    /// <returns>
    /// A task returning newly created <see cref="BookDto"/>.
    /// </returns>
    public async Task<BookDto> CreateAsync(BookCreateDto createDto)
    {
        var bookCreate = createDto.MapTo<IBookCreate>(this.mapper);

        var createdBook = await this.booksService.CreateAsync(bookCreate);

        return createdBook.MapTo<BookDto>(this.mapper);
    }

    /// <summary>
    /// Updates the book asynchronously.
    /// </summary>
    /// <param name="updateDto">The book update.</param>
    /// <returns>
    /// A task updating the book.
    /// </returns>
    public async Task UpdateAsync(BookUpdateDto updateDto)
    {
        var bookUpdate = updateDto.MapTo<IBookUpdate>(this.mapper);

        await this.booksService.UpdateAsync(bookUpdate);
    }

    /// <summary>
    /// Deletes the book asynchronously.
    /// </summary>
    /// <param name="id">The book identifier.</param>
    /// <returns>
    /// A task deleting the book.
    /// </returns>
    public Task DeleteAsync(string id) =>
        this.booksService.DeleteAsync(id);
}