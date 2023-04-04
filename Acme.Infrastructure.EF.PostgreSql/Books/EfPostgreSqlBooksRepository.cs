using Acme.Core.Books.Interfaces;
using Acme.Core.CodeExtensions;
using Acme.Core.Paging.Enums;
using Acme.Core.Paging.Interfaces;
using Acme.Infrastructure.EF.PostgreSql.Paging;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Enterwell.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Acme.Infrastructure.EF.PostgreSql.Books;

/// <summary>
/// Entity Framework PostgreSQL books repository class.
/// </summary>
/// <seealso cref="IBooksRepository"/>
public class EfPostgreSqlBooksRepository : IBooksRepository
{
    private readonly ApplicationDbContext context;
    private readonly IMapper mapper;
    private readonly ILogger<EfPostgreSqlBooksRepository> logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="EfPostgreSqlBooksRepository"/> class.
    /// </summary>
    /// <param name="context">Application db context.</param>
    /// <param name="mapper">The mapper.</param>
    /// <param name="logger">The logger.</param>
    /// <exception cref="ArgumentNullException">
    /// context
    /// or
    /// mapper
    /// </exception>
    public EfPostgreSqlBooksRepository(ApplicationDbContext context, IMapper mapper, ILogger<EfPostgreSqlBooksRepository> logger)
    {
        this.context = context ?? throw new ArgumentNullException(nameof(context));
        this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Gets all books paged.
    /// </summary>
    /// <param name="pagedRequest">Paged request.</param>
    /// <returns>
    /// A task returning a <see cref="IPagedResult{T}"/> containing paged books.
    /// </returns>
    public IPagedResult<IBook> GetAllPaged(IPagedRequest pagedRequest)
    {
        ArgumentNullException.ThrowIfNull(pagedRequest);

        var query = this.context.Books
            .AsNoTracking()
            .AsQueryable();

        var isSortDescending = pagedRequest.SortDirection == SortDirection.Descending;

        // Sort the results based on the sort column
        query = pagedRequest.SortColumn.ToLower() switch
        {
            "name" => isSortDescending
                ? query.OrderByDescending(b => b.Name)
                : query.OrderBy(b => b.Name),
            "price" => isSortDescending
                ? query.OrderByDescending(b => b.Price)
                : query.OrderBy(b => b.Price),
            "category" => isSortDescending
                ? query.OrderByDescending(b => b.Category)
                : query.OrderBy(b => b.Category),
            "author" => isSortDescending
                ? query.OrderByDescending(b => b.Author)
                : query.OrderBy(b => b.Author),
            _ => isSortDescending
                ? query.OrderByDescending(b => b.Id)
                : query.OrderBy(b => b.Id)
        };

        var projectQuery = query.ProjectTo<IBook>(this.mapper.ConfigurationProvider);

        return new PagedResult<IBook>(projectQuery);
    }

    /// <summary>
    /// Gets the book by its identifier asynchronously.
    /// </summary>
    /// <param name="id">The book identifier.</param>
    /// <returns>
    /// A task returning a book if found or <c>null</c>.
    /// </returns>
    public async Task<IBook?> GetByIdAsync(string id)
    {
        var dbBook = await this.context.Books.FindAsync(id);

        return dbBook?.MapTo<IBook>(this.mapper);
    }

    /// <summary>
    /// Creates the book asynchronously.
    /// </summary>
    /// <param name="bookCreate">The book create.</param>
    /// <returns>
    /// A task returning newly created <see cref="IBook"/>.
    /// </returns>
    public async Task<IBook> CreateAsync(IBookCreate bookCreate)
    {
        ArgumentNullException.ThrowIfNull(bookCreate);

        var dbBook = new DbBook
        {
            Name = bookCreate.Name,
            Author = bookCreate.Author,
            Category = bookCreate.Category,
            Price = bookCreate.Price
        };

        try
        {
            this.context.Books.Add(dbBook);
            await this.context.SaveChangesAsync();

            this.logger.LogInformation($"Book {dbBook.Name} created successfully.");

            return dbBook.MapTo<IBook>(this.mapper);
        }
        catch (Exception ex)
        {
            throw new EnterwellException($"Failed to create book {dbBook.Name}", ex);
        }
    }

    /// <summary>
    /// Updates the book asynchronously.
    /// </summary>
    /// <param name="bookUpdate">The book update.</param>
    /// <returns>
    /// A task updating the book.
    /// </returns>
    public async Task UpdateAsync(IBookUpdate bookUpdate)
    {
        ArgumentNullException.ThrowIfNull(bookUpdate);

        var dbBook = await this.context.Books.FindAsync(bookUpdate.Id);

        // Check if not found
        if (dbBook == null)
        {
            throw new EnterwellEntityNotFoundException($"No book was found with Id {bookUpdate.Id}");
        }

        try
        {
            dbBook.Name = bookUpdate.Name;
            dbBook.Author = bookUpdate.Author;
            dbBook.Category = bookUpdate.Category;
            dbBook.Price = bookUpdate.Price;

            await this.context.SaveChangesAsync();

            this.logger.LogInformation($"Book {bookUpdate.Name} updated successfully.");
        }
        catch (Exception ex)
        {
            throw new EnterwellException($"Failed to update book {bookUpdate.Name}", ex);
        }
    }

    /// <summary>
    /// Deletes the book asynchronously.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns>
    /// A task deleting the book.
    /// </returns>
    public async Task DeleteAsync(string id)
    {
        var dbBook = await this.context.Books.FindAsync(id);

        // Check if not found
        if (dbBook == null)
        {
            throw new EnterwellEntityNotFoundException($"No book was found with Id {id}");
        }

        try
        {
            this.context.Books.Remove(dbBook);
            await this.context.SaveChangesAsync();

            this.logger.LogInformation($"Book {dbBook.Name} deleted successfully.");
        }
        catch (Exception ex)
        {
            throw new EnterwellException($"Failed to delete book {dbBook.Name}", ex);
        }
    }
}