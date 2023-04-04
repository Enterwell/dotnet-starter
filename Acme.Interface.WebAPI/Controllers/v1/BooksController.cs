using Acme.Interface.WebAPI.Controllers.v1.Base;
using Acme.Interface.WebAPI.Models.Books;
using Acme.Interface.WebAPI.Models.Paging;
using Acme.Interface.WebAPI.Services.Books;
using Microsoft.AspNetCore.Mvc;

namespace Acme.Interface.WebAPI.Controllers.v1;

/// <summary>
/// Books Web API controller.
/// </summary>
/// <seealso cref="BaseV1ApiController"/>
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
public class BooksController : BaseV1ApiController
{
    private readonly IBooksApiService booksApiService;

    /// <summary>
    /// Initializes a new instance of the <see cref="BooksController"/> class.
    /// </summary>
    /// <param name="booksApiService">The books API service.</param>
    /// <exception cref="ArgumentNullException">
    /// booksApiService
    /// </exception>
    public BooksController(IBooksApiService booksApiService)
    {
        this.booksApiService = booksApiService ?? throw new ArgumentNullException(nameof(booksApiService));
    }

    /// <summary>
    /// Gets all books paged asynchronously.
    /// </summary>
    /// <param name="pagingDto">The paged request DTO.</param>
    /// <returns>
    /// Paged books.
    /// </returns>
    [HttpPost("search")]
    [ProducesResponseType(typeof(PagedResponseDto<BookDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllAsync([FromBody] PagedRequestDto pagingDto) =>
        this.Ok(await this.booksApiService.GetAllAsync(pagingDto));

    /// <summary>
    /// Gets the book by its identifier asynchronously.
    /// </summary>
    /// <param name="id">The book identifier.</param>
    /// <returns>
    /// <see cref="BookDto"/> if found.
    /// </returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(BookDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync([FromRoute] string id) =>
        this.Ok(await this.booksApiService.GetByIdAsync(id));

    /// <summary>
    /// Creates the book asynchronously.
    /// </summary>
    /// <param name="createDto">The book create DTO.</param>
    /// <returns>
    /// Created <see cref="BookDto"/>.
    /// </returns>
    [HttpPost]
    [ProducesResponseType(typeof(BookDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAsync([FromBody] BookCreateDto createDto)
    {
        var createdBook = await this.booksApiService.CreateAsync(createDto);

        return this.CreatedAtAction("GetById", new { id = createdBook.Id }, createdBook);
    }

    /// <summary>
    /// Updates the book asynchronously.
    /// </summary>
    /// <param name="id">The book identifier.</param>
    /// <param name="updateDto">The book update DTO.</param>
    /// <returns>
    /// No content.
    /// </returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateAsync([FromRoute] string id, [FromBody] BookUpdateDto updateDto)
    {
        if (id != updateDto.Id) return this.BadRequest();

        await this.booksApiService.UpdateAsync(updateDto);

        return this.NoContent();
    }

    /// <summary>
    /// Deletes the book asynchronously.
    /// </summary>
    /// <param name="id">The book identifier.</param>
    /// <returns>
    /// No content.
    /// </returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync([FromRoute] string id)
    {
        await this.booksApiService.DeleteAsync(id);

        return this.NoContent();
    }
}